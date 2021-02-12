using Godot;
using System;

public class Task : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private TextureRect TaskTexture;
	private Label TaskName;
	private Panel SampleTask;
	
	private FoodItem Random;
	

	public override void _Ready()
	{
		TaskTexture = GetNode<Panel>("SampleTask").GetNode<TextureRect>("TaskTexture");
		TaskName = GetNode<Panel>("SampleTask").GetNode<Label>("TaskName");
		CreateNewTask();
	}
	
	
	//fooditem.texture = texture
	//fooditem.Name = lable
	
	public void CreateNewTask(){
		Random = FoodItem.NONE.GetRandomFoodItem();
		TaskTexture.Texture = Random.GetTexture();
		TaskName.Text = Random.Name;
	}
	// Called when the node enters the scene tree for the first time.
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
