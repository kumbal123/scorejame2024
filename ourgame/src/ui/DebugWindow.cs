using Godot;
using System;

public partial class DebugWindow : ColorRect
{
	private EnemyManager enemyManager;
	private LavaMap lavaMap;	
	private Label playerStats;
	private Label enemyInfo;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (!OS.IsDebugBuild()) {
			QueueFree();
		}
		else {
			enemyManager = GetTree().CurrentScene.GetNode<EnemyManager>("EnemyManager");
			lavaMap = GetTree().CurrentScene.GetNode<LavaMap>("Node2D/LavaMapLayer");
			playerStats = GetNode<Label>("PlayerStats");
			enemyInfo = GetNode<Label>("EnemyInfo");
			RefreshInfo();
		}
	}

	public void RefreshInfo()
	{
		PlayerCharacter p = PlayerCharacter.Instance;
		playerStats.Text = $"Player Stats\nHP: {p.Hp}/{p.MaxHp}\nAttack: {p.Damage}\nSpeed: {p.Speed}";
		enemyInfo.Text = $"Enemy count: {enemyManager.GetChildCount() - 1}\nLava tiles: {lavaMap.GetUsedCellsById(0).Count}";
	}
	
}
