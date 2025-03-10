using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup bg;
    [SerializeField]
    private WindowInit windowInit;
    [SerializeField]
    private WindowConfirm windowConfirm;

    public void TriggerWindowInit()
    {
        bg.alpha = 1f;
        bg.interactable = true;
        bg.blocksRaycasts = true;

        windowInit.TurnOn();
        windowInit.onTurnOffFinishedEvent += ClosedWindow;
    }

    public void TriggerWindowConfirm()
    {
        bg.alpha = 1f;
        bg.interactable = true;
        bg.blocksRaycasts = true;

        windowConfirm.TurnOn();
        windowConfirm.onTurnOffFinishedEvent += ClosedWindow;
    }

    private void ClosedWindow()
    {
        bg.alpha = 0f;
        bg.interactable = false;
        bg.blocksRaycasts = false;
    }
}
