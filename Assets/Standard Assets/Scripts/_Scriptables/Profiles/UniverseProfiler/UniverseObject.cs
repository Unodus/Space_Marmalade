using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UniverseObject", menuName = "ScriptableObject/UniverseProfile/UniverseObject")]
public class UniverseObject : ScriptableObject
{
    public UniverseObject ContainerUniverse;
    public GridObject InsideGrid;
    public baseObject baseObject;
}
