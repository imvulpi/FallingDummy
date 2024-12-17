﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.obstacle.simple_box
{
    public partial class TntBox : ObstacleNode, IObstacle
    {
        public string ID { get; } = ObstaclePaths.TNT_BOX_ID;
        public ObstacleOvercoming ObstacleOvercoming { get; } = ObstacleOvercoming.NOTHING;
        string IObstacle.Name { get; } = "Tnt Box";

        public override void ObstacleArea_InputEvent(Node viewport, InputEvent @event, long shapeIdx)
        {
            if (@event is InputEventScreenTouch screenTouch)
            {
                TakeDamage(1f);
            }
            else if (@event is InputEventMouseButton mouseButton)
            {
                if (mouseButton.ButtonMask == MouseButtonMask.Left)
                {
                    TakeDamage(1f);
                }
            }
        }

        public override void ObstacleArea_BodyEntered(Node2D body)
        {
            return;
        }
    }
}
