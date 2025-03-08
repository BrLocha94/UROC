using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[System.Serializable]
public class SlotMachineSpinResponse
{
    public string BearerToken { get; set; }
    public SpinResultResponse SpinResult { get; set; }
    public EXT_PlayerInformation PlayerInformation { get; set; }
}

