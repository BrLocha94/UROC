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
    [SerializeField]
    private AnticipationHandler anticipationHandler;

    [Header("References")]
    [SerializeField]
    private List<GridReel> reels = new List<GridReel>();

    [Header("Sounds")]
    [SerializeField]
    private AudioClip gridSpinClip;
    [SerializeField]
    private float gripSpinVolume = 0.8f;
    [SerializeField]
    private AudioClip gridStopClip;
    [SerializeField]
    private float gripStopVolume = 0.8f;
    [SerializeField]
    private List<AudioClip> gridStopExpectiationList = new List<AudioClip>();
    [SerializeField]
    private float gripStopExpectationVolume = 0.8f;

    int reelsExecuting = 0;

    const float MIN_TIME_TO_STOP = 0.5f;
    const float MAX_TIME_TO_STOP = 1.5f;

    SpinResultPayout currentSpinData = null;

    bool hasExecutedSpin = false;

    List<SoundHolder> reelsEffectHolders = new List<SoundHolder>();

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

        if (anticipationHandler.HasAnticipation(spin))
        {
            anticipationHandler.onAnticipationFinish += StartSpin;
            anticipationHandler.StartAnticipation();
        }
        else
        {
            StartSpin();
        }
    }

    private void StartSpin()
    {
        anticipationHandler.onAnticipationFinish -= StartSpin;

        // Set the reels with correct data
        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].SetData(currentSpinData.ReelMatrix[i]);
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

            SoundHolder holder = SoundManager.Instance.ExecuteSfx(gridSpinClip, gripSpinVolume, true);
            if(holder != null)
            {
                reelsEffectHolders.Add(holder);
            }
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

        if(reelsEffectHolders.Count > 0)
        {
            // Need to improve this
            reelsEffectHolders[0].StopClip();
            reelsEffectHolders.RemoveAt(0);

            //CHECK EXPECTATION TO EXECUTE SOUND
            SoundManager.Instance.ExecuteSfx(gridStopClip, gripStopVolume);
        }

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
