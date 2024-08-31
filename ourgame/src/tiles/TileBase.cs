using Godot;
using System;

/// <summary>
/// A base class for map tiles with special effects.
/// </summary>
public partial class TileBase : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public override void _Process(double delta)
    {
    }

    /// <summary>
    /// Called when something enters the tile, be it player or enemy.
    /// </summary>
    /// <param name="body">A player or enemy body stepping into the tile.</param>
    public virtual void BodyEnteredTile(Node2D body) {}

	/// <summary>
	/// Called when something exits the tile, be it player or enemy.
	/// </summary>
	/// <param name="body">A player or enemy body stepping into the tile.</param>
	public virtual void BodyExitedTile(Node2D body) {}
}
