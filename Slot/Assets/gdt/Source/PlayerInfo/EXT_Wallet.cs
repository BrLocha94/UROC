using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public class EXT_Wallet
{
    public EXT_Cash cash { get; set; }
    public EXT_Bonus bonus { get; set; }
    public EXT_CashAmount totalAvailable { get; set; }
    public string currency { get; set; }
}

