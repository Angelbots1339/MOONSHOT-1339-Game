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
	
	private Node2D noMove ;
	private AnimatedSprite walkingLeft;
	private AnimatedSprite walkingRight;
	private AnimatedSprite flying;
	
	private double nearbyPlanets;
	private double collidePlanets;
	private String direction = "none"; // TODO can someone make this an ENUM?
	private Boolean disableLevelReset;
	
	public override void _Ready()
	{
		disableLevelReset = false;
		noMove = (Node2D) GetNode("mouse2");
		walkingLeft = (AnimatedSprite) GetNode("Mouse Walking Left");
		walkingRight = (AnimatedSprite) GetNode("Mouse Walking Right");
		flying = (AnimatedSprite) GetNode("Mouse Flying");
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
		var colliding = GetCollidingBodies();
		var doAnimation = true;
		if(colliding.Count > 0){
			doAnimation = false;
			direction = "none"; // IDK if this is what we want for final verson, but I think it was broken so mouse no longer plays animations if no buttons are pressed
			if(Input.IsActionPressed("left") && Input.IsActionPressed("right")){
				doAnimation = false;
				direction = "none";
			}

			else if (Input.IsActionPressed("left")){
				force += new Vector2(-speed, 0).Rotated(Rotation);
				doAnimation = true;
				direction = "left";
			}

			else if (Input.IsActionPressed("right")){
				force += new Vector2(speed, 0).Rotated(Rotation);
				doAnimation = true;
				direction = "right";
			}
		} else if(collidePlanets == 0)
			direction = "none";

		if (Input.IsActionJustPressed("jump") && collidePlanets > 0)
			force += new Vector2(0, -700).Rotated(Rotation);
		
		walkingLeft.Playing = doAnimation;
		walkingRight.Playing = doAnimation;
		Animate();
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
	
	public void Animate()
	{		
		noMove.Visible = true;
		walkingLeft.Visible = false;
		walkingRight.Visible = false;
		flying.Visible = false;
		
		if((nearbyPlanets == 0) || (Input.IsActionPressed("jump")))
		{
			noMove.Visible = false;
			flying.Visible = true;
		} else {
			if(direction == "none"){
				noMove.Visible = true;
				walkingLeft.Visible = false;
				walkingRight.Visible = false;
			}

			if (direction == "left"){
				noMove.Visible = false;
				walkingLeft.Visible = true;
			}
		
			if (direction == "right"){
				noMove.Visible = false;
				walkingRight.Visible = true;
			}
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

	public void _on_VisibilityNotifier2D_tree_exiting()
	{
		disableLevelReset = true;
	}

	public void _on_VisibilityNotifier2D_screen_exited()
	{
		if(!disableLevelReset) GetTree().ReloadCurrentScene();
	}
}
