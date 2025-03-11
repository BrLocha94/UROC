using System;
using System.Collections.Generic;
using UnityEngine;

public class Winline : MonoBehaviour
{
    public Action<Winline> onWinlineFinished;

    [SerializeField]
    private GameObject line;

    [SerializeField]
    private List<int> reelPositionIndex = new List<int>();

    private void Awake()
    {
        line.SetActive(false);
    }

    public void ExecuteWinline(List<GridReel> reels)
    {
        line.SetActive(true);
        
        // TRIGGER SYMBOLS ANIMATION
        for(int i = 0; i < reels.Count; i++) 
        {
            //Change color symbols
            reels[i].SymbolHighlightToogle(reelPositionIndex[i], true);
        }
        
        this.Invoke(1.5f, () =>
        {
            line.SetActive(false);
            onWinlineFinished?.Invoke(this);
        });
    }
}
