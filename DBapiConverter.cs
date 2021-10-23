using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    static class DBapiConverter
    {
        public static int DBHeaderEncode(FileStream fs, Database db)
        {
            db = new Database();
            byte[] identifier = BitConverter.GetBytes(DBHeader.IDENTIFIER);
            byte[] size = BitConverter.GetBytes(db.GetDBHeader().GetSize());
            int pointer = 0;
            fs.Write(identifier, 0, 2);
            pointer += identifier.Length;
            fs.Write(identifier, 0, 8);
            return pointer;
        }
        public static void DBEncode(FileStream fs, Database db)
        {
            int pointer = DBHeaderEncode(fs, db);
            List<Record> records = db.GetList();
            foreach (Record record in records)
            {
                DBEncodeAppend(fs, record);
            }
        }
        public static void DBEncodeAppend(FileStream fs, Record r)
        {
            byte[] id = BitConverter.GetBytes(r.GetRHeader().GetId());
            byte[] is_free = BitConverter.GetBytes(r.GetRHeader().IsFree());
            byte[] size = BitConverter.GetBytes(r.GetRHeader().GetSize());
            fs.Write(id, 0, 8);
            fs.Write(is_free, 0, 1);
            fs.Write(size, 0, 2);
            for (int i = 0; i < r.GetRHeader().GetSize(); ++i)
            {
                byte[] b = BitConverter.GetBytes(r[i]);
                fs.Write(size, 0, 1);
            }
        }
        public static void DatabaseDecode(FileStream fs, Database db)
        {
            int pointer = 0;
            byte[] identifier = new byte[2];
            byte[] size_ = new byte[8];
            fs.Read(identifier, 0, 2);
            pointer += 2;
            fs.Read(size_, 0, 8);
            pointer += 8;
            if (BitConverter.ToUInt16(identifier, 0) != DBHeader.IDENTIFIER)
            {
                return;
            }
            byte[] id = new byte[8];
            byte[] is_free = new byte[1];
            byte[] size = new byte[2];
            while (fs.Read(id, 0, 8) > 0)
            {
                fs.Read(is_free, 0, 1);
                fs.Read(size, 0, 2);
                byte[] bt = new byte[1];
                byte[] data = new byte[BitConverter.ToUInt16(size, 0)];
                for (int i = 0; i < BitConverter.ToUInt16(size, 0); ++i)
                {
                    fs.Read(bt, 0, 1);
                    data[i] = bt[0];
                }
                db.RIn(BitConverter.ToUInt64(id, 0), data);
            }
        }
        public static void Find()
        {

        }
        public static void FindFree()
        {

        }
    }
}
