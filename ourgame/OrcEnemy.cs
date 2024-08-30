using Godot;
using System;

public partial class OrcEnemy : EnemyBase
{
	public const float Speed = 5;
	public int AttackDamage { get; set; } = 20;
	public float AttackRange { get; set; } = 5;
	public float DetectionRange { get; set; } = 20;
	private Vector2 Position { get; set; } = Vector2.Zero;
	private Vector2 Velocity { get; set; } = Vector2.Zero;
	private AnimatedSprite2D _animatedSprite;

	public override void _Ready()
	{
		base._Ready();
	}

	protected override void RecalculateFollow()
	{
		Vector2 playerPosition = GetPlayerPosition(); //todo GetPlayerPosition
		if (Position.DistanceTo(playerPosition) <= DetectionRange)
		{
			CurrentDirection = (playerPosition - Position).Normalized();
		}
		else
		{
			CurrentDirection = Vector2.Zero;
		}
	}

	protected override void MoveTowardsTarget()
	{
		if (CurrentDirection != Vector2.Zero)
		{
			Velocity = CurrentDirection * Speed;
			Position += Velocity * (float)GetProcessDeltaTime();
			if (Velocity.x != 0)
			{
				// Scale = new Vector2(Mathf.Sign(Velocity.x), 1); flip sprite nejak XD
			}
		}
		else
		{
			Velocity = Vector2.Zero;
		}
	}

	private Vector2 GetPlayerPosition()
	{
		//todo
	}

	private void AttackPlayer()
	{
		if (IsPlayerInRange())
		{
			((AnimationPlayer)GetNode("Animations")).Play("attack1");
		}
	}

	private bool IsPlayerInRange()
	{
		Vector2 playerPosition = GetPlayerPosition();
		return Position.DistanceTo(playerPosition) <= AttackRange;
	}
}
