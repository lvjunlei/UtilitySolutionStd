#region IRepository 文件信息
/***********************************************************
**文 件 名：IRepository 
**命名空间：Cares.Core 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 8:59:37 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Data
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TKey">数据实体主键</typeparam>
    public interface IRepository<TEntity, TKey> :IWritableRepository<TEntity,TKey>,IReadonlyRepository<TEntity,TKey>
        where TEntity : EntityBase<TKey>
    {
        
    }
}
