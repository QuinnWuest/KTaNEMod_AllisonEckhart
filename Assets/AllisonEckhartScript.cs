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
    private List<string> SolveList = new List<string>{};

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        Clear.OnInteract += delegate () { ClearPress(); return false; };

        GenerateAllisonEckhart();
        ScreenText.text = wordWrap(promptIterations[0], 32);
    }

    private void Update()
    {
        if (!_moduleSolved)
        {
            Solves = Bomb.GetSolvedModuleIDs().Count();
            if (Solves > SolveList.Count()) {
                MostRecent = GetLatestSolve(Bomb.GetSolvedModuleIDs(), SolveList);
                if (true /*_foundMods.Contains(MostRecent)*/)
                {
                    
                }
            }
        }
    }

    void ClearPress()
    {
        if (debugMode) {
            solvedAllisonEckhartedModules++;
            ScreenText.text = wordWrap(promptIterations[solvedAllisonEckhartedModules], 32);
        }
    }

    public class AEPiece {
        public string Text;
        public int Value;

        public AEPiece(string text, int value) {
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
            } else {
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
            if (brackettedPrompt[ch] == '[') {
                currentStarts.Add(ch);
            } else if (brackettedPrompt[ch] == ']') {
                pairStart.Add(currentStarts.Last());
                currentStarts.RemoveAt(currentStarts.Count() - 1);
                pairEnd.Add(ch);
            }
        }

        char[] charSplit = brackettedPrompt.ToArray();
        List<string> hashed = new List<string>();

        for (int p = 0; p < pairStart.Count(); p++) {
            string thisHash = "";
            for (int ch = 0; ch < charSplit.Length; ch++) {
                thisHash += ch > pairStart[p] && ch < pairEnd[p] ? '#' : charSplit[ch];
            }
            charSplit = thisHash.ToArray();
            hashed.Add(thisHash);
        }

        hashed = hashed.ToArray().Reverse().ToList();

        for (int h = 0; h < hashed.Count(); h++) {
            hashed[h] = hashed[h].Replace("[", "").Replace("]", "");
            while (hashed[h].Contains("##")) {
                hashed[h] = hashed[h].Replace("##", "#");
            }
            hashed[h] = hashed[h].Replace("#", "ALLISON ECKHART");
            promptIterations.Add(hashed[h]);
        }

        promptIterations.Add(originalPrompt.Replace("[", "").Replace("]", ""));
        
        Debug.Log(promptIterations.Join(" / "));
    }

    private void GenerateAllisonEckhart()
    {
        if (alreadyRan)
            return;
        string sn = Bomb.GetSerialNumber();
        KMBombModule[] mods = FindObjectsOfType<KMBombModule>().Where(x => x.GetComponent<KMBombInfo>() != null && x.GetComponent<KMBombInfo>().GetSerialNumber() == sn).ToArray();
        List<string> names = new List<string> {  };
        for (int i = 0; i < mods.Length; i++)
        {
            string name = mods[i].ModuleDisplayName;
            if (Data.data.ContainsKey(name))
            {
                _foundMods.Add(mods[i]);
                names.Add(name);
                if (debugMode) { ProcessModule(mods[i]); }
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

    private void ProcessModule(KMBombModule module)
    {
        ModuleInfo info = Data.data[module.ModuleDisplayName];
        TextMesh[] usedMeshes = info.GetTextMeshes(module);
        Debug.Log(module.ModuleDisplayName);
        switch (module.ModuleDisplayName)
        {
            case "Allison Eckhart": break;
            case "0": SetText(usedMeshes[9], "ALLISON", 0.25f, 0.5f); SetText(usedMeshes[11], "ECKHART", 0.22f, 0.5f); break;
            case "3N+1": SetText(usedMeshes[2], "ALLISON", 0.666f, 1f); SetText(usedMeshes[0], "ECKHART", 0.555f, 1f); break;
            case "A Message": SetText(usedMeshes[3], "ALLISON", 0.666f, 1f); SetText(usedMeshes[4], "ECKHART", 0.666f, 1f); break;
            case "ASCII Art": SetText(usedMeshes[36], "ALLISON\nECKHART", 0.8f, 0.5f); break;
            case "ASCII Maze": SetText(usedMeshes[6], "ALLISON", 0.2f, 0.5f); SetText(usedMeshes[5], "ECKHART", 0.1f, 0.25f); break;
            case "Accelerando": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "Accumulation": SetText(usedMeshes[11], "ALLISON", 0.9f, 1f); SetText(usedMeshes[12], "ECKHART", 0.9f, 1f); break;
            case "Addition": SetText(usedMeshes[10], "ALLISON", 0.35f, 1f); SetText(usedMeshes[11], "ECKHART", 0.3f, 1f); break;
            case "Adjacent Letters": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.8f, 0.8f); break;
            case "Adventure Game": SetText(usedMeshes[7], "ALLISON\nECKHART", 0.428f, 0.625f); break;
            case "Affine Cycle": SetText(usedMeshes[20], "A", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "L", 1f, 1f); SetText(usedMeshes[23], "I", 1f, 1f); SetText(usedMeshes[24], "S", 1f, 1f); SetText(usedMeshes[25], "O", 1f, 1f); SetText(usedMeshes[26], "N", 1f, 1f); SetText(usedMeshes[29], "E", 1f, 1f); SetText(usedMeshes[30], "C", 1f, 1f); SetText(usedMeshes[31], "K", 1f, 1f); SetText(usedMeshes[32], "H", 1f, 1f); SetText(usedMeshes[33], "A", 1f, 1f); SetText(usedMeshes[34], "R", 1f, 1f); SetText(usedMeshes[35], "T", 1f, 1f); break;
            case "Alchemy": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Alfa-Bravo": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.54f); break;
            case "Algebra": SetText(usedMeshes[13], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[12], "ECKHART", 0.7f, 0.7f); break;
            case "Answering Can Be Fun": SetText(usedMeshes[11], "ALLISON\nECKHART", 0.6875f, 0.555f); break;
            case "Antichamber": SetText(usedMeshes[4], "ALLISON ECKHART", 1f, 1f); break;
            case "Ars Goetia Identification": SetText(usedMeshes[38], "ALLISON\nECKHART", 0.49f, 0.49f); break;
            case "Atbash Cipher": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.696f, 0.696f); break;
            case "Audio Morse": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Backgrounds": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.8f, 1f); break;
            case "Bartending": SetText(usedMeshes[5], "ALLISON", 0.909f, 1f); SetText(usedMeshes[6], "ECKHART", 0.882f, 1f); break;
            case "Base-1": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.429f, 0.5f); break;
            case "Basic Morse": SetText(usedMeshes[11], "A", 1f, 1f); SetText(usedMeshes[12], "L", 1f, 1f); SetText(usedMeshes[13], "L", 1f, 1f); SetText(usedMeshes[14], "I", 1f, 1f); SetText(usedMeshes[15], "S", 1f, 1f); SetText(usedMeshes[16], "O", 1f, 1f); SetText(usedMeshes[17], "N", 1f, 1f); SetText(usedMeshes[19], "E", 1f, 1f); SetText(usedMeshes[20], "C", 1f, 1f); SetText(usedMeshes[21], "K", 1f, 1f); SetText(usedMeshes[22], "H", 1f, 1f); SetText(usedMeshes[23], "A", 1f, 1f); SetText(usedMeshes[24], "R", 1f, 1f); SetText(usedMeshes[25], "T", 1f, 1f); break;
            case "Benedict Cumberbatch": SetText(usedMeshes[26], "ALLISON\nECKHART", 0.833f, 0.448f); break;
            case "Binary Buttons": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 0.486f); break;
            case "Binary Cipher": SetText(usedMeshes[1], "ALLISON", 0.25f, 1f); SetText(usedMeshes[2], "ECKHART", 0.24f, 1f); break;
            case "Binary Grid": SetText(usedMeshes[25], "ALLISON", 1f, 1f); SetText(usedMeshes[26], "ECKHART", 1f, 1f); break;
            case "Binary Morse": SetText(usedMeshes[12], "ALLISON", 0.18f, 1f); SetText(usedMeshes[13], "ECKHART", 0.18f, 1f); break;
            case "Binary": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Bitmaps": SetText(usedMeshes[0], "ALLI", 0.5f, 0.5f); SetText(usedMeshes[1], "SON", 0.5f, 0.5f); SetText(usedMeshes[2], "ECK", 0.5f, 0.5f); SetText(usedMeshes[3], "HART", 0.5f, 0.5f); break;
            case "Bitwise Operations": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.6f, 0.4f); break;
            case "Blackjack": SetText(usedMeshes[0], "ALLI", 1f, 1f); SetText(usedMeshes[1], "SON", 1f, 1f); SetText(usedMeshes[2], "ECK", 1f, 1f); SetText(usedMeshes[3], "HART", 1f, 1f); break;
            case "Blank Card": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.4f); break;
            case "Blaseball": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.555f); break;
            case "Blind Maze": SetText(usedMeshes[4], "ALLISON\nECKHART", 0.7f, 0.8f); break;
            case "Blockbusters": SetText(usedMeshes[0], "ALLISON ECKHART", 0.6f, 0.6f); SetText(usedMeshes[1], "ALLISON ECKHART", 0.6f, 0.6f); break;
            case "Bomb It!": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.642f); break;
            case "Bone Apple Tea": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.15f, 0.4f); break;
            case "Boolean Maze": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Boomdas": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.833f, 1f); break;
            case "Boozlesnap": SetText(usedMeshes[0], "ALLISON\nECKHART", -0.2f, -0.28f); break;
            case "Bowling": SetText(usedMeshes[6], "ALLISON\nECKHART", 0.8f, 0.6f); break;
            case "Boxing": SetText(usedMeshes[1], "A LLISON\nECKHART", 1f, 0.5f); break;
            case "Breaktime": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Bridge": SetText(usedMeshes[14], "ALLISON\nECKHART", 1f, 1f); break;
            case "Broken Buttons": SetText(usedMeshes[1], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[0], "ECKHART", 0.7f, 0.7f); break;
            case "Button Order": SetText(usedMeshes[0], "ALLI5ON", 0.211f, 1f); SetText(usedMeshes[1], "ECKHART", 0.188f, 1f); break;
            case "CA-RPS": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Caesar Cycle": SetText(usedMeshes[20], "A", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "L", 1f, 1f); SetText(usedMeshes[23], "I", 1f, 1f); SetText(usedMeshes[24], "S", 1f, 1f); SetText(usedMeshes[25], "O", 1f, 1f); SetText(usedMeshes[26], "N", 1f, 1f); SetText(usedMeshes[29], "E", 1f, 1f); SetText(usedMeshes[30], "C", 1f, 1f); SetText(usedMeshes[31], "K", 1f, 1f); SetText(usedMeshes[32], "H", 1f, 1f); SetText(usedMeshes[33], "A", 1f, 1f); SetText(usedMeshes[34], "R", 1f, 1f); SetText(usedMeshes[35], "T", 1f, 1f); break;
            case "Calculus": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
           
           //from what I recall, above this was exhaustively going through mods alphabetically

            case "Catchphrase": SetText(usedMeshes[12], "ALLISON", 0.172f, 1f); SetText(usedMeshes[11], "ECKHART", 0.15f, 1f); break;
            case "Challenge & Contact": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Chaotic Countdown": SetText(usedMeshes[14], "ALLI", 0.361f, 0.5f); SetText(usedMeshes[15], "SON", 0.361f, 0.5f); SetText(usedMeshes[16], "ECK", 0.361f, 0.5f); SetText(usedMeshes[17], "HART", 0.274f, 0.5f); break;
            case "Character Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.375f); break;
            case "Cheap Checkout": SetText(usedMeshes[4], "ALLISON", 0.6f, 0.6f); SetText(usedMeshes[13], "ECKHART", 0.6f, 0.6f); break;
            case "Cheep Checkout": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Chessword": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.64f, 0.4f); break;
            case "Chinese Strokes": SetText(usedMeshes[2], "ALLISON", 0.3125f, 1f); SetText(usedMeshes[3], "ECKHART", 0.25f, 1f); break;
            case "Chinese Zodiac": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Chord Progressions": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.866f, 1f); break;
            case "Cipher Machine": SetText(usedMeshes[34], "ALLISON", 0.22f, 1f); SetText(usedMeshes[35], "ECKHART", 0.22f, 1f); break;
            case "Cistercian Numbers": SetText(usedMeshes[8], "ALLISON", 0.25f, 1f); SetText(usedMeshes[13], "ECKHART", 0.25f, 1f); break;
            case "Coffeebucks": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Colo(u)r Talk": SetText(usedMeshes[1], "\n\nALLISON\nECKHART", 0.7f, 0.25f); break;
            case "Color Math": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.781f, 0.45f); break;
            case "Color One Two": SetText(usedMeshes[0], "ALLISON", 0.286f, 1f); SetText(usedMeshes[1], "ECKHART", 0.286f, 1f); break;
            case "Color-Cycle Button": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.425f); break;
            case "Colorful Dials": SetText(usedMeshes[39], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Colors Maximization": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.6f); break;
            case "Colour Code": SetText(usedMeshes[10], "ALLISON", 0.25f, 1f); SetText(usedMeshes[11], "ECKHART", 0.325f, 1f); break;
            case "Combination Lock": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.5f, 0.4f); break;
            case "Connection Check": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.6f); break;
            case "Cooking": SetText(usedMeshes[8], "\nALLISON\nECKHART", 0.56f, 0.383f); break;
            case "Cosmic": SetText(usedMeshes[2], "ALLISON", 0.666f, 1f); SetText(usedMeshes[0], "ECKHART", 0.555f, 1f); break;
            case "Critters": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.357f, 0.266f); break;
            case "Cruel Binary": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Cruel Boolean Maze": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Cruel Colour Flash": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 0.8f, 1f); break;
            case "Cruel Modulo": SetText(usedMeshes[13], "ALLISON", 0.333f, 1f); SetText(usedMeshes[14], "ECKHART", 0.333f, 1f); break;
            case "Cryptic Password": SetText(usedMeshes[6], "ALLISON\nECKHART", 0.875f, 0.475f); break;
            case "Crypto Market": SetText(usedMeshes[0], "ALLISON", 0.632f, 0.8f); SetText(usedMeshes[1], "ECKHART", 0.789f, 1f); break;
            case "Currents": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.837f, 0.482f); break;
            case "Curriculum": SetText(usedMeshes[2], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Customer Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 0.555f, 0.5f); break;
            case "Daylight Directions": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.75f, 0.5f); break;
            case "Deck Creating": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.764f, 0.5f); break;
            case "Dice Cipher": SetText(usedMeshes[0], "ALLISON", 0.7f, 1f); SetText(usedMeshes[1], "ECKHART", 0.866f, 1f); break;
            case "Dictation": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.8f, 0.4f); break;
            case "Digital Dials": SetText(usedMeshes[37], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Digital Grid": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[26], "ECKHART", 1f, 1f); break;
            case "Discolour Flash": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Divisible Numbers": SetText(usedMeshes[1], "ALLISON", 0.514f, 1f); SetText(usedMeshes[2], "ECKHART", 0.514f, 1f); break;
            case "Dominoes": SetText(usedMeshes[0], "ALLISON ECKHART", 0.7f, 1f); break;
            case "Double Color": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.826f, 0.6f); break;
            case "Double Digits": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.5f); break;
            case "Double Expert": SetText(usedMeshes[0], "ALLISON", 0.6f, 0.6f); SetText(usedMeshes[1], "ECKHART", 0.6f, 0.6f); break;
            case "Double Pitch": SetText(usedMeshes[16], "ALLISON", 0.222f, 1f); SetText(usedMeshes[18], "ECKHART", 0.167f, 1f); break;
            case "Dragon Energy": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.6f); break;
            case "Dual Sequences": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.171f, 0.357f); break;
            case "Eight": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.48f); break;
            case "Emotiguy Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 0.557f, 0.6f); break;
            case "Encrypted Dice": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.5f); break;
            case "Encrypted Equations": SetText(usedMeshes[0], "ALLISON", 0.267f, 1f); SetText(usedMeshes[1], "ECKHART", 0.267f, 1f); break;
            case "Encrypted Morse": SetText(usedMeshes[0], "ALLISON", 0.2f, 0.2f); SetText(usedMeshes[1], "ECKHART", 0.2f, 0.2f); break;
            case "Encryption Lingo": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.765f, 0.667f); break;
            case "English Entries": SetText(usedMeshes[1], "ALLISON", 0.4f, 1f); SetText(usedMeshes[2], "ECKHART", 0.34f, 1f); break;
            case "Equations X": SetText(usedMeshes[14], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Error Codes": SetText(usedMeshes[13], "ALLISON\nECKHART", 0.8f, 1f); break;
            case "Face Recognition": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 0.488f); break;
            case "Factory Code": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 0.5f); break;
            case "Fast Math": SetText(usedMeshes[12], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "Faulty 14 Segment Display": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.783f, 0.901f); break;
            case "Faulty Accelerando": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "Faulty Binary": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Faulty Digital Root": SetText(usedMeshes[2], "ALLISON", 0.625f, 1f); SetText(usedMeshes[3], "ECKHART", 0.625f, 1f); break;
            case "Feature Cryptography": SetText(usedMeshes[7], "ALLISON", 0.1125f, 0.5f); SetText(usedMeshes[8], "ECKHART", 0.1125f, 0.5f); break;
            case "Fishing": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Fitting In": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.606f, 0.4f); break;
            case "FizzBuzz": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Flags": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Flavor Text": SetText(usedMeshes[0], "ALLISON", 0.38f, 1f); SetText(usedMeshes[1], "ECKHART", 0.34f, 1f); break;
            case "Fruits": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.715f, 0.5f); break;
            case "Functions": SetText(usedMeshes[10], "ALLISON", 0.25f, 1f); SetText(usedMeshes[11], "ECKHART", 0.25f, 1f); break;
            case "Game of Life Cruel": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Game of Life Simple": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Generated Maze": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.3f, 0.5f); break;
            case "Genetic Sequence": SetText(usedMeshes[4], "ALLISON", 0.733f, 1f); SetText(usedMeshes[5], "ECKHART", 0.733f, 1f); break;
            case "Geometry Dash": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 0.9f, 1f); break;
            case "Geometry": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Golf": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Greek Calculus": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[17], "ECKHART", 1f, 1f); break;
            case "Greek Letter Grid": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Grid Matching": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Gridlock": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Grocery Store": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Hereditary Base Notation": SetText(usedMeshes[16], "ALLISON", 1f, 1f); SetText(usedMeshes[17], "ECKHART", 1f, 1f); break;
            case "Hertz": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Hill Cycle": SetText(usedMeshes[22], "A", 1f, 1f); SetText(usedMeshes[23], "L", 1f, 1f); SetText(usedMeshes[24], "L", 1f, 1f); SetText(usedMeshes[25], "I", 1f, 1f); SetText(usedMeshes[26], "S", 1f, 1f); SetText(usedMeshes[27], "O", 1f, 1f); SetText(usedMeshes[28], "N", 1f, 1f); SetText(usedMeshes[31], "E", 1f, 1f); SetText(usedMeshes[32], "C", 1f, 1f); SetText(usedMeshes[33], "K", 1f, 1f); SetText(usedMeshes[34], "H", 1f, 1f); SetText(usedMeshes[35], "A", 1f, 1f); SetText(usedMeshes[36], "R", 1f, 1f); SetText(usedMeshes[37], "T", 1f, 1f); break;
            case "Hold On": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Homophones": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 1f); break;
            case "Human Resources": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "I'm Not a Robot": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[5], "ECKHART", 1f, 1f); SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Ice Cream": SetText(usedMeshes[4], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Icon Reveal": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Identification Crisis": SetText(usedMeshes[37], "ALLISON\nECKHART", 1f, 1f); break;
            case "Identifying Soulless": SetText(usedMeshes[40], "ALLISON\nECKHART", 1f, 1f); break;
            case "Identity Parade": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Increasing Indices": SetText(usedMeshes[9], "ALLISON ECKHART", 1f, 1f); break;
            case "Ingredients": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Insanagrams": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[50], "ECKHART", 1f, 1f); break;
            case "Inside": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Iron Lung": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Jackbox.TV": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Johnson Solids": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Jumble Cycle": SetText(usedMeshes[25], "A", 1f, 1f); SetText(usedMeshes[26], "L", 1f, 1f); SetText(usedMeshes[27], "L", 1f, 1f); SetText(usedMeshes[28], "I", 1f, 1f); SetText(usedMeshes[29], "S", 1f, 1f); SetText(usedMeshes[30], "O", 1f, 1f); SetText(usedMeshes[31], "N", 1f, 1f); SetText(usedMeshes[34], "E", 1f, 1f); SetText(usedMeshes[35], "C", 1f, 1f); SetText(usedMeshes[36], "K", 1f, 1f); SetText(usedMeshes[37], "H", 1f, 1f); SetText(usedMeshes[38], "A", 1f, 1f); SetText(usedMeshes[39], "R", 1f, 1f); SetText(usedMeshes[40], "T", 1f, 1f); break;
            case "Kahoot!": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Keep Clicking": SetText(usedMeshes[3], "ALLISON ECKHART", 1f, 1f); break;
            case "Keypad Maze": SetText(usedMeshes[9], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 1f, 1f); break;
            case "Kyudoku": SetText(usedMeshes[36], "ALLISON ECKHART", 1f, 1f); break;
            case "LED Math": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "LEGOs": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Latin Hypercube": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[3], "SON", 1f, 1f); SetText(usedMeshes[5], "ECK", 1f, 1f); SetText(usedMeshes[7], "HART", 1f, 1f); break;
            case "Levenshtien Distance": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[10], "ECKHART", 1f, 1f); break;
            case "Life Iteration": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Line Equations": SetText(usedMeshes[16], "ALLISON", 1f, 1f); SetText(usedMeshes[19], "ECKHART", 1f, 1f); break;
            case "Lines of Code": SetText(usedMeshes[12], "ALLISON ECKHART", 1f, 1f); break;
            case "Logic": SetText(usedMeshes[16], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Mahjong Quiz Easy": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mahjong Quiz Hard": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mahjong Quiz Scrambled": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Maintenance": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Malfunctions": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 1f, 1f); break;
            case "Mashematics": SetText(usedMeshes[7], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Masked Morse": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Mastermind Cruel": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Mastermind Restricted Cruel": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mastermind Simple": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Matchematics": SetText(usedMeshes[3], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[4], "ECKHART", 0.9f, 0.9f); break;
            case "Matchmaker": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Math 'em": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "Maze Identification": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 1f); break;
            case "Maze Scrambler": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f); break;
            case "Mega Man 2": SetText(usedMeshes[10], "ALLISON ECKHART", 1f, 1f); break;
            case "Melody Memory": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Meme Review": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Meteor": SetText(usedMeshes[3], "ALLISON ECKHART", 1f, 1f); break;
            case "Meter": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Micro-Modules": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Mineseeker": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Minesweeper": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mischboozl": SetText(usedMeshes[3], "ALLISON ECKHART", 1f, 1f); break;
            case "Modern Cipher": SetText(usedMeshes[2], "A", 1f, 1f); SetText(usedMeshes[3], "L", 1f, 1f); SetText(usedMeshes[4], "L", 1f, 1f); SetText(usedMeshes[5], "I", 1f, 1f); SetText(usedMeshes[6], "S", 1f, 1f); SetText(usedMeshes[7], "O", 1f, 1f); SetText(usedMeshes[8], "N", 1f, 1f); SetText(usedMeshes[9], "E", 1f, 1f); SetText(usedMeshes[10], "C", 1f, 1f); SetText(usedMeshes[11], "K", 1f, 1f); SetText(usedMeshes[12], "H", 1f, 1f); SetText(usedMeshes[13], "A", 1f, 1f); SetText(usedMeshes[14], "R", 1f, 1f); SetText(usedMeshes[15], "T", 1f, 1f); break;
            case "Modules Against Humanity": SetText(usedMeshes[4], "ALLISON", 1f, 1f); SetText(usedMeshes[5], "ECKHART", 1f, 1f); break;
            case "Modulo": SetText(usedMeshes[13], "ALLISON", 1f, 1f); SetText(usedMeshes[14], "ECKHART", 1f, 1f); break;
            case "Modulus Manipulation": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Monsplode Trading Cards": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Morse War": SetText(usedMeshes[1], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[0], "ECKHART", 0.9f, 0.9f); break;
            case "Moved": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Mssngv Wls": SetText(usedMeshes[2], "ALLISON ECKHART", 1f, 1f); break;
            case "Murder": SetText(usedMeshes[9], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Musical Transposition": SetText(usedMeshes[2], "ALLISON ECKHART", 1f, 1f); break;
            case "My Mom": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Natures": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Negativity": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Neutralization": SetText(usedMeshes[10], "ALLISON ECKHART", 0.6f, 0.6f); break;
            case "Newline": SetText(usedMeshes[4], "ALLISON", 1f, 1f); SetText(usedMeshes[5], "ECKHART", 1f, 1f); break;
            case "Nifty Number": SetText(usedMeshes[18], "ALLISON ECKHART", 1f, 1f); break;
            case "Nonbinary Puzzle": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Nonogram": SetText(usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Not Bitmaps": SetText(usedMeshes[0], "ALLI", 1f, 1f); SetText(usedMeshes[1], "SON", 1f, 1f); SetText(usedMeshes[2], "ECK", 1f, 1f); SetText(usedMeshes[3], "HART", 1f, 1f); break;
            case "Not Connection Check": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Identification": SetText(usedMeshes[40], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Murder": SetText(usedMeshes[9], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Number Pad": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Not Poker": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Not Symbolic Coordinates": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Symbolic Password": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Notes": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Notre-Dame Cipher": SetText(usedMeshes[4], "ALLISON ECKHART", 1f, 1f); break;
            case "Number Sequence": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "Numbers": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "One Links to All": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Outrageous": SetText(usedMeshes[13], "ALLISON ECKHART", 1f, 1f); break;
            case "Over Kilo": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Palindromes": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Pandemonium Cipher": SetText(usedMeshes[39], "ALLISON ECKHART", 1f, 1f); break;
            case "Papa's Pizzeria": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Parliament": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Partial Derivatives": SetText(usedMeshes[12], "ALLISON", 1f, 1f); SetText(usedMeshes[13], "ECKHART", 1f, 1f); break;
            case "Pawns": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Pickup Identification": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Pigpen Cycle": SetText(usedMeshes[21], "A", 1f, 1f); SetText(usedMeshes[22], "L", 1f, 1f); SetText(usedMeshes[23], "L", 1f, 1f); SetText(usedMeshes[24], "I", 1f, 1f); SetText(usedMeshes[25], "S", 1f, 1f); SetText(usedMeshes[26], "O", 1f, 1f); SetText(usedMeshes[27], "N", 1f, 1f); SetText(usedMeshes[30], "E", 1f, 1f); SetText(usedMeshes[31], "C", 1f, 1f); SetText(usedMeshes[32], "K", 1f, 1f); SetText(usedMeshes[33], "H", 1f, 1f); SetText(usedMeshes[34], "A", 1f, 1f); SetText(usedMeshes[35], "R", 1f, 1f); SetText(usedMeshes[36], "T", 1f, 1f); break;
            case "Pigpen Rotations": SetText(usedMeshes[27], "ALLISON", 1f, 1f); SetText(usedMeshes[26], "ECKHART", 1f, 1f); break;
            case "Pixel Number Base": SetText(usedMeshes[17], "ALLISON\nECKHART", 1f, 1f); break;
            case "Placement Roulette": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Planets": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Plant Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 1f, 1f); break;
            case "Playfair Cycle": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Plumbing": SetText(usedMeshes[0], "ALLISON ECKHART", 0.4f, 0.6f); break;
            case "Poker": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Purchasing Properties": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Puzzle Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 1f, 1f); break;
            case "Quaternions": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Quaver": SetText(usedMeshes[13], "ALLISON\nECKHART", 1f, 1f); break;
            case "Quintuples": SetText(usedMeshes[30], "ALLISON\nECKHART", 1f, 1f); break;
            case "QuizBuzz": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Quote Crazy Talk End Quote": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "RGB Quiz": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Radiator": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 1f, 1f); break;
            case "Rain Hell": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Rain": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "ReGret-B Filtering": SetText(usedMeshes[6], "A", 1f, 1f); SetText(usedMeshes[7], "L", 1f, 1f); SetText(usedMeshes[8], "L", 1f, 1f); SetText(usedMeshes[9], "I", 1f, 1f); SetText(usedMeshes[11], "S", 1f, 1f); SetText(usedMeshes[12], "O", 1f, 1f); SetText(usedMeshes[13], "N", 1f, 1f); SetText(usedMeshes[14], "E", 1f, 1f); SetText(usedMeshes[15], "C", 1f, 1f); SetText(usedMeshes[16], "K", 1f, 1f); SetText(usedMeshes[18], "H", 1f, 1f); SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "R", 1f, 1f); SetText(usedMeshes[21], "T", 1f, 1f); break;
            case "ReGrettaBle Relay": SetText(usedMeshes[6], "A", 1f, 1f); SetText(usedMeshes[7], "L", 1f, 1f); SetText(usedMeshes[8], "L", 1f, 1f); SetText(usedMeshes[9], "I", 1f, 1f); SetText(usedMeshes[11], "S", 1f, 1f); SetText(usedMeshes[12], "O", 1f, 1f); SetText(usedMeshes[13], "N", 1f, 1f); SetText(usedMeshes[14], "E", 1f, 1f); SetText(usedMeshes[15], "C", 1f, 1f); SetText(usedMeshes[16], "K", 1f, 1f); SetText(usedMeshes[18], "H", 1f, 1f); SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "R", 1f, 1f); SetText(usedMeshes[21], "T", 1f, 1f); break;
            case "Reading Between the Lines": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[2], "SON", 1f, 1f); SetText(usedMeshes[3], "ECK", 1f, 1f); SetText(usedMeshes[4], "HART", 1f, 1f); break;
            case "Reformed Role Reversal": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Regular Hexpressions": SetText(usedMeshes[10], "ALLISON\nECKHART", 1f, 1f); break;
            case "Resistors": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Retirement": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Reverse Morse": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Reverse Polish Notation": SetText(usedMeshes[13], "ALLISON", 1f, 1f); SetText(usedMeshes[14], "ECKHART", 1f, 1f); break;
            case "Risky Wires": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Robit Programming": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 1f); break;
            case "Robot Programming": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Roguelike Game": SetText(usedMeshes[6], "ALLISON\nECKHART", 1f, 1f); break;
            case "Role Reversal": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Rubik’s Clock": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "SI-HTS": SetText(usedMeshes[4], "ALLISON ECKHART", 1f, 1f); break;
            case "SQL - Basic": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "SQL - Cruel": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "SQL - Evil": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Scalar Dials": SetText(usedMeshes[37], "ALLISON ECKHART", 1f, 1f); break;
            case "Schlag den Bomb": SetText(usedMeshes[34], "ALLISON ECKHART", 1f, 1f); break;
            case "Scipting": SetText(usedMeshes[23], "ALLISON ECKHART", 1f, 1f); break;
            case "Scratch-Off": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Sequences": SetText(usedMeshes[13], "ALLISON", 1f, 1f); SetText(usedMeshes[16], "ECKHART", 1f, 1f); break;
            case "Shapes and Bombs": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Shell Game": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Shifting Maze": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Shufflewords": SetText(usedMeshes[25], "ALLISON\nECKHART", 1f, 1f); break;
            case "Shut-the-Box": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Silly Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Silo Autorization": SetText(usedMeshes[34], "ALLISON", 1f, 1f); SetText(usedMeshes[35], "ECKHART", 1f, 1f); break;
            case "Simon Selects": SetText(usedMeshes[8], "ALLISON\nECKHART", 1f, 1f); break;
            case "Simpleton't": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Sink": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Skewed Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Skyrim": SetText(usedMeshes[7], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Snack Attack": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "Sonic the Hedgehog": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Sorry Sliders": SetText(usedMeshes[0], "A\nE", 1f, 1f); SetText(usedMeshes[2], "L\nC", 1f, 1f); SetText(usedMeshes[4], "L\nK", 1f, 1f); SetText(usedMeshes[6], "I\nH", 1f, 1f); SetText(usedMeshes[5], "S\nA", 1f, 1f); SetText(usedMeshes[3], "O\nR", 1f, 1f); SetText(usedMeshes[1], "N\nT", 1f, 1f); break;
            case "Spilling Paint": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "SpriteClub Betting Simulation": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[17], "ECKHART", 1f, 1f); break;
            case "Standard Button Masher": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Starmap Reconstruction": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "State of Aggregation": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Subscribe to Pewdiepie": SetText(usedMeshes[2], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[5], "ECKHART", 0.9f, 0.9f); break;
            case "Subways": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 1f); break;
            case "Switching Maze": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Symbolic Coordinates": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Symbolic Password": SetText(usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Taco Tuesday": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Ternary Converter": SetText(usedMeshes[8], "ALLISON", 1f, 1f); SetText(usedMeshes[9], "ECKHART", 1f, 1f); break;
            case "Ternary Tiles": SetText(usedMeshes[12], "ALLISON\nECKHART", 1f, 1f); break;
            case "Tesseractivity": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[3], "SON", 1f, 1f); SetText(usedMeshes[5], "ECK", 1f, 1f); SetText(usedMeshes[7], "HART", 1f, 1f); break;
            case "Tetramorse": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Amber Button": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Black Button": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Black Page": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "The Calculator": SetText(usedMeshes[22], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Code": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "The Colored Maze": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.6f); break;
            case "The Dealmaker": SetText(usedMeshes[1], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[2], "ECKHART", 0.9f, 0.9f); break;
            case "The Deck of Many Things": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Door": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Exploding Pen": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Festive Jukebox": SetText(usedMeshes[3], "ALLISON\nECKHART", 1.1f, 1.1f); break;
            case "The Funny Number": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "The Furloid Jukebox": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Jukebox": SetText(usedMeshes[3], "ALLISON\nECKHART", .9f, .9f); break;
            case "The Legendere Symbol": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Modkit": SetText(usedMeshes[8], "ALLISON ECKHART", 1f, 1f); SetText(usedMeshes[9], "♀", 1f, 1f); break; //This was Tas' idea I'm nowhere near this clever --Blan
            case "The Number Cipher": SetText(usedMeshes[15], "ALLISON", 1f, 1f); SetText(usedMeshes[16], "ECKHART", 1f, 1f); break;
            case "The Number": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "The Rule": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "The Stock Market": SetText(usedMeshes[17], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Tile Maze": SetText(usedMeshes[53], "ALLISON", 1f, 1f); SetText(usedMeshes[54], "ECKHART", 1f, 1f); break;
            case "The cRule": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Timezone": SetText(usedMeshes[13], "ALLISON\nECKHART", 1f, 1f); break;
            case "Toon Enough": SetText(usedMeshes[0], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[1], "ECKHART", 0.7f, 0.7f); break;
            case "Topsy Turvy": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Totally Accurate Minecraft Simulator": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Touch Transmission": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Towers": SetText(usedMeshes[21], "ALLISON", 1f, 1f); SetText(usedMeshes[20], "ECKHART", 1f, 1f); break;
            case "Training Text": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Transmitted Morse": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Tribal Council": SetText(usedMeshes[6], "ALLISON\nECKHART", 1f, 1f); break;
            case "Triple Term": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 1f); break;
            case "Truchet Tiles": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Turtle Robot": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Two Bits": SetText(usedMeshes[11], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[12], "ECKHART", 0.7f, 0.7f); break;
            case "UIN(+L)": SetText(usedMeshes[24], "ALLISON\nECKHART", 1f, 1f); break;
            case "Ultimate Cycle": SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "L", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "I", 1f, 1f); SetText(usedMeshes[23], "S", 1f, 1f); SetText(usedMeshes[24], "O", 1f, 1f); SetText(usedMeshes[25], "N", 1f, 1f); SetText(usedMeshes[28], "E", 1f, 1f); SetText(usedMeshes[29], "C", 1f, 1f); SetText(usedMeshes[30], "K", 1f, 1f); SetText(usedMeshes[31], "H", 1f, 1f); SetText(usedMeshes[32], "A", 1f, 1f); SetText(usedMeshes[33], "R", 1f, 1f); SetText(usedMeshes[34], "T", 1f, 1f); break;
            case "Ultralogic": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "Unicode": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Unown Cipher": SetText(usedMeshes[9], "ALLISON\nECKHART", 1f, 1f); break;
            case "Wack Game of Life": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Weird Al Yankovic": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "Wendithap'n": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Wolf, Goat, and Cabbage": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Wonder Cipher": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "eeB gnillepS": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Ángel Hernández": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "ƎNA Cipher": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Arithmetic Cipher": case "Blue Cipher": case "Blue Huffman Cipher": case "Brown Cipher": case "Cornflower Cipher": case "Crimson Cipher": case "Forest Cipher":
            case "Gray Cipher": case "Green Cipher": case "Indigo Cipher": case "Lempel-Ziv Cipher": case "Maroon Cipher": case "Orange Cipher": case "Pokemon Sprite Cipher": 
            case "Red Cipher": case "Shape Cipher": case "Violet Cipher": case "White Cipher": case "Yellow Cipher": case "Yellow Huffman Cipher": 
                SetText(usedMeshes[29], "ALLISON", 0.25f, 1f); SetText(usedMeshes[30], "ECKHART", 0.21f, 1f); break;
            default: if (debugMode) { for (int i = 0; i < usedMeshes.Length; i++) SetText(usedMeshes[i], i.ToString(), 0.5f, 0.5f); } break;

            //Unused =
            // QUIRKY //             case "8": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.18f, 0.18f); break;
            // GETS REMOVED //       case "1000 Words": SetText(usedMeshes[5], "ALLISON", 0.5f, 1f); SetText(usedMeshes[6], "ECKHART", 0.5f, 1f); break;
            // BUGGED //             case "14": SetText(usedMeshes[14], "ALLISON\nECKHART", 1f, 1f); break;
			// QUIRKY //             case "Access Codes": SetText(usedMeshes[7], "ALLISON", 0.18f, 1f); SetText(usedMeshes[8], "ECKHART", 0.18f, 1f); break;
            // BUGGED //             case "Amnesia": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            // GETS CHANGED //       case "Antistress": SetText(usedMeshes[5], "ALLISON", 0.1f, 1f); SetText(usedMeshes[4], "ECKHART", 0.125f, 0.75f); break;
			// QUIRKY //             case "Backdoor Hacking": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[2], "SON", 1f, 1f); SetText(usedMeshes[3], "ECK", 1f, 1f); SetText(usedMeshes[4], "HART", 1f, 1f); break;
			// QUIRKY //             case "Brainf---": SetText(usedMeshes[11], "ALLISON", 0.5f, 1f); SetText(usedMeshes[10], "ECKHART", 0.5f, 1f); break;
            // DOES NOT WORK //      case "Broken Guitar Chords": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            // GETS CHANGED //       case "Burnout": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f); break;
			// QUIRKY //             case "Castor": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 0.606f, 1f); break;
            // GETS CHANGED //       case "Dialtones": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            // BREAKS MOD //         case "DNA Mutation": SetText(usedMeshes[0], "ALLI", 0.333f, 1f); SetText(usedMeshes[1], "SON", 0.333f, 1f); SetText(usedMeshes[2], "ECK", 0.333f, 1f); SetText(usedMeshes[3], "HART", 0.266f, 1f); break;
			// QUIRKY //             case "Don't Touch Anything": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.455f, 1f); break;
            // BAD IDEA //           case "Dumb Waiters": SetText(usedMeshes[7], "ALLISON", 1f, 1f); SetText(usedMeshes[15], "ECKHART", 1f, 1f); break;
            // BUGGED //             case "Enigma Cycle": SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "L", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "I", 1f, 1f); SetText(usedMeshes[23], "S", 1f, 1f); SetText(usedMeshes[24], "O", 1f, 1f); SetText(usedMeshes[25], "N", 1f, 1f); SetText(usedMeshes[28], "E", 1f, 1f); SetText(usedMeshes[29], "C", 1f, 1f); SetText(usedMeshes[30], "K", 1f, 1f); SetText(usedMeshes[31], "H", 1f, 1f); SetText(usedMeshes[32], "A", 1f, 1f); SetText(usedMeshes[33], "R", 1f, 1f); SetText(usedMeshes[34], "T", 1f, 1f); break;
            // NOT ALWAYS VISIBLE // case "Faulty Sink": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            // GETS REMOVED //       case "Finite Loop": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
			// QUIRKY //             case "Forget Enigma": SetText(usedMeshes[0], "A", 1f, 1f); SetText(usedMeshes[1], "L", 1f, 1f); SetText(usedMeshes[2], "L", 1f, 1f); SetText(usedMeshes[3], "I", 1f, 1f); SetText(usedMeshes[4], "S", 1f, 1f); SetText(usedMeshes[5], "O", 1f, 1f); SetText(usedMeshes[6], "N", 1f, 1f); SetText(usedMeshes[7], "E", 1f, 1f); SetText(usedMeshes[8], "C", 1f, 1f); SetText(usedMeshes[9], "K", 1f, 1f); SetText(usedMeshes[10], "H", 1f, 1f); SetText(usedMeshes[11], "A", 1f, 1f); SetText(usedMeshes[12], "R", 1f, 1f); SetText(usedMeshes[13], "T", 1f, 1f); break;
			// QUIRKY //             case "Four-Card Monte": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.789f, 0.806f); break;
            // BUGGED //             case "Functional Mapping": SetText(usedMeshes[6], "ALLISON", 1f, 1f); SetText(usedMeshes[7], "ECKHART", 1f, 1f); break;
			// QUIRKY //             case "LOOK AT ME": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
			// QUIRKY //             case "Lunchtime": SetText(usedMeshes[1], "ALLISON ECKHART", 0.7f, 0.7f); break;
			// QUIRKY //             case "Password Destroyer": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
			// QUIRKY //             case "Pollux": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 0.606f, 1f); break;
			// QUIRKY //             case "Scrabble Scramble": SetText(usedMeshes[6], "ALLISON ECKHART", 1f, 1f); break;
			// AUTHOR REQUEST //     case "TetraVex": SetText(usedMeshes[36], "ALLISON\nECKHART", 1f, 1f); break;
            // QUIRKY //             case "The Swan": SetText(usedMeshes[12], "ALLISON\nECKHART", 0.6f, 0.7f); break;
			// QUIRKY //             case "Timing is Everything": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
			// QUIRKY //             case "Top 10 Numbers": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
			
        }
    }

    private void SetText(TextMesh tMesh, string text, float scaleX, float scaleY)
    {
        tMesh.text = text;
        tMesh.gameObject.transform.localScale = new Vector3(tMesh.gameObject.transform.localScale.x * scaleX, tMesh.gameObject.transform.localScale.y * scaleY, tMesh.gameObject.transform.localScale.z);
    }

    string wordWrap(string text, int limit)
    {
        if (text.Length > limit)
        {
            int edge = text.Substring(0, limit).LastIndexOf(' ');
            if (edge > 0)
            {
                string line = text.Substring(0, edge);
                string remainder = text.Substring(edge + 1);
                return line + '\n' + wordWrap(remainder, limit);
            }
        }
        return text;
    }

    private string GetLatestSolve(List<string> a, List<string> b)
    {
        string z = "";
        for(int i = 0; i < b.Count; i++)
        {
            a.Remove(b.ElementAt(i));
        }
        z = a.ElementAt(0);
        return z;
    }
}
