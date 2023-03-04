using Godot;
using System;

public partial class Player : Godot.CharacterBody2D
{
	[Export]
	public float moveSpeed = 100.0f;

	[Export]
	public Vector2 startingDirection = new Vector2(0, 1);

	[Export]
	public float healthPoints = 100.0f;
	

	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback stateMachine;
	private Timer timer;
	private HealthBar healthBar;

	public override void _Ready()
	{
		GD.Print("User Ready");

		// Cast GetNode() response to AnimationTree
		animationTree = (AnimationTree)GetNode("AnimationTree");
		timer = (Timer)GetNode("SecondTimer");
		healthBar = (HealthBar)GetNode("HealthBar");
		stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

		timer.Start();
		healthBar.HealthUpdated(healthPoints, healthPoints);
		UpdateAnimationParameters(startingDirection);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		UpdateAnimationParameters(direction);
		velocity = direction * moveSpeed;
	
		Velocity = velocity;
		MoveAndSlide();
		UpdateAnimationState();
	}

	public void UpdateAnimationParameters(Vector2 moveInput)
	{
		if (moveInput != Vector2.Zero) {
			animationTree.Set("parameters/Walk/blend_position", moveInput);
			animationTree.Set("parameters/Idle/blend_position", moveInput);
		}
	}

	public void UpdateAnimationState()
	{
		if (Velocity != Vector2.Zero)
			stateMachine.Travel("Walk");
		else
			stateMachine.Travel("Idle");
	}

	public void RemoveHealth(float amount )
	{
		healthPoints -= amount;
		healthBar.HealthUpdated(healthPoints, amount);

		if (healthPoints == 0) {
			GD.Print("Dead");
		}
	}

	public void _on_second_timer_timeout()
	{
		RemoveHealth(1);
		timer.Start();
	}

	public void _on_layer_change_bridge_body_entered(CharacterBody2D body)
	{
		GD.Print("bridge");
		SetCollisionMaskValue(1, false);
		SetCollisionMaskValue(2, true);
		ZIndex = 50;
	}

	public void _on_layer_change_ground_body_entered(CharacterBody2D body)
	{
		GD.Print("ground");
		SetCollisionMaskValue(1, true);
		SetCollisionMaskValue(2, false);
		ZIndex = 0;
	}
}
