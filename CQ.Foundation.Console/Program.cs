using System;
using CQ.Foundation.Console.Encryption;
using CQ.Foundation.Encryption;

namespace CQ.Foundation.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            string a = "9BD434965EDD46C3A2CC015A2779FE60";
            string put = null;
            Guid g = Guid.Empty;
            if (Guid.TryParse(a, out g))
            {

            }

            DESEncryptionTest.StartTest();
            System.Console.ReadLine();
        }
    }
}
