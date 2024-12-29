using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.commons.io
{
    public class FileWriter : IFileWriter
    {
        public void Write(string path, string data)
        {
            File.WriteAllText(path, data);
        }

        public void Write(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }
    }
}
