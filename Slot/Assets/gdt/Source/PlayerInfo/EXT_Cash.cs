using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class EXT_Cash
{
    public EXT_CashAmount balance { get; set; }
    public EXT_CashAmount onHold { get; set; }
    public EXT_CashAmount availableBalance { get; set; }
    public string type { get; set; }
}

