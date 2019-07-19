using Utility.Authority.Domain.Users;
using Utility.Commands;
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
        private readonly ICommandBus _commandBus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBus"></param>
        public TestAppService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        public void InsertUser(string account, string name)
        {
            var command = new AddUserCommand(account, name);
            _commandBus.PublishAsync(command);
        }

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
