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
        bool is_free;
        ushort size;
        public RHeader(ulong id, bool is_free, ushort size)
        {
            this.size = size;
            this.id = id;
            this.is_free = is_free;
        }
        public ulong GetId()
        {
            return id;
        }
        public ushort GetSize()
        {
            return size;
        }
        public bool IsFree()
        {
            return is_free;
        }
    }
}
