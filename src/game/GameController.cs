using FallingDummy.src.commons.io;
using FallingDummy.src.game.data;
using FallingDummy.src.game.data.record;
using Godot;
using System;
using System.IO;
public partial class GameController : Node
{
    [Export]
    public PackedScene GameScene { get; set; }
    [Export]
    public PackedScene GameMenuScene { get; set; }

	public Session Game { get; set; }
	public GameMenu Menu { get; set; }
    public IRecordSaver RecordSaver { get; set; } = new RecordSaver();

    private bool MenuAwait = false;
    private double CurrentTime = 0f;
    private double AwaitTime = 0.25f;

	public override void _Ready()
	{
        Menu ??= GameMenuScene.Instantiate<GameMenu>();
        Menu.GamePlay += Menu_GamePlay;
        AddChild(Menu);
	}

    private void Menu_GamePlay(object sender, EventArgs e)
    {
        RemoveChild(Menu);
        StartNewSession();
    }

    private void Game_GameEnd(object sender, float e)
    {
        RemoveChild(Game);
        string recordsDirectory = Path.Join(OS.GetUserDataDir(), DataConsts.RECORDS_DIR);
        DirectoryHelper.ValidateDirectory(recordsDirectory);
        RecordSaver.Save(new Record(DataConsts.CLASSIC_RECORD_NAME, e), Path.Join(recordsDirectory, DataConsts.RECORD_FILE));
        AddChild(Menu);
        Menu.SetProcessInput(false);
        MenuAwait = true;
        Menu.LoadRecord(DataConsts.CLASSIC_RECORD_NAME);
        Menu.LoadLostScore(e);
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
