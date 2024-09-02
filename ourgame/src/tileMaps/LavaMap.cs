using Godot;
using System;
using System.Collections.Generic;

public partial class LavaMap : TileMapLayer
{
    [Export] private float ExpansionInterval = 5.0f;
    [Export] private float Damage = 5;
    [Export] private float DamageInterval = 0.01f;
    [Export] private Vector2I LavaTileId = Vector2I.Zero; // Assuming lava tile starts at (0,0) in your atlas
    [Export] private Vector2I LavaTileIdMax = new Vector2I(5, 0); // Max atlas coordinates for lava tile

    private float timeSinceLastExpansion = 0.0f;
    private float timeSinceLastDamage = 0.0f;
    private Random random = new Random();
    private CharacterBody2D Player;

    public override void _Ready()
    {
        // Set initial lava tile with randomization
        SetRandomLavaTile(Vector2I.Zero);
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
            SetRandomLavaTile(cell);
        }
    }

    private void DamagePlayersInLava()
    {
        Vector2I playerCell = LocalToMap(ToLocal(Player.GlobalPosition));

        // Check if the player is on a lava tile
        if (GetCellSourceId(playerCell) != -1)
        {
            if (Player is PlayerCharacter playerCharacter)
            {
                // Check if the player has IceShoes
                if (playerCharacter.IceShoes > 0)
                {
                    // Reduce IceShoes count
                    playerCharacter.IceShoes -= 1;

                    // Remove the lava tile at the player's position
                    RemoveLavaTile(playerCell);
                }
                else
                {
                    // If the player doesn't have IceShoes, apply damage
                    playerCharacter.TakeDamage(Damage);
                }
            }
        }
    }

    // Method to remove a lava tile
    public void RemoveLavaTile(Vector2I tilePosition)
    {
        // Remove the tile at the specified position by setting it to an empty cell
        SetCell(tilePosition, 0, new Vector2I(-1, -1)); // -1, -1 indicates an empty cell in TileMap
    }

    // Method to set a random lava tile
    private void SetRandomLavaTile(Vector2I cellPosition)
    {
        // Randomize the x-coordinate within the range 0 to 5 (LavaTileIdMax.X)
        int randomX = random.Next(LavaTileId.X, LavaTileIdMax.X + 1);
        Vector2I randomLavaTileId = new Vector2I(randomX, LavaTileId.Y);

        // Set the cell with the randomized lava tile ID
        SetCell(cellPosition, 0, randomLavaTileId);
    }
}