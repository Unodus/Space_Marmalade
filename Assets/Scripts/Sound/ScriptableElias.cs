using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EliasProfile", menuName = "ScriptableObjects/EliasProfile", order = 1)]

public class ScriptableElias : ScriptableObject
{

    public enum Themes // All registered elias themes 
    {
        Strategy,
        Combat,
        Combat2

    };

    [Serializable]
    public class EliasPalettes // Each palette
    {
        public string EliasName;
        public Themes MusicType;
     
    }

    public EliasPalettes[] eliasPalette;    // array of all palettes

}
