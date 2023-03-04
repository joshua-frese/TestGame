using Godot;
using System;

public partial class HealthBar : Node2D
{

	private TextureProgressBar healthBar;

	public override void _Ready()
	{
		GD.Print("Healthbar");

		// Cast GetNode() response to AnimationTree
		healthBar = (TextureProgressBar)GetNode("HealthBar");
	}

	public void HealthUpdated(float health, float amount)
	{
		healthBar.Value = health;
	}

	public void MaxHealthUpdated(float maxHealth)
	{
		healthBar.MaxValue = maxHealth;
	}
}
