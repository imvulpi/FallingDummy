using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game.data.record
{
    public interface IRecord
    {
        public string Name { get; set; }
        public float Score { get; set; }
    }
}
