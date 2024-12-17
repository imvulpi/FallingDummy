using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.obstacles.obstacle
{
    public partial class ObstacleNode : CharacterBody2D
    {
        public event EventHandler DestroyEvent;

        [Export]
        public Area2D ObstacleArea { get; set; }
        public virtual float Damage { get; set; } = 1f;
        public virtual float Health { get; set; } = 1f;

        public override void _Ready()
        {
            ObstacleArea.BodyEntered += ObstacleArea_BodyEntered;
            ObstacleArea.InputEvent += ObstacleArea_InputEvent;
        }

        public virtual void ObstacleArea_InputEvent(Node viewport, InputEvent @event, long shapeIdx)
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

        public virtual void ObstacleArea_BodyEntered(Node2D body)
        {
            if(body is Dummy dummy)
            {
                dummy.DealDamage(Damage);
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                DestroyEvent.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Affects speed in implemented way
        /// </summary>
        /// <param name="speed">Current speed</param>
        /// <returns>Modified speed</returns>
        public virtual Vector2 AffectSpeed(Vector2 speed)
        {
            return speed;
        }

        /// <summary>
        /// Affects score in implemented way
        /// </summary>
        /// <param name="score">Some score</param>
        /// <returns>Modified score</returns>
        public virtual float AffectScore(float score) { 
            return score;
        }
    }
}
