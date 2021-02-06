using Godot;
using System;
using System.Collections.Generic;

/*

This is the code for the Cooking Stations

*/
public class CookingStation : Area2D
{
	[Export]
	public string[] inputs = {}; // Can only have one recipe because Godot has a fit with 2D arrays
	[Export]
	public string output;
	private List<FoodItem> inventory = new List<FoodItem>();
	public Area2D area;
	public Boolean Inbounds;
	public override void _Ready() {
		area = (Area2D)GetTree().Root.GetNode("Node2D").GetNode("Player").GetNode("InteractCollision");
	}

	///<summary>
	///If player enters, grab their food and add it to this cooking station's inventory.
	///</summary>
	public void _on_Area2D_area_entered(Area2D area)
	{
		Inbounds = true;
		GD.Print("Player collided with Cooking Station");

	}
	private void _on_Area2D_area_exited(Area2D area)
{
	Inbounds = false;
		GD.Print("Player left Cooking Station");
}
	
	public override void _PhysicsProcess(float delta){
		if (Input.IsActionPressed("interact") && Inbounds)
		{
			Player player = (Player) area.GetParent();
			GD.Print("Player collided with Cooking Station");
			FoodItem item = player.HeldItem;
			if(item != FoodItem.NONE) { // Don't grab item from empty inventory
				inventory.Add(item);
				GetParent().GetNode<Sprite>("Sprite").Texture = item.GetTexture();
				player.HeldItem = FoodItem.NONE;
				CheckOutput();
			}
		}
	}

	///<summary>
	///If inventory has all the ingredients for an output, make it (Unfinished)
	///</summary>
	private void CheckOutput() {
		GD.Print("Checking output");

		// If inventory is smaller than a recipe, we can check the next one
		// TODO: This is broken because pool string array has a set size of 4325453
		if(false && inventory.Count < inputs.Length) {
			GD.Print("Station inventory too small with " + inventory.Count + " items filling " + inputs.Length + " slots");
		}

		GD.Print("Filling Placeholders");
		// Fill placeholders 
		List<FoodItem> placeholders = new List<FoodItem>();
		foreach(string input in inputs) {
			placeholders.Add(FoodItem.getFromName(input));
		}
		GD.Print("Duplicating inventory");
		List<FoodItem> tempInventory = inventory;
		/*for(int j = 0; j < inventory.Count; j++) {
			tempInventory[j] = inventory[j].Name;
		}*/
		GD.Print("Placeholders created");

		// Check if temp inventory can create recipe
		bool complete = true;
		for(int recipeIndex = 0; recipeIndex < placeholders.Count && complete; recipeIndex++) {
			GD.Print("Checking recipe index " + recipeIndex);
			if(tempInventory.Contains(placeholders[recipeIndex])) {
				GD.Print("Station inventory meets item " + tempInventory[recipeIndex].Name);
				tempInventory.Remove(placeholders[recipeIndex]);
			} else {
				GD.Print("Station inventory does not contain item " + tempInventory[recipeIndex].Name);
				complete = false;
			}
		}

		GD.Print("Complete? " + complete);
		if(complete) {
			GD.Print("Recipe met, syncing cooking station inventory");
			inventory = tempInventory;
			GD.Print("Creating Food Item " + output);
		}
	}
}


