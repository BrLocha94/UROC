using System;
using UnityEngine;

public class AnticipationHandler : MonoBehaviour
{
    public Action onAnticipationFinish;

    [SerializeField]
    private ParticleSystem heavySnow;
    [SerializeField]
    private AudioClip anticipationSound;
    [SerializeField]
    private float anticipationTime;

    [Header("Params if spin is winner")]
    [SerializeField]
    private float WinChance = 4;
    [SerializeField]
    private float WinRange = 10;

    [Header("Params if spin is looser")]
    [SerializeField]
    private float LoseChance = 1;
    [SerializeField]
    private float LoseRange = 10;

    public bool HasAnticipation(SpinResultPayout spin)
    {
        if (spin.isWin)
        {
            return UnityEngine.Random.Range(0, WinRange) <= WinChance;
        }

        return UnityEngine.Random.Range(0, LoseRange) <= LoseChance;
    }

    public void StartAnticipation()
    {
        SoundManager.Instance.ExecuteSfx(anticipationSound, 1);
        heavySnow.Play();

        this.Invoke(anticipationTime, StopAnticipation);
    }

    public void StopAnticipation()
    {
        heavySnow.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        onAnticipationFinish?.Invoke();
    }
}
