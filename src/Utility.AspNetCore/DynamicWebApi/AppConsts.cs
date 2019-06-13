#region AppConsts 文件信息
/***********************************************************
**文 件 名：AppConsts 
**命名空间：Utility.DynamicWebApi 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:12:15 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.DynamicWebApi
{
    public static class AppConsts
    {
        public static string DefaultHttpVerb { get; set; }

        public static string DefaultAreaName { get; set; }

        public static string DefaultApiPreFix { get; set; }

        public static List<string> ControllerPostfixes { get; set; }
        public static List<string> ActionPostfixes { get; set; }

        public static List<Type> FormBodyBindingIgnoredTypes { get; set; }

        public static Dictionary<string, string> HttpVerbs { get; }

        static AppConsts()
        {
            HttpVerbs = new Dictionary<string, string>()
            {
                ["Add"] = "POST",
                ["create"] = "POST",
                ["post"] = "POST",

                ["get"] = "GET",
                ["find"] = "GET",
                ["fetch"] = "GET",
                ["query"] = "GET",

                ["update"] = "PUT",
                ["put"] = "PUT",

                ["delete"] = "DELETE",
                ["remove"] = "DELETE",
            };
        }
    }
}
