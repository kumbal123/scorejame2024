using Godot;
using System;

public partial class Stopwatch : CanvasLayer
{
    private double time = 0.0f;
    private int score = 0;
    private double timePassed = 0;

    public override void _PhysicsProcess(double delta)
    {
        time += delta;
        timePassed += delta;

        UpdateUI();
        UpdateScore();
    }

    private void UpdateUI()
    {
        int minutes = (int)time / 60;  // Calculate total minutes
        int seconds = (int)time % 60;  // Calculate remaining seconds after minutes

        // Format the string as "MM:SS"
        string formattedTime = minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');

        GetNode<Label>("Label").Text = formattedTime;
    }

    private void UpdateScore()
    {
        if (timePassed >= 1)  // Check if 1 second has passed
        {
            timePassed = 0;  // Reset the time passed counter

            if (time < 60)  // Less than 1 minute
            {
                score += 1;
            }
            else if (time < 120)  // Between 1 and 2 minutes
            {
                score += 2;
            }
            else  // More than 2 minutes
            {
                score += 3;
            }

            GetNode<Label>("ScoreLabel").Text = "Score: " + score.ToString();
        }
    }

    public void AddScoreForKill(int value)
    {
        score += value;
        GetNode<Label>("ScoreLabel").Text = "Score: " + score.ToString();
    }
	public int CheckScore(){
		return score;
	}
	public void UpgradeCost(int cost){
		score -= cost; 
	}
}
