using FallingDummy.src.obstacles.obstacle;

namespace FallingDummy.src.obstacles.factory
{
    public interface IObstacleFactory 
    {
        ObstacleNode CreateObstacle(string id);
        void AddFactory(IObstacleFactory factory, string idPart);
        void RemoveFactory(IObstacleFactory factory);
    }
}
