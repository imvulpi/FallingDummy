using FallingDummy.src.obstacles.obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.factory
{
    public interface IObstacleFactory 
    {
        ObstacleNode CreateObstacle(string id);
        void AddFactory(IObstacleFactory factory, string idPart);
        void RemoveFactory(IObstacleFactory factory);
    }
}
