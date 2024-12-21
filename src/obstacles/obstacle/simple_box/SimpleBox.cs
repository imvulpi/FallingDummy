using FallingDummy.src.commons.animations;
using Godot;

namespace FallingDummy.src.obstacles.obstacle
{
    public partial class SimpleBox : ObstacleNode, IObstacle
    {
        public ObstacleOvercoming ObstacleOvercoming { get; } = ObstacleOvercoming.TAP;
        public string ID { get; } = ObstaclePaths.SIMPLE_BOX_ID;
        string IObstacle.Name { get; } = "Simple Box";
    }
}
