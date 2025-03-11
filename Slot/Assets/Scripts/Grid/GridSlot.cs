using System;
using System.Collections;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public Action<GridSlot> onPositionReseted;
    
    [SerializeField]
    private SlotSymbol slotSymbol;

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

    public Transform GetTransform() { return transform; }
}
