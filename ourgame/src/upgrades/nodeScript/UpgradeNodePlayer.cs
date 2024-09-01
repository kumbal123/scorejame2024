using Godot;
using System;

/// <summary>
/// Garlic clone, attacks in an every every few ticks.
/// </summary>
public partial class UpgradeNodePlayer : UpgradeNode
{
	private PlayerCharacter Player;
	
	public override void _Ready()
	{
		Player = PlayerCharacter.Instance;
		// Needs to upgrade stats on first level up too, i.e. when this is attached.
		UpgradeParametersChanged();
	}

	public override void UpgradeParametersChanged()
    {
		UpgradeDataPlayer stats = (UpgradeDataPlayer)Upgrade;

		Player.MaxHp += stats.MaxHp;
		Player.Heal(stats.MaxHp);
		Player.Speed += stats.Speed;
		Player.Damage += stats.Attack;
    }
}
