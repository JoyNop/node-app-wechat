using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    public class BaseField
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 创建字段语句
        /// </summary>
        public string FieldSql { get; set; }
    }
}
