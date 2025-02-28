﻿using FallingDummy.src.commons;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

namespace FallingDummy.src.obstacles.obstacle.simple_box
{
    public partial class TntBox : Node2D, IObstacle
    {
        public event EventHandler AllowedSolution;
        public event EventHandler DisallowedSolution;
        [Export] public string ObstacleID { get; private set; } = ObstacleConsts.TNT_BOX_ID;
        [Export] public string ObstacleName { get; private set; } = "Tnt Box";
        [Export] public Array<ObstacleSolution> AllowedSolutions { get; private set; } = new Array<ObstacleSolution> {
            ObstacleSolution.NOTHING };
        [Export] public Array<ObstacleSolution> DisallowedSolutions { get; private set; } = new Array<ObstacleSolution> {
            ObstacleSolution.TAP, ObstacleSolution.DOUBLE_TAP, ObstacleSolution.LONG_PRESS };
        [Export] public Node ObstacleNode { get; set; }
        [Export] public Area2D ObstacleArea { get; set; }
        public bool Defeated {  get; set; } 
        [Export] public float Health { get; set; } = 1f;
        [Export] public float Reward { get; set; } = 1f;
        [Export] public float AttackStrength { get; set; } = 1f;

        public void InvokeAllowedSolution()
        {
            AllowedSolution.Invoke(this, new EventArgs());
        }

        public void InvokeDisallowedSolution()
        {
            DisallowedSolution.Invoke(this, new EventArgs());
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
