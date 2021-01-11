using Godot;
using System;

public class GUI : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    HBoxContainer menu;
    HBoxContainer hiddenMenu;
    public override void _Ready()
    {
        menu = GetNode<HBoxContainer>("Menu");
        hiddenMenu = GetNode<HBoxContainer>("HiddenMenu");
        ShowMenu(true);
    }

    private void ShowMenu(Boolean visible) {
        menu.Visible = visible;
        hiddenMenu.Visible = !visible;
    }

    public void _on_Pause_pressed()
    {
        GD.Print("Pause");
        ShowMenu(false);
        GetTree().Paused = true;
    }

    public void _on_Play_pressed()
    {
        GD.Print("Play");
        ShowMenu(true);
        GetTree().Paused = false;
    }
    public void _on_Main_Menu_pressed()
    {
        GD.Print("Main Menu");
    }

    public void _on_Restart_pressed()
    {
        GD.Print("Restart");
        _on_Play_pressed();
        GetTree().ReloadCurrentScene();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
