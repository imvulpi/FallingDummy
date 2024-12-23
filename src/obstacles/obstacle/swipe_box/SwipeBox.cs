using FallingDummy.src.obstacles.obstacle;
using Godot;
using Godot.Collections;
using System;

public partial class SwipeBox : Area2D, IObstacle
{
    [Export] public string ObstacleName { get; private set; } = "Swipe Box";
    [Export] public string ObstacleID { get; private set; } = ObstacleConsts.SWIPE_BOX_ID;
    [Export] public Array<ObstacleSolution> AllowedSolutions { get; private set; } = new Array<ObstacleSolution>
    {
        ObstacleSolution.SWIPE_X, ObstacleSolution.SWIPE_LEFT, ObstacleSolution.SWIPE_RIGHT,
    };
    [Export] public Array<ObstacleSolution> DisallowedSolutions { get; private set; } = new Array<ObstacleSolution>
    {
        ObstacleSolution.NOTHING,
    };
    [Export] public Node ObstacleNode { get; set; }
    [Export] public Area2D ObstacleArea { get; set; }
    public bool Defeated { get; set; }
    [Export] public float Reward { get; set; } = 1f;
    [Export] public float Health { get; private set; } = 1f;
    [Export] public float AttackStrength { get; private set; } = 1f;

    public event EventHandler AllowedSolution;
    public event EventHandler DisallowedSolution;

    public void InvokeAllowedSolution()
    {
        AllowedSolution.Invoke(this, EventArgs.Empty);
    }

    public void InvokeDisallowedSolution()
    {
        DisallowedSolution.Invoke(this, EventArgs.Empty);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Defeated = true;
        }
    }
}
