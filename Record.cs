using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class Record
    {
        byte[] data;
        ushort fill;

        public const ushort HEADER = 14;
        public Record(byte[] data)
        {
            this.data = data;
        }
        public byte[] GetData()
        {
            return data;
        }
    }
};
}
