using FallingDummy.src.commons;
using FallingDummy.src.commons.io;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FallingDummy.src.game.data.record
{
    public class RecordSaver : IRecordSaver
    {
        public RecordSaver() { }

        private readonly IFileWriter FileWriter = new FileWriter();
        private readonly IRecordLoader RecordLoader = new RecordLoader();
        public void Save(IRecord record, string path)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record), "Record cannot be null!");
            }

            RecordList recordList = GetRecordList(path);

            if (recordList.Records.TryGetValue(record.Name, out float currentScore))
            {
                if (record.Score > currentScore)
                {
                    recordList.Records[record.Name] = record.Score;
                }
                else
                {
                    return;
                }
            }
            else
            {
                recordList.Records[record.Name] = record.Score;
            }

            string scoreData = JsonSerializer.Serialize(recordList);
            FileWriter.Write(path, scoreData);
        }

        private RecordList GetRecordList(string path)
        {
            try
            {
                return RecordLoader.Load(path);
            }
            catch (Exception _ex)
            {
                RecordList newRecordList = new RecordList(new Dictionary<string, float>());
                return newRecordList;
            }
        }
    }
}
