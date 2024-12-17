using FallingDummy.src.obstacles.obstacle;
using Godot;
using System;

public partial class Dummy : CharacterBody2D
{
	public event EventHandler DeadEvent;

	[Export]
	public float Health = 1f;

	[Export]
	public float Shield = 0f;

	public void DealDamage(float damage)
	{
		if (Shield > 0)
		{
			Shield -= damage;
		}else
		{
            Health -= damage;
		}

		CheckHealth();
    }

	public void CheckHealth()
	{
		if (Health <= 0)
		{
			Dead();
		}
	}

	public void Dead()
	{
		DeadEvent.Invoke(this, new EventArgs());
	}
}