using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HACK_PTS
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            string path = "C://test.ff";
            DBapi abapi = new DBapi(path);
            abapi.CreateNew();
            byte[] a = new byte[100];
            Random r = new Random();
            byte[] b = new byte[100];
            r.NextBytes(b);
            abapi.Add(b);
            DBapi abapi2 = new DBapi(path);
            abapi2.Open();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
