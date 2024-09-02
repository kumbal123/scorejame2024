using Godot;
using System;
using System.Diagnostics;

/// <summary>
/// Base class for all enemies.
/// </summary>
public partial class EnemyBase : CharacterBody2D
{
	[Export]
	public int Health { get; set; } = 100;
	[Export]
	public int Attack { get; set; } = 5;
	[Export]
	public int Defense { get; set; } = 0;
	[Export]
	public int Speed { get; set; } = 100;
	//protected AnimatedSprite2D animatedSprite = null;
	protected AnimatedSprite2D animatedSprite { get; set; }
	protected bool _isAttacking = false;

	/// <summary>
	/// Score reward gained for killing this enemy.
	/// </summary>
	[Export]
	public int KillScoreReward { get; set; } = 100;

    public float Damage => Attack;

    protected CharacterBody2D Player;
	private AnimationPlayer anim;

	// The frame, during which enemy updates movement direction.
	// Randomly generated at start to spread out load.
	private ulong updateFrame;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		updateFrame = 1 + GD.Randi() % 60;
		Player = PlayerCharacter.Instance;
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GetNode<DamagingArea2D>("Area2D").Damage = Attack;
		animatedSprite.AnimationFinished += OnAnimationFinished;
		loadSounds();
	}

	public virtual void loadSounds(){}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Only recalculate direction every x frames to spread out calculations.
		if (Engine.GetProcessFrames() % updateFrame == 0) {
			RecalculateFollow();
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		if (_isAttacking){
			return;
		}
		MoveTowardsTarget();
    }

    /// <summary>
    /// Recalculate and update angle at which to move towards player
    /// </summary>
    // ... maybe only call every x frames to save resources
    private void RecalculateFollow()
	{
		Velocity = (Player.Position - Position).Normalized() * Speed;
	}

	/// <summary>
	/// Move in direction of Velocity
	/// </summary>
	protected virtual void MoveTowardsTarget()
	{
		if(Velocity.X > 0){
			animatedSprite.FlipH = false;
			animatedSprite.Play("walk");
		}else if(Velocity.X < 0){
			animatedSprite.FlipH = true;
			animatedSprite.Play("walk");
		}else{
			animatedSprite.Play("idle");
		}
		MoveAndSlide();
	}

	protected virtual void BodyEntered(Node2D body) {
		if (body.IsInGroup("player")) {
			((PlayerCharacter)body).EnterDamageZone();
			animatedSprite.Play("attack1");
			_isAttacking = true;
			playAttackSound();
		}
	}
	public virtual void playDeathSound(){}
	public virtual void playAttackSound(){}
	/// <summary>
	/// Incurs damage to the enemy. Takes defense into calculation.
	/// </summary>
	/// <param name="damage">Damage to take.</param>
	public void TakeDamage(int damage)
	{
		// Add some kind of defense calculation formula...
		Health -= damage;
		anim.Play("TakeDamage");
		if (Health <= 0){
			Deaded();
		}
	}

	/// <summary>
	/// Called when the enemy is defeated.
	/// </summary>
	public virtual async void Deaded()
	{
        CanvasLayer stopwatch = GetNode<CanvasLayer>("/root/Stopwatch");
        stopwatch.Call("add_score_for_kill", KillScoreReward);
		
		// Cool death animation and also darop stuff?
		playDeathSound();
		DisableEnemy();
		animatedSprite.Play("death");
		await ToSignal(animatedSprite, "animation_finished");
		QueueFree();
	}
	private void OnAnimationFinished()
    {
        if (animatedSprite.Animation == "attack1")
        {
            _isAttacking = false;
            animatedSprite.Play("idle");
        }
    }

	/// <summary>
	/// Disables enemy functions such as movement and hitboxes.
	/// </summary>
	protected void DisableEnemy()
	{
		ProcessMode = ProcessModeEnum.Disabled;
	}
	public void difficultyScale(int modifier){
		Health = Health + modifier*5;
		Defense = Defense + modifier;
		Speed = Speed + 10;
		Attack = Attack + modifier;
	}
}