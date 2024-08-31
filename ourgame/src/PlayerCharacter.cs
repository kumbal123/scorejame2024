using Godot;
using System;

/// <summary>
/// The class containing logic and controls for the player character.
/// </summary>
public partial class PlayerCharacter : CharacterBody2D
{
	// Allows other scripts to obtain the PlayerCharacter node reference using this static variable
	// Almost like making the player character node global.
	public static PlayerCharacter Instance { get; private set; } = null;

	private float Speed { get; set; } = 200;
    private float Hp { get; set; } = 1000;
	private Node Upgrades { get; set; } 

	public override void _EnterTree()
    {
        if (Instance != null) this.QueueFree();
        else Instance = this;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Upgrades = GetNode<Node>("Upgrades");
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

	/// <summary>
	/// Attaches a node that handles an upgrade's effects to the player character.
	/// </summary>
	/// <param name="node">The upgrade node obtained from UpgradeBase.GetNode</param>
	public void AddUpgradeNode(Node2D node)
	{
		Upgrades.AddChild(node);
	}

    public void TakeDamage(float value)
    {
        Hp -= value;
		GD.PrintErr(Hp);
    }

    public void Heal(float value)
    {
        Hp += value;
    }

}
