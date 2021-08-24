using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LineProfile", menuName = "ScriptableObjects/LineProfile")]

public class ScriptableLines : ScriptableObject
{
    public enum StyleType
    {
        Outline,
        InsideLine,
        Dotted
    }

    [Serializable]
    public class LineStyle
    {
        public StyleType StyleName;

       // Ref to the line Object
        public GameObject LinePrefab;
    
        // Segments represent the "resolution" of each line. 100 is good enough for this project
        public int NumOfSegments;

        // Multiplier for line width
        public float LineWidth;

        // Colour of the default Line (some lines use other colours to stand out)
        public Color LineColorStart, LineColorEnd;
    }

    public LineStyle[] LineTemplates;    // array of all palettes


}
