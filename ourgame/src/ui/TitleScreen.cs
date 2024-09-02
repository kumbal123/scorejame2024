using Godot;
using System;

public partial class TitleScreen : Control
{
    public void _on_start_button_pressed()
    {
        GetTree().ChangeSceneToFile("res://MainScene.tscn");
    }
	public void _on_quit_button_pressed()
    {
        GetTree().Quit();
    }
}