using Godot;
using System;

public class MainMenu : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public PopupPanel helpPanel;
	public override void _Ready()
	{
		helpPanel = GetNode<PopupPanel>("Panel/Help/HelpPanel");
	}

	public void _on_Quit_pressed()
	{
		GetTree().Quit();
	}

	public void _on_Help_pressed()
	{
		helpPanel.Visible = true;
	}

	public void _on_Exit_pressed()
	{
		helpPanel.Visible = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
