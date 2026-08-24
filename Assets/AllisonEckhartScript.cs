using KModkit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class AllisonEckhartScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;

    public TextMesh ScreenText;
    public Renderer ScreenTextRenderer;
    public TextMesh InputText;

    public KMSelectable ClearSel;
    public KMSelectable SubmitSel;
    public KMSelectable DebugSel;
    public KMSelectable[] NumberButtonSels;

    public int DebugNumber;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private static readonly string[] _calcStarts = "Input,Compute,Calculate,Punch in,Type in,Determine,Evaluate,Quantify".Split(',');

    private static bool _alreadyRan = false;
    private static List<KMBombModule> _foundMods = new List<KMBombModule>();
    private int _solution;
    private string _brackettedPrompt;
    private readonly List<string> _promptIterations = new List<string>();
    private int _solvedAllisonEckhartedModules = 0;

    private int _solves;
    private string _mostRecent;
    private readonly List<string> _currentSolves = new List<string>();

    private readonly ModuleProcessor _moduleProcessor = new ModuleProcessor();

    private class AllisonEckhartScriptInfo
    {
        public List<AllisonEckhartScript> AllisonEckhartModules = new List<AllisonEckhartScript>();
        public bool ModulesAreAllsionEckharted;
        public bool AllSolved { get { return AllisonEckhartModules.All(m => m._moduleSolved); } }
    }
    private static readonly Dictionary<string, AllisonEckhartScriptInfo> _infos = new Dictionary<string, AllisonEckhartScriptInfo>();
    private AllisonEckhartScriptInfo _info;
    private int _moduleIx;
    private string[] _allisonEckhartifiableModuleNames;

    private string _input = "";

    private void Start()
    {
        _moduleId = _moduleIdCounter++;

        for (int i = 0; i < NumberButtonSels.Length; i++)
            NumberButtonSels[i].OnInteract += NumberButtonPress(i);
        ClearSel.OnInteract += ClearPress;
        SubmitSel.OnInteract += SubmitPress;
        DebugSel.OnInteract += DebugPress;

        if (!_moduleProcessor.DebugFlag)
            DebugSel.gameObject.SetActive(false);

        var sn = BombInfo.GetSerialNumber();
        if (!_infos.ContainsKey(sn))
            _infos[sn] = new AllisonEckhartScriptInfo();
        _infos[sn].ModulesAreAllsionEckharted = true;
        _info = _infos[sn];
        _info.AllisonEckhartModules.Add(this);

        StartCoroutine(Setup());

        InputText.text = "";
    }

    private KMSelectable.OnInteractHandler NumberButtonPress(int i)
    {
        return delegate ()
        {
            NumberButtonSels[i].AddInteractionPunch(0.5f);
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, NumberButtonSels[i].transform);
            if (_moduleSolved)
                return false;
            _input += i.ToString();
            InputText.text = _input;
            return false;
        };
    }

    private bool _cockandballs = true;

    private bool DebugPress()
    {
        DebugSel.AddInteractionPunch(0.5f);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, DebugSel.transform);
        if (_moduleSolved)
            return false;
        _cockandballs = !_cockandballs;
        _moduleProcessor.SetTexts(_cockandballs);
        return false;
    }

    private bool ClearPress()
    {
        ClearSel.AddInteractionPunch(0.5f);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, ClearSel.transform);
        if (_moduleSolved)
            return false;
        _input = "";
        InputText.text = _input;
        return false;
    }

    private bool SubmitPress()
    {
        SubmitSel.AddInteractionPunch(0.5f);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, SubmitSel.transform);
        if (_moduleSolved)
            return false;

        int inputNum;
        Debug.LogFormat("[Allison Eckhart #{0}] Inputted {1}.", _moduleId, _input);
        if (int.TryParse(_input, out inputNum))
        {
            if (_solution == inputNum)
            {
                Debug.LogFormat("[Allison Eckhart #{0}] Module solved!", _moduleId);
                Module.HandlePass();
                _moduleSolved = true;

                if (_info.AllSolved)
                    _moduleProcessor.SetTexts(false);
                return false;
            }
        }
        Debug.LogFormat("[Allison Eckhart #{0}] Strike.", _moduleId);
        Module.HandleStrike();
        return false;
    }

    private void Update()
    {
        if (_moduleSolved)
            return;

        var solvedModules = BombInfo.GetSolvedModuleNames();
        if (solvedModules.Count == 0)
            return;

        if (_currentSolves.Count != solvedModules.Count)
        {
            solvedModules = BombInfo.GetSolvedModuleNames();
            var lastSolved = GetLastSolve(solvedModules, _currentSolves);
            if (_allisonEckhartifiableModuleNames.Contains(lastSolved))
            {
                Debug.LogFormat("[Allison Eckhart #{0}] {1} has been solved! Adjusting phrase...", _moduleId, lastSolved);
                _solvedAllisonEckhartedModules++;
                var textToDisplay = _promptIterations[_solvedAllisonEckhartedModules];
                WordWrapHelper.SetWordWrappedText(ref textToDisplay, ScreenText, ScreenTextRenderer, transform);
            }
        }
    }

    private IEnumerator Setup()
    {
        yield return null;
        Debug.LogFormat("<Allison Eckhart #{0}> Gathering info...", _moduleId);
        GatherInfo();

        yield return null;
        // Allison-Eckhart-ify all the modules
        _moduleProcessor.SetTexts(true);

        yield return null;
        Debug.LogFormat("<Allison Eckhart #{0}> Generating prompt with {1} modules...", _moduleId, _foundMods.Count);
        GeneratePrompt(Application.isEditor ? DebugNumber : _foundMods.Count);

        yield return null;
        var textToDisplay = _promptIterations[0];
        if (_foundMods.Count == 0)
            textToDisplay = _promptIterations[1];
        WordWrapHelper.SetWordWrappedText(ref textToDisplay, ScreenText, ScreenTextRenderer, transform);

    }

    private string GetLastSolve(List<string> solved, List<string> cur)
    {
        for (int i = 0; i < cur.Count; i++)
            solved.Remove(cur.ElementAt(i));
        for (int i = 0; i < solved.Count; i++)
            _currentSolves.Add(solved.ElementAt(i));
        return solved.ElementAt(0);
    }

    private void OnDestroy()
    {
        _alreadyRan = false;
        _foundMods = new List<KMBombModule>();

        if (_info != null)
        {
            _info.AllisonEckhartModules.Remove(this);

            if (_info.AllisonEckhartModules.Count == 0)
                _infos.Remove(BombInfo.GetSerialNumber());
        }
    }

    private void GatherInfo()
    {
        _moduleIx = _info.AllisonEckhartModules.IndexOf(this);
        if (_moduleIx != 0)
            return;
        string sn = BombInfo.GetSerialNumber();
        var kmBombMods = FindObjectsOfType<KMBombModule>().Where(x => x.GetComponent<KMBombInfo>() != null && x.GetComponent<KMBombInfo>().GetSerialNumber() == sn).ToArray();
        List<string> names = new List<string>();

        for (int i = 0; i < kmBombMods.Length; i++)
        {
            string name = kmBombMods[i].ModuleDisplayName;
            if (Data.data.ContainsKey(name))
            {
                _foundMods.Add(kmBombMods[i]);
                names.Add(name);
                _moduleProcessor.GatherModuleInfo(kmBombMods[i]);
            }
        }
        Debug.LogFormat("<Allison Eckhart #{0}> Found {1} mods: {2}", _moduleId, _foundMods.Count, names.ToArray().Join("; "));

        _allisonEckhartifiableModuleNames = new string[_foundMods.Count];
        for (int i = 0; i < _foundMods.Count; i++)
            _allisonEckhartifiableModuleNames[i] = _foundMods[i].ModuleDisplayName;

        /*
        while (_foundMods.Count > 10) 
        {
            int modIndex = Rnd.Range(0, _foundMods.Count);
            _foundMods.RemoveAt(modIndex);
            names.RemoveAt(modIndex);
        }
        */

        Debug.LogFormat("[Allison Eckhart #{0}] Contaminating {1} mods: {2}", _moduleId, _foundMods.Count, names.ToArray().Join("; "));
    }

    public class AEPiece
    {
        public string Text;
        public int Value;

        public AEPiece(string text, int value)
        {
            Text = text;
            Value = value;
        }
    }

    private class PromptVariant
    {
        public string Text;
        public int Value;
        public int Cost;

        public PromptVariant(string text, int value)
        {
            Text = text;
            Value = value;
            Cost = text.Split('[').Count();
        }
    }

    private void GeneratePrompt(int count)
    {
        var pieces = new KeyValuePair<string, int>[]
        {
            new KeyValuePair<string, int>("zero", 0),
            new KeyValuePair<string, int>("one", 1),
            new KeyValuePair<string, int>("two", 2),
            new KeyValuePair<string, int>("three", 3),
            new KeyValuePair<string, int>("four", 4),
            new KeyValuePair<string, int>("five", 5),
            new KeyValuePair<string, int>("seven", 7),
            new KeyValuePair<string, int>("six", 6),
            new KeyValuePair<string, int>("eight", 8),
            new KeyValuePair<string, int>("nine", 9),
            new KeyValuePair<string, int>("ten", 10),
            new KeyValuePair<string, int>("eleven", 11),
            new KeyValuePair<string, int>("twelve", 12),
            new KeyValuePair<string, int>("thirteen", 13),
            new KeyValuePair<string, int>("fourteen", 14),
            new KeyValuePair<string, int>("fifteen", 15),
            new KeyValuePair<string, int>("[module] count|number of [modules]", BombInfo.GetModuleIDs().Count()),
            //distinct modules
            //unique modules
            new KeyValuePair<string, int>("[[regular] module] count|number of [[regular] modules]|[[non-needy] module] count|number of [[non-needy] modules]", BombInfo.GetSolvableModuleIDs().Count()),
            new KeyValuePair<string, int>("[[needy] module] count|number of [[needy] modules]", BombInfo.GetModuleIDs().Count() - BombInfo.GetSolvableModuleIDs().Count()),
            new KeyValuePair<string, int>("[battery] count|number of [batteries]", BombInfo.GetBatteryCount()),
            new KeyValuePair<string, int>("[battery holder] count|number of [battery holders]", BombInfo.GetBatteryHolderCount()),
            new KeyValuePair<string, int>("[[AA] battery] count|number of [[AA] batteries]", BombInfo.GetBatteryCount(Battery.AA)),
            new KeyValuePair<string, int>("[[D] battery] count|number of [[D] batteries]", BombInfo.GetBatteryCount(Battery.D)),
            new KeyValuePair<string, int>("[indicator] count|number of [indicators]", BombInfo.GetIndicators().Count()),
            new KeyValuePair<string, int>("[[lit] indicator] count|number of [[lit] indicators]", BombInfo.GetOnIndicators().Count()),
            new KeyValuePair<string, int>("[[unlit] indicator] count|number of [[unlit] indicators]", BombInfo.GetOffIndicators().Count()),
            //new KeyValuePair<string, int>("NUMBER OF [INDICATORS CONTAINING A VOWEL]", Bomb.GetIndicators().Select(i => i.Intersect("AEIOU").Any())),
            //new KeyValuePair<string, int>("NUMBER OF [[LIT] INDICATORS CONTAINING A VOWEL]", Bomb.GetOnIndicators().Select(i => i.Intersect("AEIOU").Any())),
            //new KeyValuePair<string, int>("NUMBER OF [[UNLIT] INDICATORS CONTAINING A VOWEL]", Bomb.GetOffIndicators().Select(i => i.Intersect("AEIOU").Any())),
            //sum of characters in indicators
            new KeyValuePair<string, int>("[port] count|number of [ports]", BombInfo.GetPortCount()),
            new KeyValuePair<string, int>("[port plate] count|number of [port plates]", BombInfo.GetPortPlateCount()),
            //empty port plate count
            //non-empty port plate count
            new KeyValuePair<string, int>("[[DVI-D] port] count|number of [[DVI-D] ports]", BombInfo.GetPortCount(Port.DVI)),
            new KeyValuePair<string, int>("[[Parallel] port] count|number of [[Parallel] ports]", BombInfo.GetPortCount(Port.Parallel)),
            new KeyValuePair<string, int>("[[PS/2] port] count|number of [[PS/2] ports]", BombInfo.GetPortCount(Port.PS2)),
            new KeyValuePair<string, int>("[[RJ-45] port] count|number of [[RJ-45] ports]", BombInfo.GetPortCount(Port.RJ45)),
            new KeyValuePair<string, int>("[[Serial] port] count|number of [[Serial] ports]", BombInfo.GetPortCount(Port.Serial)),
            new KeyValuePair<string, int>("[[Stereo RCA] port] count|number of [[Stereo RCA] ports]", BombInfo.GetPortCount(Port.StereoRCA)),
            new KeyValuePair<string, int>("[first] serial number [digit]|[1st] serial number [digit]", BombInfo.GetSerialNumberNumbers().ToArray()[0]),
            new KeyValuePair<string, int>("[second] serial number [digit]|[2nd] serial number [digit]", BombInfo.GetSerialNumberNumbers().ToArray()[1]),
            new KeyValuePair<string, int>("[last] serial number [digit]", BombInfo.GetSerialNumberNumbers().ToArray()[BombInfo.GetSerialNumberNumbers().ToArray().Count()-1]),

            // THIS DOESNT CURRENTLY CHECK FOR VOLTAGE
            // new KeyValuePair<string, int>("VOLTAGE", Bomb.GetSerialNumberNumbers().ToArray()[Bomb.GetSerialNumberNumbers().ToArray().Count()-1]),
            // new KeyValuePair<string, int>("VOLTAGE ROUNDED [UP]", Bomb.GetSerialNumberNumbers().ToArray()[Bomb.GetSerialNumberNumbers().ToArray().Count()-1]),
            // new KeyValuePair<string, int>("VOLTAGE ROUNDED [DOWN]", Bomb.GetSerialNumberNumbers().ToArray()[Bomb.GetSerialNumberNumbers().ToArray().Count()-1]),
            //there's more but honestly can't be fucked atm
        };

        var variants = pieces.SelectMany(piece => piece.Key.Split('|').Select(text => new PromptVariant(text, piece.Value))).ToList();

        int promptCount = Mathf.Max(5, count);
        int allisonEckhartsRemaining = promptCount;
        string promptSoFar = "";
        List<int> values = new List<int>();

        var firstCandidates = variants.Where(v => v.Cost <= allisonEckhartsRemaining - 2).ToList();
        var firstPiece = firstCandidates.PickRandom();

        promptSoFar = "[" + firstPiece.Text + "]";
        values.Add(firstPiece.Value);
        allisonEckhartsRemaining -= firstPiece.Cost;

        while (allisonEckhartsRemaining > 0)
        {
            var candidates = variants.Where(v =>
                {
                    int remainingAfter = allisonEckhartsRemaining - 1 - v.Cost;
                    return remainingAfter == 0 || remainingAfter >= 2;
                }).ToList();

            var pickedPiece = candidates.PickRandom();
            bool negative = values.Sum() >= pickedPiece.Value && Rnd.Range(0, 2) == 1;

            promptSoFar += " [" + (negative ? "minus" : "plus") + "] [" + pickedPiece.Text + "]";
            values.Add(pickedPiece.Value * (negative ? -1 : 1));
            allisonEckhartsRemaining -= 1 + pickedPiece.Cost;
        }


        _brackettedPrompt = _calcStarts.PickRandom() + " " + promptSoFar;
        _solution = values.Sum();

        Debug.LogFormat("[Allison Eckhart #{0}] Generated phrase:", _moduleId);
        Debug.LogFormat("[Allison Eckhart #{0}] {1}", _moduleId, _brackettedPrompt);
        Debug.LogFormat("[Allison Eckhart #{0}] Solution: {1}", _moduleId, _solution);

        BuildBinaryRevealIterations(_brackettedPrompt, count);
    }

    private class RevealGroup
    {
        public RevealGroup Parent;
        public List<RevealGroup> ChildGroups = new List<RevealGroup>();
        public List<RevealNode> DirectLeaves = new List<RevealNode>();

        public bool IsComplete
        {
            get { return ChildGroups.All(g => g.IsComplete) && DirectLeaves.All(l => l.Revealed); }
        }

        public bool EnclosingLevelsRevealed
        {
            get
            {
                RevealGroup ancestor = Parent;
                while (ancestor != null)
                {
                    if (ancestor.DirectLeaves.Any(l => !l.Revealed))
                        return false;
                    ancestor = ancestor.Parent;
                }
                return true;
            }
        }
    }

    private class RevealNode
    {
        public string Text;
        public RevealNode Left;
        public RevealNode Right;
        public RevealGroup Group;
        public bool Revealed;

        public bool IsLeaf { get { return Left == null && Right == null; } }

        public RevealNode(string text, RevealGroup group)
        {
            Text = text;
            Group = group;
            Revealed = false;
        }

        public RevealNode(RevealNode left, RevealNode right)
        {
            Left = left;
            Right = right;
        }
    }

    private class VisibleRevealNode
    {
        public RevealNode Node;
        public VisibleRevealNode(RevealNode node)
        {
            Node = node;
        }
    }

    private void BuildBinaryRevealIterations(string prompt, int pressCount)
    {
        _promptIterations.Clear();

        var root = ParseRevealSequence(prompt, null);
        var visible = new List<VisibleRevealNode> { new VisibleRevealNode(root) };

        var microIterations = new List<string> { "Allison Eckhart" };

        while (visible.Any(v => !v.Node.IsLeaf))
        {
            var expandable = new List<int>();
            for (int i = 0; i < visible.Count; i++)
                if (!visible[i].Node.IsLeaf)
                    expandable.Add(i);

            int splitIndex = expandable.PickRandom();
            var splitNode = visible[splitIndex].Node;

            visible.RemoveAt(splitIndex);
            visible.Insert(splitIndex, new VisibleRevealNode(splitNode.Right));
            visible.Insert(splitIndex, new VisibleRevealNode(splitNode.Left));

            microIterations.Add(RenderRevealGeneration(visible));
        }

        while (visible.Any(v => !v.Node.Revealed))
        {
            var eligible = new List<int>();

            for (int i = 0; i < visible.Count; i++)
            {
                var leaf = visible[i].Node;
                if (leaf.Revealed)
                    continue;

                if (leaf.Group == null || leaf.Group.EnclosingLevelsRevealed)
                    eligible.Add(i);
            }

            if (eligible.Count == 0)
                eligible = Enumerable.Range(0, visible.Count).Where(i => !visible[i].Node.Revealed).ToList();

            int revealIndex = eligible.PickRandom();
            visible[revealIndex].Node.Revealed = true;
            microIterations.Add(RenderRevealGeneration(visible));
        }

        pressCount = Mathf.Max(0, pressCount);

        if (pressCount == 0)
        {
            _promptIterations.Add(microIterations.Last());
            return;
        }

        int transitionCount = microIterations.Count - 1;

        if (transitionCount >= pressCount)
        {
            _promptIterations.Add(microIterations[0]);

            var milestones = new List<int>();
            for (int press = 1; press < pressCount; press++)
            {
                int minIndex = milestones.Count == 0 ? 1 : milestones.Last() + 1;
                int maxIndex = transitionCount - (pressCount - press);

                float ideal = (float)press * transitionCount / pressCount;
                int idealIndex = Mathf.RoundToInt(ideal);
                int low = Mathf.Max(minIndex, idealIndex - 1);
                int high = Mathf.Min(maxIndex, idealIndex + 1);
                int chosen = low <= high ? Rnd.Range(low, high + 1) : minIndex;

                milestones.Add(chosen);
            }

            milestones.Add(transitionCount);

            foreach (int index in milestones)
                _promptIterations.Add(microIterations[index]);
        }
        else
        {
            _promptIterations.Add(microIterations[0]);

            for (int i = 1; i < microIterations.Count - 1; i++)
                _promptIterations.Add(microIterations[i]);

            string preFinal = microIterations.Count > 1 ? microIterations[microIterations.Count - 2] : microIterations[0];

            while (_promptIterations.Count < pressCount)
                _promptIterations.Add(preFinal);

            _promptIterations.Add(microIterations.Last());
        }
    }

    private string RenderRevealGeneration(List<VisibleRevealNode> visible)
    {
        return visible.Select(v => v.Node.Revealed ? v.Node.Text : "Allison Eckhart").ToArray().Join(" ");
    }

    private RevealNode ParseRevealSequence(string text, RevealGroup currentGroup)
    {
        var nodes = new List<RevealNode>();
        int literalStart = 0;
        int i = 0;

        while (i < text.Length)
        {
            if (text[i] != '[')
            {
                i++;
                continue;
            }

            AddRevealLiteral(nodes, text.Substring(literalStart, i - literalStart), currentGroup);

            int depth = 1;
            int end = i + 1;
            while (end < text.Length && depth > 0)
            {
                if (text[end] == '[')
                    depth++;
                else if (text[end] == ']')
                    depth--;
                end++;
            }

            if (depth != 0)
            {
                AddRevealLiteral(nodes, text.Substring(i), currentGroup);
                literalStart = text.Length;
                i = text.Length;
                break;
            }

            var childGroup = new RevealGroup { Parent = currentGroup };
            if (currentGroup != null)
                currentGroup.ChildGroups.Add(childGroup);

            string inside = text.Substring(i + 1, end - i - 2);
            nodes.Add(ParseRevealSequence(inside, childGroup));

            i = end;
            literalStart = end;
        }

        if (literalStart < text.Length)
            AddRevealLiteral(nodes, text.Substring(literalStart), currentGroup);

        return MakeRevealBinaryTree(nodes);
    }

    private void AddRevealLiteral(List<RevealNode> nodes, string literal, RevealGroup group)
    {
        string trimmed = literal.Trim();
        if (trimmed.Length == 0)
            return;

        var leaf = new RevealNode(trimmed, group);
        nodes.Add(leaf);

        if (group != null)
            group.DirectLeaves.Add(leaf);
    }

    private RevealNode MakeRevealBinaryTree(List<RevealNode> nodes)
    {
        if (nodes.Count == 0)
            return new RevealNode("", null);
        if (nodes.Count == 1)
            return nodes[0];

        int midpoint = nodes.Count / 2;
        var left = MakeRevealBinaryTree(nodes.Take(midpoint).ToList());
        var right = MakeRevealBinaryTree(nodes.Skip(midpoint).ToList());
        return new RevealNode(left, right);
    }
}
