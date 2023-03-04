using Godot;
using System;

public partial class ChangeLayerGround : Area2D
{
	public void _on_body_exited(CharacterBody2D body)
	{
		body.SetCollisionMaskValue(2, false);
		body.ZIndex = 0;
	}
}
