using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation.Tools;

namespace ConsoleApp1
{
    public class MagicLockTest
    {
        private readonly MagicLock _magicLock = new MagicLock();

        public void StartTest()
        {
            List<Task> tasks = new List<Task> { Task.Run(() => LockWork(1)), Task.Run(() => LockWork(2)), Task.Run(() => LockWork(3)), Task.Run(() => LockWork2(4)), Task.Run(() => LockWork2(5)), Task.Run(() => LockWork2(6)) };

            Task.WhenAll(tasks).Wait();
        }

        private void LockWork(int i)
        {
            lock (_magicLock.GetLock("key1"))
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                Console.WriteLine(i);
            }
        }

        private void LockWork2(int i)
        {
            lock (_magicLock.GetLock("key2"))
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                Console.WriteLine(i);
            }
        }

        private void Work(int i)
        {
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            
            Console.WriteLine(i);
        }
    }
}
