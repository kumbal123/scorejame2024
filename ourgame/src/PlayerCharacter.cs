using Godot;
using System;

/// <summary>
/// The class containing logic and controls for the player character.
/// </summary>
public partial class PlayerCharacter : CharacterBody2D
{

	private float Speed { get; set; } = 200;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

    }

	// Called every physics frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        Velocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        // Normalize the velocity to ensure consistent speed in diagonal movement
        if (Velocity.Length() > 0)
        {
            Velocity = Velocity.Normalized() * Speed;
        }

        MoveAndSlide();
	}

}
