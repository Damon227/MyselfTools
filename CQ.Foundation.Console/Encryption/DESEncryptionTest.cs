using System;
using System.Collections.Generic;
using System.Text;
using CQ.Foundation.Encryption;

namespace CQ.Foundation.Console.Encryption
{
    public class DESEncryptionTest
    {
        public static void StartTest()
        {
            string content = "你a#12";
            string key = "aabbfdhr";

            string s1 = DESEncryptHelper.DesEncrypt(content, key);
            string s2 = DESEncryptHelper.DesDecrypt(s1, key);

            if (s2.Equals(content))
            {
                System.Console.WriteLine("DES加密解密通过");
            }

            string s3 = DESEncryptHelper.DesEncrypt(content);
            string s4 = DESEncryptHelper.DesDecrypt(s3);

            if (s4.Equals(content))
            {
                System.Console.WriteLine("DES加密解密通过");
            }
        }
    }
}
