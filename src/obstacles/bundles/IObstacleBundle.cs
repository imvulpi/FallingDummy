using FallingDummy.src.obstacles.obstacle;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.bundles
{
    public interface IObstacleBundle
    {
        public Dictionary<IObstacle, float> ObstaclesAndPercentages { get; set; }

        public void NormalizePercentages()
        {
            float percentageSum = 0;
            foreach (var kvp in ObstaclesAndPercentages)
            {
                percentageSum += kvp.Value;
            }

            if (percentageSum > 1 || percentageSum < 1)
            {
                GD.PushWarning("Percentage sum of obstacle bundle is over 1, normalizing.");
                float ratio = percentageSum / percentageSum;

                foreach (var kvp in ObstaclesAndPercentages)
                {
                    ObstaclesAndPercentages[kvp.Key] = kvp.Value * ratio;
                }                   
            }
        }

        public IObstacle GetRandomObstacle()
        {
            Random random = new Random();
            double randomNum = random.NextDouble();

            foreach (var item in ObstaclesAndPercentages)
            {
                float percentage = item.Value;
                if (randomNum < percentage)
                {
                    return item.Key;
                }
                else
                {
                    randomNum -= percentage;
                }
            }

            return null;
        }
    }
}
