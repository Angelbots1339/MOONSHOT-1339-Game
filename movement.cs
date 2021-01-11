using Godot;
using System;

//test change
public class movement : RigidBody2D //extends rigidbody2D (instead of extends it's :)
{
    [Export] public int speed = 25;
    [Export] public float deceleration = 0.01f;

    public Vector2 force = new Vector2(); //this variable can only be accessed in VS
    private Node2D planets;
    private Gravity[] planetArray;

    public override void _Ready()
    {
        InitializePlanets();
    }

    private void InitializePlanets()
    {
        planets = GetParent().GetNode<Node2D>("planets");
        planetArray = new Gravity[planets.GetChildCount()];
        for(int i = 0; i < planetArray.Length; i++)
        {
            planetArray[i] = (Gravity)planets.GetChild(i);
        }
    }

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
        Node2D noMove = (Node2D) GetNode("mouse2");
        Node2D walking = (Node2D) GetNode("Mouse Walking");
        Node2D flying = (Node2D) GetNode("Mouse Flying");
        noMove.Visible = false;
        walking.Visible = false;
        flying.Visible = false;
        var nearbyPlanets = 0;
        foreach (Gravity planet in planetArray)
        {
            var distanceVector = planet.GlobalPosition - this.GlobalPosition; 
			var outsideness = (distanceVector).Length()/planet.gravityForce; //length of how far player/movementscript is from planet
            if(distanceVector.Length() <= planet.gravityForce) {
                force += distanceVector.Normalized() * (10+10/outsideness);
                nearbyPlanets++;
            }
        }
        if(nearbyPlanets > 0) walking.Visible = true;
        else flying.Visible = true; 
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
