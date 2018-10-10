using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    public class Plugin
    {
        /// <summary>
        /// 查询插件插件信息
        /// </summary>
        /// <param name="SoftKey"></param>
        /// <returns></returns>
        public static SysPluginModel QueryPlugin(string SoftKey)
        {
            SysPluginModel model = new SysPluginModel();
            SysPluginDAL dal = SysPluginDAL.Instance;
            DataTable dt = dal.GetList("", new SearchParameter("SoftKey", SoftKey)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                model.SoftKey = SoftKey;
                model.SavePath = dt.Rows[0]["SavePath"].ToString();
                model.Version = dt.Rows[0]["Version"].ToString();
                model.BaseFile = dt.Rows[0]["BaseFile"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取插件列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<SysPluginModel> GetPluginList()
        {
            List<SysPluginModel> pluginList = new List<SysPluginModel>();
            SysPluginDAL dal = SysPluginDAL.Instance;
            DataTable dt = dal.GetList().Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SysPluginModel model = new SysPluginModel();
                    model.SoftKey = dt.Rows[i]["SoftKey"].ToString();
                    model.SavePath = dt.Rows[i]["SavePath"].ToString();
                    model.Version = dt.Rows[i]["Version"].ToString();
                    model.BaseFile = dt.Rows[i]["BaseFile"].ToString();
                    pluginList.Add(model);
                }
            }

            return pluginList;
        }

        /// <summary>
        /// 添加或更新插件
        /// </summary>
        /// <returns></returns>
        public static bool AddPlugin(SysPluginModel pluginModel)
        {
            SysPluginDAL dal = SysPluginDAL.Instance;
            List<Col> listcol = new List<Col>();
            listcol.Add(new Col(nameof(pluginModel.SoftKey), pluginModel.SoftKey));
            listcol.Add(new Col(nameof(pluginModel.Version), pluginModel.Version));
            listcol.Add(new Col(nameof(pluginModel.SavePath), pluginModel.SavePath));
            listcol.Add(new Col(nameof(pluginModel.BaseFile), pluginModel.BaseFile));

            if (dal.GetList(null, new SearchParameter("SoftKey", pluginModel.SoftKey)).Tables[0].Rows.Count > 0)
            {
                return dal.Update(listcol, new SearchParameter("SoftKey", pluginModel.SoftKey)) > 0;
            }
            else
            {
                return dal.Add(listcol) > 0;
            }

        }

        /// <summary>
        /// 删除插件
        /// </summary>
        /// <param name="pluginModel"></param>
        /// <returns></returns>
        public static bool DeletePlugin(string softKey)
        {
            SysPluginDAL dal = SysPluginDAL.Instance;
            return dal.Delete(new SearchParameter("SoftKey", softKey)) > 0;
        }
    }
}
