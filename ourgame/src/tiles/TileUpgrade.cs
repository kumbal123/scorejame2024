using Godot;
using System;

/// <summary>
/// A tile that offers the player the chance to level up an upgrade.
/// </summary>
public partial class TileUpgrade : TileBase
{
	/// <summary>
	/// The upgrade that this tile holds.
	/// </summary>
	[Export]
	public UpgradeBase upgrade;
	private UpgradeTooltip tooltip;

	/// <summary>
	/// If body is player, spawn and display tooltip
	/// </summary>
    public override void BodyEnteredTile(Node2D body)
    {
        if (body.IsInGroup("player")) {
			tooltip = GD.Load<PackedScene>("res://objects/ui/UpgradeTooltip.tscn").Instantiate<UpgradeTooltip>();
			AddChild(tooltip);
		}	
    }

    public override void BodyExitedTile(Node2D body)
    {
		if (body.IsInGroup("player")) {
			tooltip.Disappear();
			tooltip = null;
		}
    }

}
