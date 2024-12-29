using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.game.data.record
{
    public interface IRecordLoader
    {
        RecordList Load(string path);
    }
}
