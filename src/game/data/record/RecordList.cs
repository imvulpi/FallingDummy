using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game.data.record
{
    public class RecordList
    {
        public Dictionary<string, float> Records { get; set; }

        public RecordList(Dictionary<string, float> records)
        {
            Records = records;
        }
    }
}
