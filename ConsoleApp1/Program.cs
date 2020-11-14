using System;
using DataStream;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Consumer(100);

            foreach (var s in a.GetDataBulk())
            {
                foreach (var keyValuePair in s)
                {
                    // TODO Данные получаются здесь. Разбор строки и расчеты написать желательно в этом файле.
                    Console.WriteLine(keyValuePair.Key + ": " + keyValuePair.Value);
                }
            }
        }
    }
}
