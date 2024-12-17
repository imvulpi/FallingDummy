using FallingDummy.src.obstacles.obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.bundles
{
    public class ObstacleBundle : IObstacleBundle
    {
        public Dictionary<IObstacle, float> ObstaclesAndPercentages { get; set; } = new Dictionary<IObstacle, float>();
    }
}
