using Godot;
using System;
using System.Collections.Generic;

/*

This is the code for the Cooking Stations

*/
public class CookingStation : Area2D
{
	[Export]
	/*
	Use the desired output food item name to find the correct recipe
	*/
	public string[] outputs = {};
	public Recipe[] recipies;
	private List<FoodItem> inventory = new List<FoodItem>();
	private Area2D playerArea;
    private PopupDialog popup;
	private Boolean Inbounds;
	public override void _Ready() {
		playerArea = (Area2D)GetTree().Root.GetNode("Node2D").GetNode("Player").GetNode("InteractCollision");
        popup = GetNode<CanvasLayer>("CanvasLayer").GetNode<PopupDialog>("PopupDialog");
		recipies = new Recipe[outputs.Length];
		for (int i = 0; i < outputs.Length; i++ ){
			recipies[i] = Recipe.searchFileForRecipe(outputs[i]);
		}
	}

	///<summary>
	///If player enters, grab their food and add it to this cooking station's inventory.
	///</summary>
	public void _on_Area2D_area_entered(Area2D area)
	{
		if(area.Equals(playerArea)) {
            Inbounds = true;
            popup.RectGlobalPosition = this.GlobalPosition;
            popup.Visible = true;
        }
	}
	private void _on_Area2D_area_exited(Area2D area)
	{
		if(area.Equals(playerArea)) {
            Inbounds = false;
            popup.Visible = false;
        }
	}
	
	public override void _PhysicsProcess(float delta){
		if (Input.IsActionPressed("interact") && Inbounds)
		{
			Player player = (Player) playerArea.GetParent();
			FoodItem item = player.HeldItem;
			if(item != FoodItem.NONE) { // Don't grab item from empty inventory
				inventory.Add(item);
				player.HeldItem = FoodItem.NONE;
				CheckOutput();	

                // Update inventory
                string text = "";
                foreach(FoodItem i in inventory) text += i.Name + '\n';
                popup.GetNode<Label>("Label").Text = text;
			}
		}
	}

	///<summary>
	///If inventory has all the ingredients for an output, make it
	///</summary>
	private void CheckOutput() {
		for(int i = 0; i < recipies.Length; i++) {
			Recipe currentRecipe = recipies[i];
			List<FoodItem> inputs = currentRecipe.InputFoodItems;
			FoodItem output = currentRecipe.OutputFoodItem;

			// If inventory is smaller than the recipe, we can check the next one
			if(inventory.Count < inputs.Count) {
				continue;
			}

			List<FoodItem> tempInventory = inventory;	

			// Check if temp inventory can create recipe
			bool complete = true;
			for(int recipeIndex = 0; recipeIndex < inputs.Count && complete; recipeIndex++) {
				if(tempInventory.Contains(inputs[recipeIndex])) {
					tempInventory.Remove(inputs[recipeIndex]);
				} else {
					complete = false;
				}
			}

			if(complete) {
				inventory = tempInventory;
                Player player = (Player) playerArea.GetParent();
                player.HeldItem = output;
                Inbounds = false; // Player HAS TO step out before next recipe can be created
                GD.Print("Gave player " + output.Name);
			}
		}
	}
}