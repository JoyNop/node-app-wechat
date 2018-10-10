using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace AddInConfigJson
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// 系统数据库
        /// </summary>
        SysDatabase
    }

    public class BaseDB
    {
        /// <summary>
        /// 系统数据库密码
        /// </summary>
        static string SysDbPwd = "CJsKSA$x42mC%8n";

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbtype"></param>
        public static void CreateDatabase(DbType dbtype)
        {
            string dbfile = GetDbFilePath(dbtype);
            if (!File.Exists(dbfile))
            {
                SQLiteConnection.CreateFile(dbfile);
                using (SQLiteConnection conn = new SQLiteConnection(GetDbConn(dbtype)))
                {
                    conn.Open();
                    conn.ChangePassword(GetDbPwd(dbtype));
                    conn.Close();
                }
            }
            else
            {
                GetDbConn(dbtype);
            }
        }

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static string GetDbConn(DbType dbtype)
        {
            string result =
                string.Format(
                    @"Data Source={0};Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=100;Password=" +
                    GetDbPwd(dbtype), GetDbFilePath(dbtype));
            ConnectionManager.Add(dbtype.ToString(), result);
            return result;
        }

        /// <summary>
        /// 获取数据库密码
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static string GetDbPwd(DbType dbtype)
        {
            switch (dbtype)
            {
                case DbType.SysDatabase:
                    return SysDbPwd;
            }

            return "";
        }

        /// <summary>
        /// 获取数据库物理路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static string GetDbFilePath(DbType dbtype)
        {
            string dbpath = AppDomain.CurrentDomain.BaseDirectory + dbtype.ToString() + ".db";

            return dbpath;
        }
    }
}