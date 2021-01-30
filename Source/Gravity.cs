using Godot;
using System;

/*

	This is the code for the character's Gravity

*/
public class Gravity : RigidBody2D
{
	[Export] public float gravityForce = 40;
	[Export] public float nearGravRange = 45;
	[Export] public Texture texture;

	public override void _Ready()
	{
		GetNode<Sprite>("Sprite").Texture = texture;
	}

	public float GetGravity()
	{
		return gravityForce;
	}

	public override void _Draw()
	{
		var color = new Color(0.5f,0.5f,0.5f,0.5f);
		DrawCircle(new Vector2(), gravityForce/Scale.x, color);
	}
}
