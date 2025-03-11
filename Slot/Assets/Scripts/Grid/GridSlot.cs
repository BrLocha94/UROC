using System;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    [SerializeField]
    private SlotSymbol slotSymbol;

    public Action<GridSlot> onPositionReseted;

    private Vector3 defaultLocalPosition;

    private void Awake()
    {
        defaultLocalPosition = transform.localPosition;
    }

    public void ForceDefaultPosition()
    {
        transform.localPosition = defaultLocalPosition;
    }

    public void SlotResetPosition()
    {
        onPositionReseted?.Invoke(this);
    }

    public void SetNewSymbol(Symbol symbol)
    {
        slotSymbol.SetSymbol(symbol);
    }
}
