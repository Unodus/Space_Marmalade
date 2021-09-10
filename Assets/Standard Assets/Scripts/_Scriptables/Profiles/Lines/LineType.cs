using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LineType", menuName = "ScriptableObject/LineProfile/LineType")]
public class LineType : ScriptableObject
{

    // Ref to the line Object
    public GameObject LinePrefab;

    // Segments represent the "resolution" of each line. 100 is good enough for this project
    public int NumOfSegments;

    // Multiplier for line width
    public float LineWidth;

    public bool Wrap;

    // Colour of the default Line (some lines use other colours to stand out)
    public Color LineColorStart, LineColorEnd;
}

