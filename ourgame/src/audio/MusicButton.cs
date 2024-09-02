using Godot;
using System;

public partial class MusicButton : TextureButton
{
    private Texture2D muteTexture = (Texture2D)GD.Load("res://asssets/music_muted.png");
    private Texture2D unmuteTexture = (Texture2D)GD.Load("res://asssets/music_unmuted.png");

    public override void _Ready()
    {
        UpdateButtonTexture(AudioServer.IsBusMute(AudioServer.GetBusIndex("Music")));
    }

    public void _on_toggled(bool mute)
    {
        int musicBus = AudioServer.GetBusIndex("Music");
        AudioServer.SetBusMute(musicBus, mute);
        UpdateButtonTexture(mute);
    }

    private void UpdateButtonTexture(bool isMuted)
    {
        TextureNormal = isMuted ? muteTexture : unmuteTexture;
    }

}
