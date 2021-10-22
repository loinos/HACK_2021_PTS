using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class RHeader
    {
        ulong id;
        ushort size;
        byte[] data;
        public RHeader(ushort size)
        {
            this.size = size;
        }
    }
}
