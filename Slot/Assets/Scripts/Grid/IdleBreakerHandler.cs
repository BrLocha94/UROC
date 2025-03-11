using System.Collections.Generic;
using UnityEngine;

public class IdleBreakerHandler : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve idleBreakerCurve;
    [SerializeField]
    private float idleBreakerTime;
    [SerializeField]
    private float idleBreakerScale;

    [SerializeField]
    private float breakerMinTime = 3f;
    [SerializeField]
    private float breakerMaxTime = 8f;

    Coroutine countDownRoutine = null;
    Coroutine idleBreakerRoutine = null;
    
    GridSlot targetSlot = null;
    Vector3 targetLocalScale = Vector3.one;

    private List<GridReel> reels = new List<GridReel>();

    public void InitializeHandler(List<GridReel> reelsList)
    {
        reels = reelsList;
    }

    public void StartIdleBreaker()
    {
        if (countDownRoutine != null) return;

        if (idleBreakerRoutine == null)
        {
            float randomCountdown = Random.Range(breakerMinTime, breakerMaxTime);

            countDownRoutine = this.Invoke(randomCountdown, () =>
            {
                ExecuteIdleBreakerAnimation();
            });
        }
    }

    private void ExecuteIdleBreakerAnimation()
    {
        int random = Random.Range(0, reels.Count);
        targetSlot = reels[random].GetRandomGridSlot();
        targetLocalScale = targetSlot.GetTransform().localScale;
        StartIdleBreakerAnimation(targetSlot.GetTransform(), idleBreakerCurve, idleBreakerScale, idleBreakerTime);
    }

    public void StopIdleBreaker()
    {
        if (countDownRoutine != null)
        {
            StopCoroutine(countDownRoutine);
            countDownRoutine = null;
        }

        if (idleBreakerRoutine != null)
        {
            StopCoroutine(idleBreakerRoutine);
            idleBreakerRoutine = null;

            targetSlot.GetTransform().localScale = targetLocalScale;
        }
    }

    private void StartIdleBreakerAnimation(Transform targetTransform, AnimationCurve curve, float scale, float time)
    {
        if (idleBreakerRoutine != null) return;

        // UP SCALE ANIMATION
        idleBreakerRoutine = this.RescaleRoutine(
            targetTransform,
            targetLocalScale,
            new Vector2(targetLocalScale.x + scale, targetLocalScale.y + scale),
            curve,
            time,
            () => {
                // DOWN SCALE ANIMATION
                idleBreakerRoutine = this.RescaleRoutine(
                    targetTransform,
                    targetTransform.localScale,
                    targetLocalScale,
                    curve,
                    time,
                    () =>
                    {
                        //FINISH ANIMATION
                        if (idleBreakerRoutine != null)
                        {
                            StopCoroutine(idleBreakerRoutine);
                            idleBreakerRoutine = null;
                        }
                        targetTransform.localScale = targetLocalScale;

                        float randomCountdown = Random.Range(breakerMinTime, breakerMaxTime);

                        countDownRoutine = this.Invoke(randomCountdown, () =>
                        {
                            ExecuteIdleBreakerAnimation();
                        });
                    });
            });
    }
}
