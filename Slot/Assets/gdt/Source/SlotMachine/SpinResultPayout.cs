using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SpinResultPayout
{
    public bool isWin { get; set; }
    public bool triggersBonus { get; set; }
    public bool triggersScatter { get; set; }
    public int ScatterTriggerSize { get; set; }
    public bool triggersMoreFreeSpins { get; set; }
    public int MoreFreeSpins { get; set; }
    public int WinLinesMultipler { get; set; }
    public decimal BaseWinAmount { get; set; }
    public decimal TotalWin { get; set; }
    public List<decimal> LineWins { get; set; }
    public List<int> WinReelLength { get; set; }
    public List<bool> WinLines { get; set; }
    public string[][] ReelMatrix { get; set; }

    public List<int> GhostReelMultiplier { get; set; }

    //SWIPING MULTIPLIERS GO ACROSS NOT VERTICAL
    public List<int> SwipeWildMultipliers { get; set; }
    public int[] SwipeWildStart { get; set; }
    public int[] SwipeWildEnd { get; set; }

    public FreeGamesResult FreeSpinResults { get; set; }
}
