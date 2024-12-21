namespace FallingDummy.src.obstacles.obstacle
{
    public interface IObstacle
    {
        public string Name { get; }
        public string ID { get; }
        public ObstacleOvercoming ObstacleOvercoming { get; }
    }
}
