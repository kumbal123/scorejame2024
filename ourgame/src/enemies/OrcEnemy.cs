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
        CanvasLayer stopwatch = GetNode<CanvasLayer>("/root/Stopwatch");
        stopwatch.Call("add_score_for_kill", KillScoreReward);

		DisableEnemy();
		
		_deathSound.Play();
		animatedSprite.Play("death");

		var timeoutTask = Task.Delay(1000);
		await Task.WhenAll(timeoutTask);
		
		QueueFree();
	}
}
