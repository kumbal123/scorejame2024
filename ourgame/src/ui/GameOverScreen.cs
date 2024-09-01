using Godot;
using System;

public partial class GameOverScreen : ColorRect
{
	private Label scoreLabel;

    public override void _Ready()
    {
		scoreLabel = GetNode<Label>("Score");
    }
    /// <summary>
    /// Plays a fade in animation and loads in the leaderboard scores and retry button.
    /// </summary>
    public void DisplayAndLoadLeaderBoard()
	{
		Label label = GetTree().Root.GetChild(1).GetNode("stopwatch").GetNode<Label>("ScoreLabel");
		scoreLabel.Text = label.Text;
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Appear");
	}

	public void Retry()
	{
		Global.Instance.RestartGame();
	}
}
