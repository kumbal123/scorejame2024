using Godot;
using System;
using System.Runtime.ExceptionServices;

/// <summary>
/// A base class representing an upgrade and its data such as parameters.
/// </summary>

// ! Note for the fam: Resources are shared by reference, so updating the Resource's variable will update it everywhere it's used.
public abstract partial class UpgradeData : Resource
{
	public int Level { get; private set; } = 0;
	public int Attack { get; protected set; }
	public float Size { get; protected set; }
	public float Speed { get; protected set; }

	public abstract string Title { get; }

	/// <summary>
	/// Returns the upgrades icon sprite.
	/// </summary>
	// return GD.Load<Texture2D>("res://imgs/upgradeIcons/someIcon.png"); 
	public abstract Texture2D GetIcon { get; }

	/// <summary>
	/// Loads and instantiates the node that spawns the upgrade's projectiles and stuff.
	/// </summary>
	// return GD.Load<PackedScene>("res://objects/upgradeInstance/").Instantiate<UpgradeNode>();
	protected abstract UpgradeNode GetUpgradeNode { get; }

	/// <summary>
	/// Emitted when the upgrade is levelled up, so that UpgradeNode can receive the info and update itself accordingly. 
	/// </summary>
	[Signal]
	public delegate void ParametersUpdatedEventHandler();


	/// <summary>
	/// Perform the logic of levelling up the upgrade and updating its parameters.
	/// An upgrade begins at Level 0 (not obtained), levelling up to 1 will activate its effects.
	/// </summary>
	public void LevelUp()
	{
		Level += 1;
		if (Level == 1)
			ObtainUpgrade();
		else
			LevelUpIncreaseParameters();
	}

	/// <summary>
	/// Called the first time the player obtains the upgrade.
	/// </summary>
	private void ObtainUpgrade()
	{
		UpgradeNode node = GetUpgradeNode;
		ParametersUpdated += node.UpgradeParametersChanged;
		PlayerCharacter.Instance.AddUpgradeNode(node);
	}

	/// <summary>
	/// Override this to do initial setup here if necessary, such as initializing parameters and data.
	/// </summary>
	protected virtual void SetupAfterObtain() {}

	/// <summary>
	/// Increase the upgrade's stats after levelling up.
	/// Implemented in the inherited Upgrade scripts so they can handle their own internal logic.
	/// </summary>
	protected abstract void LevelUpIncreaseParameters();

	/// <summary>
	/// Used by UpgradeTooltip to display to the player how and which parameters will increase with level up.
	/// Or, if level == 0, can display a brief explanation of the effect.
	/// </summary>
	/// <returns>Returns the string text of level up info.</returns>
	public abstract String LevelUpInfo();
}
