using System.Net.Sockets;

namespace Utility.Extensions
{
    /// <summary>
    /// SocketExtensions
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <param name="socket">套接字</param>
        /// <returns>bool</returns>
        public static bool IsConnected(this Socket socket)
        {
            var part1 = socket.Poll(1000, SelectMode.SelectRead);
            var part2 = (socket.Available == 0);
            return part1 & part2;
        }
    }
}
