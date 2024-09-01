using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the spawning of enemy waves.
/// </summary>
public partial class EnemyManager : Node
{
	/// <summary>
	/// The set of enemies to spawn.
	/// The format is { EnemyBase.tscn: amountToSpawn }
	/// </summary>
	public Dictionary<string, int> EnemyPool { get; set; } = new()
	{
		{"res://objects/enemies/basic/ArcherEnemy.tscn", 2},{"res://objects/enemies/basic/OrcEnemy.tscn", 2}
	};

	/// <summary>
	/// Interval in seconds, at which to first start spawning enemies.
	/// </summary>
	[Export]
	private double InitialSpawnInterval { get; set; }

	private Timer timer;
	private CharacterBody2D Player;
	private Random random;
	public override void _Ready()
	{
		random = new Random();
		Player = PlayerCharacter.Instance;
		timer = GetNode<Timer>("Timer");
		timer.WaitTime = InitialSpawnInterval;//SpawnInterval = InitialSpawnInterval;
	}

	public void SpawnTick()
	{
		foreach (KeyValuePair<string, int> spawnEntry in EnemyPool)
		{
			for (int i=0; i<spawnEntry.Value; i++)
			{
				SpawnAtRandomLocation(spawnEntry.Key);
			}
		}
		UpdateEnemyPool();
	}

	/// <summary>
	/// Spawns a supplied enemy tscn at a random location on the map.
	/// </summary>
	/// <param name="enemyTscn"></param>awnAtRandomLocation(String enemyPath)
private void SpawnAtRandomLocation(String enemyPath)
{
	EnemyBase enemy = GD.Load<PackedScene>(enemyPath).Instantiate<EnemyBase>();
		int selectedNumber = random.Next(1, 5);
		int x = 0;
		int y = 0;
		switch (selectedNumber)
		{
			case 1:
				x = random.Next((int)Player.Position.X - 1000, (int)Player.Position.X + 1000);
				y = (int)Player.Position.Y + 650;
				break;
			case 2:
				y = random.Next((int)Player.Position.Y - 650, (int)Player.Position.Y + 650);
				x = (int)Player.Position.X + 1000;
				break;
			case 3:
				x = random.Next((int)Player.Position.X - 1000, (int)Player.Position.X + 1000);
				y = (int)Player.Position.Y - 650;
				break;
			case 4:
				y = random.Next((int)Player.Position.Y - 650, (int)Player.Position.Y + 650);
				x = (int)Player.Position.X - 1000;
				break;
			default:
				break;
		}
		// 1=top, 2=right, 3=bot, 4=left
		enemy.Position = new Vector2(x, y);
		AddChild(enemy);
}

	
	/// <summary>
	/// Updates spawn pool after every tick, so that enemy numbers grow and escalate.
	/// </summary>
	private void UpdateEnemyPool()
	{
		// TODO
	}
	
}
