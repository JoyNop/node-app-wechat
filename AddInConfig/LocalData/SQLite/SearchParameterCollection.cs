using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace AddInConfigJson
{
    /// <summary>
    /// 查询参数集合
    /// </summary>
    public static class SearchParameterCollection
    {
        /// <summary>
        /// 将查询参数列表转为查询语句和参数值
        /// </summary>
        public static string ToString(this SearchParameter[] searchs, ref SQLiteParameter[] parameters, bool needWhere = true)
        {
            if (searchs == null || searchs.Length == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            List<SQLiteParameter> listParameters = new List<SQLiteParameter>();
            int i = 1;

            if (parameters != null && parameters.Length > 0)
            {
                listParameters.AddRange(parameters);
            }
            foreach (SearchParameter search in searchs)
            {
                if (search.HasLeftParen)
                {
                    sb.Append("(");
                }
                if (!string.IsNullOrWhiteSpace(search.TableName))
                {
                    sb.Append(search.TableName + ".");
                }
                switch (search.OperateChar)
                {
                    case OperateChar.No:
                        sb.Append(search.Field + "!=@" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                    case OperateChar.gt:
                        sb.Append(search.Field + ">@" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                    case OperateChar.lt:
                        sb.Append(search.Field + "<@" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                    case OperateChar.ge:
                        sb.Append(search.Field + ">=@" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                    case OperateChar.le:
                        sb.Append(search.Field + "<=@" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                    case OperateChar.lk:
                        sb.Append(search.Field + " Like @" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, "%" + search.Value + "%"));
                        break;
                    case OperateChar.RegExp:
                        sb.Append(search.Field + " Regexp @" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                    case OperateChar.Null:
                        sb.Append(search.Field + " Is Null");
                        break;
                    case OperateChar.NotNull:
                        sb.Append(search.Field + " Is Not Null");
                        break;
                    case OperateChar.In:
                        sb.Append(search.Field + " In (" + search.Value + ")");
                        break;
                    case OperateChar.NotIn://20150727增加 not in查询条件
                        sb.Append(search.Field + " NOT In (" + search.Value + ")");
                        break;
                    default:
                        sb.Append(search.Field + "=@" + search.Field + i);
                        listParameters.Add(new SQLiteParameter(search.Field + i, search.Value));
                        break;
                }
                if (search.HasRightParen)
                {
                    sb.Append(")");
                }
                if (i < searchs.Length)
                {
                    sb.Append(" " + search.LogicChar + " ");
                }
                i++;
            }
            parameters = listParameters.ToArray();
            return (needWhere ? " where " : " ") + sb.ToString();
        }
    }
}
