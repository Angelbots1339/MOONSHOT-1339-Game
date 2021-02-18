using Godot;
using System;
using System.Collections.Generic;



/*

This is the code for the Food Submition Stations

*/
public class FoodSubmitStation : Area2D
{
	

	
	private List<FoodItem> inventory = new List<FoodItem>();
	private Area2D area;
	private Boolean Inbounds;
	private string HeldFoodName;


	private Player; 

	public override void _Ready() {
		area = (Area2D)GetTree().Root.GetNode("Node2D").GetNode("Player").GetNode("InteractCollision");

		player = (Player) area.GetParent();


		
	}

	///<summary>
	/// When a player interacts with the station, take the food and submit it
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
			
			
			FoodItem item = player.HeldItem;
			if(item != FoodItem.NONE) { // Don't grab item from empty inventory
				inventory.Add(item);
				GetParent().GetNode<Sprite>("Sprite").Texture = item.GetTexture();
				
				HeldFoodName = player.HeldItem.Name;
				SubmitFood();
				
				
				
				
			}
		}
	}

	///<summary>
	/// When food is submitted... 
	///</summary>
	private void SubmitFood() {
		GD.Print("Submitting Food");

		if (HeldFoodName == FoodItem.HAMBURGER.Name) { //Just add Else-if's for each food item that can get submitted

			GetTree().Root.GetNode("Node2D").GetNode<Player>("Player").playerScore.increaseScore(1);
			player.HeldItem = FoodItem.NONE;
		} else if (HeldFoodName == FoodItem.BUN.Name){

			GetTree().Root.GetNode("Node2D").GetNode<Player>("Player").playerScore.increaseScore(1);

			player.HeldItem = FoodItem.NONE;
		}


    
		
	}
}


