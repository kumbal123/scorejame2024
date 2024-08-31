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
	}

	public override void UpgradeParametersChanged()
    {
		UpgradeDataPlayer stats = (UpgradeDataPlayer)Upgrade;

		Player.MaxHp += stats.MaxHp;
		Player.Speed += stats.Speed;
		Player.Damage += stats.Attack;
    }
}
