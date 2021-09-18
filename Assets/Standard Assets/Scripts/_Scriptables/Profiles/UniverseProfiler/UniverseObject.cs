using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UniverseObject", menuName = "ScriptableObject/UniverseProfile/UniverseObject")]
public class UniverseObject : baseObject
{
    public UniverseObject ContainerUniverse;
    public GridObject InsideGrid;
    public SpaceClass DefaultClassification;

}
