using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceshipPartProfile", menuName = "ScriptableObjects/SpaceshipPartProfile")]

public class ScriptableShipParts : ScriptableBase
{

    public enum PartType // All registered colours 
    {
        Lower,
        Wings,
        Body,
        Nose, 
        Thruster,
        Higher,
        Window
    };




    [Serializable]
    public class SpaceshipPart // Each palette
    {
        public string Name;
        public Sprite sprite;
        public string Effect;
    }
    [Serializable]
    public class ShipGroup // Each palette contains 
    {
        public string Name;
        public SpaceshipPart[] Part;
       
    }
    public ShipGroup[] Groups;    // array of all palettes

    public SpaceshipPart GetPartByName(string myName)
    {
        

        Debug.LogWarning(myName + " is not registered in the profiler");
        return null;
    }

}
