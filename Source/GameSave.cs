using Godot;
using System;

/*

This is the code for saving the game

*/
class GameSave : Node{
  public void saveGame_WINDOWS(){
    Player p = GetTree().Root.GetNode("Node2D").GetNode<Player>("Player");
    string posx = ""+p.Position.x;
    string posy = ""+p.Position.y;
    string rot = ""+p.Rotation;
    System.IO.File.WriteAllText("user://OvercookedSaveData.txt", posx+"\n"+posy+"\n"+rot);
  }

  public void loadGame_WINDOWS(){
    Player p = GetTree().Root.GetNode("Node2D").GetNode<Player>("Player");
    String loaded = System.IO.File.ReadAllText("user://OvercookedSaveData.txt");
    String[] args = loaded.Split("\n");
    float x = float.Parse(args[0]);
    float y = float.Parse(args[1]);
    p.Position = new Godot.Vector2(x, y);
    p.Rotation = float.Parse(args[2]);
  }
}