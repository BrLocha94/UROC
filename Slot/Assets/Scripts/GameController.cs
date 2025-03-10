using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Mocked handlers")]
    [SerializeField]
    private bool useMockedSpin = false;

    [Space]

    [Header("References")]
    [SerializeField]
    private WindowManager windowManager;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private Grid grid;

    private MockedSpinResultHandler mockedSpinHandler = new MockedSpinResultHandler();

    private void Start()
    {
        windowManager.TriggerWindowInit();
    }

    public void PLAY()
    {
        uiManager.ToogleButtons(false);

        if (useMockedSpin)
        {
            StartSpin(mockedSpinHandler.GetMockedSpin());
        }
        else
        {
            SlotNetwork.Instance.onRequestSucceded += OnSpinRequestSucceded;
            SlotNetwork.Instance.onRequestFailed += OnSpinRequestFailed;
            SlotNetwork.Instance.StartRequestSpin();
        }
    }


    private void OnSpinRequestSucceded()
    {
        SlotNetwork.Instance.onRequestSucceded -= OnSpinRequestSucceded;
        SlotNetwork.Instance.onRequestFailed -= OnSpinRequestFailed;
        StartSpin(SlotNetwork.Instance.spinResponse.SpinResult.Payout);
    }

    private void OnSpinRequestFailed()
    {
        SlotNetwork.Instance.onRequestSucceded -= OnSpinRequestSucceded;
        SlotNetwork.Instance.onRequestFailed -= OnSpinRequestFailed;

        uiManager.ToogleButtons(true);
        //ADD FEEDBACK WINDOW
    }

    private void StartSpin(SpinResultPayout spin)
    {
        grid.onIdleEnter += OnGridRevealFinished;
        grid.StartSpin(spin);
    }

    private void OnGridRevealFinished()
    {
        grid.onIdleEnter -= OnGridRevealFinished;

        if (useMockedSpin)
            uiManager.ToogleButtons(true);
        else
        {
            SlotNetwork.Instance.onRequestSucceded += OnConfirmRequestSucceded;
            SlotNetwork.Instance.onRequestFailed += OnConfirmRequestFailed;
            SlotNetwork.Instance.StartRequestConfirm();
        }
    }

    private void OnConfirmRequestSucceded()
    {
        SlotNetwork.Instance.onRequestSucceded -= OnConfirmRequestSucceded;
        SlotNetwork.Instance.onRequestFailed -= OnConfirmRequestFailed;

        uiManager.ToogleButtons(true);
    }

    private void OnConfirmRequestFailed()
    {
        SlotNetwork.Instance.onRequestSucceded -= OnConfirmRequestSucceded;
        SlotNetwork.Instance.onRequestFailed -= OnConfirmRequestFailed;

        uiManager.ToogleButtons(true);
        //ADD FEEDBACK WINDOW
    }
}
