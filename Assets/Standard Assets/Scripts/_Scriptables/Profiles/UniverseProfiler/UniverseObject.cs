using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UniverseObject", menuName = "ScriptableObject/UniverseProfile/UniverseObject")]
public class UniverseObject : ScriptableObject
{
    //    public UniverseObject UniqueIdentifier;
    public SpaceClass Classification;
    public Vector2 PositionInContainerGrid;
    public Vector2 InteriorGridSize;
    public UniverseObject ContainerIdentifier;
    public UniverseObject[] containedIdentities;
    
    

}
