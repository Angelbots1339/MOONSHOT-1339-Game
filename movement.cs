using Godot;
using System;

public class movement : RigidBody2D
{
	[Export] public int speed = 50;
	[Export] public float deceleration = 0.3f;

	public Vector2 force = new Vector2();

	public void GetInput()
	{
		force = new Vector2();

		if (Input.IsActionPressed("left"))
			force += new Vector2(-speed, 0).Rotated(Rotation);

		if (Input.IsActionPressed("right"))
			force += new Vector2(speed, 0).Rotated(Rotation);

		force -= LinearVelocity * deceleration;
	}

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		ApplyCentralImpulse(force);
	}
}
