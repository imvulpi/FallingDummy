using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;

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

        public static IObstacleBundle GetNewBundle()
        {
            ObstacleBundle bundle = new ObstacleBundle();
            bundle.ObstaclesAndPercentages.Add(new SimpleBox(), 1/3f);
            bundle.ObstaclesAndPercentages.Add(new TntBox(), 1/3f);
            bundle.ObstaclesAndPercentages.Add(new SwipeBox(), 1/3f);
            return bundle;
        }
    }
}
