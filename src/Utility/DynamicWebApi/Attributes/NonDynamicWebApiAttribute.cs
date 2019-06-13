#region NonDynamicWebApiAttribute 文件信息
/***********************************************************
**文 件 名：NonDynamicWebApiAttribute 
**命名空间：Utility.DynamicWebApi.Attributes 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:10:16 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;

namespace Utility.DynamicWebApi
{
    /// <summary>
    /// 非动态生成 WebAPI 标记
    /// 可标记接口、类、方法
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class NonDynamicWebApiAttribute : Attribute
    {
    }
}
