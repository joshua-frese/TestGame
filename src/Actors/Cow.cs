using Godot;
using System;

public partial class Cow : CharacterBody2D
{
	[Export]
	public float moveSpeed = 30.0f;

	[Export]
	public int maxIdleTime = 10;
	
	[Export]
	public int maxWalkTime = 4;

	enum CowStates {
		IDLE,
		WALK
	};

	private Sprite2D sprite;
	private Timer timer;
	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback stateMachine;

	private Vector2 moveDirection = Vector2.Zero;

	private CowStates currentState = CowStates.IDLE;

	public override void _Ready()
	{
		sprite = (Sprite2D)GetNode("Sprite2D");
		animationTree = (AnimationTree)GetNode("AnimationTree");
		timer = (Timer)GetNode("StateTimer");
		stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

		PickNewState();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (currentState == CowStates.WALK) {
			Velocity = moveDirection * moveSpeed;

			MoveAndSlide();
		}
	}

	public void SelectNewDirection() {
		var random = new Random();
		moveDirection = new Vector2(random.Next(-1, 2), random.Next(-1, 2));

		if (moveDirection.X < 0)
			sprite.FlipH = true;
		else if(moveDirection.X > 0)
			sprite.FlipH = false;
	}

	public void PickNewState()
	{
		var random = new Random();

		if (currentState == CowStates.IDLE) {
			SelectNewDirection();
			if (moveDirection != Vector2.Zero) {
				currentState = CowStates.WALK;
				stateMachine.Travel("WalkRight");
			}
			timer.Start(random.Next(maxWalkTime));
		} else if(currentState == CowStates.WALK) {
			stateMachine.Travel("IdleRight");
			currentState = CowStates.IDLE;
			sprite.FlipH = random.Next(0, 2) == 1;
			timer.Start(random.Next(maxIdleTime));
		}
	}

	public void _on_state_timer_timeout()
	{
		PickNewState();
	}
}
