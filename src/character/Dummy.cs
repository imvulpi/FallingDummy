using FallingDummy.src.character;
using Godot;
using System;

public partial class Dummy : StaticBody2D, IDummy
{
	public float Health { get; set; } = 1.0f;
	public float AttackStrength { get; set; } = 1.0f;
    public bool Defeated { get; set; } = false;

    public event EventHandler DeadEvent;

    public void TakeDamage(float damage)
	{
		Health -= damage;
		CheckHealth();
    }

	public void CheckHealth()
	{
		if (Health <= 0)
		{
			Defeated = true;
			DeadEvent.Invoke(this, new EventArgs());
		}
	}
}