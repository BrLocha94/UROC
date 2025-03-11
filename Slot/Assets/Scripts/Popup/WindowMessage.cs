using TMPro;
using UnityEngine;

public class WindowMessage : WindowSimple
{
    [Header("Window Message")]
    [SerializeField]
    private TextMeshProUGUI message;

    public void SetMessage(string newMessage)
    {
        message.text = newMessage;
    }
}
