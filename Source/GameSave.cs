using Godot;
using System;

/*

This is the code for saving the game

only works for position and rotation right now

*/
class GameSave : Node{
  public void saveGame(){
    Player p = GetTree().Root.GetNode("Node2D").GetNode<Player>("Player");
    float[] args = {p.Position.x, p.Position.y, p.Rotation, p.playerScore.getScore()};//add to that list: Godot.currentLevel() like function to keep track of progress
    //string currentLvl = Godot.getCurrentLevel();
    System.IO.File.WriteAllText("user://OvercookedSaveData.txt", String.Join("\n", args));//+"\n"+currentLvl
  }

  public void loadGame(){
    Player p = GetTree().Root.GetNode("Node2D").GetNode<Player>("Player");
    String loaded = System.IO.File.ReadAllText("user://OvercookedSaveData.txt");
    string[] args = loaded.Split("\n");

    float x = float.Parse(args[0]);
    float y = float.Parse(args[1]);
    p.Position = new Godot.Vector2(x, y);
    p.Rotation = float.Parse(args[2]);
    p.playerScore.setScore(int.Parse(args[3]));
    //Godot.setCurrentLevel(int.Parse(args[3]));
  }
}