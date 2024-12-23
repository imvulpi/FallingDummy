using FallingDummy.src.commons.math;
using FallingDummy.src.obstacles.obstacle;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game.obstacle_handler
{
    public class ObstacleMovementHandler
    {
        public List<IObstacle> Obstacles { get; set; }
        public Node2D ObstacleSpot { get; set; }
        public float SpeedMultiply { get; set; }
        public float BaseSpeedPerHeight { get; set; } = 500.0f; // This value is the height of one BaseSpeed, maintains same speed in different heights
        public float BaseSpeed { get; set; } = 980.0f*2; // Like gravity 9.8
        public float Distance {  get; set; }
        public void MoveIObstacle(IObstacle obstacle, double delta)
        {
            if (obstacle.ObstacleNode == null)
            {
                throw new NullReferenceException($"Obstacle ID: {obstacle.ObstacleID} contains a null .ObstacleNode");
            }

            if (obstacle.ObstacleNode is Node2D node2D)
            {
                MoveNode2D(node2D, delta);
            }
            else
            {
                GD.Print($"Movement on other ({obstacle.GetType()}) is not supported");
            }
        }
        private void MoveNode2D(Node2D node, double delta)
        {
            Vector2 newPosition = new Vector2(0, (BaseSpeed * SpeedMultiply));
            node.Position -= newPosition * (float)delta;
            Distance += newPosition.Y * (float)delta;
        }

        public float CalculateHeightRatio(float baseHeight, float height = 0)
        {
            if (height == 0) height = DisplayServer.WindowGetSize().Y;
            return height / baseHeight;
        }

        //obstacle.Velocity = (-obstacle.GetGravity() * SpeedMultiply) * CalculateHeightRatio(BaseSpeedHeight);
        //obstacle.Velocity = obstacle.AffectSpeed(obstacle.Velocity);
        //obstacle.MoveAndSlide();
    }
}
