using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public AudioSource musicAudio;
    public AudioSource[] effectsAudio;
    private float generalVolume = 1f;
    private float musicVolume = 1f;
    public float effectsVolume = 1f;

    public float GeteffectsVolume()
    {
        return effectsVolume;
    }

    private void Awake()
    {
        GameManagement.instance.volumeManager = this;
    }

    public void setGeneralVolume(float volume)
    {
        generalVolume = volume;
        setMusicVolume(musicVolume);
        setEffectsVolume(effectsVolume);
    }

    public void setMusicVolume(float volume)
    {
        musicVolume = volume;
        musicAudio.volume = generalVolume * musicVolume;
    }

    public void setEffectsVolume(float volume)
    {
        effectsVolume = volume;
        foreach(AudioSource audio in effectsAudio)
        {
            audio.volume = generalVolume * effectsVolume;
        }
    }

    public void AddAudioEffect(AudioSource audioSource)
    {
        var listEffectsAudio = effectsAudio.ToList();
        listEffectsAudio.Add(audioSource);
        effectsAudio = listEffectsAudio.ToArray();
    }
}
