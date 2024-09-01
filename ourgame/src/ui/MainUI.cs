using Godot;
using System;

public partial class MainUI : Control
{
	public void DisplayGameOver()
	{
		GetNode<GameOverScreen>("GameOverScreen").DisplayAndLoadLeaderBoard();
	}
}
