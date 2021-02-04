using Godot;
using System;

public class Score : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private int myscore;
    private Label scoreLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        scoreLabel = new Label();
        scoreLabel.Show();
        scoreLabel.Text = "Score: 0";

        myscore = 0;
        scoreLabel.Set("Score: 0", scoreLabel);
    }

    public int getScore()
    {
        return myscore;
    }

    public void setScore(int x)
    {
        myscore = x;
        
    }

    public void increaseScore()
    {
        myscore++;
    }

    public void decreaseScore()
    {
        myscore--;
    }

    public void updateScore()
    {
        scoreLabel.Text = "Score: " + myscore;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}