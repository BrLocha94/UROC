using System;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    [SerializeField]
    private SlotSymbol slotSymbol;

    public Action<GridSlot> onPositionReseted;

    public void SlotResetPosition()
    {
        onPositionReseted?.Invoke(this);
    }

    public void SetNewSymbol(Symbol symbol)
    {
        slotSymbol.SetSymbol(symbol);
    }
}
