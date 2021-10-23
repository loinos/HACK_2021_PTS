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
        public Record(ulong id, byte[] data, bool is_free)
        {
            rh = new RHeader(id, is_free, (ushort)data.Length);
            this.data = data;
        }
        public RHeader GetRHeader()
        {
            return rh;
        }
        public byte this[int index]
        {
            get
            {
                return data[index];
            }
        }
}
}
