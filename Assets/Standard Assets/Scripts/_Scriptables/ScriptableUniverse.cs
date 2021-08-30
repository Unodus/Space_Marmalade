using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UniverseProfile", menuName = "ScriptableObjects/UniverseProfile")]
public class ScriptableUniverse : ScriptableBase
{
    public enum SpaceClass
    {
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
        public ScriptableGrid Grid;
        public RecursiveUniverse[] recursiveUniverses;
        
    }

    public RecursiveUniverse currentGrid;    // array of all palettes
}
