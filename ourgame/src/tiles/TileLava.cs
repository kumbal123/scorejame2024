using Godot;
using System;

/// <summary>
/// A tile that offers the player the chance to level up an upgrade.
/// </summary>
public partial class TileLava : TileBase
{
    private Area2D hitbox;
    private float Damage { get; set; } = 50;
    private float TickLength => 1.0f;

    public override void _Ready()
    {
        SetProcessUnhandledKeyInput(false);
        hitbox = GetNode<Area2D>("Area2D");
        GetNode<Timer>("DamageTick").WaitTime = TickLength;
    }

    /// <summary>
    /// If body is player, spawn and display tooltip
    /// </summary>
    public override void BodyEnteredTile(Node2D body)
    {
        TriggerAttack();
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
    }

    public override void BodyExitedTile(Node2D body)
    {

    }

    public void TriggerAttack()
	{
		foreach (Node2D body in hitbox.GetOverlappingBodies())
		{
			if (body.IsInGroup("player")) {
                ((PlayerCharacter) body).TakeDamage(Damage);
            }			
		}
	}
}
