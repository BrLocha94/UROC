using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup bg;
    [SerializeField]
    private WindowInit windowInit;
    [SerializeField]
    private WindowConfirm windowConfirm;
    [SerializeField]
    private WindowMessage windowMessage;

    private void Awake()
    {
        windowInit.onTurnOffFinishedEvent += ClosedWindow;
        windowConfirm.onTurnOffFinishedEvent += ClosedWindow;
        windowMessage.onTurnOffFinishedEvent += ClosedWindow;
    }

    public void TriggerWindowInit()
    {
        ToogleBG(true);
        windowInit.TurnOn();
    }

    public void TriggerWindowConfirm()
    {
        ToogleBG(true);
        windowConfirm.TurnOn();
    }

    public void TriggerWindowMessage(string message)
    {
        ToogleBG(true);
        windowMessage.SetMessage(message);
        windowMessage.TurnOn();
    }

    private void ClosedWindow()
    {
        ToogleBG(false);
    }

    private void ToogleBG(bool enabled)
    {
        bg.alpha = enabled ? 1f : 0f;
        bg.interactable = enabled;
        bg.blocksRaycasts = enabled;
    }
}
