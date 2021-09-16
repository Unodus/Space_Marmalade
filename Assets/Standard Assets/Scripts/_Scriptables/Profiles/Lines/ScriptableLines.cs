using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LineProfile", menuName = "ScriptableObjects/LineProfile")]

public class ScriptableLines : ScriptableObject
{


    [Serializable]
    public class LineStyle
    {
        string Name;
       public LineType Line;
    }

    public LineStyle[] LineTemplates;    // array of all palettes


}
