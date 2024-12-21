using FallingDummy.src.obstacles.factory.box;
using FallingDummy.src.obstacles.obstacle;

namespace FallingDummy.src.obstacles.factory
{
    public static class ObstacleFactoryHelper
    {
        public static ObstacleFactory CreateDefaultObstacleFactory() {
            ObstacleFactory defaultFactory = new ObstacleFactory();
            ObstacleFactory dummydevFactory = new BoxObstacleFactory();
            defaultFactory.AddFactory(dummydevFactory, ObstaclePaths.DEV_PATH_PREFIX);
            return defaultFactory;
        }
    }
}
