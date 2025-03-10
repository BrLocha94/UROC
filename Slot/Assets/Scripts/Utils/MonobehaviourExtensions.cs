using System;
using System.Collections;
using UnityEngine;

public static class MonobehaviourExtensions
{
    public static Coroutine Invoke(this MonoBehaviour host, float delay, Action action)
    {
        return host.StartCoroutine(Invoke(delay, action));
    }

    private static IEnumerator Invoke(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public static Coroutine InvokeAfterFrame(this MonoBehaviour host, Action action)
    {
        return host.StartCoroutine(InvokeAfterFrame(action));
    }

    private static IEnumerator InvokeAfterFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }

    public static Coroutine MoveRoutine(this MonoBehaviour host, Transform transform, Vector3 initialPosition, Vector3 finalPosition, AnimationCurve curve, float time, Action callback = null)
    {
        return host.StartCoroutine(MoveRoutine(transform, initialPosition, finalPosition, curve, time, callback));
    }

    private static IEnumerator MoveRoutine(Transform transform, Vector3 initialPosition, Vector3 finalPosition, AnimationCurve curve, float time, Action callback = null)
    {
        float timer = 0f;

        while (timer < time)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, curve.Evaluate(timer / time));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = finalPosition;
        callback?.Invoke();
    }

    public static Coroutine RescaleRoutine(this MonoBehaviour host, Transform transform, Vector3 initialPosition, Vector3 finalPosition, AnimationCurve curve, float time, Action callback = null)
    {
        return host.StartCoroutine(RescaleRoutine(transform, initialPosition, finalPosition, curve, time, callback));
    }

    private static IEnumerator RescaleRoutine(Transform transform, Vector3 initialScale, Vector3 finalScale, AnimationCurve curve, float time, Action callback = null)
    {
        float timer = 0f;

        while (timer < time)
        {
            transform.localScale = Vector3.Lerp(initialScale, finalScale, curve.Evaluate(timer / time));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = finalScale;
        callback?.Invoke();
    }
}
