using Godot;
using System;

public partial class layer_change_bridge : Area2D
{
	public void _on_body_entered(CharacterBody2D body)
	{
		if (body.GetCollisionMaskValue(2))
			body.SetCollisionMaskValue(1, false);
	}

	public void _on_body_exited(CharacterBody2D body)
	{
		body.SetCollisionMaskValue(1, true);
	}
}
