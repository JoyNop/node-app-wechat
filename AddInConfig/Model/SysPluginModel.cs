using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    public class SysPluginModel
    {
        #region 本地数据库字段

        /// <summary>
        /// 插件编号
        /// </summary>
        public string SoftKey { get; set; }

        /// <summary>
        /// 插件ID 
        /// </summary>
        public int SoftId { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 执行文件名
        /// </summary>
        public string SoftName { get; set; }

        /// <summary>
        /// 图片顺序
        /// </summary>
        public int ImageListIndex { get; set; }

        #endregion
    }
}