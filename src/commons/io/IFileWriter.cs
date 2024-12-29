using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.commons.io
{
    public interface IFileWriter
    {
        void Write(string path, string data);
        void Write(string path, byte[] data);
    }
}
