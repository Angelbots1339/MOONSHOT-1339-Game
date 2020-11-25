using Godot;
using System;

public class movement : RigidBody2D //extends rigidbody2D (instead of extends it's :)
{
    [Export] public int speed = 50; //export means it is seen in the godot editor
    //you can edit these variables in the godot editor
    [Export] public float deceleration = 0.01f;

    public Vector2 force = new Vector2(); //this variable can only be accessed in VS

    public void GetInput()
    {
        if (Input.IsActionPressed("left")) //setting up the input
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
            var distanceVector = planet.GlobalPosition - this.GlobalPosition; 
			var outsideness = (distanceVector).Length()/planet.gravityForce; //length of how far player/movementscript is from planet
            if(distanceVector.Length() <= planet.gravityForce) force += distanceVector.Normalized() * (10+10/outsideness);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        force = new Vector2();
        MoveToGrav();
        if(force.Length() > 0.01){
            var error = force.Angle() - (float)Math.PI / 2 - Rotation; //makes it so the player always has it's bottom facing the planet
            if(error > Math.PI) error = 2 * (float) Math.PI - error;
            if(error < -Math.PI) error = 2 * (float) Math.PI + error;
            Rotation += 0.25f * error;
        }
        GetInput();
        ApplyCentralImpulse(force);
    }
}
