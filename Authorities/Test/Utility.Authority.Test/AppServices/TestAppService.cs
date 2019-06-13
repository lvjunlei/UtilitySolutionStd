using Utility.DynamicWebApi;

namespace Utility.Authority.Test.AppServices
{
    /// <summary>
    /// TestAppService
    /// 动态 WebAPI 接口测试
    /// </summary>
    [DynamicWebApi]
    public class TestAppService : ITestAppService, IDynamicWebApi
    {
        /// <summary>
        /// 动态 API 接口
        /// 获取NAME
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "Hello Dynamic WebAPI";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITestAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetName();
    }
}
