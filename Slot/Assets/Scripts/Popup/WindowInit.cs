using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowInit : WindowSimple
{
    [Header("Window Init")]
    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button finishButton;

    [Space]

    [SerializeField]
    private string awaitMessage = "Please await...";
    [SerializeField]
    private string succededMessage = "Connected to the server.";
    [SerializeField]
    private string failedMessage = "Cant connect to server, try again.";

    protected override IEnumerator TurnOnRoutine()
    {
        retryButton.gameObject.SetActive(false);
        finishButton.gameObject.SetActive(false);

        return base.TurnOnRoutine();
    }

    protected override void OnTurnOnFinished()
    {
        base.OnTurnOnFinished();

        // Start network connection
        SlotNetwork.Instance.onRequestSucceded += OnRequestSucceded;
        SlotNetwork.Instance.onRequestFailed += OnRequestFailed;

        message.text = awaitMessage;
        SlotNetwork.Instance.StartRequestInitialize();
    }

    private void OnRequestSucceded()
    {
        SlotNetwork.Instance.onRequestSucceded -= OnRequestSucceded;
        SlotNetwork.Instance.onRequestFailed -= OnRequestFailed;
        finishButton.gameObject.SetActive(true);
        message.text = succededMessage;
    }

    private void OnRequestFailed()
    {
        SlotNetwork.Instance.onRequestSucceded -= OnRequestSucceded;
        SlotNetwork.Instance.onRequestFailed -= OnRequestFailed;
        retryButton.gameObject.SetActive(true);
        message.text = failedMessage;
    }

    public void ButtonFinishClick()
    {
        finishButton.gameObject.SetActive(false);
        TurnOff();
    }

    public void ButtonRetryClick()
    {
        retryButton.gameObject.SetActive(false);
        message.text = awaitMessage;
        SlotNetwork.Instance.StartRequestInitialize();
    }
}
