using System;
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
            sys.BaseFile = "1";
            sys.IsOld = true;
            sys.Name = "234";
            sys.SavePath = "34";
            sys.SoftId = 3444444;
            sys.SoftKey = "c34";
            sys.SoftSize = "234";
            sys.Version = "2.34";

            Plugin.AddPlugin(sys);


            Console.ReadKey();
        }
    }
}