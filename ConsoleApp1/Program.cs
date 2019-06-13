using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Utility.Cache;
using Utility.Extensions;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var random = new Random();
            ICacheService cache = new SimpleCacheService();
            Console.WriteLine("请输入执行次数：");
            var cmd = Console.ReadLine();
            while (cmd != "exit")
            {
                if (cmd == "clear")
                {
                    cache.Clear();
                }
                else if (cmd.Contains("key_"))
                {
                    Console.WriteLine($"KEY:{cmd}, VAULE:{cache.TryGet<int>(cmd, -1)}");
                }
                else
                {
                    if (int.TryParse(cmd, out int times))
                    {
                        var sw = new Stopwatch();
                        sw.Start();
                        for (var i = 0; i < times; i++)
                        {
                            cache.TryAdd($"key_{i}", i);
                        }
                        Console.WriteLine($"存储 {times} 用时：{sw.ElapsedMilliseconds}");

                        sw.Restart();
                        for (var i = 0; i < times; i++)
                        {
                            cache.Get<int>($"key_{random.Next(0, times - 1)}");
                        }
                        Console.WriteLine($"读取 {times} 用时：{sw.ElapsedMilliseconds}");
                    }
                    else
                    {
                        Console.WriteLine($"输入值：{cmd} 非数字……");
                    }
                }

                Console.WriteLine("请输入执行次数：");
                cmd = Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}