using System;
using System.Threading;
using TaskHandler;
using Xunit;

namespace Tests
{
    public class TaskHandlerTest
    {
        [Fact]
        public void TestForTaskHandler()
        {
            var handler = TaskSequHandler.GetInstance();

            handler.AddForExecution(() => Console.WriteLine("Test1"));
            handler.AddForExecution(() => {
                Thread.Sleep(2000);
                Console.WriteLine("Test2");
            });
            handler.AddForExecution(() => Console.WriteLine("Test3"));
        }
       
    }
}
