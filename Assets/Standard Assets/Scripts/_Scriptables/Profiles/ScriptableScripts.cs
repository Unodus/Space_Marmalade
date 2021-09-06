using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ScriptProfile", menuName = "ScriptableObjects/ScriptProfile")]

public class ScriptableScripts : ScriptableBase
{
    [Serializable]
    public class ScriptableScript
    {
        public ScriptableCheatCodes  CheatCodes;
        public ScriptableElias  Elias;
        public ScriptableFont  Fonts;
        public ScriptableGameEvents GameEvents;
        public ScriptableGrid Grids;
        public ScriptableLines Lines;
        public ScriptableParticles Particles;
        public ScriptableScenes Scenes;
        public ScriptableShipParts ShipParts;
        public ScriptableShips Ships;
        public ScriptableSounds Sounds;
        public ScriptableStrings Strings;
        public ScriptableUniverse Universe;
        public ScriptableEnums Enums;
    }

    public ScriptableScript scriptable;    // array of all palettes

}
