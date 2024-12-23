using FallingDummy.src.commons.interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace FallingDummy.src.obstacles.obstacle
{
    public interface IObstacle : IHealth, IAttack, IDamage
    {
        event EventHandler AllowedSolution;
        event EventHandler DisallowedSolution;
        public string ObstacleName { get; }
        public string ObstacleID { get; }
        public Godot.Collections.Array<ObstacleSolution> AllowedSolutions { get; }
        public Godot.Collections.Array<ObstacleSolution> DisallowedSolutions { get; }
        public Node ObstacleNode { get; set; }
        public Area2D ObstacleArea { get; set; }
        public bool Defeated { get; set; }
        public float Reward { get; set; }
        public void InvokeAllowedSolution();
        public void InvokeDisallowedSolution();
    }
}
