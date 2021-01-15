using Godot;
using System;

public class MainMenu : Control
{
	public PopupPanel helpPanel;
	public override void _Ready()
	{
		helpPanel = GetNode<PopupPanel>("Panel/HelpPanel");
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
}
