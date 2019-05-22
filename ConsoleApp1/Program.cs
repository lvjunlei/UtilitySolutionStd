using System;
using System.Globalization;
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
            Console.WriteLine("people".ToPlural());
            Console.WriteLine(new Pluralizer().Pluralize("Aircraft"));
            Console.ReadLine();
        }
    }
}