using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game
{
    public partial class GameHUDController : Node
    {
        [Export] public Label ScoreLabel = new Label();
        [Export] public Label DistanceLabel = new Label();

        public void UpdateScore(float newScore)
        {
            ScoreLabel.Text = $"Score: {newScore}";
        }

        public void UpdateDistance(float newDistance)
        {
            ScoreLabel.Text = $"{newDistance}m";
        }
    }
}
