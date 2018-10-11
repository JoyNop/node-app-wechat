using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    public class SysPluginDAL: IBaseDAL
    {
        private SysPluginDAL() { }

        static SysPluginDAL dal = null;

        /// <summary>
        /// 单例访问
        /// </summary>
        public static SysPluginDAL Instance
        {
            get
            {
                if (dal == null)
                {
                    dal = new SysPluginDAL();
                }
                return dal;
            }
        }

        public override string ConnectionKey
        {
            get
            {
                return DbType.SysDatabase.ToString();
            }
        }

        public override List<BaseField> ListField
        {
            get
            {
                return new List<BaseField>();
            }
        }

        public override string TableName
        {
            get
            {
                return "SysPlugin";
            }
        }

        public override string SqlTable
        {
            get
            {
                return @"create table if not exists SysPlugin (ID integer NOT NULL PRIMARY KEY AUTOINCREMENT,SoftKey nvarchar(50),SoftId nvarchar(20),SavePath nvarchar(500),SoftName nvarchar(100),ImageListIndex nvarchar(20))";
            }
        }
    }
}
