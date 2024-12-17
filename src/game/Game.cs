using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Game : Node2D
{
	[Export]
	public Dummy Dummy { get; set; }
	[Export]
	public Node DummySpot { get; set; }
	[Export]
	public Node ObstacleSpot { get; set; }
	[Export]
	public Camera2D Camera { get; set; }

	private ObstacleGenerator ObstacleGenerator;
	private List<ObstacleNode> ObstacleNodes = new List<ObstacleNode>();
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
        Dummy.DeadEvent += Dummy_DeadEvent;

        ObstacleGenerator = new ObstacleGenerator();
		ObstacleGenerator.Init();
	}

    private void Dummy_DeadEvent(object sender, EventArgs e)
    {
		SetProcess(false);
		GD.Print("YOU LOST!");
		// Implement coming back to the menu
    }

    public override void _Process(double delta)
	{
		if (ObstacleSpot.GetChildCount() <= 0)
		{
			ObstacleNode obstacle = ObstacleGenerator.GetRandomObstacle();
            ObstacleNodes.Add(obstacle);
            ObstacleSpot.AddChild(obstacle);
            obstacle.DestroyEvent += Obstacle_DestroyEvent;
		}
		else
		{
            for (int i = 0; i < ObstacleNodes.Count; i++)
            {
				ObstacleNode obstacle = ObstacleNodes[i];
                obstacle.Velocity = -obstacle.GetGravity();
                obstacle.Velocity = obstacle.AffectSpeed(obstacle.Velocity);
                obstacle.MoveAndSlide();
				
                if (obstacle.GlobalPosition.Y < -GetTree().Root.GetVisibleRect().Size.Y * 3)
                {
                    ObstacleNodes.Remove(obstacle);
                    ObstacleSpot.RemoveChild(obstacle);
					i--;
                }
            }
		}
    }

	private bool IsObstacleInWindow(Node2D node)
	{
		Rect2 viewsize = new Rect2(Camera.GlobalPosition - Camera.GetViewportRect().Size/2, Camera.GetViewportRect().Size);
		return viewsize.HasPoint(node.Position);
	}
    private void Obstacle_DestroyEvent(object sender, EventArgs e)
    {
		if(sender is TntBox)
		{
			Dummy.DealDamage(2f);
		}else if(sender is ObstacleNode obstacle)
		{
            ObstacleNodes.Remove(obstacle);
            ObstacleSpot.RemoveChild(obstacle);
        }
    }
}
