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
	Format: "input1+input2=output:cookingtype"
	*/
	public string[] inputRecipies = {};
	public Recipe[] recipies;
	private List<FoodItem> inventory = new List<FoodItem>();
	private Area2D playerArea;
    private PopupDialog popup;
	private Boolean Inbounds;
	public override void _Ready() {
		playerArea = (Area2D)GetTree().Root.GetNode("Node2D").GetNode("Player").GetNode("InteractCollision");
        popup = GetNode<CanvasLayer>("CanvasLayer").GetNode<PopupDialog>("PopupDialog");
		recipies = new Recipe[inputRecipies.Length];
		for (int i = 0; i < inputRecipies.Length; i++ ){
			recipies[i] = new Recipe(inputRecipies[i]);
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

			// Fill placeholders 
			List<FoodItem> placeholders = new List<FoodItem>();
			foreach(FoodItem input in inputs) {
				placeholders.Add(input);
			}
			List<FoodItem> tempInventory = inventory;	

			// Check if temp inventory can create recipe
			bool complete = true;
			for(int recipeIndex = 0; recipeIndex < placeholders.Count && complete; recipeIndex++) {
				if(tempInventory.Contains(placeholders[recipeIndex])) {
					tempInventory.Remove(placeholders[recipeIndex]);
				} else {
					complete = false;
				}
			}

			if(complete) {
				inventory = tempInventory;
                Player player = (Player) playerArea.GetParent();
                player.HeldItem = output;
                Inbounds = false;
			}
		}
	}
}