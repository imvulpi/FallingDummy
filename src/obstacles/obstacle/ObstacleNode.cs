using Godot;
using Godot.Collections;
using System;

namespace FallingDummy.src.obstacles.obstacle
{
    public partial class ObstacleNode : CharacterBody2D
    {
        public event EventHandler DestroyEvent;

        [Export]
        public Area2D ObstacleArea { get; set; }
        public virtual float Damage { get; set; } = 1f;
        public virtual float Health { get; set; } = 1f;
        public virtual float ScoreReward {  get; set; } = 1f;

        public override void _Ready()
        {
            ObstacleArea.AreaEntered += ObstacleArea_AreaEntered;
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

        public virtual void ObstacleArea_AreaEntered(Area2D body)
        {
            if(body is Dummy dummy)
            {
                dummy.DealDamage(Damage);
                DestroyEvent.Invoke(this, new EventArgs());
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

        public Vector2 GetSize()
        {
            Array<Node> nodes = ObstacleArea.GetChildren();
            foreach (Node node in nodes)
            {
                if(node is CollisionShape2D cs2d)
                {
                    Shape2D shape = cs2d.Shape;
                    if (shape is RectangleShape2D rectangleShape)
                    {
                        return rectangleShape.Size;
                    }
                    else if (shape is CircleShape2D circleShape)
                    {
                        return new Vector2(circleShape.Radius, circleShape.Radius);
                    }else if(shape is CapsuleShape2D capsuleShape)
                    {
                        return new Vector2(capsuleShape.Radius, capsuleShape.Height);
                    }
                    else
                    {
                        GD.PrintErr("Shape not supported in GetSize");
                    }
                }
            }
            return new Vector2(512,512);
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
