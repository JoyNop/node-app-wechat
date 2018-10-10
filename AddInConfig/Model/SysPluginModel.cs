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
        /// 插件版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 存储路径
        /// </summary>
        public string SavePath { get; set; }
        /// <summary>
        /// 执行文件
        /// </summary>
        public string BaseFile { get; set; }
        #endregion


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 插件ID(无需存到本地数据库)
        /// </summary>
        public int SoftId { get; set; }
        public string SoftSize { get; set; }
        /// <summary>
        /// 是否为旧版设计宝插件
        /// </summary>
        public bool IsOld { get; set; }
    }
}
