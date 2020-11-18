using Godot;
using System;

public class movement : RigidBody2D
{
    [Export] public int speed = 200;

    public Vector2 velocity = new Vector2();

    public void GetInput()
    {
        LookAt(GetGlobalMousePosition());
        velocity = new Vector2();

        if (Input.IsActionPressed("left"))
            velocity = new Vector2(-speed, 0).Rotated(Rotation);

        if (Input.IsActionPressed("right"))
            velocity = new Vector2(speed, 0).Rotated(Rotation);

        velocity = velocity.Normalized() * speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        velocity = MoveAndSlide(velocity);
    }
}
