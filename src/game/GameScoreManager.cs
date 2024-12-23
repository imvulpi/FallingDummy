using FallingDummy.src.obstacles.obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game
{
    public class GameScoreManager
    {
        private float Score = 0.0f;

        public void AddScore(IObstacle obstacle)
        {
            Score += obstacle.Reward;
        }

        public float GetScore() { return Score; }
    }
}
