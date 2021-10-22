using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class Record
    {
        private RHeader rh;
        byte[] data;
        public Record(byte[] data)
        {
            rh = new RHeader(sizeof(ulong) - 1, (ushort)data.Length);
            this.data = data;
        }
        public RHeader GetRHeader()
        {
            return rh;
        }
    }
}
