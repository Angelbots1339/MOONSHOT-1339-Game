using Godot;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;


public class Recipe {

    const string recipePath = @"Persistent/Recipes.txt";

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

    public string[] readRecipiesFromFile(string filePath) {
        string lines = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
        return lines.Split('\n');
    }

    public void printRecipies() {
        string[] file = readRecipiesFromFile(recipePath);
        foreach (string i in file) {
            GD.Print(i);
        }
    }

    public Recipe(string toParse) {
        InputFoodItems = new List<FoodItem>();
        parseString(toParse);
        convertToFood();
    }

    public Recipe(int line) {
        InputFoodItems = new List<FoodItem>();
        parseString(readRecipiesFromFile(recipePath)[line]);
        convertToFood();
    }
}
