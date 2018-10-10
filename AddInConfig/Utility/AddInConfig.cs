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
    class AddInConfig
    {
        private void AddInConfigJson()
        {
            //创建一个绝对路径
            string SavePath = AppDomain.CurrentDomain.BaseDirectory;

            WritingJson(SavePath, 2, "name", "tooltip", 23, "hint", true, false, 2, true, 2);
            //ReadingJson(desktopPath);
        }


        #region Json转换

        /// <summary>
        /// 添加内容
        /// </summary>
        private static void WritingJson(string SavePath, int Id, string Name, string ToolTip, int ImageListIndex,
            string HintString, bool IsMenu, bool IsButton, int ButtonIndex, bool isChild, int SearchID)
        {
            //创建用户集合
            List<AddInConfigModel> addInConfig = new List<AddInConfigModel>();
            AddInConfigModel AddAllConfig = new AddInConfigModel();

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

            //如果指定目录有文件并且正常打开，直接追加，如果不能打开，新建json文件
            try
            {
                //IO读取
                string JsonStr = GetMyJson(SavePath);

                //转换
                var jArray = JsonConvert.DeserializeObject<List<AddInConfigModel>>(JsonStr);

                if (isChild)
                {
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        if (jArray[i].ID == SearchID)
                        {
                            jArray[i].SubItems.Add(AddAllConfig);
                        }
                    }
                }
                else
                {
                    jArray.Add(AddAllConfig);
                }

                //转成json
                string json = JsonConvert.SerializeObject(jArray, Formatting.Indented);

                //保存到文件
                SaveMyJson(SavePath, json);
            }
            catch (Exception e)
            {
                //转成json
                string json = JsonConvert.SerializeObject(addInConfig, Formatting.Indented);

                //保存到文件
                SaveMyJson(SavePath, json);
            }

            //ReadingJson(SavePath);
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
            using (FileStream fs = new FileStream(string.Format("{0}\\app.json", SavePath), FileMode.Create,
                FileAccess.Write))
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