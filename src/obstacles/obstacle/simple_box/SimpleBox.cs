using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.obstacle
{
    public partial class SimpleBox : ObstacleNode, IObstacle
    {
        public ObstacleOvercoming ObstacleOvercoming { get; } = ObstacleOvercoming.TAP;
        public string ID { get; } = ObstaclePaths.SIMPLE_BOX_ID;
        string IObstacle.Name { get; } = "Simple Box";
    }
}
