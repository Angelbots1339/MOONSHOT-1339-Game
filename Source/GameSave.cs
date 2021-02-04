using Godot;
using System;
class GameSave : Node{
  public void saveGame(){
    Player p = GetTree().Root.GetNode("Node2D").GetNode<Player>("Player");
    string posx = "PosX: "+p.Position.x;
    string posy = "PosY: "+p.Position.y;
    string rot = "Rot: "+p.Rotation;
    System.IO.File.WriteAllText(@"C:\Users\Public\OvercookedSaveData\save_test.txt", posx+"\n"+posy+"\n"+rot);
  }
}