using System.Collections.Generic;
using System;
using UnityEngine;

public class GridReel : MonoBehaviour
{
    public Action onReelSpinning;
    public Action onReelStopped;

    [SerializeField]
    private ReelTimelineHandler timelineHandler;
    [SerializeField]
    private SymbolMapping symbolMapping;

    [SerializeField]
    private List<string> initialSymbols = new List<string>();
    [SerializeField]
    private List<GridSlot> gridSlots = new List<GridSlot>();

    private const float REEL_MIN_SPEED = 3f;
    private const float REEL_MAX_SPEED = 5f;
    private const float REEL_ACCELERATION = 0.25f;

    private void Awake()
    {
        for (int i = 0; i < gridSlots.Count; i++)
        {
            GridSlot slot = gridSlots[i];
            slot.onPositionReseted += OnGridSlotPositionReseted;
            Symbol newSymbol = symbolMapping.GetSymbol(initialSymbols[i]);
            slot.SetNewSymbol(newSymbol);
        }

        timelineHandler.onReelStartSpinning += ReelSpinning;
        timelineHandler.onReelStartStopping += ReelStartStopping;
        timelineHandler.onReelFinishedStopping += ReelStopped;
    }

    private void OnDestroy()
    {
        foreach (var slot in gridSlots)
        {
            slot.onPositionReseted -= OnGridSlotPositionReseted;
        }

        timelineHandler.onReelStartSpinning -= ReelSpinning;
        timelineHandler.onReelStartStopping -= ReelStartStopping;
        timelineHandler.onReelFinishedStopping -= ReelStopped;
    }

    private void OnGridSlotPositionReseted(GridSlot target)
    {
        Symbol newSymbol = symbolMapping.GetRandomSymbol();
        target.SetNewSymbol(newSymbol);
    }

    public void StartReel()
    {
        timelineHandler.StartReel(REEL_MIN_SPEED, REEL_MAX_SPEED, REEL_ACCELERATION);
    }

    public void StopReel()
    {
        timelineHandler.StopReel();
    }

    private void ReelSpinning()
    {
        onReelSpinning?.Invoke();
    }

    private void ReelStartStopping()
    {
        // Symbols are going to be added using an list, not an random
    }

    private void ReelStopped()
    {
        onReelStopped?.Invoke();
    }
}
