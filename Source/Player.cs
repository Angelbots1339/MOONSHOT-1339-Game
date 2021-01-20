using Godot;
using System;

public class Player : Movement
{	
	private Sprite noMove ;
	private AnimatedSprite walkingLeft;
	private AnimatedSprite walkingRight;
	private AnimatedSprite flying;
	
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
		base._Ready();
		disableLevelReset = false;
		noMove = (Sprite) GetNode("mouse2");
		walkingLeft = (AnimatedSprite) GetNode("Mouse Walking Left");
		walkingRight = (AnimatedSprite) GetNode("Mouse Walking Right");
		flying = (AnimatedSprite) GetNode("Mouse Flying");
	}

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
				
				default:break;
			}
		}
	}

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
