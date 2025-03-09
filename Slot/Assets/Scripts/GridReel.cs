using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEngine.GraphicsBuffer;

public class GridReel : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private SymbolMapping symbolMapping;

    [SerializeField]
    private List<string> initialSymbols = new List<string>();
    [SerializeField]
    private List<GridSlot> gridSlots = new List<GridSlot>();


    private void Awake()
    {
        for (int i = 0; i < gridSlots.Count; i++)
        {
            GridSlot slot = gridSlots[i];
            slot.onPositionReseted += OnGridSlotPositionReseted;
            Symbol newSymbol = symbolMapping.GetSymbol(initialSymbols[i]);
            slot.SetNewSymbol(newSymbol);
        }
    }

    private void Start()
    {
        director.Pause();
    }

    private void OnDestroy()
    {
        foreach (var slot in gridSlots)
        {
            slot.onPositionReseted -= OnGridSlotPositionReseted;
        }
    }

    private void OnGridSlotPositionReseted(GridSlot target)
    {
        Symbol newSymbol = symbolMapping.GetRandomSymbol();
        target.SetNewSymbol(newSymbol);
    }

    public void StartReel()
    {
        director.extrapolationMode = DirectorWrapMode.Loop;
        director.playableGraph.GetRootPlayable(0).SetSpeed(4.5);
        director.Play();
    }

    public void OnReelAnimationFinished()
    {

    }
}
