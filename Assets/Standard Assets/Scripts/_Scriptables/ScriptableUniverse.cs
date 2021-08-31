using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UniverseProfile", menuName = "ScriptableObjects/UniverseProfile")]
public class ScriptableUniverse : ScriptableBase
{
    public enum SpaceClass
    {
        None,
        Universe,
        Galaxy,
        Star,
        Planet,
        Moon,
        Asteroid
    }

    [Serializable]
    public class RecursiveUniverse
    {
        public string Name;
        public SpaceClass Classification;
        public Vector2 PositionInParentGrid;
        public ScriptableGrid.GridSettings Grid;
        public RecursiveUniverse[] recursiveUniverses;
        
    }

    public RecursiveUniverse currentGrids;    // array of all palettes
    public ScriptableGrid displayGrid;
}
