using System;
using System.Linq;

namespace AddInConfigJson
{
    public class SearchParameter
    {
        private string _field;
        private string _tableName;

        public SearchParameter(string field, object value, OperateChar operateChar, string tableName, bool hasLeftParen, bool hasRightParen, LogicChar logicChar)
        {
            this.Field = field;
            this.Value = value;
            this.OperateChar = operateChar;
            this.TableName = tableName;
            this.HasLeftParen = hasLeftParen;
            this.HasRightParen = hasRightParen;
            this.LogicChar = logicChar;
        }

        /// <summary>
        /// 条件字段,条件值,查询条件字段所属表
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="tableName">查询条件字段所属表</param>
        public SearchParameter(string field, object value, string tableName)
            : this(field, value, OperateChar.eq, tableName, false, false, LogicChar.And) { }

        /// <summary>
        /// 条件字段,条件值,查询条件字段所属表,逻辑连接符
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="tableName">查询条件字段所属表</param>
        /// <param name="logicChar">逻辑连接符</param>
        public SearchParameter(string field, object value, string tableName, LogicChar logicChar)
            : this(field, value, OperateChar.eq, tableName, false, false, logicChar) { }

        /// <summary>
        /// 条件字段,条件值,操作符,查询条件字段所属表
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="operateChar"></param>
        /// <param name="tableName"></param>
        public SearchParameter(string field, object value, OperateChar operateChar, string tableName)
            : this(field, value, operateChar, tableName, false, false, LogicChar.And) { }

        /// <summary>
        /// 条件字段,条件值,操作符,查询条件字段所属表,逻辑连接符
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="operateChar">操作符</param>
        /// <param name="tableName">查询条件字段所属表</param>
        /// <param name="logicChar">逻辑连接符</param>
        public SearchParameter(string field, object value, OperateChar operateChar, string tableName, LogicChar logicChar)
            : this(field, value, operateChar, tableName, false, false, logicChar) { }

        /// <summary>
        /// 条件字段,条件值,操作符,查询条件字段所属表,是否有左括号,是否有右括号
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="operateChar">操作符</param>
        /// <param name="tableName">查询条件字段所属表</param>
        /// <param name="hasLeftParen">是否有左括号</param>
        /// <param name="hasRightParen">是否有右括号</param>
        public SearchParameter(string field, object value, OperateChar operateChar, string tableName, bool hasLeftParen, bool hasRightParen)
            : this(field, value, operateChar, tableName, hasLeftParen, hasRightParen, LogicChar.And) { }

        /// <summary>
        /// 条件字段,条件值
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        public SearchParameter(string field, object value)
            : this(field, value, OperateChar.eq, "", false, false, LogicChar.And) { }

        /// <summary>
        /// 条件字段,条件值,逻辑连接符
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="logicChar">逻辑连接符</param>
        public SearchParameter(string field, object value, LogicChar logicChar)
            : this(field, value, OperateChar.eq, "", false, false, logicChar) { }

        /// <summary>
        /// 条件字段,条件值,操作符,逻辑连接符
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="operateChar">操作符</param>
        /// <param name="logicChar">逻辑连接符</param>
        public SearchParameter(string field, object value, OperateChar operateChar, LogicChar logicChar)
            : this(field, value, operateChar, "", false, false, logicChar) { }

        /// <summary>
        /// 条件字段,条件值,操作符,是否有左括号,是否有右括号
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="operateChar">操作符</param>
        /// <param name="hasLeftParen">是否有左括号</param>
        /// <param name="hasRightParen">是否有右括号</param>
        public SearchParameter(string field, object value, OperateChar operateChar, bool hasLeftParen, bool hasRightParen)
            : this(field, value, operateChar, "", hasLeftParen, hasRightParen, LogicChar.And) { }

        /// <summary>
        /// 条件字段,条件值,操作符
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="operateChar"></param>
        public SearchParameter(string field, object value, OperateChar operateChar)
            : this(field, value, operateChar, "", false, false, LogicChar.And) { }

        /// <summary>
        /// 条件字段,条件值,操作符,是否有左括号,是否有右括号,逻辑连接符
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        /// <param name="operateChar">操作符</param>
        /// <param name="hasLeftParen">是否有左括号</param>
        /// <param name="hasRightParen">是否有右括号</param>
        /// <param name="logicChar">逻辑连接符</param>
        public SearchParameter(string field, object value, OperateChar operateChar, bool hasLeftParen, bool hasRightParen, LogicChar logicChar)
            : this(field, value, operateChar, "", hasLeftParen, hasRightParen, logicChar) { }

        /// <summary>
        /// 条件字段名称
        /// </summary>
        public string Field
        {
            get
            {
                return this._field;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                if (value.Contains('=') || value.Contains(' '))
                {
                    throw new ArgumentException("字段名称中含有非法字符！");
                }
                this._field = value;
            }
        }

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 操作符
        /// </summary>
        public OperateChar OperateChar
        {
            get;
            set;
        }

        /// <summary>
        /// 查询条件字段所属表
        /// </summary>
        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                if (value.Contains('=') || value.Contains(' '))
                {
                    throw new ArgumentException("字段名称中含有非法字符！");
                }
                this._tableName = value;
            }
        }

        /// <summary>
        /// 是否有左括号
        /// </summary>
        public bool HasLeftParen
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有右括号
        /// </summary>
        public bool HasRightParen
        {
            get;
            set;
        }

        /// <summary>
        /// 逻辑连接符
        /// </summary>
        public LogicChar LogicChar
        {
            get;
            set;
        }
    }
}
