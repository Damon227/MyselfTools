// ***********************************************************************
// Solution         : MyselfTools
// Project          : TaskConsole
// File             : Program.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("application start.");

            List<string> list = new List<string>(10000);

            // 若 list 长度为 10000，有10个线程，则每个线程处理1000条数据

            for (int k = 0; k < 10; k++)
            {
                new Thread(() =>
                {
                    Console.WriteLine(k);
                    // 在这里执行任务，Thread.Sleep 只是模拟任务需要的时间
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    //for (int i = 0; i < 1000; i++)
                    //{
                    //    Console.WriteLine(k * 1000 + i);
                    //}

                    // 打印当前线程是否完成任务
                    Console.WriteLine(k + "," + Thread.CurrentThread.ManagedThreadId);
                }).Start();
            }

            

            //Parallel.For(0, 1000, i =>
            //{
            //    for (int j = 0; j < 4000; j++)
            //    {
            //        a++;
            //        Console.WriteLine(a);
            //    }
            //});

            //List<Task> tasks = new List<Task>();

            //for (int i = 0; i < 10000; i++)
            //{
            //    tasks.Add(Task.Run(() =>
            //    {
            //        for (int j = 0; j < 400; j++)
            //        {
            //            Console.WriteLine($"i:{i}, j:{j}");
            //        }
            //    }));
            //}

            //Task.WhenAll(tasks).Wait();

            sw.Stop();
            Console.WriteLine("application stop.");
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);

            Console.ReadLine();
        }


        public static string GetMD5Hash(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            return ToPrintableString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(s)));
        }

        public static string ToPrintableString(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString().ToUpperInvariant();
        }
    }
}