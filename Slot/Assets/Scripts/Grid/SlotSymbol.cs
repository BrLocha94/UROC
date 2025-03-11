using UnityEngine;

public class SlotSymbol : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer targetRenderer;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private Animator targetAnimator;

    private Symbol currentSymbol;

    private Coroutine idleBreakerRoutine = null;

    public void SetSymbol(Symbol symbol)
    {
        currentSymbol = symbol;
        targetRenderer.sprite = currentSymbol.sprite;
        targetTransform.transform.localScale = new Vector2(currentSymbol.size, currentSymbol.size);
    }

    public void SetColorHighlighted(bool value)
    {
        Color targetColor = targetRenderer.color;
        targetColor.a = value ? 1f : 0.6f;
        targetRenderer.color = targetColor;
    }

    public void AnimateWinline()
    {
        targetAnimator.Play("Highlight");
    }
}
