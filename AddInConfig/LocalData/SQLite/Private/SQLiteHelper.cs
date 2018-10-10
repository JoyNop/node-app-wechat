using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace AddInConfigJson
{
    public static class SQLiteHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get;
            set;
        }
        #region 执行SQL语句

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        public static int Execute(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Execute(conn, sql, null);
            }
        }
        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        public static int Execute(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Execute(conn, sql, parameters);
            }
        }
        /// <summary>
        /// 用另外一个连接执行SQL语句
        /// </summary>
        public static int Execute(string connectionString, string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Execute(conn, sql, null);
            }
        }
        /// <summary>
        /// 用另外一个连接执行带参数的SQL语句
        /// </summary>
        public static int Execute(string connectionString, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Execute(conn, sql, parameters);
            }
        }
        /// <summary>
        /// 执行指定连接的数据库操作
        /// </summary>
        public static int Execute(SQLiteConnection conn, string sql)
        {
            return Execute(conn, sql, null);
        }
        /// <summary>
        /// 执行指定连接的数据库操作
        /// </summary>
        public static int Execute(SQLiteConnection conn, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, null, conn, sql, parameters);
                int result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return result;
            }
        }
        #endregion
        #region 批量执行SQL语句
        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        public static void ExecuteList(List<string> sqllist)
        {
            if (sqllist == null || sqllist.Count == 0)
                return;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                ExecuteList(conn, sqllist);
            }
        }
        /// <summary>
        /// 批量执行带参数的SQL语句
        /// </summary>
        public static void ExecuteList(List<SQLInfo> sqllist)
        {
            if (sqllist == null || sqllist.Count == 0)
                return;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                ExecuteList(conn, sqllist);
            }
        }
        /// <summary>
        /// 用其他连接字符串批量执行SQL语句
        /// </summary>
        public static void ExecuteList(string connectionString, List<string> sqllist)
        {
            if (sqllist == null || sqllist.Count == 0)
                return;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                ExecuteList(conn, sqllist);
            }
        }
        /// <summary>
        /// 用其他连接字符串批量执行带参数的SQL语句
        /// </summary>
        public static void ExecuteList(string connectionString, List<SQLInfo> sqllist)
        {
            if (sqllist == null || sqllist.Count == 0)
                return;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                ExecuteList(conn, sqllist);
            }
        }
        /// <summary>
        /// 用其他连接字符串批量执行SQL语句
        /// </summary>
        public static void ExecuteList(SQLiteConnection conn, List<string> sqllist)
        {
            if (sqllist == null || sqllist.Count == 0)
                return;
            conn.Open();
            SQLiteTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (string sql in sqllist)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        PrepareCommand(cmd, trans, conn, sql, null);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 用其他连接字符串批量执行带参数的SQL语句
        /// </summary>
        public static void ExecuteList(SQLiteConnection conn, List<SQLInfo> sqllist)
        {
            if (sqllist == null || sqllist.Count == 0)
                return;
            conn.Open();
            SQLiteTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (SQLInfo sql in sqllist)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        PrepareCommand(cmd, trans, conn, sql.SqlString, sql.SqlParameters);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
        #region 获取数据集
        /// <summary>
        /// 通过SQL语句返回数据集
        /// </summary>
        public static DataSet Query(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Query(conn, sql, null);
            }
        }
        /// 通过带参数的SQL语句返回数据集
        public static DataSet Query(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Query(conn, sql, parameters);
            }
        }
        /// <summary>
        /// 用其他连接获取SQL数据集合
        /// </summary>
        public static DataSet Query(string connectionString, string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Query(conn, sql, null);
            }
        }
        /// <summary>
        /// 用其他连接获取带参数的SQL数据集合
        /// </summary>
        public static DataSet Query(string connectionString, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Query(conn, sql, parameters);
            }
        }
        /// <summary>
        /// 用其他连接获取SQL数据集合
        /// </summary>
        public static DataSet Query(SQLiteConnection conn, string sql)
        {
            return Query(conn, sql, null);
        }
        /// <summary>
        /// 用其他连接获取带参数的SQL数据集合
        /// </summary>
        public static DataSet Query(SQLiteConnection conn, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, null, conn, sql, parameters);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                    }
                    return ds;
                }
            }
        }
        #endregion
        #region 获取分页数据集
        /// <summary>
        /// 通过SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string sql, int page, int count)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Query(conn, sql, page, count, null);
            }
        }
        /// <summary>
        /// 通过带参数的SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string sql, int page, int count, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Query(conn, sql, page, count, parameters);
            }
        }
        /// <summary>
        /// 通过SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string connectionString, string sql, int page, int count)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Query(conn, sql, page, count, null);
            }
        }
        /// <summary>
        /// 通过带参数的SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string connectionString, string sql, int page, int count, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Query(conn, sql, page, count, parameters);
            }
        }
        /// <summary>
        /// 通过SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(SQLiteConnection conn, string sql, int page, int count)
        {
            return Query(conn, sql, page, count, null);
        }
        /// <summary>
        /// 通过带参数的SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(SQLiteConnection conn, string sql, int page, int count, params SQLiteParameter[] parameters)
        {
            string orderfield;
            FormatSqlString(sql, out orderfield);
            if (page <= 0) page = 1;
            if (count <= 0) count = 1;
            int end = (page - 1) * count;
            string strSql = sql + string.Format(" limit {0} offset {1} ", count, end);
            return Query(conn, strSql, parameters);
        }
        #endregion
        #region 获取带有总数量的分页数据集
        /// <summary>
        /// 通过SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string sql, int page, int count, out int total)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Query(conn, sql, page, count, out total, null);
            }
        }
        /// <summary>
        /// 通过带参数的SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string sql, int page, int count, out int total, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Query(conn, sql, page, count, out total, parameters);
            }
        }
        /// <summary>
        /// 通过SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string connectionString, string sql, int page, int count, out int total)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Query(conn, sql, page, count, out total, null);
            }
        }
        /// <summary>
        /// 通过带参数的SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(string connectionString, string sql, int page, int count, out int total, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Query(conn, sql, page, count, out total, parameters);
            }
        }
        /// <summary>
        /// 通过SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(SQLiteConnection conn, string sql, int page, int count, out int total)
        {
            return Query(conn, sql, page, count, out total);
        }
        /// <summary>
        /// 通过带参数的SQL语句返回分页的指定页数据
        /// </summary>
        public static DataSet Query(SQLiteConnection conn, string sql, int page, int count, out int total, params SQLiteParameter[] parameters)
        {
            //string orderfield;
            //FormatSqlString(sql, out orderfield);
            if (page <= 0) page = 1;
            if (count <= 0) count = 1;
            int end = (page - 1) * count;
            string strSql = sql + string.Format(" limit {0} offset {1} ", count, end);
            object obj = Single(conn, "select count(0) from (" + sql + ") t", parameters);
            DataSet ds = Query(conn, strSql, parameters);
            total = Convert.ToInt32(obj.ToString());
            return ds;
        }
        #endregion
        #region 获取只读数据流
        /// <summary>
        /// 通过SQL语句返回数据集
        /// </summary>
        public static SQLiteDataReader Reader(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnectionString);
            return Reader(conn, sql, null);
        }
        /// <summary>
        /// 通过带参数的SQL语句返回数据集
        /// </summary>
        public static SQLiteDataReader Reader(string sql, params SQLiteParameter[] parameters)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnectionString);
            return Reader(conn, sql, parameters);
        }
        /// <summary>
        /// 通过SQL语句返回数据集
        /// </summary>
        public static SQLiteDataReader Reader(string connectionString, string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            return Reader(conn, sql, null);
        }
        /// <summary>
        /// 通过带参数的SQL语句返回数据集
        /// </summary>
        public static SQLiteDataReader Reader(string connectionString, string sql, params SQLiteParameter[] parameters)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            return Reader(conn, sql, parameters);
        }
        /// <summary>
        /// 通过SQL语句返回数据集
        /// </summary>
        public static SQLiteDataReader Reader(SQLiteConnection conn, string sql)
        {
            return Reader(conn, sql, null);
        }
        /// <summary>
        /// 通过带参数的SQL语句返回数据集
        /// </summary>
        public static SQLiteDataReader Reader(SQLiteConnection conn, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, null, conn, sql, parameters);
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return reader;
            }
        }
        #endregion
        #region 获取数据的第一行第一列
        /// <summary>
        /// 通过SQL语句返回第一行第一列
        /// </summary>
        public static object Single(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Single(conn, sql, null);
            }
        }
        /// <summary>
        /// 通过带参数的SQL语句返回第一行第一列
        /// </summary>
        public static object Single(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                return Single(conn, sql, parameters);
            }
        }
        /// <summary>
        /// 通过SQL语句返回第一行第一列
        /// </summary>
        public static object Single(string connectionString, string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Single(conn, sql, null);
            }
        }
        /// <summary>
        /// 通过带参数的SQL语句返回第一行第一列
        /// </summary>
        public static object Single(string connectionString, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                return Single(conn, sql, parameters);
            }
        }
        /// <summary>
        /// 通过SQL语句返回第一行第一列
        /// </summary>
        public static object Single(SQLiteConnection conn, string sql)
        {
            return Single(conn, sql, null);
        }
        /// <summary>
        /// 通过带参数的SQL语句返回第一行第一列
        /// </summary>
        public static object Single(SQLiteConnection conn, string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, null, conn, sql, parameters);
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();

                return obj;
            }
        }
        #endregion
        #region 私有函数
        //执行SQL命令
        static void PrepareCommand(SQLiteCommand cmd, SQLiteTransaction trans, SQLiteConnection conn, string sql, SQLiteParameter[] parameters)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            if (parameters != null)
            {
                foreach (SQLiteParameter parm in parameters)
                {
                    if (parm != null)
                    {
                        if (parm.Value == null)
                        {
                            parm.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }
        /// <summary>
        /// 格式化SQL语句字符串为分页需要的语句
        /// </summary>
        static string FormatSqlString(string sql, out string order_field)
        {
            int selectIndex = sql.IndexOf("select", StringComparison.OrdinalIgnoreCase);
            int orderIndex = sql.LastIndexOf("order", StringComparison.OrdinalIgnoreCase);
            int byIndex = sql.IndexOf("by", orderIndex);
            if (byIndex < 0)
                throw new ArgumentException("SQL命令中未包含有效的排序字段！");
            order_field = sql.Substring(byIndex + 3);
            return sql.Substring(7, orderIndex - 7);
        }
        #endregion

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static bool IsExistsTable(string conn, string tablename)
        {
            return Query(conn, "select name from sqlite_master where  name='" + tablename + "'").Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static List<string> GetTableField(string conn, string tablename)
        {
            DataTable dt = Query(conn, "pragma table_info([" + tablename + "])").Tables[0];
            List<string> listField = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    listField.Add(item["name"].ToString());
                }
            }
            return listField;

        }
    }
}