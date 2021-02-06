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
	
	enum AnimationState
	{
		None,
		Left,
		Right, 
		RightIdle, 
		LeftIdle
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
		flying = (AnimatedSprite) GetNode("Mouse Flying");
		HeldItem = FoodItem.HAMBURGER;
		heldItemSprite = GetNode<Sprite>("HeldItem");
	}

	// Code for handeling the Player's input
	public void GetInput()//TODO Should this really be called every physics process or can we use singals? May help performance issues if they ever come up
	{
		var colliding = GetCollidingBodies();
		var doAnimation = true;
		if(colliding.Count > 0){
			doAnimation = false; //TODO add an idle left/right animation instead of pausing current animation
			//direction = AnimationState.None;
			if(Input.IsActionPressed("left") ^ Input.IsActionPressed("right")){
				doAnimation = true;
				if (Input.IsActionPressed("left")){
					force += new Vector2(-speed, 0).Rotated(Rotation);
					direction = AnimationState.Left;
				}

				else if (Input.IsActionPressed("right")){
					force += new Vector2(speed, 0).Rotated(Rotation);
					direction = AnimationState.Right;

				} else if (Input.IsActionJustReleased("right")) {

					direction = AnimationState.RightIdle;

				} else if (Input.IsActionJustReleased("left")) {

					direction = AnimationState.LeftIdle; 

				}
			}
		} else if(collidePlanets == 0)
			direction = AnimationState.None;

		if (Input.IsActionJustPressed("jump") && collidePlanets > 0)
			force += new Vector2(0, -700).Rotated(Rotation);
		
		walkingLeft.Playing = doAnimation;
		walkingRight.Playing = doAnimation;
		Animate();
		if(HeldItem.ImagePath != "") {
			heldItemSprite.Texture = HeldItem.GetTexture();
			heldItemSprite.Visible = true;
		} else {
			heldItemSprite.Visible = false;
		}
	}

		// More code for Animations
	public void Animate()
	{		
		noMove.Visible = true;
		walkingLeft.Visible = false;
		walkingRight.Visible = false;
		flying.Visible = false;
		
		if((nearbyPlanets == 0) || (Input.IsActionPressed("jump"))) //TODO this means that while jump is pressed, no other animations play. Looks strange when walking left and holding jump. Maybe make inputs exclusive?
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

				case AnimationState.RightIdle:{
					noMove.Visible = true;
					walkingRight.Visible = false;
					break;
				}

				case AnimationState.LeftIdle:{
					noMove.Visible = true;
					walkingRight.Visible = false;
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
		disableLevelReset = true;
	}

	public void _on_VisibilityNotifier2D_screen_exited()
	{
		if(!disableLevelReset) GetTree().ReloadCurrentScene();
	}
}
