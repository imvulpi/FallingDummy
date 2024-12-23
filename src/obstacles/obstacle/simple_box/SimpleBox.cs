using Godot;
using Godot.Collections;
using System;

namespace FallingDummy.src.obstacles.obstacle
{
    public partial class SimpleBox : Area2D, IObstacle
    {
        public event EventHandler AllowedSolution;
        public event EventHandler DisallowedSolution;
        [Export] public string ObstacleName { get; private set; } = "Simple Box";
        [Export] public string ObstacleID { get; private set; } = ObstacleConsts.SIMPLE_BOX_ID;
        [Export] public Array<ObstacleSolution> AllowedSolutions { get; private set; } = new Array<ObstacleSolution> {
        ObstacleSolution.TAP, ObstacleSolution.DOUBLE_TAP, ObstacleSolution.LONG_PRESS };
        [Export] public Array<ObstacleSolution> DisallowedSolutions { get; private set; } = new Array<ObstacleSolution> {
        ObstacleSolution.NOTHING };
        [Export] public Node ObstacleNode { get; set; }
        [Export] public Area2D ObstacleArea { get; set; }
        public bool Defeated { get; set; }
        [Export] public float Health { get; set; } = 1f;
        [Export] public float AttackStrength { get; set; } = 1f;
        [Export] public float Reward { get; set; } = 1f;

        public void InvokeAllowedSolution()
        {
            AllowedSolution?.Invoke(this, new EventArgs());
        }

        public void InvokeDisallowedSolution()
        {
            DisallowedSolution?.Invoke(this, new EventArgs());
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0f)
            {
                Defeated = true;
            }
        }
    }
}
