using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName =  "GridProfile", menuName = "ScriptableObjects/GridProfile")]
public class ScriptableGrid : ScriptableBase
{
    [Serializable]
    public class GridSettings
    {
       
        public bool PolarActive; //Polar quality on/off

        [Range(2, 50)]
        public int Columns, Rows;

        [Range(0.1f, 50.0f)]
        public float Size;

        [Range(-5f, 5f)]
        public float ColDisplacement; //Size of cell dots and column scrolling

        [HideInInspector]
        public Vector2 ScreenRatio; // Vertical/Horizontal = actual screen size. Columns/Rows = grid space
 
        public float TransitionSpeed;

        public  Dictionary<Vector2, Vector2> CachedGridPositions = new Dictionary<Vector2, Vector2>();
        public  Dictionary<Vector2, Vector2> CachedPolarPositions = new Dictionary<Vector2, Vector2>();
        /// Gives you access to a reusable list of positions, so you only have to calculate each position 
    }

    public GridSettings GameGrid;    // array of all palettes
    
}
