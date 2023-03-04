using Godot;
using System;

public partial class layer_change_bridge : Area2D
{
	public void _on_body_entered(CharacterBody2D body)
	{
		AddBridgeRights(body);
	}

	public void _on_body_exited(CharacterBody2D body)
	{
		AddBridgeRights(body);
	}
	
	private void AddBridgeRights(CharacterBody2D body)
	{
		body.SetCollisionMaskValue(1, false);
		body.SetCollisionMaskValue(2, true);
		body.ZIndex = 50;
	}
}
