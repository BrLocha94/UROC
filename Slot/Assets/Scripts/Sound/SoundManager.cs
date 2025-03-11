using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private List<SoundHolder> listHoldersMusic = new List<SoundHolder>();
    [SerializeField]
    private List<SoundHolder> listHoldersSfx = new List<SoundHolder>();

    public SoundVolume currentVolume { get; private set; }

    private const SoundVolume defaultVolume = SoundVolume.MEDIUM;
    private const string VolumeKey = "SoundVolume";

    protected override void ExecuteOnAwake()
    {
        base.ExecuteOnAwake();

        if (PlayerPrefs.HasKey(VolumeKey))
            ChangeCurrentVolume((SoundVolume)PlayerPrefs.GetInt(VolumeKey));
        else
            ChangeCurrentVolume(defaultVolume);
    }

    public SoundVolume ChangeCurrentVolume()
    {
        SoundVolume volume = currentVolume;
        if(volume == SoundVolume.HIGH)
            volume = SoundVolume.MUTE;
        else
            volume++;

        return ChangeCurrentVolume(volume);
    }

    public SoundVolume ChangeCurrentVolume(SoundVolume newVolume)
    {
        currentVolume = newVolume;
        PlayerPrefs.SetInt(VolumeKey, (int)newVolume);
        PlayerPrefs.Save();

        ApplyVolumeSettings();

        return currentVolume;
    }

    private void ApplyVolumeSettings()
    {
        float volumeLevel = 0.6f;
        switch (currentVolume)
        {
            case SoundVolume.MUTE:
                volumeLevel = -80f;
                break;
            case SoundVolume.LOW:
                volumeLevel = -20f;
                break;
            case SoundVolume.MEDIUM:
                volumeLevel = 1f;
                break;
            case SoundVolume.HIGH:
                volumeLevel = 3f;
                break;
        }

        //EXPOSED MIXER PARAMS
        audioMixer.SetFloat("MasterVolume", volumeLevel);
    }

    public void ExecuteMusic(AudioClip clip, float volume, bool loop = false)
    {
        for(int i = 0; i < listHoldersMusic.Count; i++)
        {
            if (!listHoldersMusic[i].isPlaying)
            {
                listHoldersMusic[i].ExecuteClip(clip, volume, loop);
                return;
            }
        }
    }

    public void ExecuteSfx(AudioClip clip, float volume, bool loop = false)
    {
        for (int i = 0; i < listHoldersSfx.Count; i++)
        {
            if (!listHoldersSfx[i].isPlaying)
            {
                listHoldersSfx[i].ExecuteClip(clip, volume, loop);
                return;
            }
        }
    }
}

public enum SoundVolume
{
    MUTE = 0,
    LOW = 1,
    MEDIUM = 2,
    HIGH = 3
}
