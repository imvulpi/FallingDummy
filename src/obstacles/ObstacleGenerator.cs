using FallingDummy.src.obstacles;
using FallingDummy.src.obstacles.bundles;
using FallingDummy.src.obstacles.obstacle;
using Godot;
public class ObstacleGenerator
{
	public ObstacleCreator ObstacleCreator { get; set; }
	public IObstacleBundle Bundle { get; set; }
	public void Init()
	{
        if (Bundle == null)
        {
            Bundle = BundleCreator.GetClassicBundle();
            Bundle.NormalizePercentages();
        }

        ObstacleCreator ??= new ObstacleCreator();
    }

	public IObstacle GetRandomObstacle()
	{
        if(ObstacleCreator == null)
        {
            GD.PushError("Main obstacle factory cant be null");
        }
        
        if(Bundle == null)
        {
            GD.PushError("Bundle cant be null");
        }

 		return ObstacleCreator.Create(Bundle.GetRandomObstacle());
	}
}
