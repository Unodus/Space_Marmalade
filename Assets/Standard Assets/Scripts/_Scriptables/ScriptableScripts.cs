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
        public ScriptableCheatCodes m_ScriptableCheatCodes;
        public ScriptableElias m_ScriptableElias;
        public ScriptableFont m_ScriptableFont;
        public ScriptableGameEvents m_ScriptableGameEvents;
        public ScriptableGrid m_ScriptableGrid;
        public ScriptableLines m_ScriptableLines;
        public ScriptableParticles m_ScriptableParticles;
        public ScriptableScenes m_ScriptableScenes;
        public ScriptableShipParts m_ScriptableShipParts;
        public ScriptableShips m_ScriptableShips;
        public ScriptableSounds m_ScriptableSounds;
        public ScriptableStrings m_ScriptableStrings;
    }

    public ScriptableScript scriptables;    // array of all palettes

}
