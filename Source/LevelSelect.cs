using Godot;
using System;

/*

	This is the code for the level select screen and changing levels

*/

public class LevelSelect : GridContainer
{
	[Export] public String[] levelPaths;

	public override void _Ready()
	{
		// Create the User Interface for selecting levels

		for(int i = 0; i < levelPaths.Length; i++)
		{
			MenuButton button = new MenuButton();
			button.LevelPath = levelPaths[i];
			button.Level = i + 1;
			button.Name = ("Level " + button.Level);
			button.Text = button.Name;
			button.RectMinSize = new Vector2(50.0f, 50.0f);
			this.AddChild(button, true);
			button.Connect("pressed", button, nameof(MenuButton.LevelSelect));
		}
	}

}

class MenuButton : Button
{
	public int Level{get; set;}
	public string LevelPath{get; set;}
	
	public void LevelSelect()
	{
		// The code that actually changes the level

		GD.Print("Level " + Level + " selected");
		GD.Print("Level path is " + LevelPath);
		GetTree().ChangeScene(LevelPath);
	}
}
