using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundProfile", menuName = "ScriptableObjects/SoundProfile", order = 1)]
public class ScriptableSounds : ScriptableObject
{
    // Scriptable Object containing all the potential palette options
    public enum GameSounds // All registered colours 
    {
        None,
        ButtonPress,
        Explosion,
        PowerUp
    };

  

    [Serializable]
    public class SoundPalettes // Each palette contains 
    {
        public GameSounds name;
        public AudioClip  file;
        public float volume = 1;
    }

    public SoundPalettes[] soundPalette;    // array of all palettes



    

}

