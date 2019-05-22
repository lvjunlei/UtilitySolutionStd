using System.ComponentModel;

namespace Utility.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    [Description("数据库类型")]
    public enum DatabaseType
    {
        /// <summary>
        /// SQLServer 数据库
        /// </summary>
        [Description("SQLServer 数据库")]
        SQLServer = 0,

        /// <summary>
        /// Oracle 数据库
        /// </summary>
        [Description("Oracle 数据库")]
        Oracle = 1,

        /// <summary>
        /// MySql 数据库
        /// </summary>
        [Description("MySql 数据库")]
        MySQL = 2,
    }
}
