using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using AddInConfigJson.Utility;


namespace AddInConfigJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            new AddInConfig().AddInConfigJson();
            new AddImage().ChangeImg("http://pic.maidiyun.com/c29mdC9sb2dv_1_70x70.png", "zToolbarLarge");
             

            Console.ReadKey();
        }
    }
}