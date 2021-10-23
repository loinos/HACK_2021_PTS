using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    static class DBapi
    {
        static Database db;
        public static void CreateNew(string path, string name)
        {
            path = "C://t.ff";
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    db = new Database();
                    DBHeader dbh = db.GetDBHeader();
                    writer.Write(DBHeader.GetIdentifier());
                    writer.Write(dbh.GetSize());
                }
            } catch (Exception ex) { }
        }
        public static void Open(string path)
        {
            path = "C://t.ff";
            try
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
                {
                    if (reader.ReadInt16() != DBHeader.GetIdentifier())
                    {
                        return;
                    }
                    ulong size = reader.ReadUInt64();
                    Database db = new Database();
                    DBHeader dbh = new DBHeader(size);
                }
            }
            catch (Exception ex) { }
        }
        public static void Add(string path, byte[] array)
        {
            if (array.Length > 2000 || array.Length < 1)
            {
                return;
            }
            path = "C://t.ff";
            try
            {
                using (FileStream fstream = new FileStream(path, FileMode.Open))
                {
                    byte[] idefeidentifier = new byte[2];
                    fstream.Read(idefeidentifier, 0, 2);
                    if (BitConverter.ToUInt16(idefeidentifier, 0) != DBHeader.IDENTIFIER)
                    {
                        throw new Exception("is not db file");
                    }
                    byte[] size_b = new byte[8];
                    fstream.Read(idefeidentifier, 1, 8);
                    ulong size = BitConverter.ToUInt64(idefeidentifier, 0);
                    fstream.Seek(0, SeekOrigin.End);
                    db.RIn(array);
                    RHeader rh = db.GetLast().GetRHeader();
                    byte[] b = rh.
                    fstream.Write(array, 0, array.Length);
                    fstream.Write(array, 0, array.Length);
                    fstream.Write(array, 0, array.Length);
                }
            }
            catch (Exception ex) { }
        }
        public static void Drop() { }

    }
}
