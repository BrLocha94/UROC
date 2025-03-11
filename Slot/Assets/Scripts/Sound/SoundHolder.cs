using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHolder : MonoBehaviour
{
    private AudioSource targetSource;

    private void Awake()
    {
        targetSource = GetComponent<AudioSource>();
    }

    public void ExecuteClip(AudioClip clip, float volume, bool loop)
    {
        targetSource.clip = clip;
        targetSource.volume = volume;
        targetSource.loop = loop;

        targetSource.Play();
    }

    public bool isPlaying => targetSource.isPlaying;
}
