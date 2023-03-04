using Godot;
using System;

public partial class layer_change_ground : Area2D
{	
	public void _on_body_entered(CharacterBody2D body)
	{
		this.RemoveBridRights(body);
	}

	public void _on_body_exited(CharacterBody2D body)
	{
		this.RemoveBridRights(body);
	}

	private void RemoveBridRights(CharacterBody2D body)
	{
		body.SetCollisionMaskValue(1, true);
		body.SetCollisionMaskValue(2, false);
		body.ZIndex = 0;
	}
}
