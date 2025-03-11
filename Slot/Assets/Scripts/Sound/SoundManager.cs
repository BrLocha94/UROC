using System;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
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
                volumeLevel = 0f;
                break;
            case SoundVolume.LOW:
                volumeLevel = 0.3f;
                break;
            case SoundVolume.MEDIUM:
                volumeLevel = 0.6f;
                break;
            case SoundVolume.HIGH:
                volumeLevel = 1f;
                break;
        }

        AudioListener.volume = volumeLevel;
    }
}

public enum SoundVolume
{
    MUTE = 0,
    LOW = 1,
    MEDIUM = 2,
    HIGH = 3
}
