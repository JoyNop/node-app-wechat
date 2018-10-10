using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AddInConfigJson
{
    /// <summary>
    /// 全局委托
    /// </summary>
    public class GlobalDelegate
    {
        private GlobalDelegate()
        {
        }

        static GlobalDelegate global = null;

        public static GlobalDelegate Instance
        {
            get
            {
                if (global == null)
                {
                    global = new GlobalDelegate();
                }

                return global;
            }
        }


        public delegate void OpenPluginEventHandler(SysPluginModel pluginModel);

        /// <summary>
        /// 打开插件操作，1详情页面0不是
        /// </summary>
        public event OpenPluginEventHandler OpenPluginEvent;

        /// <summary>
        /// 打开插件操作，1详情页面0不是
        /// </summary>
        /// <param name="softKey"></param>
        public void OpenPlugin(SysPluginModel pluginModel)
        {
            OpenPluginEvent?.Invoke(pluginModel);
        }


        public delegate void DownloadPluginEventHanlder(SysPluginModel pluginModel);

        public event DownloadPluginEventHanlder DownloadPluginEvent;

        /// <summary>
        /// 插件下载
        /// </summary>
        /// <param name="pluginModel"></param>
        public void DownloadPlugin(SysPluginModel pluginModel)
        {
            DownloadPluginEvent?.Invoke(pluginModel);
        }

        public delegate string[] LocalPluginExistsEventHandler(string softKey);

        /// <summary>
        /// 判断本地插件是否存在
        /// </summary>
        public event LocalPluginExistsEventHandler LocalPluginExistsEvent;

        /// <summary>
        /// 判断本地插件是否存在
        /// </summary>
        /// <param name="softKey"></param>
        /// <returns>[结果,插件对象]</returns>
        public string[] LocalPluginExists(string softKey)
        {
            return LocalPluginExistsEvent?.Invoke(softKey);
        }

        public delegate void RefreshPluginEventHandler();

        /// <summary>
        /// 通知刷新插件列表
        /// </summary>
        public event RefreshPluginEventHandler RefreshPluginEvent;

        /// <summary>
        /// 通知刷新插件列表
        /// </summary>
        public void RefreshPlugin()
        {
            RefreshPluginEvent?.Invoke();
        }
    }
}