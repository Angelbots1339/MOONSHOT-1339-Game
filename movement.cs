using Godot;
using System;

public class movement : RigidBody2D
{
    [Export] public int speed = 25;
    [Export] public float deceleration = 0.01f;

    public Vector2 force = new Vector2();

    public void GetInput()
    {
        if (Input.IsActionPressed("left"))
            force += new Vector2(-speed, 0).Rotated(Rotation);

        if (Input.IsActionPressed("right"))
            force += new Vector2(speed, 0).Rotated(Rotation);

        if (Input.IsActionJustPressed("jump"))
            force += new Vector2(0, -700).Rotated(Rotation);
    }

    public void MoveToGrav()
    {
        Node planets = GetParent().GetNode("planets");
        foreach (Gravity planet in planets.GetChildren())
        {
			var outsideness = (planet.GlobalPosition - this.GlobalPosition).Length()/planet.gravityForce;
            var distanceVector = planet.GlobalPosition - this.GlobalPosition;
            if(distanceVector.Length() <= planet.gravityForce) force += distanceVector.Normalized() * (10+10/outsideness);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        force = new Vector2();
        MoveToGrav();
        if(force.Length() > 0.01){
            var error = force.Angle() - (float)Math.PI / 2 - Rotation;
            if(error > Math.PI) error = 2 * (float) Math.PI - error;
            if(error < -Math.PI) error = 2 * (float) Math.PI + error;
            Rotation += 0.25f * error;
        }
        GetInput();
        ApplyCentralImpulse(force);
    }
}
