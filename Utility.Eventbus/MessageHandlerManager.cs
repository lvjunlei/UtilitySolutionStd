#region MessageHandlerManager 文件信息
/***********************************************************
**文 件 名：MessageHandlerManager 
**命名空间：Utility.Eventbus.RabbitMQ 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-29 21:54:57 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Events;
using Utility.Events.Handlers;
using Utility.Extensions;

namespace Utility.Eventbus.RabbitMQ
{
    /// <summary>
    /// MessageHandlerManager
    /// </summary>
    public class MessageHandlerManager : IEventHandlerManager, IDisposable
    {
        #region 私有属性

        /// <summary>
        /// 事件字典集合
        /// </summary>
        private readonly ConcurrentDictionary<string, Dictionary<Type, object>> _handlers;

        /// <summary>
        /// 队列消费者集合
        /// </summary>
        private readonly ConcurrentDictionary<string, EventingBasicConsumer> _consumers;

        /// <summary>
        /// 连接工厂
        /// </summary>
        private readonly IConnectionFactory _connectionFactory;
        #endregion

        #region 公共属性

        /// <summary>
        /// RabbitMQ配置信息
        /// </summary>
        public MqConfig Config { get; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化 MessageHandlerManager
        /// </summary>
        public MessageHandlerManager(MqConfig config)
        {
            Config = config;
            _connectionFactory = new ConnectionFactory
            {
                UserName = Config.UserName,
                Password = Config.Password,
                HostName = Config.HostIp,
                Port = Config.Port,
                VirtualHost = Config.VirtualHost
            };
            _handlers = new ConcurrentDictionary<string, Dictionary<Type, object>>();
            _consumers = new ConcurrentDictionary<string, EventingBasicConsumer>();
        }

        #endregion

        #region GetHandlers

        /// <summary>
        /// 获取指定 事件类型 的事件处理器
        /// </summary>
        /// <returns>事件处理器集合</returns>
        public List<IEventHandler<IEvent>> GetHandlers(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                return new List<IEventHandler<IEvent>>();
            }
            var vs = new List<IEventHandler<IEvent>>();
            if (_handlers.ContainsKey(eventName))
            {
                foreach (var value in _handlers[eventName].Values)
                {
                    vs.Add((IEventHandler<IEvent>)value);
                }
            }

            return vs;
        }

        /// <summary>
        /// 获取指定 事件类型 的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器集合</returns>
        public List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent
        {
            var key = typeof(TEvent).Name;
            var vs = new List<IEventHandler<TEvent>>();
            if (_handlers.ContainsKey(key))
            {
                foreach (var value in _handlers[key].Values)
                {
                    vs.Add((IEventHandler<TEvent>)value);
                }
            }

            return vs;
        }

        /// <summary>
        /// 获取指定 事件类型 的事件处理器
        /// 异步
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器集合</returns>
        public Task<List<IEventHandler<TEvent>>> GetHandlersAsync<TEvent>() where TEvent : IEvent
        {
            return Task.Run(() => GetHandlers<TEvent>());
        }

        #endregion

        #region Register

        /// <summary>
        /// 批量注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handlers">事件处理器</param>
        /// <returns></returns>
        public void Registers<TEvent>(IEnumerable<IEventHandler<TEvent>> handlers)
            where TEvent : class, IEvent
        {
            foreach (var handler in handlers)
            {
                Register(handler);
            }
        }

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">处理器</param>
        public void Register<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : class, IEvent
        {
            var key = typeof(TEvent).Name;
            if (!_handlers.ContainsKey(key))
            {
                _handlers.TryAdd(key, new Dictionary<Type, object>());
            }
            _handlers[key].AddOrUpdate(handler.GetType(), handler);
            var h = handler as IMessageHandler<TEvent>;
            if (h != null)// 如果是 IMessageHandler 类型的处理器则建立MQ队列
            {
                var connection = _connectionFactory.CreateConnection();
                var channel = connection.CreateModel();
                // 队列名称
                var queueName = handler.GetType().Name;

                // 声明交换机
                channel.ExchangeDeclare(Config.Exchange, Config.ExchangeType, Config.Durable, Config.AutoDelete);

                // 声明队列
                var r = channel.QueueDeclare(queueName, h.Durable, h.Exclusive, h.AutoDelete);

                // 绑定路由key
                channel.QueueBind(queueName, Config.Exchange, typeof(TEvent).Name);

                // 创建队列消费者
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    // 处理接收到的消息
                    HandleEventAsync<TEvent>(message);
                };

                // 激活消费者
                channel.BasicConsume(queueName, true, consumer);

                // 把消费者入队管理
                _consumers.TryAdd(handler.GetType().Name, consumer);
            }
        }

        /// <summary>
        /// 处理消息接收事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        private void HandleEventAsync<TEvent>(string message)
            where TEvent : class, IEvent
        {
            var @event = JsonConvert.DeserializeObject(message, typeof(TEvent)) as TEvent;
            var handlers = GetHandlers<TEvent>();
            if (handlers != null && handlers.Any())
            {
                foreach (var handler in handlers)
                {
                    if (handler == null)
                    {
                        continue;
                    }
                    // 消息只发送给 IMessageHandler<TEvent> 类型的处理器
                    if (handler is IMessageHandler<TEvent>)
                    {
                        handler.HandleAsync(@event);
                    }
                    // handler.HandleAsync(@event);
                }
            }
        }

        #endregion

        #region Publish

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        /// <returns></returns>
        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            return Task.Run(async () =>
            {
                #region 发送内存事件

                var handlers = GetHandlers<TEvent>();
                if (handlers != null && handlers.Any())
                {
                    foreach (var handler in handlers)
                    {
                        if (handler == null)
                        {
                            continue;
                        }
                        await handler.HandleAsync(@event);
                    }
                }

                #endregion

                #region 发送消息到 RabbitMQ 消息

                await Send(@event);

                #endregion
            });
        }

        /// <summary>
        /// 发送 MQ 消息
        /// </summary>
        /// <param name="event">事件</param>
        private Task Send(IEvent @event)
        {
            return Task.Run(() =>
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(Config.Exchange, Config.ExchangeType);

                        var message = JsonConvert.SerializeObject(@event);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(Config.Exchange,
                            @event.GetType().Name,
                            null,
                            body);
                    }
                }
            });
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _handlers.Clear();
        }

        #endregion
    }
}
