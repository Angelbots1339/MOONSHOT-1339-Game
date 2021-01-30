using Godot;
using System;

/*

	This is the code for the Main Menu

*/
public class MainMenu : Control
{

	// Create the Menu User Interface
	public PopupPanel helpPanel;
	public override void _Ready()
	{
		helpPanel = GetNode<PopupPanel>("Panel/HelpPanel");
	}

	// Code for handeling when the Quit button is pressed
	public void _on_Quit_pressed()
	{
		GetTree().Quit();
	}

	// Code for handling when the Help button is pressed
	public void _on_Help_pressed()
	{
		helpPanel.Visible = true;
	}

	//C ode for handling when the Exit button is pressed
	public void _on_Exit_pressed()
	{
		helpPanel.Visible = false;
	}
}
