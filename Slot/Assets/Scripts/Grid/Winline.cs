using System;
using System.Collections.Generic;
using UnityEngine;

public class Winline : MonoBehaviour
{
    public Action<Winline> onWinlineFinished;

    [SerializeField]
    private Animator lineAnimator;

    [SerializeField]
    private List<int> reelPositionIndex = new List<int>();


    public void ExecuteWinline(List<GridReel> reels)
    {
        lineAnimator.Play("Highlight");
        
        // TRIGGER SYMBOLS ANIMATION
        for(int i = 0; i < reels.Count; i++) 
        {
            reels[i].AnimateWinline(reelPositionIndex[i], true);
        }
        
        this.Invoke(1.8f, () =>
        {
            onWinlineFinished?.Invoke(this);
        });
    }
}
