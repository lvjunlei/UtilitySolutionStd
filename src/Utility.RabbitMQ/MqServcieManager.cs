using System;
using System.Collections.Generic;
using System.Threading;
using Utility.RabbitMQ.Common;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// 服务监控管理类，该类内部定义一个简单的定时器功能，不间断的对 RabbitMQ 的通讯进行侦听，
    /// 一旦发现有断开的连接，就自动创建一个新的通道，并移除旧的通道；
    /// 同时，提供 Start/Stop 两个方法，以供程序 启动/停止
    /// </summary>
    public class MqServcieManager
    {
        /// <summary>
        /// 
        /// </summary>
        public int TimerTick { get; set; } = 10 * 1000;

        /// <summary>
        /// 服务监视定时器
        /// </summary>
        private readonly Timer _timer = null;

        /// <summary>
        /// 
        /// </summary>
        public Action<MessageLevel, string, Exception> OnAction = null;

        /// <summary>
        /// 
        /// </summary>
        public MqServcieManager()
        {
            _timer = new Timer(OnInterval, "", TimerTick, TimerTick);
        }

        /// <summary>
        ///  自检，配合 RabbitMQ 内部自动重连机制
        /// </summary>
        /// <param name="sender"></param>
        private void OnInterval(object sender)
        {
            int error = 0, reconnect = 0;
            OnAction?.Invoke(MessageLevel.Information, $"{DateTime.Now} 正在执行自检", null);
            foreach (var item in Services)
            {
                foreach (var c in item.Channels)
                {
                    if (c.Connection == null || !c.Connection.IsOpen)
                    {
                        error++;
                        OnAction?.Invoke(MessageLevel.Information, $"{c.Exchange} {c.Queue} {c.Routingkey} 重新创建订阅", null);
                        try
                        {
                            c.Stop();
                            var channel = item.CreateChannel(c.Queue, c.Routingkey, c.ExchangeType);
                            item.Channels.Remove(c);
                            item.Channels.Add(channel);

                            OnAction?.Invoke(MessageLevel.Information, $"{c.Exchange} {c.Queue} {c.Routingkey} 重新创建完成", null);
                            reconnect++;
                        }
                        catch (Exception ex)
                        {
                            OnAction?.Invoke(MessageLevel.Information, ex.Message, ex);
                        }
                    }
                }
            }
            OnAction?.Invoke(MessageLevel.Information, $"{DateTime.Now} 自检完成，错误数：{error}，重连成功数：{reconnect}", null);
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            foreach (var item in Services)
            {
                try
                {
                    item.Start();
                }
                catch (Exception e)
                {
                    OnAction?.Invoke(MessageLevel.Error, $"启动服务出错 | {e.Message}", e);
                }
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void Stop()
        {
            try
            {
                foreach (var item in Services)
                {
                    item.Stop();
                }
                Services.Clear();
                _timer.Dispose();
            }
            catch (Exception e)
            {
                OnAction?.Invoke(MessageLevel.Error, $"停止服务出错 | {e.Message}", e);
            }
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="mqService"></param>
        public void AddService(IMqService mqService)
        {
            Services.Add(mqService);
        }

        /// <summary>
        /// 
        /// </summary>
        public List<IMqService> Services { get; } = new List<IMqService>();
    }
}
