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

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private static bool alreadyRan = false;
    private static List<KMBombModule> _foundMods = new List<KMBombModule>();
    public bool debugMode = true;
    public int debugNumber = 7;
    private int solution;
    string brackettedPrompt;
    List<string> promptIterations = new List<string>();
    int solvedAllisonEckhartedModules = 0;

    private int Solves;
    private string MostRecent;
    private List<string> SolveList = new List<string> { };

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        Clear.OnInteract += delegate () { ClearPress(); return false; };

        GenerateAllisonEckhart();
        Debug.Log("<>" + promptIterations[0]);

        var textToDisplay = promptIterations[0];
        WordWrapHelper.SetWordWrappedText(ref textToDisplay, ScreenText, ScreenTextRenderer, transform);
    }

    private void Update()
    {
        if (!_moduleSolved)
        {
            Solves = Bomb.GetSolvedModuleIDs().Count();
            if (Solves > SolveList.Count())
            {
                MostRecent = GetLatestSolve(Bomb.GetSolvedModuleIDs(), SolveList);
                if (true /*_foundMods.Contains(MostRecent)*/)
                {

                }
            }
        }
    }

    void ClearPress()
    {
        if (debugMode)
        {
            solvedAllisonEckhartedModules++;
            var textToDisplay =
            promptIterations[solvedAllisonEckhartedModules];

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

    private void GeneratePrompt(int count)
    {
        int allisoneckhartsremaining = count;

        string[] starts = "INPUT,COMPUTE,CALCULATE,PUNCH IN,TYPE IN,DETERMINE,EVALUATE,QUANTIFY".Split(',');

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
            new KeyValuePair<string, int>("SIXTEEN", 16),
            new KeyValuePair<string, int>("SEVENTEEN", 17),
            new KeyValuePair<string, int>("EIGHTEEN", 18),
            new KeyValuePair<string, int>("NINETEEN", 19),
            new KeyValuePair<string, int>("TWENTY", 20),
            new KeyValuePair<string, int>("THIRTY", 30),
            new KeyValuePair<string, int>("FORTY", 40),
            new KeyValuePair<string, int>("FIFTY", 50),
            new KeyValuePair<string, int>("SIXTY", 60),
            new KeyValuePair<string, int>("SEVENTY", 70),
            new KeyValuePair<string, int>("EIGHTY", 80),
            new KeyValuePair<string, int>("NINETY", 90),
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
            //there's more but honestly can't be fucked atm
        };

        if (count < 2)
        {
            var singleton = pieces[Rnd.Range(0, 28)];
            brackettedPrompt = count == 0 ? (starts.PickRandom() + " " + singleton.Key) : (starts.PickRandom() + " [" + singleton.Key + "]");
            solution = singleton.Value;
            return;
        }

        string promptSoFar = "";
        bool multiple = false;
        List<int> values = new List<int> { };

        while (allisoneckhartsremaining != 0)
        {
            if (multiple && allisoneckhartsremaining <= 1)
            {
                //start over
                promptSoFar = "";
                allisoneckhartsremaining = count;
                multiple = false;
                values.Clear();
            }
            var pickedPiece = pieces.PickRandom();
            string pieceString = pickedPiece.Key.Split('|').PickRandom();
            if (!multiple)
            {
                promptSoFar += "[" + pieceString + "]";
                values.Add(pickedPiece.Value);
                allisoneckhartsremaining -= pieceString.Split('[').Count();
                multiple = true;
            }
            else
            {
                bool negative = values.Sum() < pickedPiece.Value ? false : Rnd.Range(0, 2) == 1;
                promptSoFar += " [" + (negative ? "MINUS" : "PLUS") + "] [" + pieceString + "]";
                values.Add(pickedPiece.Value * (negative ? -1 : 1));
                allisoneckhartsremaining -= 1 + pieceString.Split('[').Count();
            }
        }


        brackettedPrompt = starts.PickRandom() + " " + promptSoFar;
        solution = values.Sum();

        Debug.Log("Generated \"" + brackettedPrompt + "\", answer is " + solution);

        string originalPrompt = brackettedPrompt;
        List<int> pairStart = new List<int>();
        List<int> pairEnd = new List<int>();
        List<int> currentStarts = new List<int>();

        for (int ch = 0; ch < brackettedPrompt.Length; ch++)
        {
            if (brackettedPrompt[ch] == '[')
            {
                currentStarts.Add(ch);
            }
            else if (brackettedPrompt[ch] == ']')
            {
                pairStart.Add(currentStarts.Last());
                currentStarts.RemoveAt(currentStarts.Count() - 1);
                pairEnd.Add(ch);
            }
        }

        char[] charSplit = brackettedPrompt.ToArray();
        List<string> hashed = new List<string>();

        for (int p = 0; p < pairStart.Count(); p++)
        {
            string thisHash = "";
            for (int ch = 0; ch < charSplit.Length; ch++)
            {
                thisHash += ch > pairStart[p] && ch < pairEnd[p] ? '#' : charSplit[ch];
            }
            charSplit = thisHash.ToArray();
            hashed.Add(thisHash);
        }

        hashed = hashed.ToArray().Reverse().ToList();

        for (int h = 0; h < hashed.Count(); h++)
        {
            hashed[h] = hashed[h].Replace("[", "").Replace("]", "");
            while (hashed[h].Contains("##"))
            {
                hashed[h] = hashed[h].Replace("##", "#");
            }
            hashed[h] = hashed[h].Replace("#", "ALLISON ECKHART");
            promptIterations.Add(hashed[h]);
        }

        promptIterations.Add(originalPrompt.Replace("[", "").Replace("]", ""));

        // Debug.Log(promptIterations.Join(" / "));
    }

    private void GenerateAllisonEckhart()
    {
        if (alreadyRan)
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
                if (debugMode) { ModuleProcessor.ProcessModule(mods[i], debugMode); }
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
        GeneratePrompt(debugMode ? debugNumber : _foundMods.Count);
        alreadyRan = true;
    }

    private void OnDestroy()
    {
        alreadyRan = false;
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
