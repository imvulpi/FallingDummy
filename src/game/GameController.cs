using Godot;
using System;
public partial class GameController : Node
{
    [Export]
    public PackedScene GameScene { get; set; }
    [Export]
    public PackedScene GameMenuScene { get; set; }

	public Session Game { get; set; }
	public GameMenu Menu { get; set; }

    private bool MenuAwait = false;
    private double CurrentTime = 0f;
    private double AwaitTime = 0.25f;

	public override void _Ready()
	{
        Menu ??= GameMenuScene.Instantiate<GameMenu>();
        Menu.GamePlay += Menu_GamePlay;
        Start();
	}

    private void Start()
    {
        AddChild(Menu);
    }

    private void Menu_GamePlay(object sender, EventArgs e)
    {
        RemoveChild(Menu);
        StartNewSession();
    }

    private void Game_GameEnd(object sender, float e)
    {
        CallDeferred(MethodName.RemoveChild, Game);
		AddChild(Menu);
        Menu.SetProcessInput(false);
        MenuAwait = true;
    }

    private void StartNewSession()
    {
        Game = GameScene.Instantiate<Session>();
        Game.GameEnd += Game_GameEnd;
        // Here any session settings would be loaded.
        Game.Dummy = ResourceLoader.Load<PackedScene>("res://src/character/Dummy.tscn").Instantiate<Dummy>();

        AddChild(Game);
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
