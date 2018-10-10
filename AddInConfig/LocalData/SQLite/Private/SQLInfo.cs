using System.Data.SQLite;

namespace AddInConfigJson
{
    public class SQLInfo
    {
        /// <summary>
        /// SQL SERVER命令类
        /// </summary>
        public SQLInfo(string sql, SQLiteParameter[] parameters)
        {
            
            this.SqlString = sql;
            this.SqlParameters = parameters;
        }

        /// <summary>
        /// SQL执行语句
        /// </summary>
        public string SqlString
        {
            get;
            set;
        }

        /// <summary>
        /// SQL执行语句的参数
        /// </summary>
        public SQLiteParameter[] SqlParameters
        {
            get;
            set;
        }
    }
}
