using System;
using CQ.Foundation.Console.Encryption;
using CQ.Foundation.Encryption;

namespace CQ.Foundation.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            DESEncryptionTest.StartTest();
            System.Console.ReadLine();
        }
    }
}
