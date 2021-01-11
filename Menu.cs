using Godot;
using System;

public class Menu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void _OnPausePressed()
    {
        GD.Print("Pause");
        GetTree().Paused = !GetTree().Paused;
    }

    public void _OnRestartPressed()
    {
        GD.Print("Restart");
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
