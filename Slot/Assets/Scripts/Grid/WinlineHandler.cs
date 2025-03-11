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

    private List<GridReel> reels = new List<GridReel>();

    public void InitializeHandler(List<GridReel> reelsList)
    {
        reels = reelsList;
    }

    public void ExecuteWinlines(List<bool> newWinlines)
    {
        foreach (var reel in reels)
        {
            reel.SymbolHighlightToogle(false);
        }

        currentWinlines = newWinlines;
        winlineExecuting = 0;

        for (int i = 0; i < winlines.Count; i++)
        {
            if (currentWinlines[i])
            {
                winlines[i].onWinlineFinished += OnWinlineFinished;
                winlines[i].ExecuteWinline(reels);
                winlineExecuting++;
            }
        }

        // PROTECTION TO CASE WINLINE DATA IS FALSE
        if (winlineExecuting == 0)
        {
            foreach (var reel in reels)
            {
                reel.SymbolHighlightToogle(true);
            }
            onWinlinesFinishedExecuting?.Invoke();
        }
    }

    private void OnWinlineFinished(Winline target)
    {
        target.onWinlineFinished -= OnWinlineFinished;

        winlineExecuting--;
        if(winlineExecuting <= 0)
        {
            winlineExecuting = 0;
            foreach (var reel in reels)
            {
                reel.SymbolHighlightToogle(true);
            }
            onWinlinesFinishedExecuting?.Invoke();
        }
    }
}
