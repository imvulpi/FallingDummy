using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game.data.record
{
    internal class Record : IRecord
    {
        public Record(string name, float score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; set; }
        public float Score { get; set; } = 0.0f;
    }
}
