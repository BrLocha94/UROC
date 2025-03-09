using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    private List<SpinResultPayout> mockedList = new List<SpinResultPayout>();

    int currentMockedResult = 0;

    private void Awake()
    {
        SpinResultPayout resultPayout_01 = new SpinResultPayout();
        resultPayout_01.ReelMatrix = new string[][]
        {
            new string[] { "AA", "BB", "Smash" },
            new string[] { "BB", "BB", "Spin" },
            new string[] { "BG", "BB", "FF"}
        };

        SpinResultPayout resultPayout_02 = new SpinResultPayout();
        resultPayout_02.ReelMatrix = new string[][]
        {
            new string[] { "WD", "BB", "SC" },
            new string[] { "KK", "KK", "JJ" },
            new string[] { "WD", "HH", "KK"}
        };

        SpinResultPayout resultPayout_03 = new SpinResultPayout();
        resultPayout_03.ReelMatrix = new string[][]
        {
            new string[] { "AA", "BB", "CC" },
            new string[] { "DD", "EE", "FF" },
            new string[] { "GG", "HH", "JJ"}
        };

        mockedList.Add(resultPayout_01);
        mockedList.Add(resultPayout_02);
        mockedList.Add(resultPayout_03);
    }

    public void PLAY()
    {
        grid.StartSpin(mockedList[currentMockedResult]);
        
        currentMockedResult++;
        if (currentMockedResult >= mockedList.Count)
            currentMockedResult = 0;
    }
}
