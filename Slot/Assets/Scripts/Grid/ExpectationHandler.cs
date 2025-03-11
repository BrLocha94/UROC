using System.Collections.Generic;
using UnityEngine;

public class ExpectationHandler : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> gridStopExpectiationList = new List<AudioClip>();
    [SerializeField]
    private float gripStopExpectationVolume = 0.8f;

    [Header("Params if spin is winner")]
    [SerializeField]
    private float WinChance = 4;
    [SerializeField]
    private float WinRange = 10;

    public float level_00 { get; private set; } = 0.7f;
    public float level_01 { get; private set; } = 2f;

    public float gridStopVolume => gripStopExpectationVolume;
    public bool hasExpectation { get; private set; } = false;

    public void CheckExpectation(SpinResultPayout spin)
    {
        if (spin.isWin)
        {
            hasExpectation = Random.Range(0, WinRange) <= WinChance;
            return;
        }
    }

    public AudioClip GetAudioClip(int index)
    {
        return gridStopExpectiationList[index];
    }
}
