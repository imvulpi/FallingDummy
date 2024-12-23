using FallingDummy.src.game;
using FallingDummy.src.game.input;
using FallingDummy.src.game.obstacle_handler;
using FallingDummy.src.obstacles.bundles;
using Godot;
using System;
public partial class Session : Node2D
{
	public event EventHandler<float> GameEnd;

    [ExportGroup("Game")]
    [Export] public Node2D DummySpot { get; set; }
	[Export] public Node2D ObstacleSpot { get; set; }
    [Export] public Dummy Dummy { get; set; }
    [Export] public Camera2D Camera { get; set; }
    [Export] public float SpeedMultiply { get; set; } = 1;
	[Export] public float BaseSpeedPerHeight { get; set; } = 500;

    [ExportGroup("UI")]
    [Export] public GameHUDController GameHUDController { get; set; }
    private SessionHandler SessionHandler { get; set; }
    private GameScoreManager ScoreManager { get; set; }
    private GameInputHandler GameInputHandler { get; set; }
    private ObstacleSolutionsController ObstacleSolutionsController { get; set; }
    public override void _Ready()
    {
        ScoreManager ??= new GameScoreManager();
        GameInputHandler ??= new GameInputHandler()
        {
            Window = GetTree().Root.GetWindow()
        };

        ObstacleSolutionsController ??= new ObstacleSolutionsController()
        {
            Dummy = Dummy,
            ScoreManager = ScoreManager,
            GameHUDController = GameHUDController,
        };

        SessionHandler ??= new SessionHandler()
        {
            Camera = Camera,
            ObstacleGenerator = new ObstacleGenerator()
            {
                Bundle = BundleCreator.GetNewBundle()
            },
            ObstacleMovementHandler = new ObstacleMovementHandler()
            {
                SpeedMultiply = SpeedMultiply,
                BaseSpeedPerHeight = BaseSpeedPerHeight,
                ObstacleSpot = ObstacleSpot,
            },
            ObstacleSpot = ObstacleSpot,
            GameHUDController = GameHUDController,
            GameScoreManager = ScoreManager,
            GameInputHandler = GameInputHandler,
            ObstacleSolutionsController = ObstacleSolutionsController
        };

        ObstacleSolutionsController.SessionHandler = SessionHandler;

        SessionHandler.ObstacleGenerator.Init();
        Dummy.DeadEvent += Dummy_DeadEvent;
        DummySpot.AddChild(Dummy);
    }

    private void Dummy_DeadEvent(object sender, EventArgs e)
    {
        GameEnd.Invoke(this, ScoreManager.GetScore());
    }

    public override void _PhysicsProcess(double delta)
    {
        SessionHandler?.Handle(delta);
    }
}
