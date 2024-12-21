using FallingDummy.src.commons.animations;
using FallingDummy.src.commons.math;
using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;
using Godot;
using System;
using System.Collections.Generic;

namespace FallingDummy.src.game
{
    public class GameObstacleHandler
    {
        public ObstacleGenerator ObstacleGenerator;
        private readonly List<ObstacleNode> ObstacleNodes = new List<ObstacleNode>();
        public Node2D ObstacleSpot { get; set; }
        public Dummy Dummy { get; set; }
        public Camera2D Camera { get; set; }
        public Label PointsLabel { get; set; }

        // Specific to the handler:
        public float SpeedMultiply { get; set; } = 1.0f;
        public float BaseSpeedHeight { get; set; } = 500;
        private float Points { get; set; } = 0.0f;

        public void Handle(double delta)
        {
            if(!Camera.IsInsideTree()) return;

            if (ObstacleSpot.GetChildCount() <= 0)
            {
                ObstacleNode obstacle = ObstacleGenerator.GetRandomObstacle();
                ObstacleSpot.Position = new Vector2(0, (MathHelper.CalculateCamera2DSize(Camera.GetTree(), Camera).Y / 2) + obstacle.GetSize().Y);
                ObstacleNodes.Add(obstacle);
                ObstacleSpot.AddChild(obstacle);
                obstacle.DestroyEvent += Obstacle_DestroyEvent;
            }
            else
            {
                for (int i = 0; i < ObstacleNodes.Count; i++)
                {
                    ObstacleNode obstacle = ObstacleNodes[i];

                    if (ObstaclePassed(obstacle))
                    {
                        obstacle.Visible = false;
                        ObstacleNodes.Remove(obstacle);
                        ObstacleSpot.RemoveChild(obstacle);
                        i--;
                        continue;
                    }

                    obstacle.Velocity = (-obstacle.GetGravity() * SpeedMultiply) * CalculateHeightRatio(BaseSpeedHeight);
                    obstacle.Velocity = obstacle.AffectSpeed(obstacle.Velocity);
                    obstacle.MoveAndSlide();
                }
            }
        }
        private bool ObstaclePassed(ObstacleNode node)
        {
            if (node.Position.Y < (-MathHelper.CalculateCamera2DSize(node.GetTree(), Camera).Y - (node.GetSize().Y * 2)))
            {
                Points += node.ScoreReward;
                Points = node.AffectScore(Points);
                PointsLabel.Text = $"Score: {Points}";
                return true;
            }
            return false;
        }

        private void Obstacle_DestroyEvent(object sender, EventArgs e)
        {
            bool shouldAddPoints = true;
            if (sender is TntBox)
            {
                shouldAddPoints = false;
                Dummy.DealDamage(2f);

            }

            if (sender is ObstacleNode obstacle)
            {
                ObstacleNodes.Remove(obstacle);
                ObstacleSpot.CallDeferred(Node2D.MethodName.RemoveChild, obstacle);
                if (shouldAddPoints)
                {
                    Points += obstacle.ScoreReward;
                    Points = obstacle.AffectScore(Points);
                    PointsLabel.Text = $"Score: {Points}";
                }
            }

        }

        public float CalculateHeightRatio(float baseHeight, float height = 0)
        {
            if (height == 0) height = DisplayServer.ScreenGetSize().Y;
            return height / baseHeight;
        }
    }
}
