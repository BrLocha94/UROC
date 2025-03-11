using System.Collections.Generic;
using System;
using UnityEngine;

public class GridReel : MonoBehaviour
{
    public Action<GridReel> onReelSpinning;
    public Action<GridReel> onReelStopped;

    [SerializeField]
    private ReelTimelineHandler timelineHandler;
    [SerializeField]
    private SymbolMapping symbolMapping;

    [SerializeField]
    private List<string> initialSymbols = new List<string>();
    [SerializeField]
    private List<GridSlot> gridSlots = new List<GridSlot>();

    private const float REEL_MIN_SPEED = 1f;
    private const float REEL_MAX_SPEED = 10f;
    private const float REEL_ACCELERATION = 0.2f;

    private string[] lastData = null;
    private List<Symbol> currentData = new List<Symbol>();

    private bool isStopping = false;

    private void Awake()
    {
        lastData = new string[gridSlots.Count];

        for (int i = 0; i < gridSlots.Count; i++)
        {
            GridSlot slot = gridSlots[i];
            slot.onPositionReseted += OnGridSlotPositionReseted;
            Symbol newSymbol = symbolMapping.GetSymbol(initialSymbols[i]);
            slot.SetNewSymbol(newSymbol);
            lastData[i] = newSymbol.name;
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
        Symbol newSymbol;

        if (isStopping)
        {
            newSymbol = currentData[currentData.Count - 1];
            currentData.RemoveAt(currentData.Count - 1);
            target.SetNewSymbol(newSymbol);
            return;
        }

        newSymbol = symbolMapping.GetRandomSymbol();
        target.SetNewSymbol(newSymbol);
    }

    public void SetData(string[] reelData)
    {
        lastData = reelData;

        // 1 extra slot on end
        currentData = new List<Symbol>{symbolMapping.GetRandomSymbol()};

        for(int i = 0; i < reelData.Length; i++)
        {
            currentData.Add(symbolMapping.GetSymbol(reelData[i]));
        }

        // 1 extra slot on begin
        currentData.Add(symbolMapping.GetRandomSymbol());
    }

    public void StartReel()
    {
        timelineHandler.StartReel(REEL_MIN_SPEED, REEL_MAX_SPEED, REEL_ACCELERATION);
        isStopping = false;
    }

    public void StopReel()
    {
        timelineHandler.StopReel();
    }

    private void ReelSpinning()
    {
        onReelSpinning?.Invoke(this);
    }

    private void ReelStartStopping()
    {
        isStopping = true;
    }

    private void ReelStopped()
    {
        for (int i = 0; i < gridSlots.Count; i++)
        {
            gridSlots[i].ForceDefaultPosition();
        }

        isStopping = false;
        onReelStopped?.Invoke(this);
    }

    public GridSlot GetRandomGridSlot()
    {
        // EXCLUDE THE EXTRAS
        int random = UnityEngine.Random.Range(1, gridSlots.Count - 1);
        return gridSlots[random];
    }
    
    public void SymbolHighlightToogle(bool value)
    {
        foreach (GridSlot slot in gridSlots) 
        {
            slot.ToogleSymbolHighlight(value);
        }
    }

    public void SymbolHighlightToogle(int index, bool value)
    {
        // COUNT EXTRA SLOT
        int updatedIndex = index + 1;

        gridSlots[updatedIndex].ToogleSymbolHighlight(value);
    }
}
