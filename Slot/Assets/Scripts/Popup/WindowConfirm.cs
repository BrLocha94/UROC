using UnityEditor;
using UnityEngine;

public class WindowConfirm : WindowSimple
{
    public void ButtonNoClicked()
    {
        TurnOff();
    }

    public void ButtonYesClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExecuteMenuItem("Edit/Play");
#else
        Application.Quit();
#endif
    }
}
