using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttons = new List<Button>();

    public void ToogleButtons(bool enabled)
    {
        foreach (Button button in buttons)
        {
            button.interactable = enabled;
        }
    }
}
