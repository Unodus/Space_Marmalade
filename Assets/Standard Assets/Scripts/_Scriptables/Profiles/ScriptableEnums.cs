using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum GlobalEnum
{
    None,
    CommandType,
    InputEvent,
    NodeMode,
    TurnPhase,
    Particle,
    StringCategories,
    FontCategories
}

[CreateAssetMenu(fileName = "EnumProfile", menuName = "ScriptableObjects/EnumProfile")]
public class ScriptableEnums : ScriptableBase
{
    [Serializable]
    public class ScriptableEnum  // Each palette
    {
        public string Name;
        public GlobalEnum EnumName;
        public ScriptableObject[] Enum;
    }

    public ScriptableEnum[] enums;



}
