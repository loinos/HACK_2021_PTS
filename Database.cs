using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class Database
    {
        List<Record> data;
        public const ushort IDENTIFICATION = 24565;
        public const ushort HEADER = 12;
        
        Database()
        {
            data = new List<Record>();
        }
    };
}
