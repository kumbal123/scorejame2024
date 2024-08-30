using Godot;
using System;

/// <summary>
/// Base class for all enemies.
/// </summary>
public partial class EnemyBase : Node
{

	public int Health { get; set; }

	// The direction towards which the enemy is moving.
	private Vector2 CurrentDirection { get; set; } = Vector2.Zero;
	private CharacterBody2D Player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetTree().Root.GetNode("MainScene").GetNode<CharacterBody2D>("PlayerCharacter");
		// Vector2 pos = Player.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RecalculateFollow();
		MoveTowardsTarget();
	}


	/// <summary>
	/// Recalculate and update angle at which to move towards player
	/// </summary>
	// ... maybe only call every x frames to save resources
	private void RecalculateFollow()
	{

	}

	/// <summary>
	/// Move in direction of currentDirection
	/// </summary>
	private void MoveTowardsTarget()
	{

	}
}
