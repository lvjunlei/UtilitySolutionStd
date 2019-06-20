#region Command 文件信息
/***********************************************************
**文 件 名：Command 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 15:35:49 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;

namespace Utility.Commands
{
    /// <summary>
    /// Command基础实现
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Command ID
        /// </summary>
        public Guid CommandId { get; }

        /// <summary>
        /// Command创建时间
        /// </summary>
        public DateTime CommandCreatedTime { get; }

        public Command()
        {
            CommandId = Guid.NewGuid();
            CommandCreatedTime = DateTime.Now;
        }
    }
}
