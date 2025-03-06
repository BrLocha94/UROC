using Assets.Source.SlotMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SlotMachineSessionResponse : GameplaySession
{
    public SlotMachine GameOverview { get; set; }

    public SlotMachineSpinResponse SpinResultResponse { get; set; }
}

