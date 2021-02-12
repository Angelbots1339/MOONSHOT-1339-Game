using Godot;
using System;

/*

	This is the code for the Character's Animation and the Player's Input

*/

public class Player : Movement
{	
	// Code for the Character's Animation
	
	private Sprite noMove ;
	private AnimatedSprite walkingLeft;
	private AnimatedSprite walkingRight;
	private AnimatedSprite flying;
	public Score playerScore;
	private AnimatedSprite idleLeft;
	private AnimatedSprite idleRight;

	
	enum AnimationState
	{
		None,
		Left,
		Right, 
		RightIdle, 
		LeftIdle,
	}
	private AnimationState direction = AnimationState.None;
	private Boolean disableLevelReset;
	private Sprite heldItemSprite;
	public FoodItem HeldItem {get;set;}

	// Different Animation states: 
	public override void _Ready()
	{
		base._Ready();
		disableLevelReset = false;
		noMove = (Sprite) GetNode("mouse2");
		walkingLeft = (AnimatedSprite) GetNode("Mouse Walking Left");
		walkingRight = (AnimatedSprite) GetNode("Mouse Walking Right");
		idleRight = (AnimatedSprite) GetNode("Idle Right");
		idleLeft = (AnimatedSprite) GetNode("idle Left");
		flying = (AnimatedSprite) GetNode("Mouse Flying");
		HeldItem = FoodItem.HAMBURGER;
		heldItemSprite = GetNode<Sprite>("HeldItem");
		playerScore = new Score();
		playerScore.setScore(10);
		GD.Print("Score on load: " + playerScore.toString());
	}

	// Code for handeling the Player's input
	public void GetInput()//TODO Should this really be called every physics process or can we use singals? May help performance issues if they ever come up
	{
		var colliding = GetCollidingBodies();
		var doAnimation = true;
		if(colliding.Count > 0){
			doAnimation = false;
			// Change Animation state based on keys pressed 
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
			if (!doAnimation){
				if(direction == AnimationState.Right) {
					direction = AnimationState.RightIdle;
					idleRight.Frame = 1;
				} else if (direction == AnimationState.Left) {
					direction = AnimationState.LeftIdle; 
					idleLeft.Frame = 1;
				}
			}
		} else if (collidePlanets == 0){
			direction = AnimationState.None;
		}

		if (Input.IsActionJustPressed("jump") && collidePlanets > 0)
			force += new Vector2(0, -700).Rotated(Rotation);
		
		walkingLeft.Playing = doAnimation; //This is needed for all animations that have multiple frames
		walkingRight.Playing = doAnimation;
		idleLeft.Playing = doAnimation;
		idleRight.Playing = doAnimation;

		Animate();

		if (HeldItem.ImagePath != "") {
			heldItemSprite.Texture = HeldItem.GetTexture();
			heldItemSprite.Visible = true;
		} else {
			heldItemSprite.Visible = false;
		}
	}

		// More code for Animations
	public void Animate()
	{		
		noMove.Visible = false;
		walkingLeft.Visible = false;
		walkingRight.Visible = false;
		flying.Visible = false;
		idleRight.Visible = false;
		idleLeft.Visible = false;
		
		if ((nearbyPlanets == 0) || (Input.IsActionPressed("jump"))) //TODO this means that while jump is pressed, no other animations play. Looks strange when walking left and holding jump. Maybe make inputs exclusive?
		{
			noMove.Visible = false;
			flying.Visible = true;
			
		} else {
			switch (direction) {
				case AnimationState.None:{
					noMove.Visible = true;
					break;
				}

				case AnimationState.Left:{
					walkingLeft.Visible = true;
					break;
				}
			
				case AnimationState.Right:{
					walkingRight.Visible = true;
					break;
				}

				case AnimationState.RightIdle:{
					idleRight.Visible = true;
					break;
				}

				case AnimationState.LeftIdle:{
					idleLeft.Visible = true;
					break;
				}
				
				default:break;
			}
		}
	}

	// Handels Physics and some of the Character's Interactions
	public override void _PhysicsProcess(float delta)
	{
		MoveToGrav();
		FacePlanet();
		GetInput();
		ApplyCentralImpulse(force);
		force = new Vector2();
	}

	public void _on_VisibilityNotifier2D_tree_exiting()
	{
		walkingLeft.Frame = 1;
		walkingRight.Frame = 1;
		flying.Frame = 1;
		idleRight.Frame = 1;
		idleLeft.Frame = 1;
		disableLevelReset = true;
	}

	public void _on_VisibilityNotifier2D_screen_exited()
	{
		if(!disableLevelReset) GetTree().ReloadCurrentScene();

	}
}
