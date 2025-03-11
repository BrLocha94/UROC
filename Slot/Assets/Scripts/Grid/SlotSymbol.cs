using UnityEngine;

public class SlotSymbol : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer targetRenderer;
    [SerializeField]
    private Transform targetTransform;

    private Symbol currentSymbol;

    private Coroutine idleBreakerRoutine = null;

    public void SetSymbol(Symbol symbol)
    {
        currentSymbol = symbol;
        targetRenderer.sprite = currentSymbol.sprite;
        targetTransform.transform.localScale = new Vector2(currentSymbol.size, currentSymbol.size);
    }
}
