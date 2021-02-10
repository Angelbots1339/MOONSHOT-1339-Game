using Godot;
using System;

/*

This is the code for creating food Pickup Stations

*/
public class PickupStation : Area2D
{
    Area2D latestArea;

    [Export] public string foodName;
    public void _on_Area2D_area_entered(Area2D area) {
        latestArea = area;
    }
    public void _on_Area2D_area_exited(Area2D area) {
        latestArea = null;
    }

    public override void _Ready() {
        ((Sprite) GetNode("PickupItem")).Texture = FoodItem.getFromName(foodName).GetTexture();
    }

    public override void _PhysicsProcess(float delta){

        //Give the player food when they interact with the station

        if(latestArea != null && latestArea.GetParent().Name == "Player" && Input.IsActionPressed("interact"))
        {
            Player player = (Player) latestArea.GetParent();
            if(player.HeldItem == FoodItem.NONE) player.HeldItem = FoodItem.getFromName(foodName);
        }
    }
}