using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class RHeader
    {
        ulong hash;
        ushort size;
        public RHeader(ulong hash, ushort size)
        {
            this.size = size;
            this.hash = hash;
        }

    }
}
