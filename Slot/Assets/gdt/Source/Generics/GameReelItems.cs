using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameReelItems
{
	public long ReelItemId { get; set; }
	public long GameId { get; set; }
	public string ReelCode { get; set; }
	public string ReelName { get; set; }
	public string ReelArtUrl { get; set; }
	public string ReelArtFolder { get; set; }
	public bool hasAnimation { get; set; }
	public string ReelAnimationFolder { get; set; }
	public string ReelType { get; set; }
	public int zIndex { get; set; }
	public int TriggerAmount { get; set; }

	public decimal Multiplier_2x { get; set; }
	public decimal Multiplier_3x { get; set; }
	public decimal Multiplier_4x { get; set; }
	public decimal Multiplier_5x { get; set; }
	public decimal Multiplier_6x { get; set; }
	public decimal Multiplier_7x { get; set; }
	public bool isWild { get; set; }
	/// <summary>
	/// Ensures bonus, scatter symbols only appear once per reel line
	/// </summary>
	public bool AllowOnePerXAxis { get; set; }
	/// <summary>
	/// Limits the reel type to specific reels. NOTE! This is 0 based array so 0 is the first reel!!
	/// </summary>
	public int[] LimitToSpecificReels { get; set; }
	/// <summary>
	/// When the reel items list has "Limit to specific" or "Allow One" set, can this symbol be used as a replacement character?
	/// </summary>
	public bool AcceptableSpecialReplacement { get; set; } = true;
	public string ReelFolder { get; set; }
	public bool WildsMultiply { get; set; }
	public decimal AllWildBaseMultiplier { get; set; }
	public string WildMultiplierType { get; set; }
	public string GenerationWeights { get; set; }
}
