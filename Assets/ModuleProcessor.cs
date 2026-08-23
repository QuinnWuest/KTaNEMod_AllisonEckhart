using System.Collections.Generic;
using UnityEngine;

public class ModuleProcessor
{
    bool DebugFlag = true;

    public class AEModuleInfo
    {
        public string ModuleName;
        public TextMesh ModuleTextMesh;

        public string OriginalString;
        public float OriginalXScale;
        public float OriginalYScale;

        public string AEString;
        public float AEXScale;
        public float AEYScale;

        public AEModuleInfo(string moduleName, TextMesh moduleTextMesh, string originalString, float originalXScale, float originalYScale, string aeString, float aeXScale, float aeYScale)
        {
            ModuleName = moduleName;
            ModuleTextMesh = moduleTextMesh;

            OriginalString = originalString;
            OriginalXScale = originalXScale;
            OriginalYScale = originalYScale;

            AEString = aeString;
            AEXScale = aeXScale;
            AEYScale = aeYScale;
        }
    }

    public AEModuleInfo GetAEModuleInfo(string moduleName, TextMesh tm, string aeText, float xScale, float yScale)
    {
        return new AEModuleInfo(moduleName, tm, tm.text, tm.gameObject.transform.localScale.x, tm.gameObject.transform.localScale.y, aeText, tm.gameObject.transform.localScale.x * xScale, tm.gameObject.transform.localScale.y * yScale);
    }

    public List<AEModuleInfo> _aeModuleInfos = new List<AEModuleInfo>();

    public void SetTexts(bool setToAllisonEckhart)
    {
        if (setToAllisonEckhart)
        {
            foreach (var aeinfo in _aeModuleInfos)
            {
                var tMesh = aeinfo.ModuleTextMesh;
                tMesh.text = aeinfo.AEString;
                tMesh.gameObject.transform.localScale = new Vector3(aeinfo.AEXScale, aeinfo.AEYScale, tMesh.gameObject.transform.localScale.z);
            }
        }
        else
        {
            foreach (var aeinfo in _aeModuleInfos)
            {
                var tMesh = aeinfo.ModuleTextMesh;
                tMesh.text = aeinfo.OriginalString;
                tMesh.gameObject.transform.localScale = new Vector3(aeinfo.OriginalXScale, aeinfo.OriginalYScale, tMesh.gameObject.transform.localScale.z);
            }
        }
    }

