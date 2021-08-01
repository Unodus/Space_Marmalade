using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CheatProfile", menuName = "ScriptableObjects/CheatProfile")]
public class ScriptableCheatCodes : ScriptableBase
{
    [Serializable]
    public class CheatType
    {
        public string CheatCode;
        public CommandType InternalCode;
        public int CommandMessageReference;
    }

    public CheatType[] Commands;    // array of all palettes

    public enum CommandType 
    { 
    None,
    CheatEnable,
    PlaySound,
    EliasChange
    }

}
