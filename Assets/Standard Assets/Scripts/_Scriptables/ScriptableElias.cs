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

        public bool useSetLevel;
        public EliasSetLevel setLevel;

        public bool useSetLevelOnTrack;
        public EliasSetLevelOnTrack setLevelOnTrack;

        public bool usePlayStinger;
        public EliasPlayStinger playStinger;

        public bool useActionPreset;
        public string[] actionPresetName;
        public bool allowRequiredThemeMissmatch;

        public bool useDoubleEffectParam;
        public EliasSetEffectParameterDouble doubleEffectParam;

        public bool useSetSendVolume;
        public EliasSetSendVolume setSendVolume;
    }

    public EliasPalettes[] eliasPalette;    // array of all palettes
    public GameObject PopupPrefab;

  


}
