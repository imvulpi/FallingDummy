using FallingDummy.src.commons.animations;
using FallingDummy.src.commons.math;
using FallingDummy.src.game.input;
using FallingDummy.src.game.obstacle_handler;
using FallingDummy.src.game.session;
using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;
using Godot;
using System;
using System.Collections.Generic;

namespace FallingDummy.src.game
{
    public class SessionHandler
    {
        public ObstacleGenerator ObstacleGenerator { get; set; }
        public ObstacleMovementHandler ObstacleMovementHandler { get; set; }
        public GameScoreManager GameScoreManager { get; set; }
        public GameHUDController GameHUDController { get; set; }
        public GameInputHandler GameInputHandler { get; set; }
        public ObstacleSolutionsController ObstacleSolutionsController { get; set; }
        public Node2D ObstacleSpot { get; set; }
        public Camera2D Camera { get; set; }

        // Settings:
        public ProgressType ProgressType { get; set; } = ProgressType.Score;


        private readonly List<IObstacle> ObstacleNodes = new();
        private float SpeedMultiply = 1.0f;
        public void Handle(double delta)
        {
            if (ObstacleSpot.GetChildCount() <= 0)
            {
                SpeedMultiply = GetSpeedMultiply();
                IObstacle obstacle = CreateObstacle();
                ObstacleSpot.AddChild(obstacle.ObstacleNode);
                GameInputHandler.PlugArea2DInput(obstacle, obstacle.ObstacleArea);
                ObstacleSolutionsController.PlugAllowedSolution(obstacle);
                ObstacleSolutionsController.PlugDisallowedSolution(obstacle);
            }
            else
            {
                for (int i = 0; i < ObstacleNodes.Count; i++)
                {
                    IObstacle obstacle = ObstacleNodes[i];

                    if (ObstaclePassedY(obstacle))
                    {
                        ObstacleNodes.Remove(obstacle);
                        ObstacleSpot.RemoveChild(obstacle.ObstacleNode);
                        i--;
                        continue;
                    }

                    ObstacleMovementHandler.SpeedMultiply = SpeedMultiply;
                    ObstacleMovementHandler.MoveIObstacle(obstacle, delta);
                }
            }
        }

        public IObstacle GetLeadingObstacle()
        {
            for (int i = 0; i < ObstacleNodes.Count; i++)
            {
                IObstacle obstacle = ObstacleNodes[i];
                
                return obstacle;
            }

            return null;
        }

        public float GetSpeedMultiply()
        {
            if (ProgressType == ProgressType.Score)
            {
                if (GameScoreManager.GetScore() < 50 && SpeedMultiply < 2.0f)
                    return MathF.Pow(1.015f, GameScoreManager.GetScore());
                else
                {
                    return 2.0f + MathF.Pow(1.015f, GameScoreManager.GetScore())-1;
                }
            }
            else if (ProgressType == ProgressType.Distance)
            {
                return 1.0f;
            }
            else
            {
                return 1.0f;
            }
        }

        public void RemoveObstacle(IObstacle obstacle)
        {
            // defeat animation handling here
            ObstacleNodes.Remove(obstacle);
            ObstacleSpot.RemoveChild(obstacle.ObstacleNode);
        }

        private IObstacle CreateObstacle()
        {
            IObstacle obstacleInfo = ObstacleGenerator.GetRandomObstacle();
            Vector2 obstacleAreaSize = MathHelper.GetArea2DSize(obstacleInfo.ObstacleArea);
            // Y Half of camera size and y size positioned from center down.
            Vector2 obstacleSpotPosition = new(0, (MathHelper.CalculateCamera2DSize(Camera.GetTree(), Camera).Y / 2) + obstacleAreaSize.Y);
            ObstacleSpot.Position = obstacleSpotPosition;
            ObstacleNodes.Add(obstacleInfo);
            return obstacleInfo;
        }

        private bool ObstaclePassedY(IObstacle obstacleInfo)
        {
            Area2D area = obstacleInfo.ObstacleArea;
            Vector2 areaSize = MathHelper.GetArea2DSize(area);
            bool isPassedY = area.GlobalPosition.Y < (-MathHelper.CalculateCamera2DSize(area.GetTree(), Camera).Y/2 - (areaSize.Y));

            if (isPassedY)
            {
                if (obstacleInfo.AllowedSolutions.Contains(ObstacleSolution.NOTHING))
                {
                    GameScoreManager.AddScore(obstacleInfo);
                    GameHUDController.UpdateScore(GameScoreManager.GetScore());
                }
                return true;
            }
            return false;
        }
    }
}
