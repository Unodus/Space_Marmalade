using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObject/GridProfile/GridSettings")]
public class GridSettings : ScriptableObject
{
    [HideInInspector]
    public GameObject GameObjectRef;
    public bool PolarActive; //Polar quality on/off

    [Range(0.1f, 50.0f)]
    public float Size;

    [Range(-5f, 5f)]
    public float ColDisplacement; //Size of cell dots and column scrolling

    public Vector2 ScreenRatio; // Vertical/Horizontal = actual screen size. Columns/Rows = grid space

    public float TransitionSpeed;


    /// Gives you access to a reusable list of positions, so you only have to calculate each position    
}