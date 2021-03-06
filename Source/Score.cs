using Godot;
using System;

/*

This is the code for the Score

*/
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

        // Create Score on HUD
        scoreLabel = new Label();
        scoreLabel.Show();
        scoreLabel.Text = "Score: 0";

        myscore = 0;
        scoreLabel.Set("Score: 0", scoreLabel);
    }

    //Get the score
    public int getScore()
    {
        return myscore;
    }
    //Set the score to the specified value
    public void setScore(int x)
    {
        myscore = x;
        
    }
    //Increase the score by the specified value
    public void increaseScore(int x)
    {
        myscore += x;
    }
    //Decrease the score by the specified value
    public void decreaseScore(int x)
    {
        myscore -= x;
    }
    //Update the HUD with the latest score
    public void updateScore()
    {
        scoreLabel.Text = "Score: " + myscore;
    }
    //Turn the score into a string for the HUD
    public String toString()
    {
        int score = this.getScore();
        String scoreString = score.ToString();
        return scoreString;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
