using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.obstacle
{
    public interface IObstacle
    {
        public string Name { get; }
        public string ID { get; }
        public ObstacleOvercoming ObstacleOvercoming { get; }
    }
}
