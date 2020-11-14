using System;
using DataStream;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Consumer(10);

            foreach (var s in a.GetDataBulk())
            {
                foreach (var keyValuePair in s)
                {
                    Console.WriteLine(keyValuePair.Key + ": " + keyValuePair.Value);
                }
            }
        }
    }
}
