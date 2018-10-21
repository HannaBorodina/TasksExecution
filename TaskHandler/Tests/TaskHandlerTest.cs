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
            var handler = TaskHandlerSequentally.GetInstance();

            int count = 0;

            handler.AddForExecution(() => count++);
            handler.AddForExecution(() => count++);

            Thread.Sleep(1000);
            Assert.Equal(2, count);

            handler.AddForExecution(() => count++);
            handler.AddForExecution(() => count++);

            Thread.Sleep(1000);
            Assert.Equal(4, count);
        }

    }
}
