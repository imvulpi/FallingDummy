using FallingDummy.src.obstacles.factory;
using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.bundles
{
    public static class BundleCreator
    {
        public static IObstacleBundle GetClassicBundle()
        {
            ObstacleBundle bundle = new ObstacleBundle();
            bundle.ObstaclesAndPercentages.Add(new SimpleBox(), 0.5f);
            bundle.ObstaclesAndPercentages.Add(new TntBox(), 0.5f);
            return bundle;
        }
    }
}
