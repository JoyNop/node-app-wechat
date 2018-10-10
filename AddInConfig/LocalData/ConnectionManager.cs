using System.Collections.Generic;

namespace AddInConfigJson
{
    /// <summary>
    /// 数据库连接管理器
    /// </summary>
    public class ConnectionManager
    {
        private readonly static Dictionary<string, string> connections = new Dictionary<string, string>();
        /// <summary>
        /// 新增一个数据库连接到管理器
        /// </summary>
        public static void Add(string key, string connectionString)
        {
            if (!connections.ContainsKey(key))
            {
                connections.Add(key, connectionString);
            }
            else
            {
                connections[key] = connectionString;
            }
        }

        /// <summary>
        /// 从连接管理器中移除数据库连接
        /// </summary>
        public static void Delete(string key)
        {
            if (connections.ContainsKey(key))
            {
                connections.Remove(key);
            }
        }

        /// <summary>
        /// 获取指定关键字的链接字符串
        /// </summary>
        public static string Get(string key)
        {
            if (connections.ContainsKey(key))
            {
                return connections[key];
            }
            return "";
        }
    }
}
