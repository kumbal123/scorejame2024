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
	public UpgradeData upgrade;
	private UpgradeTooltip tooltip;
	private Sprite2D sprite2D;

    public override void _Ready()
    {
        SetProcessUnhandledKeyInput(false);
    }

	/// <summary>
	/// Handle the input for player buying upgrade.
	/// </summary>
	public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (@event.IsActionReleased("ui_aux_confirm")) {
			upgrade.LevelUp();
			tooltip.BuyUpgrade(upgrade);
		}
    }

    /// <summary>
    /// If body is player, spawn and display tooltip
    /// </summary>
    public override void BodyEnteredTile(Node2D body)
    {
        if (body.IsInGroup("player")) {
			SetProcessUnhandledKeyInput(true);
			tooltip = GD.Load<PackedScene>("res://objects/ui/UpgradeTooltip.tscn").Instantiate<UpgradeTooltip>();
			tooltip.LoadUpgradeData(upgrade);
			AddChild(tooltip);
		}	
    }

	/// <summary>
	/// Despawn tooltip upon player exiting tile.
	/// </summary>
    public override void BodyExitedTile(Node2D body)
    {
		if (body.IsInGroup("player")) {
			SetProcessUnhandledKeyInput(false);
			tooltip.Disappear();
			tooltip = null;
		}
    }

}
