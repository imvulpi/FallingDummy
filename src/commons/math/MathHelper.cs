using Godot;
using Godot.Collections;

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
        public static Vector2 GetArea2DSize(Area2D ObstacleArea)
        {
            Array<Node> nodes = ObstacleArea.GetChildren();
            foreach (Node node in nodes)
            {
                if (node is CollisionShape2D cs2d)
                {
                    Shape2D shape = cs2d.Shape;
                    if (shape is RectangleShape2D rectangleShape)
                    {
                        return rectangleShape.Size;
                    }
                    else if (shape is CircleShape2D circleShape)
                    {
                        return new Vector2(circleShape.Radius, circleShape.Radius);
                    }
                    else if (shape is CapsuleShape2D capsuleShape)
                    {
                        return new Vector2(capsuleShape.Radius, capsuleShape.Height);
                    }
                    else
                    {
                        GD.PrintErr("Shape not supported in GetSize");
                    }
                }
            }
            return new Vector2(512, 512);
        }
    }
}
