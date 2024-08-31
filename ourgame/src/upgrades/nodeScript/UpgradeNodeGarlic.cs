using Godot;
using System;

public partial class UpgradeNodeGarlic : UpgradeNode
{
	private Area2D hitbox;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hitbox = GetNode<Area2D>("Area2D");
	}

	public override void UpgradeParametersChanged()
    {
        Scale = Vector2.One * Upgrade.Size;
    }

    public void TriggerAttack()
	{
		foreach (Node2D body in hitbox.GetOverlappingBodies())
		{
			// if body.IsInGroup("enemy")
			// Shouldn't be necessary if Area2D is only set to detect a specific layer, which all enemies will be set to.
			((EnemyBase) body).TakeDamage(Upgrade.Attack);
		}
	}

}
