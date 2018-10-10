using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace AddInConfigJson
{
    /// <summary>
    /// 数据库访问层基础类
    /// </summary>
    public abstract class IBaseDAL
    {
 

        #region 基础属性       
        private void SwitchUserEvent()
        {
            SwitchUser();
        }

        /// <summary>
        /// 非个人数据库则不需要实现该方法（切换用户需要重新检查数据库）
        /// </summary>
        public virtual void SwitchUser()
        {

        }

        /// <summary>
        /// 该访问层需要的数据库连接
        /// </summary>
        public virtual string ConnectionKey { get; }

        /// <summary>
        /// 当前类操作的表名称
        /// </summary>
        public virtual string TableName { get; }

        /// <summary>
        /// 更新字段列表
        /// </summary>
        public virtual List<BaseField> ListField { get; }

        /// <summary>
        /// 创建表结构语句
        /// </summary>
        public virtual string SqlTable { get; }

        object lockobj = new object();

        /// <summary>
        /// 更新字段和表结构
        /// </summary>
        public void UpdateFieldAndTable()
        {
            lock (lockobj)
            {
                DbType dbtype = (DbType)Enum.Parse(typeof(DbType), ConnectionKey);
                BaseDB.CreateDatabase(dbtype);
                if (!SQLiteHelper.IsExistsTable(ConnectionManager.Get(ConnectionKey), TableName))
                {
                    SQLiteHelper.Execute(ConnectionManager.Get(ConnectionKey), SqlTable);
                }
                else
                {
                    if (ListField != null && ListField.Count > 0)
                    {
                        List<string> listCol = SQLiteHelper.GetTableField(ConnectionManager.Get(ConnectionKey), TableName);
                        foreach (BaseField filed in ListField)
                        {
                            if (listCol.Find(x => x == filed.FieldName) == null)
                            {
                                SQLiteHelper.Execute(ConnectionManager.Get(ConnectionKey), filed.FieldSql);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 自定义sql语句操作     
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteSql(string sql)
        {
            return SQLiteHelper.Execute(sql) > 0;
        }

        /// <summary>
        /// 批量执行sql语句
        /// </summary>
        /// <param name="listSql"></param>
        public void ExecuteList(List<string> listSql)
        {
            SQLiteHelper.ExecuteList(ConnectionManager.Get(ConnectionKey), listSql);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable QuerySql(string sql)
        {
            return SQLiteHelper.Query(sql).Tables[0];
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable QuerySqlPage(string sql, int pageSize, int pageIndex, out int total)
        {
            return SQLiteHelper.Query(ConnectionManager.Get(ConnectionKey), sql, pageIndex, pageSize, out total).Tables[0];
        }
        #endregion

        #region 自定义结构数据操作

        int BatchCount = 0;

        /// <summary>
        /// sql指令列表
        /// </summary>
        List<SQLInfo> SQLList = null;
        /// <summary>
        /// 开始批量指令操作
        /// </summary>
        public void BeginCommand()
        {
            if (BatchCount == 0)
            {
                SQLList = new List<SQLInfo>();
            }
            BatchCount++;
        }

        /// <summary>
        /// 提交批量指令操作
        /// </summary>
        public void CommitCommand()
        {
            try
            {
                SQLiteHelper.ExecuteList(ConnectionManager.Get(ConnectionKey), SQLList);
            }
            finally
            {
                BatchCount--;

                if (BatchCount == 0)
                {
                    SQLList = null;
                }
            }
        }

        /// <summary>
        /// 撤销批量指令操作
        /// </summary>
        public void RollbackCommand()
        {
            BatchCount--;

            if (BatchCount == 0)
            {
                SQLList = null;
            }
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        public int Add(params Col[] cols)
        {
            SQLiteParameter[] parameters = new SQLiteParameter[cols.Length];
            StringBuilder builder = new StringBuilder();

            builder.Append("insert into " + TableName + " (");

            for (int i = 0; i < cols.Length - 1; i++)
            {
                builder.Append(cols[i].Name + ",");
            }
            builder.Append(cols[cols.Length - 1].Name + ") values (");

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].IsInnerValue)
                {
                    builder.Append(cols[i].Value + ",");
                }
                else
                {
                    builder.Append("@" + cols[i].Name + ",");
                    parameters[i] = new SQLiteParameter("@" + cols[i].Name, cols[i].Value);
                }
            }
            builder.Length -= 1;
            builder.Append(");select last_insert_rowid();");

            if (SQLList != null)
            {
                SQLList.Add(new SQLInfo(builder.ToString(), parameters));
            }
            else
            {
                object obj = SQLiteHelper.Single(ConnectionManager.Get(ConnectionKey), builder.ToString(), parameters);

                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToInt32(obj.ToString());
                }
            }
            return 0;
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        public int Add(List<Col> cols)
        {
            return this.Add(cols.ToArray());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(int id, params Col[] cols)
        {
            return this.Update(cols, new SearchParameter("id", id));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(int id, List<Col> cols)
        {
            return this.Update(cols.ToArray(), new SearchParameter("id", id));
        }

        /// <summary>
        /// 更新指定条件的数据
        /// </summary>
        public int Update(Col[] cols, params SearchParameter[] searchs)
        {
            SQLiteParameter[] parameters = new SQLiteParameter[cols.Length];
            StringBuilder builder = new StringBuilder();

            builder.Append("update " + TableName + " set ");

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].IsInnerValue)
                {
                    builder.Append(cols[i].Name + "=" + cols[i].Value + ",");
                }
                else
                {
                    builder.Append(cols[i].Name + "=@" + cols[i].Name + ",");
                    parameters[i] = new SQLiteParameter("@" + cols[i].Name, cols[i].Value);
                }
            }
            builder.Length -= 1;
            builder.Append(searchs.ToString(ref parameters));

            if (SQLList != null)
            {
                SQLList.Add(new SQLInfo(builder.ToString(), parameters));

                return 0;
            }
            else
            {
                int rows = SQLiteHelper.Execute(ConnectionManager.Get(ConnectionKey), builder.ToString(), parameters);

                return rows;
            }
        }

        /// <summary>
        /// 更新指定条件的数据
        /// </summary>
        public int Update(List<Col> cols, params SearchParameter[] searchs)
        {
            return Update(cols.ToArray(), searchs);
        }

        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        public DataSet GetInfo(int id)
        {
            string sql;
            sql = "select * from " + TableName + " where id=@id";

            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@id", id)
            };

            DataSet ds = SQLiteHelper.Query(ConnectionManager.Get(ConnectionKey), sql, parameters);

            return ds;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {
            return this.Delete(new SearchParameter("id", id));
        }

        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        public int Delete(params SearchParameter[] searchs)
        {
            SQLiteParameter[] parameters = null;
            string sql;
            sql = "delete from " + TableName + searchs.ToString(ref parameters);

            if (SQLList != null)
            {
                SQLList.Add(new SQLInfo(sql, parameters));

                return 0;
            }
            else
            {
                int rows = SQLiteHelper.Execute(ConnectionManager.Get(ConnectionKey), sql, parameters);

                return rows;
            }
        }

        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        public int Delete(List<SearchParameter> searchs)
        {
            return Delete(searchs.ToArray());
        }

        /// <summary>
        /// 根据执行条件查询数据列表
        /// </summary>
        public DataSet GetList(string orderBy = null, params SearchParameter[] searchs)
        {
            return GetList("*", orderBy, searchs);
        }

        /// <summary>
        /// 根据执行条件查询数据列表
        /// </summary>
        public DataSet GetList(string fields, string orderBy, params SearchParameter[] searchs)
        {
            SQLiteParameter[] parameters = null;
            StringBuilder builder = new StringBuilder();
            builder.Append("select " + (fields ?? "*") + " from " + TableName + searchs.ToString(ref parameters));
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                builder.Append(" order by " + orderBy);
            }

            DataSet ds = SQLiteHelper.Query(ConnectionManager.Get(ConnectionKey), builder.ToString(), parameters);

            return ds;
        }

        /// <summary>
        /// 根据执行条件查询数据列表
        /// </summary>
        public DataSet GetList(string orderBy, List<SearchParameter> searchs)
        {
            return this.GetList("*", orderBy, searchs.ToArray());
        }

        /// <summary>
        /// 根据执行条件查询数据列表
        /// </summary>
        public DataSet GetList(string fields, string orderBy, List<SearchParameter> searchs)
        {
            return this.GetList(fields, orderBy, searchs.ToArray());
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string orderBy = null, params SearchParameter[] searchs)
        {
            return GetList(page, count, "*", orderBy, searchs);
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string fields, string orderBy, params SearchParameter[] searchs)
        {
            SQLiteParameter[] parameters = null;
            StringBuilder builder = new StringBuilder();
            builder.Append("select " + (fields ?? "*") + " from " + TableName + searchs.ToString(ref parameters));
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                builder.Append(" order by " + orderBy);
            }

            DataSet ds = SQLiteHelper.Query(ConnectionManager.Get(ConnectionKey), builder.ToString(), page, count, parameters);

            return ds;
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string orderBy, List<SearchParameter> searchs)
        {
            return GetList(page, count, "*", orderBy, searchs.ToArray());
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string fields, string orderBy, List<SearchParameter> searchs)
        {
            return GetList(page, count, fields, orderBy, searchs.ToArray());
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string orderBy, ref int total, params SearchParameter[] searchs)
        {
            return GetList(page, count, "*", orderBy, ref total, searchs);
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string fields, string orderBy, ref int total, params SearchParameter[] searchs)
        {
            SQLiteParameter[] parameters = null;
            StringBuilder builder = new StringBuilder();
            builder.Append("select " + (fields ?? "*") + " from " + TableName + searchs.ToString(ref parameters));
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                builder.Append(" order by " + orderBy);
            }

            DataSet ds = SQLiteHelper.Query(ConnectionManager.Get(ConnectionKey), builder.ToString(), page, count, out total, parameters);

            return ds;
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string orderBy, ref int total, List<SearchParameter> searchs)
        {
            return GetList(page, count, "*", orderBy, ref total, searchs.ToArray());
        }

        /// <summary>
        /// 根据执行条件查询分页数据列表
        /// </summary>
        public DataSet GetList(int page, int count, string fields, string orderBy, ref int total, List<SearchParameter> searchs)
        {
            return GetList(page, count, fields, orderBy, ref total, searchs.ToArray());
        }
        #endregion
    }
}
