using FallingDummy.src.obstacles.obstacle;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game
{
    public class ObstacleSolutionsController
    {
        public Dummy Dummy { get; set; }
        public SessionHandler SessionHandler { get; set; }
        public GameScoreManager ScoreManager { get; set; }
        public GameHUDController GameHUDController { get; set; }

        public void PlugDisallowedSolution(IObstacle obstacle)
        {
            obstacle.DisallowedSolution += Obstacle_DisallowedSolution;
        }

        public void PlugAllowedSolution(IObstacle obstacle)
        {
            obstacle.AllowedSolution += Obstacle_AllowedSolution;
        }
        
        private void Obstacle_DisallowedSolution(object sender, EventArgs e)
        {
            if(sender is IObstacle obstacle)
            {
                Dummy.TakeDamage(obstacle.AttackStrength);
            }
            else
            {
                GD.PrintErr("Non obstacles arent allowed");
            }
        }

        private void Obstacle_AllowedSolution(object sender, EventArgs e)
        {
            if (sender is IObstacle obstacle)
            {
                obstacle.TakeDamage(Dummy.AttackStrength);
                if (obstacle.Defeated)
                {
                    ScoreManager.AddScore(obstacle);
                    GameHUDController.UpdateScore(ScoreManager.GetScore());
                    SessionHandler.RemoveObstacle(obstacle);
                }
            }
            else
            {
                GD.PrintErr("Non obstacles arent allowed");
            }
        }
    }
}