    public void GatherModuleInfo(KMBombModule module)
    {
        ModuleInfo info = Data.data[module.ModuleDisplayName];
        TextMesh[] usedMeshes = info.GetTextMeshes(module);

        switch (module.ModuleDisplayName)
        {
            case "0":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON", 0.25f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.22f, 0.5f));
                break;
            case "3N+1":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 0.666f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 0.555f, 1f));
                break;
            case "A Message":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 0.666f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 0.666f, 1f));
                break;
            case "ASCII Art":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[36], "ALLISON\nECKHART", 0.8f, 0.5f));
                break;
            case "ASCII Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON", 0.2f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECKHART", 0.1f, 0.25f));
                break;
            case "Accelerando":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.5f));
                break;
            case "Accumulation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 0.9f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 0.9f, 1f));
                break;
            case "Addition":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 0.35f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.3f, 1f));
                break;
            case "Adjacent Letters":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 0.8f, 0.8f));
                break;
            case "Adventure Game":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ALLISON\nECKHART", 0.428f, 0.625f));
                break;
            case "Affine Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[29], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[32], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[33], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "T", 1f, 1f));
                break;
            case "Alchemy":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Alfa-Bravo":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.54f));
                break;
            case "Algebra":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 0.7f, 0.7f));
                break;
            case "Answering Can Be Fun":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON\nECKHART", 0.6875f, 0.555f));
                break;
            case "Antichamber":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Ars Goetia Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[38], "ALLISON\nECKHART", 0.49f, 0.49f));
                break;
            case "Atbash Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.696f, 0.696f));
                break;
            case "Audio Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Backgrounds":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 0.8f, 1f));
                break;
            case "Bartending":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 0.909f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 0.882f, 1f));
                break;
            case "Base-1":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON\nECKHART", 0.429f, 0.5f));
                break;
            case "Basic Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[19], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "T", 1f, 1f));
                break;
            case "Benedict Cumberbatch":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "ALLISON\nECKHART", 0.833f, 0.448f));
                break;
            case "Binary Buttons":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON\nECKHART", 1f, 0.486f));
                break;
            case "Binary Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 0.24f, 1f));
                break;
            case "Binary Grid":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "ECKHART", 1f, 1f));
                break;
            case "Binary Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON", 0.18f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ECKHART", 0.18f, 1f));
                break;
            case "Binary":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Bitmaps":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLI", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "SON", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECK", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "HART", 0.5f, 0.5f));
                break;
            case "Bitwise Operations":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON\nECKHART", 0.6f, 0.4f));
                break;
            case "Blackjack":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "HART", 1f, 1f));
                break;
            case "Blank Card":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.4f));
                break;
            case "Blaseball":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 0.555f));
                break;
            case "Blind Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON\nECKHART", 0.7f, 0.8f));
                break;
            case "Blockbusters":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 0.6f, 0.6f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 0.6f, 0.6f));
                break;
            case "Bomb It!":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 0.642f));
                break;
            case "Bone Apple Tea":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.15f, 0.4f));
                break;
            case "Boolean Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 1f, 1f));
                break;
            case "Boomdas":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON\nECKHART", 0.833f, 1f));
                break;
            case "Boozlesnap":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", -0.2f, -0.28f));
                break;
            case "Bowling":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON\nECKHART", 0.8f, 0.6f));
                break;
            case "Boxing":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "A LLISON\nECKHART", 1f, 0.5f));
                break;
            case "Breaktime":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Bridge":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Broken Buttons":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 0.7f, 0.7f));
                break;
            case "Button Order":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLI5ON", 0.211f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.188f, 1f));
                break;
            case "CA-RPS":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Caesar Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[29], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[32], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[33], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "T", 1f, 1f));
                break;
            case "Calculus":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;

            //from what I recall, above this was exhaustively going through mods alphabetically

            case "Catchphrase":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON", 0.172f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.15f, 1f));
                break;
            case "Challenge & Contact":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Chaotic Countdown":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ALLI", 0.361f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "SON", 0.361f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ECK", 0.361f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "HART", 0.274f, 0.5f));
                break;
            case "Character Slots":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 0.375f));
                break;
            case "Cheap Checkout":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON", 0.6f, 0.6f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ECKHART", 0.6f, 0.6f));
                break;
            case "Cheep Checkout":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 1f, 1f));
                break;
            case "Chessword":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.64f, 0.4f));
                break;
            case "Chinese Strokes":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 0.3125f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 0.25f, 1f));
                break;
            case "Chinese Zodiac":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Chord Progressions":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 0.866f, 1f));
                break;
            case "Cipher Machine":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "ALLISON", 0.22f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "ECKHART", 0.22f, 1f));
                break;
            case "Cistercian Numbers":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ECKHART", 0.25f, 1f));
                break;
            case "Coffeebucks":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Colo(u)r Talk":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "\n\nALLISON\nECKHART", 0.7f, 0.25f));
                break;
            case "Color Math":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.781f, 0.45f));
                break;
            case "Color One Two":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.286f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.286f, 1f));
                break;
            case "Color-Cycle Button":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 0.425f));
                break;
            case "Colorful Dials":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[39], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Colors Maximization":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 0.6f));
                break;
            case "Colour Code":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.325f, 1f));
                break;
            case "Combination Lock":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 0.5f, 0.4f));
                break;
            case "Connection Check":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.6f));
                break;
            case "Cooking":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "\nALLISON\nECKHART", 0.56f, 0.383f));
                break;
            case "Cosmic":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 0.666f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 0.555f, 1f));
                break;
            case "Critters":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 0.357f, 0.266f));
                break;
            case "Cruel Binary":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 1f, 1f));
                break;
            case "Cruel Boolean Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 1f, 1f));
                break;
            case "Cruel Colour Flash":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 0.8f, 1f));
                break;
            case "Cruel Modulo":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON", 0.333f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ECKHART", 0.333f, 1f));
                break;
            case "Cryptic Password":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON\nECKHART", 0.875f, 0.475f));
                break;
            case "Crypto Market":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.632f, 0.8f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.789f, 1f));
                break;
            case "Currents":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON\nECKHART", 0.837f, 0.482f));
                break;
            case "Curriculum":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Customer Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "ALLISON\nECKHART", 0.555f, 0.5f));
                break;
            case "Daylight Directions":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.75f, 0.5f));
                break;
            case "Deck Creating":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.764f, 0.5f));
                break;
            case "Dice Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.7f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.866f, 1f));
                break;
            case "Dictation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.8f, 0.4f));
                break;
            case "Digital Dials":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[37], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Digital Grid":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "ECKHART", 1f, 1f));
                break;
            case "Discolour Flash":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Divisible Numbers":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 0.514f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 0.514f, 1f));
                break;
            case "Dominoes":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 0.7f, 1f));
                break;
            case "Double Color":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.826f, 0.6f));
                break;
            case "Double Digits":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.5f));
                break;
            case "Double Expert":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.6f, 0.6f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.6f, 0.6f));
                break;
            case "Double Pitch":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ALLISON", 0.222f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[18], "ECKHART", 0.167f, 1f));
                break;
            case "Dragon Energy":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.6f));
                break;
            case "Dual Sequences":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON\nECKHART", 0.171f, 0.357f));
                break;
            case "Eight":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.48f));
                break;
            case "Emotiguy Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "ALLISON\nECKHART", 0.557f, 0.6f));
                break;
            case "Encrypted Dice":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 0.5f));
                break;
            case "Encrypted Equations":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.267f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.267f, 1f));
                break;
            case "Encrypted Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.2f, 0.2f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.2f, 0.2f));
                break;
            case "Encryption Lingo":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.765f, 0.667f));
                break;
            case "English Entries":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 0.4f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 0.34f, 1f));
                break;
            case "Equations X":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Error Codes":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON\nECKHART", 0.8f, 1f));
                break;
            case "Face Recognition":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 0.488f));
                break;
            case "Factory Code":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON\nECKHART", 1f, 0.5f));
                break;
            case "Fast Math":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON\nECKHART", 0.4f, 0.5f));
                break;
            case "Faulty 14 Segment Display":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.783f, 0.901f));
                break;
            case "Faulty Accelerando":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.5f));
                break;
            case "Faulty Binary":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Faulty Digital Root":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 0.625f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 0.625f, 1f));
                break;
            case "Feature Cryptography":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ALLISON", 0.1125f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ECKHART", 0.1125f, 0.5f));
                break;
            case "Fishing":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Fitting In":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.606f, 0.4f));
                break;
            case "FizzBuzz":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Flags":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Flavor Text":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.38f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.34f, 1f));
                break;
            case "Fruits":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.715f, 0.5f));
                break;
            case "Functions":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.25f, 1f));
                break;
            case "Game of Life Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Game of Life Simple":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Generated Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.3f, 0.5f));
                break;
            case "Genetic Sequence":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON", 0.733f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECKHART", 0.733f, 1f));
                break;
            case "Geometry Dash":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.9f, 1f));
                break;
            case "Geometry":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Golf":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Greek Calculus":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "ECKHART", 1f, 1f));
                break;
            case "Greek Letter Grid":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Grid Matching":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 1f, 1f));
                break;
            case "Gridlock":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Grocery Store":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Hereditary Base Notation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "ECKHART", 1f, 1f));
                break;
            case "Hertz":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 1f, 1f));
                break;
            case "Hill Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[27], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[28], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[32], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[33], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[36], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[37], "T", 1f, 1f));
                break;
            case "Hold On":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Homophones":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Human Resources":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "I'm Not a Robot":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECKHART", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Ice Cream":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Icon Reveal":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Identification Crisis":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[37], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Identifying Soulless":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Identity Parade":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Increasing Indices":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Ingredients":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 1f, 1f));
                break;
            case "Insanagrams":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[50], "ECKHART", 1f, 1f));
                break;
            case "Inside":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Iron Lung":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 1f, 1f));
                break;
            case "Jackbox.TV":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Johnson Solids":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Jumble Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[27], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[28], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[29], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[36], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[37], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[38], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[39], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "T", 1f, 1f));
                break;
            case "Kahoot!":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Keep Clicking":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Keypad Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 1f, 1f));
                break;
            case "Kyudoku":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[36], "ALLISON ECKHART", 1f, 1f));
                break;
            case "LED Math":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 1f, 1f));
                break;
            case "LEGOs":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Latin Hypercube":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "HART", 1f, 1f));
                break;
            case "Levenshtien Distance":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ECKHART", 1f, 1f));
                break;
            case "Life Iteration":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Line Equations":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[19], "ECKHART", 1f, 1f));
                break;
            case "Lines of Code":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Logic":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Mahjong Quiz Easy":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Mahjong Quiz Hard":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Mahjong Quiz Scrambled":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Maintenance":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Malfunctions":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 1f, 1f));
                break;
            case "Mashematics":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Masked Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Mastermind Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Mastermind Restricted Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Mastermind Simple":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Matchematics":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 0.9f, 0.9f));
                break;
            case "Matchmaker":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Math 'em":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 1f, 1f));
                break;
            case "Maze Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Maze Scrambler":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f));
                break;
            case "Mega Man 2":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Melody Memory":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Meme Review":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Meteor":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Meter":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Micro-Modules":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Mineseeker":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Minesweeper":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Mischboozl":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Modern Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "T", 1f, 1f));
                break;
            case "Modules Against Humanity":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECKHART", 1f, 1f));
                break;
            case "Modulo":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ECKHART", 1f, 1f));
                break;
            case "Modulus Manipulation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Monsplode Trading Cards":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Morse War":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 0.9f, 0.9f));
                break;
            case "Moved":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Mssngv Wls":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Murder":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Musical Transposition":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON ECKHART", 1f, 1f));
                break;
            case "My Mom":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Natures":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 1f, 1f));
                break;
            case "Negativity":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Neutralization":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON ECKHART", 0.6f, 0.6f));
                break;
            case "Newline":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECKHART", 1f, 1f));
                break;
            case "Nifty Number":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[18], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Nonbinary Puzzle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Nonogram":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Not Bitmaps":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "HART", 1f, 1f));
                break;
            case "Not Connection Check":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Not Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Not Murder":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Not Number Pad":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 1f, 1f));
                break;
            case "Not Poker":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Not Symbolic Coordinates":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Not Symbolic Password":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Notes":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Notre-Dame Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Number Sequence":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 1f, 1f));
                break;
            case "Numbers":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "One Links to All":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Outrageous":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Over Kilo":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Palindromes":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Pandemonium Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[39], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Papa's Pizzeria":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Parliament":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Partial Derivatives":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ECKHART", 1f, 1f));
                break;
            case "Pawns":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 1f, 1f));
                break;
            case "Pickup Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Pigpen Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[27], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[32], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[33], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[36], "T", 1f, 1f));
                break;
            case "Pigpen Rotations":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[27], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[26], "ECKHART", 1f, 1f));
                break;
            case "Pixel Number Base":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Placement Roulette":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Planets":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Plant Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Playfair Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Plumbing":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 0.4f, 0.6f));
                break;
            case "Poker":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Purchasing Properties":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Puzzle Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[40], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Quaternions":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 1f, 1f));
                break;
            case "Quaver":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Quintuples":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "QuizBuzz":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ECKHART", 1f, 1f));
                break;
            case "Quote Crazy Talk End Quote":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "RGB Quiz":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Radiator":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 1f, 1f));
                break;
            case "Rain Hell":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Rain":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "ReGret-B Filtering":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[18], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[19], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "T", 1f, 1f));
                break;
            case "ReGrettaBle Relay":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[18], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[19], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "T", 1f, 1f));
                break;
            case "Reading Between the Lines":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "HART", 1f, 1f));
                break;
            case "Reformed Role Reversal":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Regular Hexpressions":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Resistors":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Retirement":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Reverse Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Reverse Polish Notation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ECKHART", 1f, 1f));
                break;
            case "Risky Wires":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Robit Programming":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Robot Programming":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Roguelike Game":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Role Reversal":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Rubik’s Clock":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "SI-HTS":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON ECKHART", 1f, 1f));
                break;
            case "SQL - Basic":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "SQL - Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "SQL - Evil":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Scalar Dials":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[37], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Schlag den Bomb":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Scipting":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Scratch-Off":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Sequences":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ECKHART", 1f, 1f));
                break;
            case "Shapes and Bombs":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Shell Game":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Shifting Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Shufflewords":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Shut-the-Box":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Silly Slots":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Silo Autorization":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[35], "ECKHART", 1f, 1f));
                break;
            case "Simon Selects":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Simpleton't":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Sink":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Skewed Slots":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Skyrim":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 1f, 1f));
                break;
            case "Snack Attack":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Sonic the Hedgehog":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Sorry Sliders":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "A\nE", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "L\nC", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "L\nK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "I\nH", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "S\nA", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "O\nR", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "N\nT", 1f, 1f));
                break;
            case "Spilling Paint":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "SpriteClub Betting Simulation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "ECKHART", 1f, 1f));
                break;
            case "Standard Button Masher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Starmap Reconstruction":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "State of Aggregation":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Subscribe to Pewdiepie":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECKHART", 0.9f, 0.9f));
                break;
            case "Subways":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Switching Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Symbolic Coordinates":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Symbolic Password":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Taco Tuesday":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Ternary Converter":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ECKHART", 1f, 1f));
                break;
            case "Ternary Tiles":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Tesseractivity":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "HART", 1f, 1f));
                break;
            case "Tetramorse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Amber Button":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Black Button":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Black Page":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 1f, 1f));
                break;
            case "The Calculator":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Code":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 1f, 1f));
                break;
            case "The Colored Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.6f));
                break;
            case "The Dealmaker":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 0.9f, 0.9f));
                break;
            case "The Deck of Many Things":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "The Door":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "The Exploding Pen":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Festive Jukebox":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 1.1f, 1.1f));
                break;
            case "The Funny Number":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 1f, 1f));
                break;
            case "The Furloid Jukebox":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "The Jukebox":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", .9f, .9f));
                break;
            case "The Legendere Symbol":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "The Modkit":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ALLISON ECKHART", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "♀", 1f, 1f));
                break; //This was Tas' idea I'm nowhere near this clever --Blan
            case "The Number Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[16], "ECKHART", 1f, 1f));
                break;
            case "The Number":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 1f, 1f));
                break;
            case "The Rule":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "The Stock Market":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[17], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Tile Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[53], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[54], "ECKHART", 1f, 1f));
                break;
            case "The cRule":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Timezone":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Toon Enough":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 0.7f, 0.7f));
                break;
            case "Topsy Turvy":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Totally Accurate Minecraft Simulator":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Touch Transmission":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Towers":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "ECKHART", 1f, 1f));
                break;
            case "Training Text":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Transmitted Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Tribal Council":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Triple Term":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Truchet Tiles":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Turtle Robot":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Two Bits":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ECKHART", 0.7f, 0.7f));
                break;
            case "UIN(+L)":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Ultimate Cycle":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[19], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "L", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "I", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "S", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "O", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "N", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[28], "E", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[29], "C", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "K", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "H", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[32], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[33], "R", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "T", 1f, 1f));
                break;
            case "Ultralogic":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Unicode":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Unown Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Wack Game of Life":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
                break;
            case "Weird Al Yankovic":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Wendithap'n":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Wolf, Goat, and Cabbage":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
                break;
            case "Wonder Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "eeB gnillepS":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECKHART", 1f, 1f));
                break;
            case "Ángel Hernández":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "ƎNA Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Arithmetic Cipher":
            case "Blue Cipher":
            case "Blue Huffman Cipher":
            case "Brown Cipher":
            case "Cornflower Cipher":
            case "Crimson Cipher":
            case "Forest Cipher":
            case "Gray Cipher":
            case "Green Cipher":
            case "Indigo Cipher":
            case "Lempel-Ziv Cipher":
            case "Maroon Cipher":
            case "Orange Cipher":
            case "Pokemon Sprite Cipher":
            case "Red Cipher":
            case "Shape Cipher":
            case "Violet Cipher":
            case "White Cipher":
            case "Yellow Cipher":
            case "Yellow Huffman Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[29], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "ECKHART", 0.21f, 1f));
                break;
            default:
                if (DebugFlag)
                {
                    for (int i = 0; i < usedMeshes.Length; i++)
                        _aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[i], i.ToString(), 0.5f, 0.5f));
                }
                break;

                /*
                //Unused =
                // QUIRKY //             case "8":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.18f, 0.18f));
				break;
                // GETS REMOVED //       case "1000 Words":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 0.5f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ECKHART", 0.5f, 1f));
				break;
                // BUGGED //             case "14":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[14], "ALLISON\nECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "Access Codes":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ALLISON", 0.18f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "ECKHART", 0.18f, 1f));
				break;
                // BUGGED //             case "Amnesia":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECKHART", 1f, 1f));
				break;
                // GETS CHANGED //       case "Antistress":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "ALLISON", 0.1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "ECKHART", 0.125f, 0.75f));
				break;
                // QUIRKY //             case "Backdoor Hacking":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLI", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "SON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "ECK", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "HART", 1f, 1f));
				break;
                // QUIRKY //             case "Brainf---":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON", 0.5f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ECKHART", 0.5f, 1f));
				break;
                // DOES NOT WORK //      case "Broken Guitar Chords":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
				break;
                // GETS CHANGED //       case "Burnout":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f));
				break;
                // QUIRKY //             case "Castor":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.606f, 1f));
				break;
                // GETS CHANGED //       case "Dialtones":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 1f, 1f));
				break;
                // BREAKS MOD //         case "DNA Mutation":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLI", 0.333f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "SON", 0.333f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ECK", 0.333f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "HART", 0.266f, 1f));
				break;
                // QUIRKY //             case "Don't Touch Anything":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON\nECKHART", 0.455f, 1f));
				break;
                // BAD IDEA //           case "Dumb Waiters":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[15], "ECKHART", 1f, 1f));
				break;
                // BUGGED //             case "Enigma Cycle":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[19], "A", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[20], "L", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[21], "L", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[22], "I", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[23], "S", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[24], "O", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[25], "N", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[28], "E", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[29], "C", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[30], "K", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[31], "H", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[32], "A", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[33], "R", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[34], "T", 1f, 1f));
				break;
                // NOT ALWAYS VISIBLE // case "Faulty Sink":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
				break;
                // GETS REMOVED //       case "Finite Loop":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "Forget Enigma":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "A", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "L", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "L", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[3], "I", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[4], "S", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[5], "O", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "N", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "E", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[8], "C", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[9], "K", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "H", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "A", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "R", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[13], "T", 1f, 1f));
				break;
                // QUIRKY //             case "Four-Card Monte":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 0.789f, 0.806f));
				break;
                // BUGGED //             case "Functional Mapping":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[7], "ECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "LOOK AT ME":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[0], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "Lunchtime":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON ECKHART", 0.7f, 0.7f));
				break;
                // QUIRKY //             case "Password Destroyer":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[2], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "Pollux":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[10], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ECKHART", 0.606f, 1f));
				break;
                // QUIRKY //             case "Scrabble Scramble":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[6], "ALLISON ECKHART", 1f, 1f));
				break;
                // AUTHOR REQUEST //     case "TetraVex":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[36], "ALLISON\nECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "The Swan":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[12], "ALLISON\nECKHART", 0.6f, 0.7f));
				break;
                // QUIRKY //             case "Timing is Everything":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[1], "ALLISON\nECKHART", 1f, 1f));
				break;
                // QUIRKY //             case "Top 10 Numbers":
				_aeModuleInfos.Add(GetAEModuleInfo(module.ModuleDisplayName, usedMeshes[11], "ALLISON\nECKHART", 1f, 1f));
				break;
				*/
        }
    }
}
