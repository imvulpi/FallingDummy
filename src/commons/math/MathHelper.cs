using Godot;

namespace FallingDummy.src.commons.math
{
    public static class MathHelper
    {
        public static Vector2 CalculateCamera2DSize(SceneTree tree, Camera2D camera)
        {
            Vector2 cameraSize = new Vector2(
                tree.Root.GetVisibleRect().Size.X / camera.Zoom.X,
                tree.Root.GetVisibleRect().Size.Y / camera.Zoom.Y);
            return cameraSize;
        }
    }
}
