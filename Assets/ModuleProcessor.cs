using System;
using System.Collections.Generic;
using UnityEngine;

public class ModuleProcessor
{
    public bool DebugFlag = true;

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

    public void GatherModuleInfo(KMBombModule mod)
    {
        ModuleInfo info = Data.data[mod.ModuleDisplayName];
        TextMesh[] meshes = info.GetTextMeshes(mod);

        try {

        switch (mod.ModuleDisplayName)
        {
            case "The 1, 2, 3 Game":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Astrology":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "POOR\nECKHART", 0.6f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "NO\nECKHART", 0.6f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "GOOD\nECKHART", 0.6f, 1f));
                break;
            case "Baccarat":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART\nTABLE LIMITS\n$50 - $30,000", 1f, 1f));
                break;
            case "Boomtar the Great":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.591f, 0.799f));
                break;
            case "Challenge & Contact":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.8f, 0.667f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[30], "ALLISON\nECKHART", 0.75f, 0.6f));
                break;
            case "Connection Check":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.4f, 0.6f));
                break;
            case "Crazy Hamburger":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Add Allison Eckhart", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "Eat Allison Eckhart", 1f, 1f));
                break;
            case "Cursed":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Curse of\nAllison\nEckhart!", 1f, 1f));
                break;
            case "The Deck of Many Things":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.9f, 1f));
                break;
            case "European Travel":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 1f, 1f));
                break;
            case "Grocery Store":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
                break;
            case "Identity Parade":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Inside":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison Eckhart", 0.888f, 1f));
                break;
            case "The Instar":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON/ECKHART", 0.8f, 1f));
                break;
            case "Matchematics":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 0.9f, 0.9f));
                break;
            case "Monsplode Trading Cards":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "Orientation Cube":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.453f, 0.5f));
                break;
            case "Papa's Pizzeria":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\n\nECKHART", 1f, 1f));
                break;
            case "Prime Checker":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "NOT\nALLISON\nECKHART", 0.8f, 1f));
                break;
            case "Poker":
            case "Not Poker":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison", 0.82f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "Eckhart", 0.82f, 1f));
                break;
            case "Simpleton't":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Tennis":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison Eckhart", 1f, 1f));
                break;
            case "Tyler Verifies":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Notify\nAllison\nEckhart", 0.8f, 0.8f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "@eckhart", 1f, 1f));
                 break;

            //everything above this was double-checked for quality, eventually it will all be merged into one enormous list

            case "0":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON", 0.25f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.22f, 0.5f));
                break;
            case "3N+1":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.666f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 0.555f, 1f));
                break;
            case "A Message":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON", 0.666f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 0.666f, 1f));
                break;
            case "ASCII Art":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[36], "ALLISON\nECKHART", 0.8f, 0.5f));
                break;
            case "ASCII Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ALLISON", 0.2f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ECKHART", 0.1f, 0.25f));
                break;
            case "Accelerando":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.4f, 0.5f));
                break;
            case "Access Codes":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "ALLISON", 0.18f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ECKHART", 0.18f, 1f));
			    break;
            case "Accumulation":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.9f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 0.9f, 1f));
                break;
            case "Addition":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON", 0.35f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.3f, 1f));
                break;
            case "Adjacent Letters":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "Allison Eckhart", 0.6f, 1f));
                break;
            case "Adventure Game":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "ALLISON\nECKHART", 0.428f, 0.625f));
                break;
            //case "Affine Cycle": //why this isn't working is beyond me :[
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A * E + A = E", 1f, 1f));
            //    for (int m = 10; m >= 19; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    for (int m = 20; m >= 35; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISON  ECKHART"[m-20].ToString(), 1f, 1f)); }
            //    break;
            case "Alchemy":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
                break;
            case "Alfa-Bravo":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.7f, 0.54f));
                break;
            case "Algebra":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 0.7f, 0.7f));
                break;
            case "Answering Can Be Fun":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON\nECKHART", 0.6875f, 0.555f));
                break;
            case "Antichamber":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Ars Goetia Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[38], "ALLISON\nECKHART", 0.49f, 0.49f));
                break;
            case "Atbash Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.696f, 0.696f));
                break;
            case "Audio Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Backdoor Hacking":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLI", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "SON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECK", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "HART", 1f, 1f));
				break;
            case "Backgrounds":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.8f, 1f));
                break;
            case "Bartending":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 0.909f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ECKHART", 0.882f, 1f));
                break;
            case "Base-1":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON\nECKHART", 0.429f, 0.5f));
                break;
            //case "Basic Morse":
            //    for (int m = 0; m >= 10; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    for (int m = 11; m >= 25; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISON ECKHART"[m-11].ToString(), 1f, 1f)); }
            //    break;
            case "Benedict Cumberbatch":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[26], "ALLISON\nECKHART", 0.833f, 0.448f));
                break;
            case "Binary Buttons":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON\nECKHART", 1f, 0.486f));
                break;
            case "Binary Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.24f, 1f));
                break;
            case "Binary Grid":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[25], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[26], "ECKHART", 1f, 1f));
                break;
            case "Binary Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON", 0.18f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ECKHART", 0.18f, 1f));
                break;
            case "Binary":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 1f, 1f));
                break;
            case "Bitmaps":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "SON", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECK", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "HART", 0.5f, 0.5f));
                break;
            case "Bitwise Operations":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON\nECKHART", 0.6f, 0.4f));
                break;
            case "Blackjack":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "HART", 1f, 1f));
                break;
            case "Blank Card":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.6f, 0.4f));
                break;
            case "Blaseball":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.555f));
                break;
            case "Blind Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON\nECKHART", 0.7f, 0.8f));
                break;
            case "Blockbusters":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.6f, 0.6f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART", 0.6f, 0.6f));
                break;
            case "Bomb It!":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.642f));
                break;
            case "Bone Apple Tea":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.15f, 0.4f));
                break;
            case "Boolean Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ECKHART", 1f, 1f));
                break;
            case "Boomdas":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON\nECKHART", 0.833f, 1f));
                break;
            case "Boozlesnap":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", -0.2f, -0.28f));
                break;
            case "Bowling":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ALLISON\nECKHART", 0.8f, 0.6f));
                break;
            case "Boxing":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "A LLISON\nECKHART", 1f, 0.5f));
                break;
            case "Breaktime":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Bridge":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Broken Buttons":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 0.7f, 0.7f));
                break;
            case "Button Order":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI5ON", 0.211f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.188f, 1f));
                break;
            case "CA-RPS":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            //case "Caesar Cycle": //???
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A → E", 1f, 1f));
            //    for (int m = 10; m >= 19; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    for (int m = 20; m >= 35; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISON  ECKHART"[m-20].ToString(), 1f, 1f)); }
            //    break;
            case "Calculus":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Castor":
            case "Pollux":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.606f, 1f));
				break;

            //from what I recall, above this was exhaustively going through mods alphabetically

            case "Catchphrase":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON", 0.172f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.15f, 1f));
                break;
            case "Chaotic Countdown":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ALLI", 0.361f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[15], "SON", 0.361f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ECK", 0.361f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[17], "HART", 0.274f, 0.5f));
                break;
            case "Character Slots":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.375f));
                break;
            case "Cheap Checkout":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON", 0.6f, 0.6f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ECKHART", 0.6f, 0.6f));
                break;
            case "Cheep Checkout":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 1f, 1f));
                break;
            case "Chessword":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.64f, 0.4f));
                break;
            case "Chinese Strokes":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.3125f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 0.25f, 1f));
                break;
            case "Chinese Zodiac":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Chord Progressions":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 0.866f, 1f));
                break;
            case "Cipher Machine":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[34], "ALLISON", 0.22f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[35], "ECKHART", 0.22f, 1f));
                break;
            case "Cistercian Numbers":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ECKHART", 0.25f, 1f));
                break;
            case "Coffeebucks":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Colo(u)r Talk":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "\n\nALLISON\nECKHART", 0.7f, 0.25f));
                break;
            case "Color Math":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.781f, 0.45f));
                break;
            case "Color One Two":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.286f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.286f, 1f));
                break;
            case "Color-Cycle Button":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.425f));
                break;
            case "Colorful Dials":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[39], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Colors Maximization":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.6f));
                break;
            case "Colour Code":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.325f, 1f));
                break;
            case "Combination Lock":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.5f, 0.4f));
                break;
            case "Cooking":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "\nALLISON\nECKHART", 0.56f, 0.383f));
                break;
            case "Cosmic":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.666f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 0.555f, 1f));
                break;
            case "Critters":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.357f, 0.266f));
                break;
            case "Cruel Binary":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 1f, 1f));
                break;
            case "Cruel Boolean Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ECKHART", 1f, 1f));
                break;
            case "Cruel Colour Flash":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.8f, 1f));
                break;
            case "Cruel Modulo":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON", 0.333f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ECKHART", 0.333f, 1f));
                break;
            case "Cryptic Password":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ALLISON\nECKHART", 0.875f, 0.475f));
                break;
            case "Crypto Market":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.632f, 0.8f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.789f, 1f));
                break;
            case "Currents":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON\nECKHART", 0.837f, 0.482f));
                break;
            case "Curriculum":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Customer Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[40], "ALLISON\nECKHART", 0.555f, 0.5f));
                break;
            case "Daylight Directions":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.75f, 0.5f));
                break;
            case "Deck Creating":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.764f, 0.5f));
                break;
            case "Dice Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.7f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.866f, 1f));
                break;
            case "Dictation":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.8f, 0.4f));
                break;
            case "Digital Dials":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[37], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Digital Grid":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[26], "ECKHART", 1f, 1f));
                break;
            case "Discolour Flash":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
                break;
            case "Divisible Numbers":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.514f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.514f, 1f));
                break;
            case "Dominoes":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.7f, 1f));
                break;
            case "Double Color":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.826f, 0.6f));
                break;
            case "Double Digits":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.8f, 0.5f));
                break;
            case "Double Expert":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.6f, 0.6f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.6f, 0.6f));
                break;
            //case "Double Pitch": //*eyebrow raise*
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ALLISON", 0.222f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[18], "ECKHART", 0.167f, 1f));
            //    break;
            case "Dragon Energy":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.8f, 0.6f));
                break;
            case "Dual Sequences":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON\nECKHART", 0.171f, 0.357f));
                break;
            case "Eight":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.7f, 0.48f));
                break;
            case "Emotiguy Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[40], "ALLISON\nECKHART", 0.557f, 0.6f));
                break;
            case "Encrypted Dice":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.5f));
                break;
            case "Encrypted Equations":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.267f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.267f, 1f));
                break;
            case "Encrypted Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.2f, 0.2f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.2f, 0.2f));
                break;
            case "Encryption Lingo":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.765f, 0.667f));
                break;
            case "English Entries":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.4f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.34f, 1f));
                break;
            case "Equations X":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Error Codes":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON\nECKHART", 0.8f, 1f));
                break;
            case "Face Recognition":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 1f, 0.488f));
                break;
            //case "Factory Code": //inconsistent
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART", 0.6f, 1f));
            //    break;
            case "Fast Math":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON\nECKHART", 0.4f, 0.5f));
                break;
            case "Faulty 14 Segment Display":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.783f, 0.901f));
                break;
            case "Faulty Accelerando":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.4f, 0.5f));
                break;
            case "Faulty Binary":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 1f, 1f));
                break;
            case "Faulty Digital Root":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.625f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 0.625f, 1f));
                break;
            case "Feature Cryptography":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "ALLISON", 0.1125f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ECKHART", 0.1125f, 0.5f));
                break;
            case "Fishing":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Fitting In":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.606f, 0.4f));
                break;
            case "FizzBuzz":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Flags":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Flavor Text":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.38f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.34f, 1f));
                break;
            case "Fruits":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.715f, 0.5f));
                break;
            case "Functions":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.25f, 1f));
                break;
            case "Game of Life Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "Game of Life Simple":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "Generated Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.3f, 0.5f));
                break;
            case "Genetic Sequence":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON", 0.733f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ECKHART", 0.733f, 1f));
                break;
            case "Geometry Dash":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.9f, 1f));
                break;
            case "Geometry":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.75f, 0.5f));
                break;
            case "Golf":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "Allison Eckhart", 0.5f, 1f));
                break;
            case "Greek Calculus":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[17], "ECKHART", 0.8f, 1f));
                break;
            case "Greek Letter Grid":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.9f, 1f));
                break;
            case "Grid Matching":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 0.25f, 1f));
                break;
            case "Gridlock":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Hereditary Base Notation":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ALLISON", 0.46f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[17], "ECKHART", 0.4f, 1f));
                break;
            case "Hertz":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 0.47f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ECKHART", 0.47f, 1f));
                break;
            //case "Hill Cycle": //ok for loops break everything,
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison", 0.4f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "Eckhart", 0.35f, 1f));
            //    for (int m = 12; m >= 21; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    for (int m = 22; m >= 37; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISON  ECKHART"[m-22].ToString(), 1f, 1f)); }
            //    break;
            case "Hold On":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison\nEckhart", 0.4f, 0.55f));
                break;
            case "Homophones":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON\nECKHART", 0.68f, 0.64f));
                break;
            case "Human Resources":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.66f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.66f, 1f));
                break;
            case "I'm Not a Robot":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Ice Cream":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Icon Reveal":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Identification Crisis":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[37], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Identifying Soulless":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[59], "ALLISON\nECKHART", 0.5f, 1f));
                break;
            case "Increasing Indices":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Ingredients":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "Allison", 0.95f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "Eckhart", 0.72f, 1f));
                break;
            case "Insanagrams":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.88f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 0.77f, 1f));
                break;
            case "Iron Lung":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "E", 1f, 1f));
                break;
            case "Jackbox.TV":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Johnson Solids":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.33f, 1f));
                break;
            //case "Jumble Cycle": //yea for loops are broken
            //    for (int m = 0; m >= 5; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "AE", 1f, 1f)); }
            //    for (int m = 15; m >= 24; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    for (int m = 25; m >= 31; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISON"[m-25].ToString(), 1f, 1f)); }
            //    for (int m = 34; m >= 40; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ECKHART"[m-34].ToString(), 1f, 1f)); }
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[32], "", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[33], "", 1f, 1f));
            //    break;
            case "Kahoot!":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 1f, 1f));
                break;
            case "Keep Clicking":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON ECKHART", 0.8f, 1f));
                break;
            case "Kyudoku":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[37], "ALLISON ECKHART", 0.43f, 1f));
                break;
            case "LED Math":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "E", 1f, 1f));
                break;
            case "LEGOs":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Latin Hypercube":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "HART", 1f, 1f));
                break;
            case "Levenshtein Distance": //another try
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.2f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ECKHART", 0.2f, 1f));
                break;
            case "Life Iteration":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "Line Equations":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ALLISON", 0.3f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[19], "ECKHART", 0.25f, 1f));
                break;
            case "Lines of Code":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON ECKHART", 0.3f, 1f));
                break;
            case "Logic":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Lunchtime":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART", 0.7f, 0.7f));
				break;
            case "Mahjong Quiz Easy":
            case "Mahjong Quiz Hard":
            case "Mahjong Quiz Scrambled":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.75f, 1f));
                break;
            case "Maintenance":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI\nSON\nECK\nHART", 0.5f, 0.8f));
                break;
            case "Malfunctions":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON\nECKHART", 0.25f, 0.5f));
                break;
            case "Mashematics":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Masked Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.3f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.3f, 1f));
                break;
            case "Mastermind Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 1f, 1f));
                break;
            case "Mastermind Restricted Cruel":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "Mastermind Simple":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 1f, 1f));
                break;
            case "Matchmaker":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.8f, 1f));
                break;
            case "Maze Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON\nECKHART", 0.35f, 0.4f));
                break;
            case "Maze Scrambler":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.7f, 0.9f));
                break;
            case "Mega Man 2":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 1f, 1f));
                break;
            case "Melody Memory":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "Eckhart", 1f, 1f));
                break;
            case "Meme Review":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.9f, 1f));
                break;
            case "Meteor":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON ECKHART", 0.5f, 1f));
                break;
            case "Micro-Modules":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Mineseeker":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON ECKHART", 0.4f, 1f));
                break;
            case "Minesweeper":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            //case "Modern Cipher": //fuck me dude
            //    for (int m = 2; m >= 15; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISONECKHART"[m-2].ToString(), 1f, 1f)); }
            //    for (int m = 16; m >= 27; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    break;
            case "Modules Against Humanity":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ECKHART", 0.8f, 1f));
                break;
            case "Modulo":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON", 0.4f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ECKHART", 0.4f, 1f));
                break;
            case "Modulus Manipulation":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Morse War": //small but fine
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 0.9f, 0.9f));
                break;
            case "Moved":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.25f, 0.42f));
                break;
            case "Mssngv Wls":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON ECKHART", 0.6f, 1f));
                break;
            case "Murder":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Musical Transposition":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON ECKHART", 0.5f, 1f));
                break;
            case "My Mom":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.53f, 1f));
                break;
            case "Natures":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 1f, 1f));
                break;
            case "Negativity":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.28f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 0.25f, 1f));
                break;
            case "Neutralization":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON ECKHART", 0.6f, 0.6f));
                break;
            case "Nonbinary Puzzle":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.6f, 1f));
                break;
            case "Nonogram":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Not Bitmaps":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "SON", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECK", 0.5f, 0.5f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "HART", 0.5f, 0.5f));
                break;
            case "Not Connection Check":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.5f, 1f));
                break;
            case "Not Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[40], "ALLISON ECKHART", 0.26f, 1f));
                break;
            case "Not Murder":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON ECKHART", 0.4f, 1f));
                break;
            case "Not Number Pad":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.6f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 0.6f, 1f));
                break;
            case "Not Symbolic Coordinates":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.66f, 1f));
                break;
            case "Not Symbolic Password":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.6f, 1f));
                break;
            case "Notes":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.7f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.7f, 1f));
                break;
            case "Notre-Dame Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON ECKHART", 0.7f, 1f));
                break;
            case "Number Sequence":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.2f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 0.2f, 1f));
                break;
            //case "Numbers": //?????????????????????????
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
            //    break;
            case "One Links To All":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.5f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 0.5f, 1f));
                break;
            case "Over Kilo":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A", 0.8f, 0.8f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "E", 0.8f, 0.8f));
                break;
            case "Palindromes":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.49f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.2f, 1f));
                break;
            case "Pandemonium Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[39], "ALLISON ECKHART", 0.35f, 1f));
                break;
            case "Parliament":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "Partial Derivatives":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "E", 1f, 1f));
                break;
            case "Password Destroyer":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.5f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.5f, 1f));
				break;
            case "Pawns":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ECKHART", 0.8f, 1f));
                break;
            //case "Pickup Identification": //god i am so dumb
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
            //    break;
            //case "Pigpen Cycle": //oom
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[21], "A", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[22], "L", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[23], "L", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[24], "I", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[25], "S", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[26], "O", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[27], "N", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[30], "E", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[31], "C", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[32], "K", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[33], "H", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[34], "A", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[35], "R", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[36], "T", 1f, 1f));
            //    break;
            case "Pigpen Rotations":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[27], "ALLISON", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[26], "ECKHART", 1f, 1f));
                break;
            case "Pixel Number Base":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[17], "ALLISON\nECKHART", 0.8f, 0.8f));
                break;
            //case "Placement Roulette": //wrong thing
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
            //   break;
            case "Planets":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON\nECKHART", 1f, 0.6f));
                break;
            case "Plant Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[40], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            //case "Playfair Cycle": //???
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
            //    break;
            case "Plumbing":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.36f, 0.6f));
                break;
            case "Purchasing Properties":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Puzzle Identification":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[40], "ALLISON\nECKHART", 0.8f, 0.8f));
                break;
            case "Quaternions":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ECKHART", 0.8f, 1f));
                break;
            //case "Quaver":
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON\nECKHART", 1f, 1f));
            //    break;
            case "Quintuples":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[30], "ALLISON\nECKHART", 1f, 0.8f));
                break;
            case "RGB Quiz":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.45f));
                break;
            case "Radiator":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 1f, 1f));
                break;
            case "Rain":
            case "Rain Hell":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.75f));
                break;
            //case "ReGret-B Filtering": //bork
            //case "ReGrettaBle Relay":
            //    for (int m = 6; m >= 21; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLI SONECK HART"[m-6].ToString(), 1f, 1f)); }
            //    break;
            case "Reading Between the Lines":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLI", 0.67f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "SON", 0.73f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECK", 0.78f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "HART", 0.54f, 1f));
                break;
            case "Reformed Role Reversal":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI", 0.3f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "SON", 0.3f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECK", 0.3f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "HART", 0.3f, 1f));
                break;
            case "Regular Hexpressions":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON ECKHART", 0.4f, 1f));
                break;
            case "Resistors":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.9f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.8f, 1f));
                break;
            case "Retirement":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "Allison Eckhart", 0.55f, 1f));
                break;
            case "Reverse Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.48f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.48f, 1f));
                break;
            case "Reverse Polish Notation":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON", 0.63f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ECKHART", 0.58f, 1f));
                break;
            case "Risky Wires":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
                break;
            case "Robit Programming":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON\nECKHART", 1f, 0.43f));
                break;
            case "Robot Programming":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.23f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.2f, 1f));
                break;
            case "Roguelike Game":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ALLISON\nECKHART", 0.92f, 0.6f));
                break;
            case "Role Reversal":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.4f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ECKHART", 0.37f, 1f));
                break;
            case "SI-HTS": //this is a try, might not be right
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "Allison", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "Eckhart", 1f, 1f));
                break;
            case "SQL - Basic":
            case "SQL - Cruel":
            case "SQL - Evil":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART", 0.6f, 1f));
                break;
            case "Scalar Dials":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[37], "Allison Eckhart", 0.45f, 1f));
                break;
            case "Schlag den Bomb": 
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[34], "ALLISON\nECKHART", 0.25f, 0.5f));
                break;
            case "Scripting":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[29], "allison eckhart", 0.45f, 1f));
                break;
            case "Scratch-Off":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.5f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART", 0.5f, 1f));
                break;
            case "Sequences":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON", 0.5f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ECKHART", 0.43f, 1f));
                break;
            case "Shapes And Bombs":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "Allison", 0.9f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "Eckhart", 0.8f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "Keter", 0.1f, 0.1f));
                break;
            case "Shell Game":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.36f, 0.66f));
                break;
            case "Shifting Maze":
            //case "Switching Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.85f, 0.47f));
                break;
            case "Shufflewords":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[25], "ALLISON\nECKHART", 0.36f, 0.84f));
                break;
            case "Shut-the-Box":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.7f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.65f, 1f));
                break;
            case "Silly Slots":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.41f, 1f));
                break;
            case "Silo Authorization":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[34], "Allison", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[35], "Eckhart", 1f, 1f));
                break;
            case "Sink":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.45f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.4f, 1f));
                break;
            case "Skewed Slots":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "Skyrim":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Snack Attack":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Sonic the Hedgehog":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.6f, 0.6f));
                break;
            case "Sorry Sliders":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A\nE", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "L\nC", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "L\nK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "I\nH", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "S\nA", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "O\nR", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "N\nT", 1f, 1f));
                break;
            case "Spilling Paint":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "allison\neckhart", 0.6f, 0.3f));
                break;
            case "Standard Button Masher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.625f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.81f, 1f));
                break;
            case "Starmap Reconstruction":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "E", 1f, 1f));
                break;
            case "State of Aggregation":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON ECKHART", 0.5f, 1f));
                break;
            case "Subscribe to Pewdiepie":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ECKHART", 0.9f, 0.9f));
                break;
            case "Subways":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON\nECKHART", 0.9f, 1f));
                break;
            case "Symbolic Coordinates":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.59f));
                break;
            case "Symbolic Password":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON ECKHART", 0.5f, 0.5f));
                break;
            case "Taco Tuesday":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Ternary Converter":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON", 0.7f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ECKHART", 0.6f, 1f));
                break;
            case "Ternary Tiles":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON\nECKHART", 1f, 0.53f));
                break;
            case "Tesseractivity":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLI", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "SON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ECK", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "HART", 1f, 1f));
                break;
            case "Tetramorse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ALLISON\nECKHART", 0.5f, 0.6f));
                break;
            case "The Amber Button":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.2f, 0.25f));
                break;
            case "The Black Button":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ae", 1f, 1f));
                break;
            case "The Black Page":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON", 0.625f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 0.78f, 1f));
                break;
            case "The Calculator":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON ECKHART ", 1f, 1f));
                break;
            case "The Code":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.4f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 0.37f, 1f));
                break;
            case "The Colored Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.7f, 0.6f));
                break;
            case "The Dealmaker":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 0.9f, 0.9f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 0.9f, 0.9f));
                break;
            case "The Door":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "E", 1f, 1f));
                break;
            case "The Exploding Pen":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "The Festive Jukebox":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 1.1f, 1.1f));
                break;
            case "The Funny Number":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 1f, 1f));
                break;
            case "The Furloid Jukebox":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                break;
            case "The Jukebox":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", .9f, .9f));
                break;
            case "The Legendre Symbol":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "E", 1f, 1f));
                break;
            //case "The Modkit": //sobb
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "ALLISON ECKHART", 1f, 1f));
            //    _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "♀", 1f, 1f)); //This was Tas' idea I'm nowhere near this clever --Blan
            //    break;
            case "The Number Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[15], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[16], "ECKHART", 0.2f, 1f));
                break;
            case "The Number":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.3f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 0.3f, 1f));
                break;
            case "The Rule":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.5f, 0.5f));
                break;
            case "The Stock Market":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON\nECKHART", 0.8f, 0.4f));
                break;
            case "The Tile Maze":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[49], "   Alli", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[50], "son   ", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[51], "", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[52], "", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[53], "Eck", 0.5f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[54], "hart", 0.4f, 1f));
                break;
            case "The cRule":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 0.4f));
                break;
            case "The Swan":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ALLISON\nECKHART", 0.6f, 0.6f));
				break;
            case "Timezone":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Toon Enough":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.7f, 0.7f));
                break;
            case "Topsy Turvy":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON\nECKHART", 0.3f, 0.3f));
                break;
            case "Totally Accurate Minecraft Simulator":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ALLISON", 0.6f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.6f, 1f));
                break;
            case "Touch Transmission":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.6f, 0.5f));
                break;
            case "Towers":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[21], "A", 0.8f, 0.8f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[20], "E", 0.8f, 0.8f));
                break;
            case "Training Text":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Transmitted Morse":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.27f, 0.45f));
                break;
            case "Triple Term":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON\nECKHART", 0.8f, 0.7f));
                break;
            case "Truchet Tiles":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.3f, 0.5f));
                break;
            case "Two Bits":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.7f, 0.7f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "ECKHART", 0.7f, 0.7f));
                break;
            case "UIN(+L)":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Allison Eckhart", 0.77f, 1f));
                break;
            //case "Ultimate Cycle":
            //    for (int m = 9; m >= 18; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "", 1f, 1f)); }
            //    for (int m = 19; m >= 34; m++) { _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[m], "ALLISON  ECKHART"[m-19].ToString(), 1f, 1f)); }
            //    break;
            case "Ultralogic":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 0.9f, 1f));
                break;
            case "Unicode":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.22f, 0.37f));
                break;
            case "Unown Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "ALLISON\nECKHART", 0.7f, 0.34f));
                break;
            case "Wack Game of Life":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
                break;
            case "Weird Al Yankovic":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "ALLISON\nECKHART", 1f, 1f));
                break;
            case "Wolf, Goat, and Cabbage":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 0.5f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 0.6f, 1f));
                break;
            case "Wonder Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.8f, 0.66f));
                break;
            case "eeB gnillepS":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "A", 1f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "E", 1f, 1f));
                break;
            case "Ángel Hernández":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "Állison Eckhárt", 0.7f, 1f));
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
            case "Pokémon Sprite Cipher":
            case "Red Cipher":
            case "Shape Cipher":
            case "Violet Cipher":
            case "White Cipher":
            case "Yellow Cipher":
            case "Yellow Huffman Cipher":
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[29], "ALLISON", 0.25f, 1f));
                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[30], "ECKHART", 0.21f, 1f));
                break;
            default:
                if (DebugFlag)
                {
                    for (int i = 0; i < meshes.Length; i++)
                        _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[i], i.ToString(), 1f, 1f));
                }
                break;

                /*
                //Unused =
                //BOSS (remember: to un Allison Eckhart Allison Eckhart, you need to solve the module in question!)// case "8":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.18f, 0.18f));
			    break;
                //BOSS// case "Brainf---":
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON", 0.5f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ECKHART", 0.5f, 1f));
                                break;
                //END// case "Don't Touch Anything":
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.455f, 1f));
                                break;
                //BOSS// case "Forget Enigma":
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "A", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "L", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "L", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "I", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "S", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "O", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "N", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "E", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[8], "C", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[9], "K", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "H", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "A", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[12], "R", 1f, 1f));
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[13], "T", 1f, 1f));
                                break;
                //BOSSISH// case "Four-Card Monte":
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 0.789f, 0.806f));
                                break;
                //TIME// case "Timing is Everything":
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 1f, 1f));
                                break;
                //BOSS// case "Top 10 Numbers":
                                _aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ALLISON\nECKHART", 1f, 1f));
                                break;
                
                //PROBS DOESN'T WORK IDK FOR CERTAIN NOR DO I REALLY CARE// case "LOOK AT ME":
                	_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
                	_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
                	break;
                // GETS REMOVED //       case "1000 Words":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 0.5f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ECKHART", 0.5f, 1f));
				break;
                // BUGGED //             case "14":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[14], "ALLISON\nECKHART", 1f, 1f));
				break;
                // BUGGED //             case "Amnesia":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECKHART", 1f, 1f));
				break;
                // GETS CHANGED //       case "Antistress":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[5], "ALLISON", 0.1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[4], "ECKHART", 0.125f, 0.75f));
				break;
                // DOES NOT WORK //      case "Broken Guitar Chords":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
				break;
                // GETS CHANGED //       case "Burnout":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 0.7f, 0.9f));
				break;
                // QUIRKY //             case "Castor":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[10], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[11], "ECKHART", 0.606f, 1f));
				break;
                // GETS CHANGED //       case "Dialtones":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON\nECKHART", 1f, 1f));
				break;
                // BREAKS MOD //         case "DNA Mutation":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLI", 0.333f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "SON", 0.333f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[2], "ECK", 0.333f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[3], "HART", 0.266f, 1f));
				break;
                // BAD IDEA //           case "Dumb Waiters":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[15], "ECKHART", 1f, 1f));
				break;
                // BUGGED //             case "Enigma Cycle":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[19], "A", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[20], "L", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[21], "L", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[22], "I", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[23], "S", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[24], "O", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[25], "N", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[28], "E", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[29], "C", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[30], "K", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[31], "H", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[32], "A", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[33], "R", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[34], "T", 1f, 1f));
				break;
                // NOT ALWAYS VISIBLE // case "Faulty Sink":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[0], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ECKHART", 1f, 1f));
				break;
                // GETS REMOVED //       case "Finite Loop":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[1], "ALLISON\nECKHART", 1f, 1f));
				break;
                // BUGGED //             case "Functional Mapping":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[6], "ALLISON", 1f, 1f));
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[7], "ECKHART", 1f, 1f));
				break;
                // AUTHOR REQUEST //     case "TetraVex":
				_aeModuleInfos.Add(GetAEModuleInfo(mod.ModuleDisplayName, meshes[36], "ALLISON\nECKHART", 1f, 1f));
				break;
				*/
        }

        } catch (Exception error) //an exception should only be caught in the case of IndexOutOfRangeException
        {
            Debug.LogFormat("<Allison Eckhart Module Processor> Error with {0}: {1}", mod.ModuleDisplayName, error);
        }
    }
}
