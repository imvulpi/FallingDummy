using Godot;
using System;

public partial class GameController : Node
{
    [Export]
    public PackedScene GameScene { get; set; }
    [Export]
    public PackedScene GameMenuScene { get; set; }

	public Game Game { get; set; }
	public GameMenu Menu { get; set; }

    private bool MenuAwait = false;
    private double CurrentTime = 0f;
    private double AwaitTime = 0.2f;

	public override void _Ready()
	{
        Game ??= GameScene.Instantiate<Game>();
        Menu ??= GameMenuScene.Instantiate<GameMenu>();
        
        Game.GameEnd += Game_GameEnd;
        Menu.GamePlay += Menu_GamePlay;
        Game.SpeedMultiply = 1.0f;
        Start();
	}

    private void Start()
    {
        AddChild(Menu);
    }

    private void Menu_GamePlay(object sender, EventArgs e)
    {
        Game.Reset();
        RemoveChild(Menu); // Temp removal so it doesnt run
        AddChild(Game);
    }

    private void Game_GameEnd(object sender, float e)
    {
        RemoveChild(Game); // Temp removal so it doesnt run
		AddChild(Menu);
        Menu.SetProcessInput(false);
        MenuAwait = true;
    }

    public override void _Process(double delta)
	{
        if (MenuAwait)
        {
            CurrentTime += delta;
            if (CurrentTime > AwaitTime)
            {
                MenuAwait = false;
                CurrentTime = 0;
                Menu.SetProcessInput(true);
            }
        }
	}
}
