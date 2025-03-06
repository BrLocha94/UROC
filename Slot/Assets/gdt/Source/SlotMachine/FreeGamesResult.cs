using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FreeGamesResult
{
    public List<SpinResultPayout> SpinsResult { get; set; }
    public long GameId { get; set; }
    public decimal TotalWin { get; set; }
    public int InitialFreeSpins { get; set; }
    public int TotalFreeGames { get; set; }
}

