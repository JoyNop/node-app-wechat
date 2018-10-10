namespace AddInConfigJson
{
    /// <summary>
    /// 查询条件的逻辑符号
    /// </summary>
    public enum OperateChar
    {
        /// <summary>
        /// =
        /// </summary>
        eq,
        /// <summary>
        /// >
        /// </summary>
        gt,
        /// <summary>
        /// <
        /// </summary>
        lt,
        /// <summary>
        /// >=
        /// </summary>
        ge,
        /// <summary>
        /// <=
        /// </summary>
        le,
        /// <summary>
        /// Like
        /// </summary>
        lk,
        /// <summary>
        /// IN
        /// </summary>
        In,
        /// <summary>
        /// Is Null
        /// </summary>
        Null,
        /// <summary>
        /// Not Null
        /// </summary>
        NotNull,
        /// <summary>
        /// NotIn
        /// </summary>
        NotIn,
        /// <summary>
        /// 正则
        /// </summary>
        RegExp,
        /// <summary>
        /// !=
        /// </summary>
        No
    }
}
