using EmitMapper;
using System;
using Nelibur.ObjectMapper;

namespace EmitMaperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var stu = new Student
            {
                Name = "Test123456",
                Age = 19
            };

            var dto = stu.MapTo<Student, Student>();
            var dto2 = TinyMapper.Map<Student>(stu);


            Console.ReadLine();
        }
    }

    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }

    public static class EmitMapperExtensions
    {
        /// <summary>
        /// 映射为对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="tFrom"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource tFrom)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TSource, TDestination>();
            return mapper.Map(tFrom);
        }
    }
}
