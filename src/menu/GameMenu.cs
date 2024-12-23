using Godot;
using System;
public partial class GameMenu : Control
{
	public event EventHandler GamePlay;
    [Export] Label LostLabel { get; set; }
    [Export] Label BestScoreLabel { get; set; }
    [Export] Label PlayLabel { get; set; }
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
}
