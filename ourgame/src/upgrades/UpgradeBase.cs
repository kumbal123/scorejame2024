using Godot;
using System;

/// <summary>
/// A base class representing an upgrade and its data such as parameters.
/// </summary>

// ! Note for the fam: Resources are shared by reference, so updating the Resource's variable will update it everywhere it's used.
public abstract partial class UpgradeBase : Resource
{
	public virtual string Title { get; protected set; } = "NaN";
	public int Level { get; private set; } = 0;

	// return GD.Load<Texture2D>("res://imgs/upgradeIcons/someIcon.png"); 
	public virtual Texture2D GetIcon { get { return null; } }


	/// <summary>
	/// Perform the logic of levelling up the upgrade and updating its parameters.
	/// An upgrade begins at Level 0 (not obtained), levelling up to 1 will activate its effects.
	/// </summary>
	public void LevelUp()
	{
		Level += 1;
		if (Level == 1)
			InitiateEffects();
		else
			LevelUpIncreaseParameters();
	}

	/// <summary>
	/// Called the first time the player obtains the upgrade. Do initial setup here such as spawning Node that does the actual attacks etc.
	/// </summary>
	public abstract void InitiateEffects();

	/// <summary>
	/// Increase the upgrade's stats after levelling up.
	/// Implemented in the inherited Upgrade scripts so they can handle their own internal logic.
	/// </summary>
	public abstract void LevelUpIncreaseParameters();

	/// <summary>
	/// Used by UpgradeTooltip to display to the player how and which parameters will increase with level up.
	/// </summary>
	/// <returns>Returns the string text of level up info.</returns>
	public abstract String LevelUpInfo();
}
