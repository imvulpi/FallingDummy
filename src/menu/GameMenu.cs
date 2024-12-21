using Godot;
using System;
public partial class GameMenu : Control
{
	public event EventHandler GamePlay;

    public override void _Ready()
    {

    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch screenTouch)
        {
            GD.Print("Clicked");
            GamePlay.Invoke(this, new EventArgs());
        }
        else if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.IsPressed())
            {
                if (mouseButton.ButtonIndex == MouseButton.Left || mouseButton.ButtonIndex == MouseButton.Right)
                {
                    GD.Print("Clicked");
                    GamePlay.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
