using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    public class DirManage
    {
        private DirManage() { }

        static DirManage dirManage = null;

        public static DirManage Instance
        {
            get
            {
                if (dirManage == null)
                {
                    dirManage = new DirManage();
                }
                return dirManage;
            }
        }

        /// <summary>
        /// 安全创建目录
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public string CreateSafeDir(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                if (!dirInfo.Exists)
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;
            }
            catch (Exception)
            {
            }
            return "";
        }
 
 
 
 
    }
}
