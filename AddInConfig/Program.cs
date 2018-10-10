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
            sys.BaseFile = "1123";
            sys.IsOld = true;
            sys.Name = "2341111";
            sys.SavePath = "3423";
            sys.SoftId = 39;
            sys.SoftKey = "c34234";
            sys.SoftSize = "22344";
            sys.Version = "234234";

            Plugin.AddPlugin(sys);
            int a=Plugin.GetPluginList().Count;

            Console.ReadKey();
        }
    }
}