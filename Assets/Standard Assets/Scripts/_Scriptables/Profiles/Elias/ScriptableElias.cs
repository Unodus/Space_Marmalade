using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EliasProfile", menuName = "ScriptableObjects/EliasProfile", order = 1)]

public class ScriptableElias : ScriptableBase
{


    [Serializable]
    public class EliasPalettes // Each palette
    {
        public string Name;

        public EliasObject eliasObject;
    }

    public EliasPalettes[] eliasPalette;    // array of all palettes
    public GameObject PopupPrefab;

  


}
