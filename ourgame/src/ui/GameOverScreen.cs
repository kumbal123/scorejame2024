using Godot;
using System;

public partial class GameOverScreen : ColorRect
{

	/// <summary>
	/// Plays a fade in animation and loads in the leaderboard scores and retry button.
	/// </summary>
	public void DisplayAndLoadLeaderBoard()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Appear");
	}

	public void Retry()
	{
		Global.Instance.RestartGame();
	}
}
