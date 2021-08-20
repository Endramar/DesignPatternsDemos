using System;
using System.Threading;
using System.Threading.Tasks;

namespace PerThreadSingleton
{
    /// <summary>
    /// The singleton class per thread
    /// </summary>
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance = new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());

        public int Id;

        //Make the constructor private to prevent instance creation
        private PerThreadSingleton()
        {
            // To see the id of the current thread
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => threadInstance.Value;
    }

    /// <summary>
    /// A project for creating singleton design not for entire application but for each thread
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // First thread
            var thread1 = Task.Run(() =>
            {
                Console.WriteLine("T1 : " + PerThreadSingleton.Instance.Id);
            });

            // Second thread
            var thread2 = Task.Run(() =>
            {
                Console.WriteLine("T2 : " + PerThreadSingleton.Instance.Id);
                Console.WriteLine("T2 : " + PerThreadSingleton.Instance.Id);
            });

            Task.WaitAll(thread1, thread2);

        }
    }
}
