using Godot;
using System;

/*

This is the code for the Trash Stations

*/
public class TrashStation : Area2D
{
	Area2D latestArea;
	public void _on_Area2D_area_entered(Area2D area) {
		latestArea = area;
	}
	public void _on_Area2D_area_exited(Area2D area) {
		latestArea = null;
	}

	// Remove held food item when the player interacts with the trash
	public override void _PhysicsProcess(float delta){
		if(latestArea != null && latestArea.GetParent().Name == "Player" && Input.IsActionPressed("interact"))
		{
			Player player = (Player) latestArea.GetParent();
			player.HeldItem = FoodItem.NONE;
		}
	}
}
