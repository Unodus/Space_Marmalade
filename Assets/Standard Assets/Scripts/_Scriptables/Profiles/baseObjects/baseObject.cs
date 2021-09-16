using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "baseObject", menuName = "ScriptableObject/ParticleProfile/baseObject")]
public class baseObject : ScriptableObject
{
    public SpaceClass Classification;
    public Vector2 GridPosition;
    public GameObject imageObject;
}