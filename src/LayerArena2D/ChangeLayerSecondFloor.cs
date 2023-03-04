using Godot;
using System;

public partial class ChangeLayerSecondFloor : Area2D
{
	public void _on_body_exited(CharacterBody2D body)
	{
		body.SetCollisionMaskValue(2, true);
		body.ZIndex = 10;
	}
}
