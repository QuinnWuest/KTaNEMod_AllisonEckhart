using KModkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class AllisonEckhartScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo Bomb;
    public KMAudio Audio;

    public TextMesh ScreenText;
    public Renderer ScreenTextRenderer;
    public TextMesh InputText;

    public KMSelectable Clear;
    public KMSelectable Submit;
    public KMSelectable[] NumberButtons;

    public int DebugNumber;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private static readonly string[] _calcStarts = "INPUT,COMPUTE,CALCULATE,PUNCH IN,TYPE IN,DETERMINE,EVALUATE,QUANTIFY".Split(',');

    private static bool _alreadyRan = false;
    private static List<KMBombModule> _foundMods = new List<KMBombModule>();
    private int _solution;
    private string _brackettedPrompt;
    private readonly List<string> _promptIterations = new List<string>();
    private int _solvedAllisonEckhartedModules = 0;

    private int _solves;
    private string _mostRecent;
    private readonly List<string> _solveList = new List<string>();

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        Clear.OnInteract += delegate () { ClearPress(); return false; };

        GenerateAllisonEckhart();

        var textToDisplay = _promptIterations[0];
        WordWrapHelper.SetWordWrappedText(ref textToDisplay, ScreenText, ScreenTextRenderer, transform);
    }

    private void Update()
    {
        if (!_moduleSolved)
        {
            _solves = Bomb.GetSolvedModuleIDs().Count();
            if (_solves > _solveList.Count())
            {
                _mostRecent = GetLatestSolve(Bomb.GetSolvedModuleIDs(), _solveList);
                if (true /*_foundMods.Contains(MostRecent)*/)
                {

                }
            }
        }
    }

    void ClearPress()
    {
        if (Application.isEditor)
        {
            if (_solvedAllisonEckhartedModules >= _promptIterations.Count - 1)
                return;

            _solvedAllisonEckhartedModules++;
            var textToDisplay = _promptIterations[_solvedAllisonEckhartedModules];

            WordWrapHelper.SetWordWrappedText(ref textToDisplay, ScreenText, ScreenTextRenderer, transform);
        }
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
            new KeyValuePair<string, int>("ZERO", 0),
            new KeyValuePair<string, int>("ONE", 1),
            new KeyValuePair<string, int>("TWO", 2),
            new KeyValuePair<string, int>("THREE", 3),
            new KeyValuePair<string, int>("FOUR", 4),
            new KeyValuePair<string, int>("FIVE", 5),
            new KeyValuePair<string, int>("SIX", 6),
            new KeyValuePair<string, int>("SEVEN", 7),
            new KeyValuePair<string, int>("EIGHT", 8),
            new KeyValuePair<string, int>("NINE", 9),
            new KeyValuePair<string, int>("TEN", 10),
            new KeyValuePair<string, int>("ELEVEN", 11),
            new KeyValuePair<string, int>("TWELVE", 12),
            new KeyValuePair<string, int>("THIRTEEN", 13),
            new KeyValuePair<string, int>("FOURTEEN", 14),
            new KeyValuePair<string, int>("FIFTEEN", 15),
            new KeyValuePair<string, int>("[MODULE] COUNT|NUMBER OF [MODULES]", Bomb.GetModuleIDs().Count()),
            //distinct modules
            //unique modules
            new KeyValuePair<string, int>("[[REGULAR] MODULE] COUNT|NUMBER OF [[REGULAR] MODULES]|[[NON-NEEDY] MODULE] COUNT|NUMBER OF [[NON-NEEDY] MODULES]", Bomb.GetSolvableModuleIDs().Count()),
            new KeyValuePair<string, int>("[[NEEDY] MODULE] COUNT|NUMBER OF [[NEEDY] MODULES]", Bomb.GetModuleIDs().Count() - Bomb.GetSolvableModuleIDs().Count()),
            new KeyValuePair<string, int>("[BATTERY] COUNT|NUMBER OF [BATTERIES]", Bomb.GetBatteryCount()),
            new KeyValuePair<string, int>("[BATTERY HOLDER] COUNT|NUMBER OF [BATTERY HOLDERS]", Bomb.GetBatteryHolderCount()),
            new KeyValuePair<string, int>("[[AA] BATTERY] COUNT|NUMBER OF [[AA] BATTERIES]", Bomb.GetBatteryCount(Battery.AA)),
            new KeyValuePair<string, int>("[[D] BATTERY] COUNT|NUMBER OF [[D] BATTERIES]", Bomb.GetBatteryCount(Battery.D)),
            new KeyValuePair<string, int>("[INDICATOR] COUNT|NUMBER OF [INDICATORS]", Bomb.GetIndicators().Count()),
            new KeyValuePair<string, int>("[[LIT] INDICATOR] COUNT|NUMBER OF [[LIT] INDICATORS]", Bomb.GetOnIndicators().Count()),
            new KeyValuePair<string, int>("[[UNLIT] INDICATOR] COUNT|NUMBER OF [[UNLIT] INDICATORS]", Bomb.GetOffIndicators().Count()),
            //new KeyValuePair<string, int>("NUMBER OF [INDICATORS CONTAINING A VOWEL]", Bomb.GetIndicators().Select(i => i.Intersect("AEIOU").Any())),
            //new KeyValuePair<string, int>("NUMBER OF [[LIT] INDICATORS CONTAINING A VOWEL]", Bomb.GetOnIndicators().Select(i => i.Intersect("AEIOU").Any())),
            //new KeyValuePair<string, int>("NUMBER OF [[UNLIT] INDICATORS CONTAINING A VOWEL]", Bomb.GetOffIndicators().Select(i => i.Intersect("AEIOU").Any())),
            //sum of characters in indicators
            new KeyValuePair<string, int>("[PORT] COUNT|NUMBER OF [PORTS]", Bomb.GetPortCount()),
            new KeyValuePair<string, int>("[PORT PLATE] COUNT|NUMBER OF [PORT PLATES]", Bomb.GetPortPlateCount()),
            //empty port plate count
            //non-empty port plate count
            new KeyValuePair<string, int>("[[DVI-D] PORT] COUNT|NUMBER OF [[DVI-D] PORTS]", Bomb.GetPortCount(Port.DVI)),
            new KeyValuePair<string, int>("[[PARALLEL] PORT] COUNT|NUMBER OF [[PARALLEL] PORTS]", Bomb.GetPortCount(Port.Parallel)),
            new KeyValuePair<string, int>("[[PS/2] PORT] COUNT|NUMBER OF [[PS/2] PORTS]", Bomb.GetPortCount(Port.PS2)),
            new KeyValuePair<string, int>("[[RJ-45] PORT] COUNT|NUMBER OF [[RJ-45] PORTS]", Bomb.GetPortCount(Port.RJ45)),
            new KeyValuePair<string, int>("[[SERIAL] PORT] COUNT|NUMBER OF [[SERIAL] PORTS]", Bomb.GetPortCount(Port.Serial)),
            new KeyValuePair<string, int>("[[STEREO RCA] PORT] COUNT|NUMBER OF [[STEREO RCA] PORTS]", Bomb.GetPortCount(Port.StereoRCA)),
            new KeyValuePair<string, int>("[FIRST] SERIAL NUMBER [DIGIT]|[1ST] SERIAL NUMBER [DIGIT]", Bomb.GetSerialNumberNumbers().ToArray()[0]),
            new KeyValuePair<string, int>("[SECOND] SERIAL NUMBER [DIGIT]|[2ND] SERIAL NUMBER [DIGIT]", Bomb.GetSerialNumberNumbers().ToArray()[1]),
            new KeyValuePair<string, int>("[LAST] SERIAL NUMBER [DIGIT]", Bomb.GetSerialNumberNumbers().ToArray()[Bomb.GetSerialNumberNumbers().ToArray().Count()-1]),

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

            promptSoFar += " [" + (negative ? "MINUS" : "PLUS") + "] [" + pickedPiece.Text + "]";
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

        var microIterations = new List<string> { "ALLISON ECKHART" };

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
        return visible.Select(v => v.Node.Revealed ? v.Node.Text : "ALLISON ECKHART").ToArray().Join(" ");
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

    private void GenerateAllisonEckhart()
    {
        if (_alreadyRan)
            return;
        string sn = Bomb.GetSerialNumber();
        KMBombModule[] mods = FindObjectsOfType<KMBombModule>().Where(x => x.GetComponent<KMBombInfo>() != null && x.GetComponent<KMBombInfo>().GetSerialNumber() == sn).ToArray();
        List<string> names = new List<string> { };
        for (int i = 0; i < mods.Length; i++)
        {
            string name = mods[i].ModuleDisplayName;
            if (Data.data.ContainsKey(name))
            {
                _foundMods.Add(mods[i]);
                names.Add(name);
                ModuleProcessor.ProcessModule(mods[i]);
            }
        }
        Debug.LogFormat("<Allison Eckhart #{0}> Found {1} mods: {2}", _moduleId, _foundMods.Count, names.ToArray().Join("; "));
        /*
        while (_foundMods.Count > 10) 
        {
            int modIndex = Rnd.Range(0, _foundMods.Count);
            _foundMods.RemoveAt(modIndex);
            names.RemoveAt(modIndex);
        }
        */
        Debug.LogFormat("[Allison Eckhart #{0}] Possessing {1} mods: {2}", _moduleId, _foundMods.Count, names.ToArray().Join("; "));
        //TODO(?): If multiple Allison Eckharts are present, divy up the supported modules among the Allison Eckharts.
        GeneratePrompt(Application.isEditor ? DebugNumber : _foundMods.Count);
        _alreadyRan = true;
    }

    private void OnDestroy()
    {
        _alreadyRan = false;
        _foundMods = new List<KMBombModule>();
    }

    private string GetLatestSolve(List<string> a, List<string> b)
    {
        string z = "";
        for (int i = 0; i < b.Count; i++)
        {
            a.Remove(b.ElementAt(i));
        }
        z = a.ElementAt(0);
        return z;
    }
}
