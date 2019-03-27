using System.Text;
using RabbitMQ.Client;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 连接管理类，用于创建连接，关闭连接
    /// </summary>
    public class MqConnection
    {
        private readonly string _vhost;
        private IConnection _connection = null;
        private readonly MqConfig _config = null;

        /// <summary>
        ///  构造无 utf8 标记的编码转换器
        /// </summary>
        public static UTF8Encoding Utf8 { get; set; } = new UTF8Encoding(false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="vhost"></param>
        public MqConnection(MqConfig config, string vhost)
        {
            _config = config;
            _vhost = vhost;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var factory = new ConnectionFactory
                    {
                        AutomaticRecoveryEnabled = true,
                        UserName = _config.UserName,
                        Password = _config.Password,
                        HostName = _config.HostIp,
                        VirtualHost = _vhost,
                        Port = _config.Port
                    };
                    _connection = factory.CreateConnection();
                }

                return _connection;
            }
        }
    }
}