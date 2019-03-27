#region IRegisterAdapter 文件信息
/***********************************************************
**文 件 名：IRegisterAdapter 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/8/7 8:50:19 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Data
{
    public interface IRegisterAdapter
    {
        void Register(Type typeClass, Type typeInterface);
    }
}
