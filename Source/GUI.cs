using Godot;
using System;

/*

This is the code for the User Interface

*/
public class GUI : Control
{
	[Export] string mainMenuPath;
	HBoxContainer menu;
	HBoxContainer hiddenMenu;
	//child node popup menu
	PopupPanel helpPanel;

	
	
	
	
	public override void _Ready()
	{
		menu = GetNode<HBoxContainer>("Menu");
		hiddenMenu = GetNode<HBoxContainer>("HiddenMenu");
		helpPanel = GetNode<PopupPanel>("HelpPanel");
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
	
	public void _on_Help_pressed() {
		helpPanel.Visible = !helpPanel.Visible;
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
	
	public void randomcustomermenu(){
		//popup men has popup menu
		// make new popup menu of hamburger with popup of bun and meat
		// have other options for adding tomato, lettuce, cheese (do this with random)
		// 3 popups to begin, when one is full add another, randomly generate how many show up
	}
}
