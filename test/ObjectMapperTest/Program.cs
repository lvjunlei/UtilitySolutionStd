using Mapster;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ObjectMapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var stus = new List<Student>();
            for (var i = 0; i < 100000; i++)
            {
                stus.Add(new Student
                {
                    Name = $"Test {i}",
                    Age = 19,
                    EntryTime = new DateTime(2018, 9, 1),
                    Address = new Address
                    {
                        Code = "600100",
                        Telphone = "18552557749"
                    }
                });
            }
            //var stu = new Student
            //{
            //    Name = "Test",
            //    Age = 19,
            //    EntryTime = new DateTime(2018, 9, 1),
            //    Address = new Address
            //    {
            //        Code = "600100",
            //        Telphone = "18552557749"
            //    }
            //};
            var stw = new Stopwatch();
            stw.Start();
            var dto = stus.Adapt<List<StudentDto>>();
            stw.Stop();
            Console.WriteLine($"映射 {stus.Count} 条数据，用时：{stw.ElapsedMilliseconds}");
            Console.ReadLine();
        }

    }


    public class StudentDto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime EntryTime { get; set; }

        public string AddressCode { get; set; }

        public string AddressTelphone { get; set; }
    }



    public class Student
    {
        public string Name { get; set; }

        public long Age { get; set; }

        public DateTime EntryTime { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        public string Code { get; set; }

        public string Telphone { get; set; }
    }
}
