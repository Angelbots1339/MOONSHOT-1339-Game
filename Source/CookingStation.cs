using Godot;
using System;

public class CookingStation : Area2D
{
    public void _on_Area2D_area_entered(Area2D area) {
        if(area.GetParent().Name == "Player")
        {
            GD.Print("Player collided with Cooking Station");
            //Put code for collision with player here
        }
    }

}
