using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Сonsumer(2);

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
