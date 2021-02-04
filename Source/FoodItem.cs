using Godot;
using System;
using System.Collections.Generic;

/*

    This is the code for creating instances of Food Items

*/
public class FoodItem
{
    public static readonly FoodItem NONE = new FoodItem("None", "");
    public static readonly FoodItem BUN = new FoodItem("Bun", "res://assets/Hamburger/bun2.png");
    public static readonly FoodItem CHEESE = new FoodItem("Cheese", "res://assets/Hamburger/cheese2.png");
    public static readonly FoodItem HAMBURGER = new FoodItem("Hamburger", "res://assets/Hamburger/hamburger2 copy.png");
    public static readonly FoodItem LETTUCE = new FoodItem("Lettuce", "res://assets/Hamburger/lettuce2.png");
    public static readonly FoodItem PATTY = new FoodItem("Patty", "res://assets/Hamburger/patty2.png");
    public static readonly FoodItem TOMATO = new FoodItem("Tomato", "res://assets/Hamburger/tomato2.png");

    public static FoodItem[] FoodItems = {
            NONE,
            BUN,
            CHEESE,
            HAMBURGER,
            LETTUCE,
            PATTY,
            TOMATO
        };

    public static FoodItem getFromName(string name)
    {
        foreach (FoodItem food in FoodItem.FoodItems)
        {
            if (food.Name.Equals(name))
            {
                return food;
            }
        }
        return FoodItem.NONE;
    }

    // Get the Food Item's Name and the file path to its texture
    public string Name { get; private set; }
    public string ImagePath { get; private set; }


    // Get the texture for the food item
    public Texture GetTexture()
    {
        return (Texture)GD.Load(ImagePath);
    }

    FoodItem(string name, string imagePath) =>
        (Name, ImagePath) = (name, imagePath);
}
