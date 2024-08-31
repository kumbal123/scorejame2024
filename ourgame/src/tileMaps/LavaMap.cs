using Godot;
using System;
using System.Collections.Generic;

public partial class LavaMap : TileMapLayer
{
    [Export] private float ExpansionInterval = 5.0f;
    [Export] private float Damage = 50;
    [Export] private float DamageInterval = 1.0f;
    [Export] private Vector2I LavaTileId = Vector2I.Zero; // Assuming lava tile is at (0,0) in your atlas

    private float timeSinceLastExpansion = 0.0f;
    private float timeSinceLastDamage = 0.0f;
    private Random random = new Random();
	private CharacterBody2D Player;

    public override void _Ready()
    {
        // Set initial lava tile
        SetCell(Vector2I.Zero, 0, LavaTileId);
		Player = PlayerCharacter.Instance;
    }

    public override void _Process(double delta)
    {
        timeSinceLastExpansion += (float)delta;
        timeSinceLastDamage += (float)delta;

        if (timeSinceLastExpansion >= ExpansionInterval)
        {
            ExpandLava();
            timeSinceLastExpansion = 0.0f;
        }

        if (timeSinceLastDamage >= DamageInterval)
        {
            DamagePlayersInLava();
            timeSinceLastDamage = 0.0f;
        }
    }

    private void ExpandLava()
    {
        var cellsToAdd = new HashSet<Vector2I>();
        var usedCells = GetUsedCells();

        foreach (Vector2I cell in usedCells)
        {
            Vector2I[] neighbors = new Vector2I[]
            {
                cell + Vector2I.Right,
                cell + Vector2I.Left,
                cell + Vector2I.Down,
                cell + Vector2I.Up
            };

            foreach (Vector2I neighbor in neighbors)
            {
                if (GetCellSourceId(neighbor) == -1 && random.NextDouble() < 0.5)
                {
                    cellsToAdd.Add(neighbor);
                }
            }
        }

        foreach (Vector2I cell in cellsToAdd)
        {
            SetCell(cell, 0, LavaTileId);
        }
    }

    private void DamagePlayersInLava()
    {
        Vector2I playerCell = LocalToMap(ToLocal(Player.GlobalPosition));
		if (GetCellSourceId(playerCell) != -1)  // If the player is on a lava tile
		{
			if (Player is PlayerCharacter playerCharacter)
			{
				playerCharacter.TakeDamage(Damage);
			}
		}
    }
}