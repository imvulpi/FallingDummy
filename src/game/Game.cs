using FallingDummy.src.commons.animations;
using FallingDummy.src.commons.math;
using FallingDummy.src.game;
using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;
using Godot;
using System;
using System.Collections.Generic;
public partial class Game : Node2D
{
	public event EventHandler<float> GameEnd;

    [Export]
	public Dummy Dummy { get; set; }
	[Export]
	public Node2D DummySpot { get; set; }
	[Export]
	public Node2D ObstacleSpot { get; set; }
	[Export]
	public Camera2D Camera { get; set; }
	[Export]
	public float SpeedMultiply { get; set; } = 1;
	[Export]
	public float BaseSpeedHeight { get; set; } = 500;
    [Export] public Label PointsLabel { get; set; }

	public ObstacleGenerator ObstacleGenerator;
    private readonly List<ObstacleNode> ObstacleNodes = new List<ObstacleNode>();
	private bool Lost = false;
    private GlitchAnimation GlitchAnimation { get; set; }
	private GameObstacleHandler ObstacleHandler { get; set; }
    public override void _Ready()
    {
        ObstacleGenerator ??= new ObstacleGenerator();
        Dummy ??= ResourceLoader.Load<PackedScene>("res://src/character/Dummy.tscn").Instantiate<Dummy>();
        ObstacleHandler ??= new GameObstacleHandler()
        {
            BaseSpeedHeight = BaseSpeedHeight,
            SpeedMultiply = SpeedMultiply,
            ObstacleGenerator = ObstacleGenerator,
            ObstacleSpot = ObstacleSpot,
            Dummy = Dummy,
            Camera = Camera,
            PointsLabel = PointsLabel,
        };

        if (DummySpot == null)
		{
			DummySpot = new Node2D();
			AddChild(DummySpot);
		}

		DummySpot.AddChild(Dummy);
        Dummy.DeadEvent += Dummy_DeadEvent;
        ObstacleGenerator.Init();
    }

	public void Reset()
	{
		ObstacleNodes.Clear();
		Godot.Collections.Array<Node> nodes = ObstacleSpot.GetChildren();
		foreach (Node node in nodes)
		{
			ObstacleSpot.RemoveChild(node);
		}

        Lost = false;
        GlitchAnimation?.Reset();
        SetProcess(true);
    }

    private void Dummy_DeadEvent(object sender, EventArgs e)
    {
		RunEndAnimation();
    }

	private void RunEndAnimation()
	{
        ColorRect glitchRect = new ColorRect();
        Vector2 newSize = MathHelper.CalculateCamera2DSize(GetTree(), Camera) * 2;
        glitchRect.Size = newSize;
        glitchRect.Position = -newSize / 2;
        GlitchAnimation = new(glitchRect, this);
        GlitchAnimation.AnimationEnd += GlitchAnimation_AnimationEnd;
		GlitchAnimation.Setup(this).Start();
		Lost = true;
    }

    private void GlitchAnimation_AnimationEnd(object sender, EventArgs e)
    {
		GlitchAnimation.Stop();
        GameEnd.Invoke(this, 0f);
        SetProcess(false);
    }

    public override void _Process(double delta)
	{
        GlitchAnimation?.Update(delta);
        if (!Lost)
        {
            ObstacleHandler?.Handle(delta);
        }
    }
}
