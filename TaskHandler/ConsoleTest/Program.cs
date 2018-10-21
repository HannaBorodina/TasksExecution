using System;
using System.Threading;
using System.Threading.Tasks;
using TaskHandler;
using Tests;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = TaskHandlerSequentally.GetInstance();
            int i = 0;

            handler.AddForExecution(() => TestHelper.TestMethod(++i, 1));
            handler.AddForExecution(() => TestHelper.TestMethod(++i, 2));

            Thread.Sleep(2000);

            handler.AddForExecution(() => TestHelper.TestMethod(++i));
            handler.AddForExecution(() => TestHelper.TestMethod(++i, 1));

            Thread.Sleep(2000);

            handler.AddForExecution(
                    () => TestHelper.TestMethod(++i),
                    () => TestHelper.TestMethod(++i),
                    () => TestHelper.TestMethod(++i),
                    () => TestHelper.TestMethod(++i)
                    );

            for (int j = 9; j < 15; j++)
            {
                var tmp = j;
                Thread t = new Thread(() => handler.AddForExecution(() =>
                {
                    Console.WriteLine($"Thread with order {tmp} will add task");
                    TestHelper.TestMethod(tmp, 2);
                }));
                t.Start();
            }

            Console.ReadLine();
        }


    }
}
