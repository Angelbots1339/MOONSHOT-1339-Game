using Godot;
using System;

public class TrashStation : Area2D
{
    public void _on_Area2D_area_entered(Area2D area) {
        if(area.GetParent().Name == "Player" && Input.IsActionPressed("interact"))
        {
            Player player = (Player) area.GetParent();
            player.HeldItem = FoodItem.NONE;
        }
    }

}
