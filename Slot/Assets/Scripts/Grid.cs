using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private GridState currentState = GridState.Idle;
    [SerializeField]
    private List<GridReel> reels = new List<GridReel>();

    private void Start()
    {
        currentState = GridState.StartSpin;
    }
}

public enum GridState
{
    Idle,
    StartSpin,
    Spinning,
    Stoping,
    Winlining
}
