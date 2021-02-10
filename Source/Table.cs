using Godot;
using System;

/*

This is the code for creating food Pickup Stations

*/
public class Table : Area2D
{
    Area2D latestArea;

    public string foodName = "None";
    public void _on_Area2D_area_entered(Area2D area) {
        latestArea = area;
    }
    public void _on_Area2D_area_exited(Area2D area) {
        latestArea = null;
    }

    public override void _PhysicsProcess(float delta){

        //Give the player food when they interact with the station

        if(latestArea != null && latestArea.GetParent().Name == "Player" && Input.IsActionJustPressed("interact"))
        {
            Player player = (Player) latestArea.GetParent();
            var temp = player.HeldItem;
            player.HeldItem = FoodItem.getFromName(foodName);
            foodName = temp.Name;
            ((Sprite) GetNode("PickupItem")).Texture = FoodItem.getFromName(foodName).GetTexture();
        }
    }
}