using Godot;
using System;
using System.Collections.Generic;


/*

This is the code for the Stations where you can store Food. 

*/
public class FoodRestingStation : Area2D
{
	public Score playerScore = new Score();
	[Export]
	public string[] inputs = {}; 
	[Export]
	public string output;
	private List<FoodItem> inventory = new List<FoodItem>();
	public Area2D area;
	public Boolean Inbounds;
	public string HeldFoodName;
	public override void _Ready() {
		area = (Area2D)GetTree().Root.GetNode("Node2D").GetNode("Player").GetNode("InteractCollision");


		
	}

	///<summary>
	/// When a player interacts with the station, take the food and set it down
	///</summary>
	public void _on_Area2D_area_entered(Area2D area)
	{
		Inbounds = true;
		

	}
	private void _on_Area2D_area_exited(Area2D area)
{
	Inbounds = false;
		
}
	
	public override void _PhysicsProcess(float delta){
		if (Input.IsActionPressed("interact") && Inbounds)
		{
			Player player = (Player) area.GetParent();
			
			FoodItem item = player.HeldItem;
			if(item != FoodItem.NONE) { // Don't grab item from empty inventory
				inventory.Add(item);
				GetParent().GetNode<Sprite>("Sprite").Texture = item.GetTexture();
				
				HeldFoodName = player.HeldItem.Name;
				RestFood();
				
				
				
				player.HeldItem = FoodItem.NONE;
			}
		}
	}

	///<summary>
	/// When food is submitted... 
	///</summary>
	private void RestFood() {
		GD.Print("Resting Food");


    
		
	}
}


