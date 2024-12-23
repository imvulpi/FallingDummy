using FallingDummy.src.obstacles.obstacle;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles
{
    public class ObstacleCreator
    {
        public Dictionary<string, string> IdAndScenePath = new Dictionary<string, string>()
        {
            {ObstacleConsts.SIMPLE_BOX_ID, ObstacleConsts.SIMPLE_BOX_PATH},
            {ObstacleConsts.TNT_BOX_ID, ObstacleConsts.TNT_BOX_PATH},
            {ObstacleConsts.SWIPE_BOX_ID, ObstacleConsts.SWIPE_BOX_PATH}
        };

        public IObstacle Create(IObstacle obstacle)
        {
            if(IdAndScenePath.TryGetValue(obstacle.ObstacleID, out var scenePath))
            {
                Node obstacleNode = ResourceLoader.Load<PackedScene>(scenePath).Instantiate<Node>();
                if (obstacleNode is IObstacle obstacleInfo)
                {
                    obstacleInfo.ObstacleNode ??= obstacleNode;
                    if(obstacleInfo.ObstacleArea == null && obstacleNode is Area2D areaNode)
                    {
                        obstacleInfo.ObstacleArea = areaNode;
                    }
                    else if(obstacleInfo.ObstacleArea == null)
                    {
                        GD.PushWarning($"Obstacle with ID: {obstacle.ObstacleID} has no area assigned!");
                    }
                    return obstacleInfo;
                }
                else
                {
                    throw new NotSupportedException($"Obstacle with ID: {obstacle.ObstacleID} doesn't implement IObstacle, thus it can't be created.");
                }
            }
            else
            {
                throw new NotSupportedException($"Obstacle with ID: {obstacle.ObstacleID} is not supported inside of ObstacleCreator");
            }
        }
    }
}
