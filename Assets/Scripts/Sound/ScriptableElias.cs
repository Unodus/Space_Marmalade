using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EliasProfile", menuName = "ScriptableObjects/EliasProfile", order = 1)]

public class ScriptableElias : ScriptableObject
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
        public string actionPresetName;
        public bool allowRequiredThemeMissmatch;

        public bool useDoubleEffectParam;
        public EliasSetEffectParameterDouble doubleEffectParam;

        public bool useSetSendVolume;
        public EliasSetSendVolume setSendVolume;
    }

    public EliasPalettes[] eliasPalette;    // array of all palettes
    public GameObject PopupPrefab;

    public EliasPalettes GetEliasName(string myTheme)
    {



        foreach (EliasPalettes i in eliasPalette)
        {
            if (i.Name == myTheme)
            {
             return i;
            }
        }


        Debug.LogWarning("No Theme Detected");
        return null;

    }


    public void ChangeElias(EliasPlayer eliasPlayer, string myTheme)
    {
        EliasPalettes i = GetEliasName(myTheme);
        
            if (i.useSetLevel)
            {
                eliasPlayer.QueueEvent(i.setLevel.CreateSetLevelEvent(eliasPlayer.Elias));
            }
            if (i.useSetLevelOnTrack)
            {
                eliasPlayer.QueueEvent(i.setLevelOnTrack.CreateSetLevelOnTrackEvent(eliasPlayer.Elias));
            }
            if (i.usePlayStinger)
            {
                eliasPlayer.QueueEvent(i.playStinger.CreatePlayerStingerEvent(eliasPlayer.Elias));
            }
            if (i.useActionPreset)
            {
                eliasPlayer.RunActionPreset(i.actionPresetName, i.allowRequiredThemeMissmatch);

                CreatePopUp(myTheme, eliasPlayer);

            }
            if (i.useDoubleEffectParam)
            {
                eliasPlayer.QueueEvent(i.doubleEffectParam.CreateSetEffectParameterEvent(eliasPlayer.Elias));
            }
            if (i.useSetSendVolume)
            {
                eliasPlayer.QueueEvent(i.setSendVolume.CreateSetSendVolumeEvent(eliasPlayer.Elias));
            }
        
    }

    private void CreatePopUp(string input, EliasPlayer eliasPlayer)
    {
        if (!PopupPrefab)
            return;

        GameObject newCanvasObject = new GameObject();
        newCanvasObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;


        GameObject Popup = Instantiate(PopupPrefab, newCanvasObject.transform);
        Popup.GetComponent<CornerPopUp>().CustomStart(eliasPlayer.GetActiveTheme() + ": " + input);
        Destroy(newCanvasObject, 5);
    }
}
