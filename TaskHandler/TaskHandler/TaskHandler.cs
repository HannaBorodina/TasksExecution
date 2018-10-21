using System;

namespace TaskHandler
{
    public class TaskHandler
    {
        private static TaskHandler _instance;
        private static object @lock = new Object();
        private

        public static TaskHandler GetInstanse()
        {
            if (_instance == null)
            {
                lock (@lock)
                {
                    if (_instance == null)
                        _instance = new TaskHandler();
                }
            }
            return _instance;
        }

        public void AddForExecution(Action taskToHandle)
        {

        }
    }
}
