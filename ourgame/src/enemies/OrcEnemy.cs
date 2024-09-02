using Godot;
using System;
using System.Threading.Tasks;

public partial class OrcEnemy : EnemyBase
{
    private SoundPool _swordSound;
    private AudioStreamPlayer _deathSound;
    public override void loadSounds()
	{
		_swordSound = GetNode<SoundPool>("SwordSound");
        _deathSound = GetNode<AudioStreamPlayer>("DeathSound");
	}
    public override void playAttackSound() {
		_swordSound.PlayRandomSound();
	}

	public override async void Deaded()
	{
        CanvasLayer stopwatch = GetTree().Root.GetChild(1).GetNode<CanvasLayer>("stopwatch");
        stopwatch.Call("AddScoreForKill", KillScoreReward);

		DisableEnemy();
		
		_deathSound.Play();
		animatedSprite.Play("death");

		var timeoutTask = Task.Delay(1000);
		await Task.WhenAll(timeoutTask);
		
		QueueFree();
	}
}
