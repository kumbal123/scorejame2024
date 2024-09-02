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
		{"res://objects/enemies/basic/ArcherEnemy.tscn", 0},  // Archers start at 0
		{"res://objects/enemies/basic/OrcEnemy.tscn", 2}
	};

	/// <summary>
	/// Interval in seconds, at which to first start spawning enemies.
	/// </summary>
	[Export]
	private double InitialSpawnInterval { get; set; }

	private Timer timer;
	private CharacterBody2D Player;
	private Random random;
	private int waveCount = 0;  // Tracks the number of waves that have spawned

	public override void _Ready()
	{
		random = new Random();
		Player = PlayerCharacter.Instance;
		timer = GetNode<Timer>("Timer");
		timer.WaitTime = InitialSpawnInterval;
		timer.Connect("timeout", new Callable(this, nameof(SpawnTick)));
		timer.Start();
	}

	public void SpawnTick()
	{
		waveCount++;  // Increment the wave count each time enemies spawn

		foreach (KeyValuePair<string, int> spawnEntry in EnemyPool)
		{
			for (int i = 0; i < spawnEntry.Value; i++)
			{
				SpawnAtRandomLocation(spawnEntry.Key);
			}
		}
		UpdateEnemyPool();  // Update the pool after spawning enemies
	}

	/// <summary>
	/// Spawns a supplied enemy tscn at a random location on the map.
	/// </summary>
	private void SpawnAtRandomLocation(String enemyPath)
	{
		EnemyBase enemy = GD.Load<PackedScene>(enemyPath).Instantiate<EnemyBase>();
		enemy.difficultyScale(waveCount);
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
		// 1=top, 2=right, 3=bottom, 4=left
		enemy.Position = new Vector2(x, y);
		AddChild(enemy);
	}

	/// <summary>
	/// Updates spawn pool after every tick, so that enemy numbers grow and escalate.
	/// </summary>
	private void UpdateEnemyPool()
	{
		if (waveCount % 3 == 0)
		{
			foreach (var enemyType in EnemyPool.Keys)
			{
				EnemyPool[enemyType]++;
			}
		}
		if (waveCount >= 3)
		{
			EnemyPool["res://objects/enemies/basic/ArcherEnemy.tscn"] = Math.Max(EnemyPool["res://objects/enemies/basic/ArcherEnemy.tscn"], 1);
		}
	}
}