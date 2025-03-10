using System;
using UnityEngine;

public class Winline : MonoBehaviour
{
    public Action<Winline> onWinlineFinished;

    [SerializeField]
    private GameObject line;

    private void Awake()
    {
        line.SetActive(false);
    }

    public void ExecuteWinline()
    {
        line.SetActive(true);
        this.Invoke(1.5f, () =>
        {
            line.SetActive(false);
            onWinlineFinished?.Invoke(this);
        });
    }
}
