using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridObject", menuName = "ScriptableObject/GridProfile/GridObject")]
public class GridObject : ScriptableObject
{

    [Range(2, 50)]
    public int Columns, Rows;

    public List<UniverseObject> ObjectsInGrid;



}
