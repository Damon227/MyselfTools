using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public static class QueueTest
    {
        private static readonly ConcurrentQueue<string> s_concurrentQueue = new ConcurrentQueue<string>(new List<string>
        {
            "a", "b", "c", "d"
        });

        public static void Peek()
        {
            if (s_concurrentQueue.TryPeek(out string message))
            {
                Console.WriteLine(message);
            }
        }

        public static void Dequeue()
        {
            if (s_concurrentQueue.TryDequeue(out string message))
            {
                Console.WriteLine(message);
            }
        }
    }
}
