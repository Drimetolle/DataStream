using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new StreamFactory(new List<string>()
            {
                @"C:\Users\drime\AppData\Roaming\source\repos\DataStream\Data\1.txt",
                @"C:\Users\drime\AppData\Roaming\source\repos\DataStream\Data\2.txt"
            });

            var ids = factory.StartStreams();

            while (true)
            {
                foreach (var id in ids)
                {
                    foreach (var data in factory.GetDataByStreamId(id))
                    {
                        Console.WriteLine(id + ": " + data);
                    }
                }
            }
        }
    }
}
