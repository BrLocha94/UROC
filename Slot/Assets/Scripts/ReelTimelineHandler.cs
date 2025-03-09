using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class ReelTimelineHandler : MonoBehaviour
{
    public Action onReelStartSpinning;
    public Action onReelStartStopping;
    public Action onReelFinishedStopping;

    [SerializeField]
    private PlayableDirector director;

    private TimelineState timelineState;

    Coroutine acelerationRoutine = null;

    private void Start()
    {
        director.extrapolationMode = DirectorWrapMode.None;
        director.Pause();
        timelineState = TimelineState.Idle;
    }

    public void StopReel()
    {
        timelineState = TimelineState.Waiting;
    }

    public void StartReel(float speed)
    {
        director.extrapolationMode = DirectorWrapMode.Loop;
        director.playableGraph.GetRootPlayable(0).SetSpeed(speed);
        director.Play();
        timelineState = TimelineState.Executing;
        onReelStartSpinning?.Invoke();
    }

    public void StartReel(float minSpeed, float maxSpeed, float aceleration)
    {
        ClearRoutine();
        acelerationRoutine = StartCoroutine(AcelerationRoutine(
            minSpeed, 
            maxSpeed,
            aceleration,
            () => onReelStartSpinning?.Invoke()));
    }

    private IEnumerator AcelerationRoutine(float minSpeed, float maxSpeed, float aceleration, Action callback = null)
    {
        float currentSpeed = minSpeed;

        director.extrapolationMode = DirectorWrapMode.Loop;
        director.playableGraph.GetRootPlayable(0).SetSpeed(currentSpeed);
        director.Play();

        while(currentSpeed < maxSpeed)
        {
            currentSpeed += aceleration;
            if (currentSpeed > maxSpeed)
                currentSpeed = maxSpeed;

            director.playableGraph.GetRootPlayable(0).SetSpeed(currentSpeed);

            yield return null;
        }

        callback?.Invoke();
    }

    public void OnReelAnimationFinished()
    {
        if(timelineState == TimelineState.Waiting)
        {
            timelineState = TimelineState.Stoping;
            onReelStartStopping?.Invoke();
            return;
        }

        if (timelineState == TimelineState.Stoping)
        {
            timelineState = TimelineState.Idle;
            director.extrapolationMode = DirectorWrapMode.Hold;
            director.Pause();
            onReelFinishedStopping?.Invoke();
            return;
        }
    }

    private void ClearRoutine()
    {
        if(acelerationRoutine != null) 
        {
            StopCoroutine(acelerationRoutine);
            acelerationRoutine = null;
        }
    }
}

public enum TimelineState
{
    Idle,
    Executing,
    Waiting,
    Stoping
}