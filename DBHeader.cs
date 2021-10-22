using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class DBHeader
    {
        public const ushort IDENTIFIER = 24565;
        private ulong size;
        public DBHeader(ulong size)
        {
            this.size = size;
        }
        public static ushort GetIdentifier()
        {
            return IDENTIFIER;
        }
        public ulong GetSize()
        {
            return size;
        }
    }
}
