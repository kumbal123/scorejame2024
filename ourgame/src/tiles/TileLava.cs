using Godot;
using System;

/// <summary>
/// A tile that offers the player the chance to level up an upgrade.
/// </summary>
public partial class TileLava : TileBase
{
    private Area2D hitbox;

    [Export]
    private float TickInterval { get; set; } = 1.0f;

    /// <summary>
    /// If body is player do damage and activate timer to tick damage if continued in lava.
    /// </summary>
    public override void BodyEnteredTile(Node2D body)
    {
        if (body.IsInGroup("player")) {
            ((PlayerCharacter)body).EnterDamageZone();
        }
    }
}
