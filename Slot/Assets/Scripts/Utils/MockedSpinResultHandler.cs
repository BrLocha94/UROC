using System.Collections.Generic;
using UnityEngine;

public class MockedSpinResultHandler
{
    private List<SpinResultPayout> mockedList = new List<SpinResultPayout>();

    int currentMockedResult = 0;

    public MockedSpinResultHandler()
    {
        SpinResultPayout resultPayout_01 = new SpinResultPayout();
        resultPayout_01.ReelMatrix = new string[][]
        {
            new string[] { "AA", "BB", "Smash" },
            new string[] { "BB", "Spin", "Spin" },
            new string[] { "BG", "BB", "FF"}
        };
        resultPayout_01.isWin = false;
        resultPayout_01.WinLines = new List<bool> { false, false, false, false, false };

        SpinResultPayout resultPayout_02 = new SpinResultPayout();
        resultPayout_02.ReelMatrix = new string[][]
        {
            new string[] { "WD", "BB", "SC" },
            new string[] { "KK", "KK", "JJ" },
            new string[] { "Smash", "HH", "KK"}
        };
        resultPayout_02.isWin = true;
        resultPayout_02.WinLines = new List<bool> { false, false, false, false, true };

        SpinResultPayout resultPayout_03 = new SpinResultPayout();
        resultPayout_03.ReelMatrix = new string[][]
        {
            new string[] { "AA", "BB", "CC" },
            new string[] { "DD", "EE", "FF" },
            new string[] { "GG", "HH", "JJ"}
        };
        resultPayout_03.isWin = false;
        resultPayout_03.WinLines = new List<bool> { false, false, false, false, false };

        SpinResultPayout resultPayout_04 = new SpinResultPayout();
        resultPayout_04.ReelMatrix = new string[][]
        {
            new string[] { "AA", "BB", "JJ" },
            new string[] { "KK", "WD", "JJ" },
            new string[] { "JJ", "HH", "JJ"}
        };
        resultPayout_04.isWin = true;
        resultPayout_04.WinLines = new List<bool> { false, true, true, false, false };

        SpinResultPayout resultPayout_05 = new SpinResultPayout();
        resultPayout_05.ReelMatrix = new string[][]
        {
            new string[] { "JJ", "AA", "FF" },
            new string[] { "JJ", "AA", "FF" },
            new string[] { "JJ", "AA", "FF"}
        };
        resultPayout_05.isWin = true;
        resultPayout_05.WinLines = new List<bool> { true, true, false, true, false };

        mockedList.Add(resultPayout_01);
        mockedList.Add(resultPayout_02);
        mockedList.Add(resultPayout_03);
        mockedList.Add(resultPayout_04);
        mockedList.Add(resultPayout_05);
    }

    public SpinResultPayout GetMockedSpin()
    {
        SpinResultPayout spin = mockedList[currentMockedResult];

        currentMockedResult++;
        if (currentMockedResult >= mockedList.Count)
            currentMockedResult = 0;

        return spin;
    }
}
