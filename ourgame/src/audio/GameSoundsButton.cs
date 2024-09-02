using Godot;
using System;

public partial class GameSoundsButton : TextureButton
{
	private Texture2D muteTexture = (Texture2D)GD.Load("res://asssets/game_muted.png");
    private Texture2D unmuteTexture = (Texture2D)GD.Load("res://asssets/game_unmuted.png");

    public override void _Ready()
    {
        UpdateButtonTexture(AudioServer.IsBusMute(AudioServer.GetBusIndex("GameSounds")));
    }

    public void _on_toggled(bool mute)
    {
        int musicBus = AudioServer.GetBusIndex("GameSounds");
        AudioServer.SetBusMute(musicBus, mute);
        UpdateButtonTexture(mute);
    }

    private void UpdateButtonTexture(bool isMuted)
    {
        TextureNormal = isMuted ? muteTexture : unmuteTexture;
    }
}
