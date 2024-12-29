using FallingDummy.src.commons.io;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FallingDummy.src.game.data.record
{
    public class RecordLoader : IRecordLoader
    {
        public RecordList Load(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                return new RecordList(new Dictionary<string, float>());
            }
            string recordListString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<RecordList>(recordListString);    
        }
    }
}
