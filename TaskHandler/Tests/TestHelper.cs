using System;
using System.Threading;

namespace Tests
{
    public class TestHelper
    {
        public static void TestMethod(int order, int secDelay = 0)
        {
            Console.WriteLine($"Test method started: task order - {order}, delay - {secDelay} seconds");
            if (secDelay != 0)
                Thread.Sleep(secDelay * 1000);
            Console.WriteLine($"Test method ended:   task order - {order}");
            Console.WriteLine();
        }
    }
}
