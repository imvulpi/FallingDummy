using Godot;

namespace FallingDummy.src.game.input
{
    public class ScreenDragHandler
    {
        public Vector2 DragMargin {  get; set; } = new Vector2(5,5);
        public Vector2 GetDragDirection(InputEventScreenDrag screenDrag)
        {
            Vector2 direction = Vector2.Zero;
            Vector2 relative = screenDrag.Relative;
            Vector2 absRelative = new Vector2(Mathf.Abs(relative.X), Mathf.Abs(relative.Y));

            if(absRelative.X > absRelative.Y && absRelative.X - DragMargin.X > 0)
            {
                if(relative.X < 0)
                {
                    direction = Vector2.Left;
                }else
                {
                    direction = Vector2.Right;
                }
            }
            else if(absRelative.Y - DragMargin.Y > 0)
            {
                if (relative.Y < 0)
                {
                    direction = Vector2.Up;
                }
                else
                {
                    direction = Vector2.Down;
                }
            }

            return direction;
        }
    }
}
