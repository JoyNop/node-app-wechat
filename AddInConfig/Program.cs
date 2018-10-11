using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;


namespace AddInConfigJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");

            SysPluginModel sys = new SysPluginModel();
            sys.SoftName = "1123";
            sys.ImageListIndex = 23;

            sys.SavePath = "3423";
            sys.SoftId = 39;
            sys.SoftKey = "c34234";



            Plugin.AddPlugin(sys);
            int a = Plugin.GetPluginList().Count;

            Console.ReadKey();
        }
    }
}