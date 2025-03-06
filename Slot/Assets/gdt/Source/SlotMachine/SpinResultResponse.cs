using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SpinResultResponse
{
    public decimal betSize { get; set; }
    public long gameId { get; set; }
    public string uid { get; set; }
    public string spinAction { get; set; }
    public SpinResultPayout Payout { get; set; }
}

