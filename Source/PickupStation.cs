using Godot;
using System;

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

    public override void _PhysicsProcess(float delta){
        if(latestArea != null && latestArea.GetParent().Name == "Player" && Input.IsActionPressed("interact"))
        {
            Player player = (Player) latestArea.GetParent();
            if(player.HeldItem == FoodItem.NONE) player.HeldItem = FoodItem.getFromName(foodName);
        }
    }
}