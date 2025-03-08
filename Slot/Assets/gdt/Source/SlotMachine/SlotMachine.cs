using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.SlotMachine
{
    [System.Serializable]
    public class SlotMachine
    {
        public long GameId { get; set; }
        public long BonusGameId { get; set; }
        public string GameName { get; set; }
        public string Publisher { get; set; }
        public string FullDescription { get; set; }

        public string ShortCode { get; set; }
        public int xReels { get; set; }
        public int yReels { get; set; }
        public int PayLines { get; set; }
        public int TotalReelItems { get; set; }
        public string BaseFileDirectory { get; set; }
        public string PayTableFilename { get; set; }
        public string GameStatus { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string GameTypeCode { get; set; }

        public bool HasScatterWin { get; set; }
        public bool HasBonusWin { get; set; }

        public bool HasGhostWilds { get; set; }

        /// <summary>
        ///  SWIPE WILDS DO HORIZONTAL WILD MATRIXING
        /// </summary>
        public bool HasSwipeWilds { get; set; }
        public int SwipeWildMultiplier_Min { get; set; }
        public int SwipetWildMultiplier_Max { get; set; }

        public bool ScatterTriggersWheel { get; set; }
        public bool ScatterTriggersPicker { get; set; }


        public bool SupportsBoostBet { get; set; }
        public string GameType { get; set; }

        public GameReelItems ReelItems { get; set; }
        public WinlineReelMatrix WinMatrix { get; set; }
    }
}
