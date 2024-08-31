using Godot;
using System;

/// <summary>
/// Node set to be an Autoload: a global Node always present in the tree.
/// Access it from other scripts using GetNode("/root/Global") or Global.Instance
/// </summary>
public partial class Global : Node
{
	// Allows other scripts to obtain the PlayerCharacter node reference using this static variable
	// Almost like making the player character node global.
	public static Global Instance { get; private set; } = null;

	public override void _EnterTree()
    {
        if (Instance != null) this.QueueFree();
        else Instance = this;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initializes the seed to a random one for RNG calls.
		GD.Randomize();
	}
}
