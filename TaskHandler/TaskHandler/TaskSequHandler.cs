using System;
using System.Collections.Concurrent;

namespace TaskHandler
{
    public class TaskSequHandler
    {
        private static TaskSequHandler _instance;
        private static object @lock = new object();
        private ConcurrentQueue<Action> _tasksToHandle = new ConcurrentQueue<Action>();

        public void CheckQueue()
        {
            lock (@lock)
            {
                if (_tasksToHandle.IsEmpty)
                    return;

                while (!_tasksToHandle.IsEmpty)
                {
                    if (_tasksToHandle.TryDequeue(out Action @task))
                        @task();
                }
            }
        }

        public static TaskSequHandler GetInstance()
        {
            if (_instance == null)
            {
                lock (@lock)
                {
                    if (_instance == null)
                        _instance = new TaskSequHandler();
                }
            }
            return _instance;
        }

        public void AddForExecution(Action taskToHandle)
        {
            _tasksToHandle.Enqueue(taskToHandle);
            CheckQueue();
        }

    }

}
