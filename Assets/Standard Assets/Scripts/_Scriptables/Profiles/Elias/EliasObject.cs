using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EliasObject", menuName = "ScriptableObject/EliasProfile/EliasObject")]
public class EliasObject : ScriptableObject
{
//    public string Name;

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
