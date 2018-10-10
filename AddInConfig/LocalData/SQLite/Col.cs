namespace AddInConfigJson
{
    /// <summary>
    /// 数据库字段名称和值等
    /// </summary>
    public class Col
    {
        public Col(string name, object value, bool isInner = false)
        {
            this.Name = name;
            this.Value = value;
            this.IsInnerValue = isInner;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 该字段对应的值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 是否数据库内部字段值（即几个字段之间的相互影响），是的时候外部不用定义参数
        /// </summary>
        public bool IsInnerValue
        {
            get;
            set;
        }
    }
}
