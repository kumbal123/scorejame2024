using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class SoundPool : Node
{
	private List<SoundQueue> _sounds = new List<SoundQueue>();
	private RandomNumberGenerator _random = new RandomNumberGenerator();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var child in GetChildren()){
			if (child is SoundQueue soundQueue){
				_sounds.Add(soundQueue);
			}
		}
	}

	public override string[] _GetConfigurationWarnings(){
		int numberOfSoundQueueChildren = 0;
		foreach(var child in GetChildren()){
			if (child is SoundQueue soundQueue){
				numberOfSoundQueueChildren++;
			}
		}
		if (numberOfSoundQueueChildren < 2){
			return new string[]{"Expected 2 or more soundqueue"};
		}
		return base._GetConfigurationWarnings();
	}
	public void PlayRandomSound(){
	
		int index = _random.RandiRange(0, _sounds.Count - 1);
		_sounds[index].PlaySound();
	}
}
