using Godot;
using System;

public class CookingStation : Area2D
{
    public void _on_Area2D_area_entered(Area2D area) {
        if(area.GetParent().Name == "Player")
        {
            Player player = (Player) area.GetParent();
            GD.Print("Player collided with Cooking Station");
            if(player.HeldItem != FoodItem.NONE) {
                GetParent().GetNode<Sprite>("Sprite").Texture = player.HeldItem.GetTexture();
                player.HeldItem = FoodItem.NONE;
            }
        }
    }

}
