using Godot;
using System;

public partial class Game : Node2D
{
	[Export]
	public Dummy Dummy { get; set; }
	[Export]
	public Node DummySpot { get; set; }

	public override void _Ready()
	{
		if (Dummy == null)
		{
			Dummy = ResourceLoader.Load<PackedScene>("res://src/character/Dummy.tscn").Instantiate<Dummy>();
		}

		if(DummySpot == null)
		{
			DummySpot = new Node();
			AddChild(DummySpot);
		}

		DummySpot.AddChild(Dummy);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
