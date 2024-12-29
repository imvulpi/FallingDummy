using FallingDummy.src.commons.io;
using FallingDummy.src.game.data;
using FallingDummy.src.game.data.record;
using Godot;
using System;
using System.Data;
using System.IO;
public partial class GameMenu : Control
{
	public event EventHandler GamePlay;
    [Export] Label LostLabel { get; set; }
    [Export] Label BestScoreLabel { get; set; }
    [Export] Label PlayLabel { get; set; }
    public RecordLoader RecordLoader { get; set; } = new RecordLoader();
    private string currentMode = DataConsts.CLASSIC_RECORD_NAME;
    public override void _Ready()
    {
        LoadRecord(currentMode);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch screenTouch)
        {
            GamePlay.Invoke(this, new EventArgs());
        }
        else if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.IsPressed())
            {
                if (mouseButton.ButtonIndex == MouseButton.Left || mouseButton.ButtonIndex == MouseButton.Right)
                {
                    GamePlay.Invoke(this, new EventArgs());
                }
            }
        }
    }

    public void LoadLostScore(float score)
    {
        LostLabel.Text = $"Lost at: {score}";
    }

    public void LoadRecord(string name)
    {
        string recordsDirectory = Path.Join(OS.GetUserDataDir(), DataConsts.RECORDS_DIR);
        DirectoryHelper.ValidateDirectory(recordsDirectory);
        RecordList recordList = RecordLoader.Load(Path.Join(recordsDirectory, DataConsts.RECORD_FILE));
        if (recordList.Records.TryGetValue(name, out var record))
        {
            BestScoreLabel.Text = $"Best: {record}";
        }
    }
}
