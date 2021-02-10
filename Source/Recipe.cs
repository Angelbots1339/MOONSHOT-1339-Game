using Godot;
using System.Collections.Generic;

public class Recipe {

    char cookingStationDelimiter = ':';
    char foodIODelimiter = '=';
    char foodItemDelimiter = '+';

    public string CookingStationType {get; private set;} // Currently redundant
    public List<string> Inputs {get; private set;}
    public List<FoodItem> InputFoodItems {get; private set;}
    public string Output {get; private set;}
    public FoodItem OutputFoodItem {get; private set;}
    private void parseString(string toParse) {
        string[] splitForStation = toParse.Split(cookingStationDelimiter);
        CookingStationType = splitForStation[1]; // Station type is last in string
        string[] splitForFoodIO = splitForStation[0].Split(foodIODelimiter);
        Output = splitForFoodIO[1];
        string[] splitForFoodInputs = splitForFoodIO[0].Split(foodItemDelimiter);
        Inputs = new List<string>(splitForFoodInputs);
    }

    private void convertToFood() {
        OutputFoodItem = FoodItem.getFromName(Output);
        GD.Print("Output: " + OutputFoodItem.Name);
        foreach(string i in Inputs) {
            InputFoodItems.Add(FoodItem.getFromName(i));
            GD.Print("Input: " + FoodItem.getFromName(i).Name);
        }
    }

    public Recipe(string toParse) {
        InputFoodItems = new List<FoodItem>();
        parseString(toParse);
        convertToFood();
    }
}