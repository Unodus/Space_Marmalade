using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CheatProfile", menuName = "ScriptableObjects/CheatProfile")]
public class ScriptableCheatCodes : ScriptableBase
{
    [Serializable]
    public class CommandType
    {
        public string Name;
        public CheatType InternalCode;
    }

    public  CommandType[] Commands;    // array of all palettes
 
}
