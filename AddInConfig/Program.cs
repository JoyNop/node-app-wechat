using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AddInConfigJson.Utility;


namespace AddInConfigJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            new AddInConfig().AddInConfigJson();
            new AddImage().ChangeImg("http://pic.maidiyun.com/c29mdC9sb2dv_224_100x100.png", "zToolbarLarge");

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == "chrome")
                {
                  // MessageBox.Show(process.Id.ToString());
                }
            }
            Console.ReadKey();
        }
    }
}