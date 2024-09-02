using Godot;
using System;
using System.Threading.Tasks;

public partial class ArcherEnemy : EnemyBase
{
    private SoundPool _bowSound;
	private PackedScene arrowScene;
    private AudioStreamPlayer _deathSound;

	private float DistanceThreshold = 500.0f;
    public override void loadSounds()
	{
		_bowSound = GetNode<SoundPool>("BowSound");
        _deathSound = GetNode<AudioStreamPlayer>("DeathSound");
		arrowScene = GD.Load<PackedScene>("res://objects/misc/arrow.tscn");
	}
    public override void playAttackSound() {
		_bowSound.PlayRandomSound();
	}
	// archers are OP, maybe no collision damage?
	protected override void BodyEntered(Node2D body) {}

	protected override void MoveTowardsTarget(){
		Vector2 directionToPlayer = Player.Position - GlobalPosition;
		float distanceToPlayer = directionToPlayer.Length();

		if (distanceToPlayer > DistanceThreshold)
		{
			// Player is out of range, move towards the player
			Velocity = directionToPlayer.Normalized() * Speed; // Speed is your movement speed

			if (Velocity.X > 0)
			{
				animatedSprite.FlipH = false;
				animatedSprite.Play("walk");
			}
			else if (Velocity.X < 0)
			{
				animatedSprite.FlipH = true;
				animatedSprite.Play("walk");
			}

			// Move the character
			MoveAndSlide();
		}
		else
		{
			_isAttacking = true;
			Velocity = Vector2.Zero;
			FireArrow();
			if (Player.Position.X > GlobalPosition.X){
				animatedSprite.FlipH = false;
			}
			else{
				animatedSprite.FlipH = true;
			}
			animatedSprite.Play("attack1");
			playAttackSound();
		}
	}
	protected void FireArrow()
	{
		if (arrowScene == null)
		{
			GD.PrintErr("ArrowScene is not assigned!");
			return;
		}
		Arrow arrowInstance = (Arrow)arrowScene.Instantiate();
		arrowInstance.setDamage(Attack);
		AddChild(arrowInstance);
		arrowInstance.GlobalPosition = GlobalPosition;
		Vector2 direction = (Player.GlobalPosition - GlobalPosition).Normalized();
		arrowInstance.LaunchArrow(direction);
	}

	public override async void Deaded()
	{
        CanvasLayer stopwatch = GetTree().Root.GetChild(1).GetNode<CanvasLayer>("stopwatch");
        stopwatch.Call("add_score_for_kill", KillScoreReward);

		DisableEnemy();
		
		_deathSound.Play();
		animatedSprite.Play("death");

		var timeoutTask = Task.Delay(1000);
		await Task.WhenAll(timeoutTask);
		
		QueueFree();
	}
}
