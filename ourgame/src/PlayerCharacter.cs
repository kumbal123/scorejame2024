using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The class containing logic and controls for the player character.
/// </summary>
public partial class PlayerCharacter : CharacterBody2D
{
	// Allows other scripts to obtain the PlayerCharacter node reference using this static variable
	// Almost like making the player character node global.
	public static PlayerCharacter Instance { get; private set; } = null;

	public float Speed { get { return _speed; } set { _speed = value; } }
    public float Hp { get { return _hp; } private set {_hp = value; HpBar.Value = value; } }
	public float MaxHp { get { return _maxHp; } set {_maxHp = value; HpBar.MaxValue = value; } }
	public int Damage { get {return _damage; } set { _damage = value; }}

    private float _speed = 200;
	private float _hp;
	private float _maxHp;
	private int _damage = 50;



	private bool _isAttacking = false;

	/// <summary>
	/// A timer that ticks every 1 second while player is still taking damage, invulnerable until the next tick.
	/// </summary>
	private Timer DamageTick { get; set; }
	private Area2D Hitbox { get; set; }
	private Area2D SlashHitbox { get; set; }
	private Node Upgrades { get; set; }
	private ProgressBar HpBar { get; set; }
	private AnimationPlayer Anim { get; set; }
	private AnimatedSprite2D AnimSprite { get; set; }

	public override void _EnterTree()
    {
        if (Instance != null) this.QueueFree();
        else Instance = this;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DamageTick = GetNode<Timer>("DamageTick");
		Hitbox = GetNode<Area2D>("Area2D");
		SlashHitbox = GetNode<Area2D>("Slash");
		Upgrades = GetNode<Node>("Upgrades");
		HpBar = GetNode<ProgressBar>("HealthBar");
		Anim = GetNode<AnimationPlayer>("AnimationPlayer");
		AnimSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Hp = 100;
		MaxHp = 100;
		AnimSprite.AnimationFinished += OnAnimationFinished;
    }

	// Called every physics frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_isAttacking) {
			return;
		}

        PlayerMovement();
		PlayerInteraction();
	}

	private void PlayerMovement() {
		Velocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        // Normalize the velocity to ensure consistent speed in diagonal movement
        if (Velocity.Length() > 0) {
            Velocity = Velocity.Normalized() * Speed;
			if (Velocity.X < 0) {
				AnimSprite.FlipH = true;
				SlashHitbox.Scale = new Vector2(-1, 1);
			} else if (Velocity.X > 0) {
				AnimSprite.FlipH = false;
				SlashHitbox.Scale = new Vector2(1, 1);
			}

			AnimSprite.Play("walk");
        } else {
			AnimSprite.Play("idle");
		}

        MoveAndSlide();
	}

	private void PlayerInteraction() {
		if (Input.IsActionJustPressed("ui_accept")) {
			AnimSprite.Play("slash");
			_isAttacking = true;

			foreach (Node2D body in SlashHitbox.GetOverlappingBodies()) {
				((EnemyBase) body).TakeDamage(Damage);
			}

		}
	}

	/// <summary>
	/// Attaches a node that handles an upgrade's effects to the player character.
	/// </summary>
	/// <param name="node">The upgrade node obtained from UpgradeBase.GetNode</param>
	public void AddUpgradeNode(Node2D node)
	{
		Upgrades.AddChild(node);
	}

	/// <summary>
	/// Called when the player collides with something damaging, such as an enemy or lava tile.
	/// Damage is gained immediately upon first contact, but then gain invincibility for 1 second.
	/// Each second, if there is still something damaging the player, the thing dealing the highest damage is dealt.
	/// </summary>
	public void EnterDamageZone()
	{
		if (DamageTick.IsStopped()) {
			DamageTick.Start();
			DamageCheck();
		}
	}

	public void DamageCheck()
	{
		// Go through all Area2D's, and enemy CharacterBody2D's to see which one is dealing the highest damage
		int highestDamage = 0;
		foreach (Area2D area in Hitbox.GetOverlappingAreas())
		{
			if (area is DamagingArea2D danger && danger.Damage > highestDamage) {
				highestDamage = danger.Damage;
			}
		}

		if (highestDamage == 0) {
			DamageTick.Stop();
		}
		else {
			TakeDamage(highestDamage);
		}
	}

	/// <summary>
	/// Deal damage to player.
	/// </summary>
	/// <param name="value">The initial damage value, will then be processed through defense formula.</param>
	public void TakeDamage(float value)
    {
		// Stop() so that the damage animation starts over if new damage is taken while animation still in progress.
		// Otherwise default behavior would just finish playing the animation from previous damage
		Anim.Stop();
		Anim.Play("TakeDamage");
		// TODO: defense calculation
        Hp -= value;
		if (Hp <= 0) {
			GameOver();
		}
    }

    public void Heal(float value)
    {
        Hp += value;
		if (Hp > MaxHp) Hp = MaxHp;
    }

	/// <summary>
	/// When the player has deaded. Show scoreboard and retry button?
	/// </summary>
	public void GameOver() {
		// TODO
	}

	private void OnAnimationFinished()
    {
        if (AnimSprite.Animation == "slash")
        {
            _isAttacking = false;
            AnimSprite.Play("idle");
        }
    }

}
