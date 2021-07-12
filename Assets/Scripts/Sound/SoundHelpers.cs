using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHelpers : MonoBehaviour
{
    public ScriptableSounds SoundProfile;
    public ScriptableSounds.GameSounds DefaultSound;

    public void PlaySound()
    {
        PlaySound(DefaultSound);
    }

    public void PlaySound(ScriptableSounds.GameSounds Sound)
    {
        Vector3 CameraPos = Camera.main.transform.position;
        PlaySound(Sound, CameraPos);
    }
    public void PlaySound(ScriptableSounds.GameSounds soundName, Vector3 AtHere)
    {
        if (soundName == 0) return;
        ScriptableSounds.SoundPalette i = SoundProfile.GetSoundFromPalette(soundName);
        AudioClip PlayMe = i.Sound[Random.Range(0, i.Sound.Length)];
        if (PlayMe == null) return;
        GameObject MyObject = new GameObject(PlayMe.name);
        MyObject.transform.position = AtHere;
        AudioSource MyAudio = MyObject.AddComponent<AudioSource>();
        if (MyAudio == null) return;

        MyAudio.volume = i.volume;
        MyAudio.pitch = MyAudio.pitch + (Random.Range(-i.RandomPitchVariation, i.RandomPitchVariation));
        MyAudio.clip = PlayMe;
        MyAudio.outputAudioMixerGroup = SoundProfile.DefaultAudioMixer;

        MyAudio.Play();
        Destroy(MyObject, MyAudio.clip.length);
    }


}
