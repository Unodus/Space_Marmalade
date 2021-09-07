using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName =  "GridProfile", menuName = "ScriptableObjects/GridProfile")]
public class ScriptableGrid : ScriptableBase
{
    [Serializable]
    public class GridProfile
    {
        public GridSettings settings;
        public GridObject gridObject;

    }

    public GridProfile GameGrid;    // array of all palettes
    
}
