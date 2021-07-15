using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CheatProfile", menuName = "ScriptableObjects/CheatProfile")]
public class ScriptableCheatCodes : ScriptableObject
{
    [Serializable]
    public class CheatType
    {
        public string CheatCode;
        public int InternalCode;
    }

    public CheatType[] Commands;    // array of all palettes


    public int GetCodeByName(string Code)
    {
        foreach (CheatType i in Commands)
        {
            if (i.CheatCode == Code)
            {
                return i.InternalCode ;
            }
        }

        Debug.LogWarning(Code + " is not registered in the profiler");
        return 0;
    }


}
