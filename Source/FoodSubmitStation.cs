using Godot;
using System;
using System.Collections.Generic;

/*

This is the code for the Food Submition Stations

*/
public class FoodSubmitStation : Area2D
{
	[Export]
	public string[] inputs = {}; 
	[Export]
	public string output;
	private List<FoodItem> inventory = new List<FoodItem>();
	public Area2D area;
	public Boolean Inbounds;
	public override void _Ready() {
		area = (Area2D)GetTree().Root.GetNode("Node2D").GetNode("Player").GetNode("InteractCollision");
	}

	///<summary>
	/// When a player interacts with the station, take the food and submit it
	///</summary>
	public void _on_Area2D_area_entered(Area2D area)
	{
		Inbounds = true;
		GD.Print("Player collided with Food Submition Station");

	}
	private void _on_Area2D_area_exited(Area2D area)
{
	Inbounds = false;
		GD.Print("Player left Food Submition Station");
}
	
	public override void _PhysicsProcess(float delta){
		if (Input.IsActionPressed("interact") && Inbounds)
		{
			Player player = (Player) area.GetParent();
			GD.Print("Player collided with Food Submition Station");
			FoodItem item = player.HeldItem;
			if(item != FoodItem.NONE) { // Don't grab item from empty inventory
				inventory.Add(item);
				GetParent().GetNode<Sprite>("Sprite").Texture = item.GetTexture();
				player.HeldItem = FoodItem.NONE;
				SubmitFood();
			}
		}
	}

	///<summary>
	/// When food is submitted... 
	///</summary>
	private void SubmitFood() {
		GD.Print("Submitting Food");


    //Code for food submition goes here
		
	}
}


