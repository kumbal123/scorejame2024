using Godot;
using System;

public partial class OrcEnemy : EnemyBase
{
    private SoundPool _swordSound;
    private SoundQueue _deathSound;
    public override void loadSounds()
	{
		_swordSound = GetNode<SoundPool>("SwordSound");
        _deathSound = GetNode<SoundQueue>("DeathSound");
	}
    public override void playAttackSound() {
		_swordSound.PlayRandomSound();
	}
    public override void playDeathSound() {
		_deathSound.PlaySound();
	}
}
