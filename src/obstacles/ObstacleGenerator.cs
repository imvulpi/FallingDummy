using FallingDummy.src.obstacles.bundles;
using FallingDummy.src.obstacles.factory;
using FallingDummy.src.obstacles.obstacle;
using Godot;
public class ObstacleGenerator
{
	public IObstacleFactory MainObstacleFactory { get; set; }
	public IObstacleBundle Bundle { get; set; }
	public void Init()
	{
        if (Bundle == null)
        {
            Bundle = BundleCreator.GetClassicBundle();
            Bundle.NormalizePercentages();
        }

        MainObstacleFactory ??= ObstacleFactoryHelper.CreateDefaultObstacleFactory();
    }

	public ObstacleNode GetRandomObstacle()
	{
        if(MainObstacleFactory == null)
        {
            GD.PushError("Main obstacle factory cant be null");
        }
        
        if(Bundle == null)
        {
            GD.PushError("Bundle cant be null");
        }
 		return MainObstacleFactory.CreateObstacle(Bundle.GetRandomObstacle().ID);
	}
}
