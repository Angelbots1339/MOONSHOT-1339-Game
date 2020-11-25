using Godot;
using System;

public class Gravity : RigidBody2D
{
	[Export] public float gravityForce = 40;

	// Called when the node enters the scene tree for the first time.

	public float GetGravity()
	{
		return gravityForce;
	}

	public override void _Draw()
	{
		var color = new Color(0.5f,0.5f,0.5f,0.5f);
		DrawCircle(new Vector2(), gravityForce, color);
	}
}
