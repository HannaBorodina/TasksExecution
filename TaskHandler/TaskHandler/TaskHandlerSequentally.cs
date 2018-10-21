using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TaskHandler
{
    /// <summary>
    /// Class for handling Tasks:
    /// --- Tasks are executed sequentally
    /// --- Only one task at a time
    /// </summary>
    public class TaskHandlerSequentally
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static TaskHandlerSequentally _instance;

        /// <summary>
        /// Lock object
        /// </summary>
        private static object @lock = new object();

        /// <summary>
        /// Queue for tasks
        /// </summary>
        private ConcurrentQueue<Action> _tasksToHandle = new ConcurrentQueue<Action>();

        /// <summary>
        /// Flag - true if queue is checkin now
        /// </summary>
        private bool _isChecking;

        /// <summary>
        /// To get class instance
        /// </summary>
        /// <returns></returns>
        public static TaskHandlerSequentally GetInstance()
        {
            if (_instance == null)
            {
                lock (@lock)
                {
                    if (_instance == null)
                        _instance = new TaskHandlerSequentally();
                }
            }
            return _instance;
        }

        /// <summary>
        /// To handle tasks in queue
        /// </summary>
        public void CheckQueue()
        {
            if (_isChecking)
                return;

            lock (@lock)
            {
                _isChecking = true;

                if (_tasksToHandle.IsEmpty)
                    return;

                while (!_tasksToHandle.IsEmpty)
                {
                    if (_tasksToHandle.TryDequeue(out Action @task))
                        @task();
                }

                _isChecking = false;
            }
        }

        /// <summary>
        /// To add task(s) for execution
        /// </summary>
        /// <param name="taskToHandle"></param>
        public void AddForExecution(params Action[] taskToHandle)
        {
            foreach (var t in taskToHandle)
            {
                _tasksToHandle.Enqueue(t);
            }

            Task.Run(() => CheckQueue());
        }

    }

}
