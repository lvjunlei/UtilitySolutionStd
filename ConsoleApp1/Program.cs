using System;
using Utility.Extensions;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello".ToPlural());
            Console.WriteLine("lvjunlei".ToPlural());
            Console.WriteLine("Happy".ToPlural());
            Console.WriteLine("Areu".ToPlural());

            Console.ReadLine();
        }
    }
}