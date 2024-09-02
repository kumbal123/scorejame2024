using Godot;
using System;

public partial class Arrow : RigidBody2D
{
    public float MaxDistance = 600;
    public float Impulse = 800;
    public float Life = 1;
    public int Damage; // Define the damage value

    private Timer timer;
    private Vector2 originalPosition;
    private AudioStreamPlayer _arrowSound;
    private bool damaged = false;

    public override void _Ready()
{
    // Cache the reference to the AudioStreamPlayer node
    _arrowSound = GetNode<AudioStreamPlayer>("ArrowHit");

    // Connect the body_entered signal to the method
    Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
}
    public void setDamage(int dmg){
        Damage = dmg;
    }

    public void LaunchArrow(Vector2 direction)
    {
        originalPosition = this.Position;
        Rotation = direction.Angle(); // Rotate arrow to face the direction
        this.ApplyCentralImpulse(direction * Impulse); // Apply impulse to the arrow

        // Setup and start timer for the arrow's lifetime
        timer = new Timer();
        AddChild(timer);
        timer.WaitTime = Life;
        timer.OneShot = true;
        timer.Connect("timeout", new Callable(this, nameof(OnTimeToDie)));
        timer.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        // If the arrow dealt damage and the sound finished playing, remove the arrow
        if (damaged && !_arrowSound.Playing)
        {
            QueueFree();
        }
    }

    private void OnBodyEntered(Node2D body)
{
    if (body.IsInGroup("player"))
    {
        if (!damaged)
        {
            // Damage the player once
            PlayerCharacter player = (PlayerCharacter)body;
            player.TakeDamage(Damage);
            damaged = true;

            // Play the hit sound
            _arrowSound.Play();
        }
    }
}

    private void OnTimeToDie()
    {
        QueueFree();
    }
}