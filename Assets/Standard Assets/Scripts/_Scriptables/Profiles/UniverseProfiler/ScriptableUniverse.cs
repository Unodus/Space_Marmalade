using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UniverseProfile", menuName = "ScriptableObjects/UniverseProfile")]
public class ScriptableUniverse : ScriptableBase
{
 
    [Serializable]
    public class RecursiveUniverse
    {
        public string Name;
        public UniverseObject universe;
    }

    
    public RecursiveUniverse[] currentGrids;    // array of all palettes
    public ScriptableGrid displayGrid;
    public UniverseObject startGrid;
}
