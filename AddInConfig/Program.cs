using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AddInConfigJson
{
    class Program
    {
        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //创建一个绝对路径
            string SavePath = AppDomain.CurrentDomain.BaseDirectory;
            WritingJson(SavePath, 2, "name", "tooltip", 23, "hint", true, false, 2);
            //ReadingJson(desktopPath);
            Console.ReadKey();
        }


        #region Json转换

        /// <summary>
        /// 读取json
        /// </summary>
        /// <param name="desktopPath"></param>
        private static void ReadingJson(string desktopPath)
        {
            string myStr = null;
            //IO读取
            myStr = GetMyJson(desktopPath);
            //转换
            var jArray = JsonConvert.DeserializeObject<List<AddInConfig>>(myStr);
            //进一步的转换我就不写啦
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="desktopPath"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="ToolTip"></param>
        /// <param name="ImageListIndex"></param>
        /// <param name="HintString"></param>
        /// <param name="IsMenu"></param>
        /// <param name="IsButton"></param>
        /// <param name="ButtonIndex"></param>
        /// <param name="iszj"></param>
        private static void WritingJson(string SavePath, int Id, string Name, string ToolTip, int ImageListIndex,
            string HintString, bool IsMenu, bool IsButton, int ButtonIndex)
        {
            //创建用户集合
            List<AddInConfig> addInConfig = new List<AddInConfig>();
            AddInConfig AddAllConfig = new AddInConfig();
            //将添加内容

            AddAllConfig.ID = Id;
            AddAllConfig.Name = Name;
            AddAllConfig.ToolTip = ToolTip;
            AddAllConfig.ImageListIndex = ImageListIndex;
            AddAllConfig.HintString = HintString;
            AddAllConfig.IsMenu = IsMenu;
            AddAllConfig.IsButton = IsButton;
            AddAllConfig.ButtonIndex = ButtonIndex;

            addInConfig.Add(AddAllConfig);

            //转成json
            string json = JsonConvert.SerializeObject(addInConfig, Formatting.Indented);
            //保存到桌面的文件
            SaveMyJson(SavePath, json);
        }

        #endregion


        #region IO读写

        /// <summary>
        /// IO读取本地json
        /// </summary>
        /// <param name="SavePath"></param>
        /// <returns></returns>
        private static string GetMyJson(string SavePath)
        {
            using (FileStream fsRead = new FileStream(string.Format("{0}\\app.json", SavePath), FileMode.Open))
            {
                //读取加转换
                int fsLen = (int) fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                return System.Text.Encoding.UTF8.GetString(heByte);
            }
        }

        /// <summary>
        ///  将json保存到本地
        /// </summary>
        /// <param name="SavePath"></param>
        /// <param name="json"></param>
        private static void SaveMyJson(string SavePath, string json)
        {
            using (FileStream fs = new FileStream(string.Format("{0}\\app.json", SavePath), FileMode.Append))
            {
                //写入
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(json);
                }
            }
        }

        #endregion
    }
}