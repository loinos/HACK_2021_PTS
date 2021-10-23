using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
     class DBapi
    {
        private Database db;
        string path;
        public DBapi(string path)
        {
            this.path = path;
            db = new Database();
        }
        public void CreateNew()
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    DBapiConverter.DBHeaderEncode(fs, db);
                }
            }
            catch (Exception ex) { }
        }
        public void Open()
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    DBapiConverter.DatabaseDecode(fs, db);
                }
            }
            catch (Exception ex) { }
        }
        public void Add(byte[] array)
        {
            if (array.Length > 2000 || array.Length < 1)
            {
                return;
            }
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    Record r = new Record(db.GetDBHeader().GetSize(), array, true);
                    DBapiConverter.DBEncodeAppend(fs, r);
                    db.RIn(db.GetDBHeader().GetSize(), array);
                }
            }
            catch (Exception ex) { }
        }
        public static void Drop() { }

    }
}
