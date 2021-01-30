using Godot;
using System;	
using System.Collections.Generic;	

/*

    This is the code for creating instances of Food Items

*/
public class FoodItem	
{	
    public static readonly FoodItem NONE = new FoodItem("None", "");	
    public static readonly FoodItem BUN   = new FoodItem("Bun", "assets/Food/bun2.png");	
    public static readonly FoodItem CHEESE   = new FoodItem("Cheese", "assets/Food/cheese2.png");	
    public static readonly FoodItem HAMBURGER    = new FoodItem("Hamburger", "assets/Food/hamburger2 copy.png");	
    public static readonly FoodItem LETTUCE = new FoodItem("Lettuce", "assets/Food/lettuce2.png");	
    public static readonly FoodItem PATTY  = new FoodItem("Patty", "assets/Food/patty2.png");	
    public static readonly FoodItem TOMATO  = new FoodItem("Tomato", "assets/Food/tomato2.png");	

    public static IEnumerable<FoodItem> Values	
    {	
        get	
        {	
            yield return NONE;	
            yield return BUN;	
            yield return CHEESE;	
            yield return HAMBURGER;	
            yield return LETTUCE;	
            yield return PATTY;	
            yield return TOMATO;	
        }	
    }	

    // Get the Food Item's Name and the file path to its texture
    public string Name   { get; private set; }	
    public string ImagePath   { get; private set; }	


    // Get the texture for the food item
    public Texture GetTexture()
    {
        return (Texture) GD.Load("res://" + ImagePath);
    }

    FoodItem(string name, string imagePath) => 	
        (Name, ImagePath) = (name, imagePath);	
} 	
