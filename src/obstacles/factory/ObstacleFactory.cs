using FallingDummy.src.obstacles.obstacle;
using System.Collections.Generic;
using System.Linq;

namespace FallingDummy.src.obstacles.factory
{
    public class ObstacleFactory : IObstacleFactory
    {
        public Dictionary<string, IObstacleFactory> factories = new Dictionary<string, IObstacleFactory>();
        public virtual void AddFactory(IObstacleFactory factory, string idPart)
        {
            factories[idPart] = factory;
        }

        public virtual ObstacleNode CreateObstacle(string id)
        {
            string firstPart = id.Split('.')[0];
            if (factories.TryGetValue(firstPart, out IObstacleFactory factory))
            {
                return factory.CreateObstacle(id.Substring(firstPart.Length+1)); // +1 accounts for (.)
            }
            return null;
        }

        public virtual void RemoveFactory(IObstacleFactory factory)
        {
            List<KeyValuePair<string, IObstacleFactory>> factoriesToRemove = factories.Where(item => item.Value == factory).ToList();
            foreach (KeyValuePair<string, IObstacleFactory> item in factoriesToRemove) {
                factories.Remove(item.Key);
            }
        }
    }
}
