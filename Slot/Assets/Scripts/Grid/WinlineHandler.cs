using System;
using System.Collections.Generic;
using UnityEngine;

public class WinlineHandler : MonoBehaviour
{
    public Action onWinlinesFinishedExecuting;

    [SerializeField]
    private float initialDelay = 0.5f;
    [SerializeField]
    private AudioClip winlineClip;
    [SerializeField]
    private float winlineVolume = 0.6f;
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
        // PROTECTION TO CASE WINLINE DATA IS FALSE
        if (!newWinlines.Find(x => x == true))
        {
            onWinlinesFinishedExecuting?.Invoke();
            return;
        }

        this.Invoke(initialDelay, () =>
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

            SoundManager.Instance.ExecuteSfx(winlineClip, winlineVolume);
        });
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
