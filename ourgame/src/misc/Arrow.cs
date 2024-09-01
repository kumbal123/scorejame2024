using Godot;
using System;

public partial class Arrow : RigidBody2D
{
    public float MaxDistance = 600;
    public float Impulse = 800;
    public float Life = 1;
    public int Damage = 10; // Define the damage value

    private Timer timer;
    private Vector2 originalPosition;
    private AudioStreamPlayer _arrowSound;
    private bool _soundPlayed = false;

    public void LaunchArrow(Vector2 direction)
    {
        originalPosition = this.Position;
        Rotation = direction.Angle(); // Rotate arrow to face the direction
        this.ApplyCentralImpulse(direction * Impulse); // Apply impulse to the arrow

        // Setup and start timer for the arrow's lifetime
        timer = new Timer();
        this.AddChild(timer);
        timer.WaitTime = this.Life;
        timer.OneShot = true;
        timer.Connect("timeout", new Callable(this, nameof(OnTimeToDie)));
        timer.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        // Check if the sound has finished playing
        if (_soundPlayed && !_arrowSound.Playing)
        {
            OnSoundFinished();
            _soundPlayed = false; // Prevent multiple calls
        }
    }


    private void BodyEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            PlayerCharacter player = (PlayerCharacter)body;
            _arrowSound = GetNode<AudioStreamPlayer>("ArrowHit");
            _arrowSound.Play();
            player.TakeDamage(Damage);
        }
    }
    private void OnSoundFinished()
    {
        GD.Print("Sound has finished playing");
        QueueFree();
    }

    private void OnTimeToDie()
    {
        QueueFree(); // Destroy the arrow after its lifetime ends
    }
}