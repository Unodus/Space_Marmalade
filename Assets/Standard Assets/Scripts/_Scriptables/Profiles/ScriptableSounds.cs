using System;
using UnityEngine;
using UnityEngine.Audio;
 

[CreateAssetMenu(fileName = "SoundProfile", menuName = "ScriptableObjects/SoundProfile", order = 1)]
public class ScriptableSounds : ScriptableObject
{
    // Scriptable Object containing all the potential palette options
    public enum GameSounds // All registered colours 
    {
        None,
        ButtonPress,
        Explosion,
        PowerUp,
        ShipMoveEnemy,
        ShipMovePlayer,
    };



    [Serializable]
    public class SoundPalette // Each palette contains 
    {
        public GameSounds Name;
        public AudioClip[] Sound;
        [Range(0.0f, 1.0f)]
        public float volume, RandomPitchVariation;

    }

    [Serializable]
    public class SoundGroup // Each palette contains 
    {
        public string Name;
        public SoundPalette[] SoundFiles;
        [Range(0.0f, 1.0f)]
        public float GroupVolume = 1;
    }

    public SoundGroup[] SoundGroups;    // array of all palettes
    public AudioMixerGroup DefaultAudioMixer; // refererence to the audio mixer



}
