using Godot;
using System;

/*

	This is the code for handeling the Player's Movement

*/
public class Movement : RigidBody2D //extends rigidbody2D (instead of extends it's :)
{
	

	[Export] public int speed = 25;
	[Export] public float deceleration = 0.01f;

	public Vector2 force{get;set;} //this variable can only be accessed in VS
	private Node2D planets;
	private Gravity[] planetArray;
	public double nearbyPlanets{get;set;}
	public double collidePlanets{get;set;}
	
	public override void _Ready()
	{
		force = new Vector2();
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

	

	public void MoveToGrav()
	{
		nearbyPlanets = 0;
		collidePlanets = 0;
		
		foreach (Gravity planet in planetArray)
		{
			var distanceVector = planet.GlobalPosition - this.GlobalPosition; 
			var outsideness = (distanceVector).Length()/planet.gravityForce; //length of how far player/movementscript is from planet
			if(distanceVector.Length() <= planet.gravityForce) {
				force += distanceVector.Normalized() * (10+10/outsideness);
				nearbyPlanets++;
			}
			if(distanceVector.Length() <= planet.nearGravRange) {
				collidePlanets++;
			}
		}
	}
	
	public void FacePlanet()
	{
		if(force.Length() > 0.01){
			var error = force.Angle() - (float)Math.PI / 2 - Rotation; //makes it so the player always has it's bottom facing the planet
			if(error > Math.PI) error = 2 * (float) Math.PI - error;
			if(error < -Math.PI) error = 2 * (float) Math.PI + error;
			Rotation += 0.25f * error;
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		MoveToGrav();
		FacePlanet();
		ApplyCentralImpulse(force);
		force = new Vector2();
	}
}
