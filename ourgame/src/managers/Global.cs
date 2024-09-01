using Godot;
using System;

/// <summary>
/// Node set to be an Autoload: a global Node always present in the tree.
/// Access it from other scripts using GetNode("/root/Global") or Global.Instance
/// </summary>

public partial class Global : Node
{
    public float swtime = 0;
    public int score = 0;
    // Allows other scripts to obtain the PlayerCharacter node reference using this static variable
    // Almost like making the player character node global.
    public static Global Instance { get; private set; } = null;

	public override void _EnterTree()
    {
        if (Instance != null) QueueFree();
        else Instance = this;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Randomize();
	}

    public void RestartGame()
    {
        GetTree().Paused = false;
        GetTree().Root.GetNode<GodotObject>("Stopwatch").Set("time", 0.0);
        GetTree().Root.GetNode<GodotObject>("Stopwatch").Set("score", 0);
        GetTree().Root.GetNode<GodotObject>("Stopwatch").Set("time_passed", 0);
		GetTree().ChangeSceneToFile("res://MainScene.tscn");
    }
}
