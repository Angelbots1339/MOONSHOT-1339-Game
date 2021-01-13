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
	private Camera2D camera;
	
	private Node2D noMove ;
	private Node2D walkingLeft;
	private Node2D walkingRight;
	private Node2D flying;
	
	private double nearbyPlanets;
	
	public override void _Ready()
	{
		camera = GetParent().GetNode<Camera2D>("Camera2D");
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
		if (Input.IsActionPressed("left"))
			force += new Vector2(-speed, 0).Rotated(Rotation);

		if (Input.IsActionPressed("right"))
			force += new Vector2(speed, 0).Rotated(Rotation);

		if (Input.IsActionJustPressed("jump"))
			force += new Vector2(0, -700).Rotated(Rotation);
	}

	public void MoveToGrav()
	{
		nearbyPlanets = 0;
		
		foreach (Gravity planet in planetArray)
		{
			var distanceVector = planet.GlobalPosition - this.GlobalPosition; 
			var outsideness = (distanceVector).Length()/planet.gravityForce; //length of how far player/movementscript is from planet
			if(distanceVector.Length() <= planet.gravityForce) {
				force += distanceVector.Normalized() * (10+10/outsideness);
				nearbyPlanets++;
			}
		}
		
		Animate();
	}
	
	public void Animate()
	{
		noMove = (Node2D) GetNode("mouse2");
		walkingLeft = (Node2D) GetNode("Mouse Walking Left");
		walkingRight = (Node2D) GetNode("Mouse Walking Right");
		flying = (Node2D) GetNode("Mouse Flying");
		
		noMove.Visible = true;
		walkingLeft.Visible = false;
		walkingRight.Visible = false;
		flying.Visible = false;
		
		if(nearbyPlanets > 0){
			if (Input.IsActionPressed("left")){
				noMove.Visible = false;
				walkingLeft.Visible = true;
				walkingRight.Visible = false;
				flying.Visible = false;
			}
		
			if (Input.IsActionPressed("right")){
				noMove.Visible = false;
				walkingLeft.Visible = false;
				walkingRight.Visible = true;
				flying.Visible = false;
			}
		}
		if((nearbyPlanets == 0) || (Input.IsActionPressed("jump")))
		{
			noMove.Visible = false;
			walkingLeft.Visible = false;
			walkingRight.Visible = false;
			flying.Visible = true;
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

	public void _on_VisibilityNotifier2D_screen_exited()
	{
		GetTree().ReloadCurrentScene();
	}
}
