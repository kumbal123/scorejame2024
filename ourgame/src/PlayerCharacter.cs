using Godot;
using System;

/// <summary>
/// The class containing logic and controls for the player character.
/// ... maybe change the Node type to something more specialized like CharacterBody..?
/// </summary>
public partial class PlayerCharacter : CharacterBody2D
{

	public int Speed = 200;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Velocity = Vector2.Zero;

        // Process input for movement
        if (Input.IsActionPressed("ui_up"))  // W key
        {
            Velocity += new Vector2(0, -1);
        }
        if (Input.IsActionPressed("ui_down"))  // S key
        {
            Velocity += new Vector2(0, 1);
        }
        if (Input.IsActionPressed("ui_left"))  // A key
        {
            Velocity += new Vector2(-1, 0);
        }
        if (Input.IsActionPressed("ui_right"))  // D key
        {
            Velocity += new Vector2(1, 0);
        }

        // Normalize the velocity to ensure consistent speed in diagonal movement
        if (Velocity.Length() > 0)
        {
            Velocity = Velocity.Normalized() * Speed;
        }

        MoveAndSlide();
	}
}
