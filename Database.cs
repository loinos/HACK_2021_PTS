﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class Database
    {
        private DBHeader dbh;
        private List<Record> db;
        public Database()
        {
            dbh = new DBHeader(0);
        }
        public DBHeader GetDBHeader()
        {
            return dbh;
        }
        public void RIn(byte[] array) 
        {
            Record r = new Record(array);
            db.Add(r);
        }
        public void ROut() { }
        public Record GetLast()
        {
            return db[db.Count - 1];
        }
    }
}
