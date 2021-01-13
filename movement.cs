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
	enum AnimationState
	{
		None,
		Left,
		Right
	}
	private AnimationState direction = AnimationState.None;
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
			if(Input.IsActionPressed("left") ^ Input.IsActionPressed("right")){
				doAnimation = true;
				if (Input.IsActionPressed("left")){
					force += new Vector2(-speed, 0).Rotated(Rotation);
					direction = AnimationState.Left;
				}

				else if (Input.IsActionPressed("right")){
					force += new Vector2(speed, 0).Rotated(Rotation);
					direction = AnimationState.Right;
				}
			}
		} else if(collidePlanets == 0)
			direction = AnimationState.None;

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
			switch (direction) {
				case AnimationState.None:{
					noMove.Visible = true;
					walkingLeft.Visible = false;
					walkingRight.Visible = false;
					break;
				}

				case AnimationState.Left:{
					noMove.Visible = false;
					walkingLeft.Visible = true;
					break;
				}
			
				case AnimationState.Right:{
					noMove.Visible = false;
					walkingRight.Visible = true;
					break;
				}
				
				default:break;
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
