using FallingDummy.src.obstacles.obstacle;
using FallingDummy.src.obstacles.obstacle.simple_box;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.factory.box
{
    public class BoxObstacleFactory : ObstacleFactory, IObstacleFactory
    {
        public override ObstacleNode CreateObstacle(string id)
        {
            if (ObstaclePaths.SIMPLE_BOX_ID.Contains(id))
            {
                return ResourceLoader.Load<PackedScene>(ObstaclePaths.SIMPLE_BOX_PATH).Instantiate<SimpleBox>();
            }else if (ObstaclePaths.TNT_BOX_ID.Contains(id))
            {
                return ResourceLoader.Load<PackedScene>(ObstaclePaths.TNT_BOX_PATH).Instantiate<TntBox>();
            }
            else
            {
                if (factories.TryGetValue(id, out IObstacleFactory factory))
                {
                    return factory.CreateObstacle(id);
                }

                string splitId = id.Split('.')[0];
                if (factories.TryGetValue(splitId, out IObstacleFactory partFactory))
                {
                    return partFactory.CreateObstacle(id.Substring(splitId.Length+1)); // +1 for .
                }
            }
            return null;
        }
    }
}
