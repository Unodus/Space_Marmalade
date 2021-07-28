using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceshipProfile", menuName = "ScriptableObjects/SpaceshipProfile")]

public class ScriptableShips : ScriptableObject
{
    
    [Serializable]
    public class Spaceship : PointFollow.SpaceshipBase // Each palette
    {
   //     public string Name;
    }

    public Spaceship[] Spaceships;    // array of all palettes

    public Spaceship GetShipByName(string myName)
    {
        foreach (Spaceship i in Spaceships)
        {
            if (i.Name == myName)
            {
                return i;
            }
        }

        Debug.LogWarning(myName + " is not registered in the profiler");
        return null;
    }

}
