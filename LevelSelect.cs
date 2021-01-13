using Godot;
using System;

public class LevelSelect : GridContainer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	[Export] public String[] levelPaths;

	public override void _Ready()
	{
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

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

class MenuButton : Button
{
	public int Level{get; set;}
	public string LevelPath{get; set;}
	
	public void LevelSelect()
	{
		GD.Print("Level " + Level + " selected");
		GD.Print("Level path is " + LevelPath);
		GetTree().ChangeScene(LevelPath);
	}
}
