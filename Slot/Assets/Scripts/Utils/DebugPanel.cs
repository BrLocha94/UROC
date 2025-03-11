using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI isWin;
    [SerializeField]
    private TextMeshProUGUI reel01;
    [SerializeField]
    private TextMeshProUGUI reel02;
    [SerializeField]
    private TextMeshProUGUI reel03;
    [SerializeField]
    private TextMeshProUGUI winlines;
    [SerializeField]
    private Toggle toogle;

    [SerializeField]
    private GameController gameController;

    private void Start()
    {
        toogle.isOn = gameController.useMockedSpin;
    }

    public void SetData(SpinResultPayout spin)
    {
        isWin.text = "IsWin: " + spin.isWin.ToString();

        reel01.text = "Reel01: " + TranslateReelMatrix(spin.ReelMatrix[0]);
        reel02.text = "Reel02: " + TranslateReelMatrix(spin.ReelMatrix[1]);
        reel03.text = "Reel03: " + TranslateReelMatrix(spin.ReelMatrix[2]);

        winlines.text = "Winlines: " + TranslateWinlines(spin.WinLines);
    }

    private string TranslateReelMatrix(string[] matrix)
    {
        string response = string.Empty;

        for (int i = 0; i < matrix.Length; i++) 
        {
            response += (matrix[i].ToString() + " ");
        }

        return response;
    }

    private string TranslateWinlines(List<bool> winlines)
    {
        string response = string.Empty;

        for (int i = 0; i < winlines.Count; i++)
        {
            response += (winlines[i] ? "T " : "F ");
        }

        return response;
    }

    public void OnToogleValueChanged(bool value)
    {
        gameController.ToogleUseMockedSpin(value);
    }
}
