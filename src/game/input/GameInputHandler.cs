using FallingDummy.src.character;
using FallingDummy.src.commons.animations;
using FallingDummy.src.obstacles.obstacle;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game.input
{
    public class GameInputHandler
    {
        public Window Window { get; set; }
        public SessionHandler SessionHandler { get; set; }
        /// <summary>
        /// Should Window handle the inputs of the most leading box?
        /// </summary>
        public bool WindowHandleLeadingBoxInputs { get; set; } = false;

        private ScreenDragHandler screenDragHandler = new ScreenDragHandler();

        public void Init()
        {
            Window.WindowInput += Window_WindowInput;
        }
        
        public void PlugArea2DInput(IObstacle obstacle, Area2D area)
        {
            area.InputEvent += (Node viewport, InputEvent @event, long shapeIdx) =>
            {
                ObstacleSolution? solutionOrNull = GetSolutionBox(@event);
                CheckObstacleSolutions(obstacle, solutionOrNull);
            };

            area.BodyEntered += (Node2D body) =>
            {
                GD.Print("Body entered!");
                if (body is IDummy dummy)
                {
                    GD.Print("Dummy inside Area2D");
                    if (obstacle.DisallowedSolutions.Contains(ObstacleSolution.NOTHING))
                    {
                        dummy.TakeDamage(obstacle.AttackStrength);
                        obstacle.InvokeDisallowedSolution();
                    }
                }
            };
        }
        ObstacleSolution? GetSolutionBox(InputEvent inputEvent) {
            if (inputEvent == null) return null;

            if (inputEvent is InputEventMouseButton mouse)
            {
                return GetMouseSolution(mouse);
            }
            else if (inputEvent is InputEventScreenTouch screenTouch && inputEvent.IsPressed())
            {
                return GetScreenTouchSolution(screenTouch);
            }else if (inputEvent is InputEventScreenDrag screenDrag)
            {
                return GetScreenDragSolution(screenDrag);
            }

            return null;
        }
        ObstacleSolution? GetMouseSolution(InputEventMouseButton mouse)
        {
            if (mouse.ButtonIndex == MouseButton.Left || mouse.ButtonIndex == MouseButton.Right)
            {
                if (mouse.DoubleClick) return ObstacleSolution.DOUBLE_TAP;
                return ObstacleSolution.TAP;
            }

            return null;
        }

        ObstacleSolution? GetScreenTouchSolution(InputEventScreenTouch screenTouch)
        {
            if (screenTouch.DoubleTap) return ObstacleSolution.DOUBLE_TAP;
            return ObstacleSolution.TAP;
        }

        ObstacleSolution? GetScreenDragSolution(InputEventScreenDrag screenDrag)
        {
            Vector2 dragDirection = screenDragHandler.GetDragDirection(screenDrag);
            if(dragDirection == Vector2.Left)
            {
                return ObstacleSolution.SWIPE_LEFT;
            }else if(dragDirection == Vector2.Right)
            {
                return ObstacleSolution.SWIPE_RIGHT;
            }else if(dragDirection == Vector2.Down)
            {
                return ObstacleSolution.SWIPE_DOWN;
            }else if(dragDirection == Vector2.Up)
            {
                return ObstacleSolution.SWIPE_UP;
            }

            return null;
        }

        private void Window_WindowInput(InputEvent @event)
        {
            if (@event == null) return;
            if (WindowHandleLeadingBoxInputs) {
                ObstacleSolution? solution = GetSolutionBox(@event);
                CheckObstacleSolutions(SessionHandler.GetLeadingObstacle(), solution);
            }
            var accelerometer = Input.GetAccelerometer();
            var gyroscope = Input.GetGyroscope();
        }

        private void CheckObstacleSolutions(IObstacle obstacle, ObstacleSolution? solution)
        {
            if(solution == null) return;
            if (obstacle == null) return;

            ObstacleSolution realSolution = (ObstacleSolution)solution;
            if (obstacle.AllowedSolutions.Contains(realSolution))
            {
                obstacle.InvokeAllowedSolution();
            }
            else if (obstacle.DisallowedSolutions.Contains(realSolution))
            {
                obstacle.InvokeDisallowedSolution();
            } // Else solution is neutral (neither allowed or disallowed)
        }
    }
}
