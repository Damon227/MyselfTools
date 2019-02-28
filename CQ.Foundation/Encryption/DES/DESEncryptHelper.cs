using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CQ.Foundation.Encryption
{
    /// <summary>
    ///     DES加密解密帮助类
    /// </summary>
    public static class DESEncryptHelper
    {
        /// <summary>
        ///     key 只能是8位。
        /// </summary>
        private static readonly string s_defaultKey = "b0puqMzV";

        /// <summary>
        ///     DES加密算法
        /// </summary>
        /// <param name="encryptContent">需要加密的字符串</param>
        public static string DesEncrypt(string encryptContent)
        {
            return DesEncrypt(encryptContent, s_defaultKey);
        }

        /// <summary>
        ///     DES加密算法
        ///     key为8位
        /// </summary>
        /// <param name="encryptContent">需要加密的字符串</param>
        /// <param name="key">密钥</param>
        public static string DesEncrypt(string encryptContent, string key)
        {
            if (string.IsNullOrEmpty(encryptContent) || string.IsNullOrWhiteSpace(encryptContent))
            {
                throw new ArgumentNullException(nameof(encryptContent));
            }

            if (key.Length != 8)
            {
                throw new ArgumentException("密钥长度必须为8位");
            }

            StringBuilder ret = new StringBuilder();

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(encryptContent);
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }

            return ret.ToString();
        }

        /// <summary>
        ///     DES解密算法
        /// </summary>
        /// <param name="decryptContent">需要解密的字符串</param>
        public static string DesDecrypt(string decryptContent)
        {
            return DesDecrypt(decryptContent, s_defaultKey);
        }

        /// <summary>
        ///     DES解密算法
        ///     key为8位
        /// </summary>
        /// <param name="decryptContent">需要解密的字符串</param>
        /// <param name="key">密钥</param>
        public static string DesDecrypt(string decryptContent, string key)
        {
            if (string.IsNullOrEmpty(decryptContent) || string.IsNullOrWhiteSpace(decryptContent))
            {
                throw new ArgumentNullException(nameof(decryptContent));
            }

            if (key.Length != 8)
            {
                throw new ArgumentException("密钥长度必须为8位");
            }

            MemoryStream ms = new MemoryStream();

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[decryptContent.Length / 2];
            for (int x = 0; x < decryptContent.Length / 2; x++)
            {
                int i = (Convert.ToInt32(decryptContent.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(key);

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
