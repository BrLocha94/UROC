using System.Collections.Generic;
using UnityEngine;

public class GridReel : MonoBehaviour
{
    [SerializeField]
    private SymbolMapping symbolMapping;
    [SerializeField]
    private List<GridSlot> gridSlots = new List<GridSlot>();
}
