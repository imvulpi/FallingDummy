using FallingDummy.src.obstacles.factory.box;
using FallingDummy.src.obstacles.obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
