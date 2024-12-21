using FallingDummy.src.obstacles.obstacle;
using System.Collections.Generic;

namespace FallingDummy.src.obstacles.bundles
{
    public class ObstacleBundle : IObstacleBundle
    {
        public Dictionary<IObstacle, float> ObstaclesAndPercentages { get; set; } = new Dictionary<IObstacle, float>();
    }
}
