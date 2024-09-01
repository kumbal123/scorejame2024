using Godot;
using System;

/// <summary>
/// Base class for the actual node that spawns an upgrades effects such as projectiles and stuff.
/// </summary>
public abstract partial class UpgradeNode : Node2D
{
	/// <summary>
	/// The resource that contains the data for the upgrade.
	/// </summary>
	public UpgradeData Upgrade { get; set; }

	/// <summary>
	/// Called when the upgrade's parameters changed, such as when it is levelled up.
	/// </summary>
	public abstract void UpgradeParametersChanged();
}
