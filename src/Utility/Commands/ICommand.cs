#region ICommand 文件信息
/***********************************************************
**文 件 名：ICommand 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 15:20:21 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;

namespace Utility.Commands
{
    /// <summary>
    /// ICommand接口定义
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command ID 
        /// </summary>
        Guid CommandId { get; }

        /// <summary>
        /// Command创建时间
        /// </summary>
        DateTime CommandCreatedTime { get; }
    }
}
