using System;
using System.Collections.Generic;
using UnityEngine;

public class WinlineHandler : MonoBehaviour
{
    public Action onWinlinesFinishedExecuting;

    [SerializeField]
    private List<Winline> winlines = new List<Winline>();

    private List<bool> currentWinlines = new List<bool> { false, false, false, false, false };

    private int winlineExecuting = 0;

    public void ExecuteWinlines(List<bool> newWinlines)
    {
        currentWinlines = newWinlines;
        winlineExecuting = 0;

        for (int i = 0; i < winlines.Count; i++) 
        {
            if (currentWinlines[i]) 
            {
                winlines[i].onWinlineFinished += OnWinlineFinished;
                winlines[i].ExecuteWinline();
            }
        }
    }

    private void OnWinlineFinished(Winline target)
    {
        target.onWinlineFinished -= OnWinlineFinished;

        winlineExecuting--;
        if(winlineExecuting <= 0)
        {
            winlineExecuting = 0;
            onWinlinesFinishedExecuting?.Invoke();
        }
    }
}
