using Godot;
using System;

public class GUI : CanvasLayer
{
	[Export] string mainMenuPath;
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
		ShowMenu(false);
		GetTree().Paused = true;
	}

	public void _on_Play_pressed()
	{
		ShowMenu(true);
		GetTree().Paused = false;
	}
	public void _on_Main_Menu_pressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeScene(mainMenuPath);
	}

	public void _on_Restart_pressed()
	{
		_on_Play_pressed();
		GetTree().ReloadCurrentScene();
	}
}
