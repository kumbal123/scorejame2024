using Godot;
using System;

/// <summary>
/// UI Tooltip that is shown when stepping on TileUpgrade.
/// Contains information such as title, icon and effects of the level up.
/// </summary>
public partial class UpgradeTooltip : TextureRect
{
	/// <summary>
	/// Loads the tooltip data from an upgrade.
	/// </summary>
	/// <param name="upgrade">The upgrade the tooltip is to display.</param>
	public void LoadUpgradeData(UpgradeData upgrade)
	{
		GetNode<Label>("Title").Text = upgrade.Title;
		GetNode<TextureRect>("Icon").Texture = upgrade.GetIcon;
		GetNode<Label>("LevelIndicator").Text = $"Level {upgrade.Level} -> {upgrade.Level + 1}";
		GetNode<Label>("Description").Text = upgrade.LevelUpInfo();
	}

	/// <summary>
	/// Plays animation and sound feedback for buying upgrade
	/// </summary>
	/// /// <param name="upgrade">The displayed upgrade. Necessary for updating the tooltip with the next level data.</param>
	public void BuyUpgrade(UpgradeData upgrade)
	{
		GetNode<AnimationPlayer>("Anim2").Play("Buy");
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		LoadUpgradeData(upgrade);
	}

	/// <summary>
	/// Fades the tooltip out and automatically frees it afterwards.
	/// </summary>
	public void Disappear()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Disappear");
	}
}
