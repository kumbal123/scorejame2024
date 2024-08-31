using Godot;
using System;

/// <summary>
/// Garlic clone, attacks in an every every few ticks.
/// </summary>
public partial class UpgradeNodeGarlic : UpgradeNode
{
	private Area2D hitbox;
	
	public override void _Ready()
	{
		hitbox = GetNode<Area2D>("Area2D");
	}

	public override void UpgradeParametersChanged()
    {
        Scale = Vector2.One * Upgrade.Size;
        GetNode<Timer>("DamageTick").WaitTime = ((UpgradeDataGarlic)Upgrade).TickInterval;
		// Triggers the attack for free when upgrading
		TriggerAttack();
    }

    public void TriggerAttack()
	{
		foreach (Node2D body in hitbox.GetOverlappingBodies())
		{
			// if body.IsInGroup("enemy")
			// Shouldn't be necessary if Area2D is only set to detect a specific layer = 4, which all enemies will be set to.
			((EnemyBase) body).TakeDamage(Upgrade.Attack);
		}
	}
}
