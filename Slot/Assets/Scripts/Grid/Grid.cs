using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Action onIdleEnter; 

    [SerializeField]
    private GridState currentState = GridState.Idle;

    [Header("Handlers")]
    [SerializeField]
    private WinlineHandler winlineHandler;
    [SerializeField]
    private IdleBreakerHandler idleBreakerHandler;

    [Header("References")]
    [SerializeField]
    private List<GridReel> reels = new List<GridReel>();

    int reelsExecuting = 0;

    const float MIN_TIME_TO_STOP = 0.5f;
    const float MAX_TIME_TO_STOP = 1.5f;

    SpinResultPayout currentSpinData = null;

    bool hasExecutedSpin = false;

    private void Start()
    {
        idleBreakerHandler.InitializeHandler(reels);
        winlineHandler.InitializeHandler(reels);
        ChangeState(GridState.Idle);
    }

    public void StartSpin(SpinResultPayout spin)
    {
        hasExecutedSpin = true;
        currentSpinData = spin;

        // Set the reels with correct data
        for (int i = 0; i < reels.Count; i++) 
        {
            reels[i].SetData(spin.ReelMatrix[i]);
        }

        ChangeState(GridState.StartSpin);
    }

    private void ChangeState(GridState state)
    {
        currentState = state;

        switch (currentState)
        {
            case GridState.Idle:
                ExecuteOnIdle();
                break;
            case GridState.StartSpin:
                ExecuteOnStartSpin();
                break;
            case GridState.Spinning:
                ExecuteOnSpinning();
                break;
            case GridState.Stoping:
                ExecuteOnStopping();
                break;
            case GridState.Winlining:
                ExecuteOnWinlining();
                break;
        }
    }

    private void ExecuteOnIdle()
    {
        if (hasExecutedSpin)
        {
            idleBreakerHandler.StartIdleBreaker();
        }

        onIdleEnter?.Invoke();
    }

    private void ExecuteOnStartSpin()
    {
        idleBreakerHandler.StopIdleBreaker();

        reelsExecuting = 0;

        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].onReelSpinning += ReelSpiningCallback;
            reelsExecuting++;
            reels[i].StartReel();
        }
    }

    private void ReelSpiningCallback(GridReel target)
    {
        target.onReelSpinning -= ReelSpiningCallback;
        reelsExecuting -= 1;
        if (reelsExecuting == 0)
            ChangeState(GridState.Spinning);
    }

    private void ExecuteOnSpinning()
    {
        // CHECK ANY ANTICIPATION EVENTS HERE
        float timer = UnityEngine.Random.Range(MIN_TIME_TO_STOP, MAX_TIME_TO_STOP);
        this.Invoke(timer, () => ChangeState(GridState.Stoping));
    }

    private void ExecuteOnStopping()
    {
        reelsExecuting = 0;

        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].onReelStopped += ReelStoppedCallback;
            reelsExecuting++;
        }

        int targetReel = reels.Count - reelsExecuting;
        reels[targetReel].StopReel();
    }

    private void ReelStoppedCallback(GridReel target)
    {
        target.onReelStopped -= ReelStoppedCallback;
        reelsExecuting -= 1;

        if (reelsExecuting == 0)
        {
            // Check winline or not
            if (currentSpinData.isWin)
            {
                ChangeState(GridState.Winlining);
                return;
            }
            
            ChangeState(GridState.Idle);
        }
        else
        {
            int targetReel = reels.Count - reelsExecuting;
            reels[targetReel].StopReel();
        }
    }

    private void ExecuteOnWinlining()
    {
        winlineHandler.onWinlinesFinishedExecuting += WinlineFinishedCallback;
        winlineHandler.ExecuteWinlines(currentSpinData.WinLines);
    }

    private void WinlineFinishedCallback()
    {
        winlineHandler.onWinlinesFinishedExecuting -= WinlineFinishedCallback;
        ChangeState(GridState.Idle);
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
