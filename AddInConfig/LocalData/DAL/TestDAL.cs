using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    public class TestDAL : IBaseDAL
    {
        private TestDAL() { }

        static TestDAL dal = null;

        /// <summary>
        /// 单例访问
        /// </summary>
        public static TestDAL Instance
        {
            get
            {
                if (dal == null)
                {
                    dal = new TestDAL();
                }
                return dal;
            }
        }



        public override string ConnectionKey
        {
            get
            {
                return "连接Key";
            }
        }

        /// <summary>
        /// 每次更新以后需要添加的字段
        /// </summary>
        public override List<BaseField> ListField
        {
            get
            {
                List<BaseField> listfield = new List<BaseField>();
                listfield.Add(new BaseField() { FieldName = "字段名称", FieldSql = "alter table 表名称 add Column 字段名称 字段类型" });
                return listfield;
            }
        }

        public override string TableName
        {
            get
            {
                return "表名称";
            }
        }

        /// <summary>
        /// 表结构字段必须完整
        /// </summary>
        public override string SqlTable
        {
            get
            {
                return "create table if not exists 表名称 (字段名称 字段类型,...)";
            }
        }
    }
}
