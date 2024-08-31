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
		{"res://objects/enemies/basic/EnemyFirst.tscn", 4},
	};

	/// <summary>
	/// Interval in seconds, at which to first start spawning enemies.
	/// </summary>
	[Export]
	private double InitialSpawnInterval { get; set; }

	private Timer timer;

    public override void _Ready()
    {
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
	/// <param name="enemyTscn"></param>
	private void SpawnAtRandomLocation(String enemyPath)
	{
		EnemyBase enemy = GD.Load<PackedScene>(enemyPath).Instantiate<EnemyBase>();
		// TODO: Random spawning.
		enemy.Position = new Vector2(1000,10);
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
