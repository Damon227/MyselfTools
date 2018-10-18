using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Foundation.Tools;

namespace ConsoleApp1
{
    class Program
    {
        private static void Main(string[] args)
        {
            CertTest();

            Console.ReadKey();
        }

        private static void CertTest()
        {
            string privateKey = "MIIEowIBAAKCAQEAsBMGWETjJpEA0fl8aHKbHy73/gGtWLzbJ0RGPzk06pLcBhM8GfZqWJS8JxGyT904Vkp++3aYp7MGuz/3gUWKA73MjUzlMpe/cDUus4yHZ6Wmd6m+VYdkgq0HjSdSIRUCkK7DgNj2WEYNp/BaPi1eNvhnAIAqJLrNz6ilG3QXXYSRLmulpcV2G73J7y8nUMOHAY0wvXQ4K2YS/upCj6MnzLQylIuLXrV3DYPj6Zl6NTdtQa4SiIEIAs63vlX5SySmglqblqKh6+liEh5/GH3bkaQZ8PpQ5wnkI52198oDeBD9ysb6rOYqbLd5pHFt7GvNprqLP7L6DTaX6Bd8K3QnMQIDAQABAoIBAHecpP3H5mvnq5/5FSu7V2fqt2ul5gXXXiWhuvruOOV12OSDopuci4gbmmoMGo8ooEH2n6biXTfnZZZFETWPcP+zq0b+SuGAtvtaFZzBxaaCoMC/3l4p+PzQ4MViRefVZwW0w3k/arJy/InuvM9qcIt1Wju71WVYRcJhNbCdv9W1vKWnyzGzqUGz/TyNrSNIihfDVHDLLLazYIvEWkcca7RR0AHsRwA7UJ/grectF+ewwdy/YgYO761/Q1MpPVMPL4gH6QKyOSOHdz4S5AXjiOqdEj55wXSvjrkL+CcyfzzwiG81NIAHr2dFwUq9abuxSaa6V7zRxZRFQkvFdpnZGUECgYEA5kasSnkwbeNNzhsB8YtiTijWBuR8v/AxBDqIbhi8l1izHRaosfiECZ15i/+70gf8nMKweyOLCK7aQXpkN0GIRk46UIgZbMjrwIv4jAyn1C+Gmk3hwNT0Z6e4fnlX3OINSXAPmf33xefWtPEgYVVW7Q0KgW0wKTBNeC/MGmE80UkCgYEAw75QxWvrmCMzb4AX7MqBRVtlPZ2a2w8TIu6PR0eaIpN4tnqLLwbU6MauXELt2UcqJEiWaECnsbALBUeDDeiIilJi6GYevWAHwJ+IvcFwJ9AdWBofeBKtFc6JmugH+nxqlryzNGxpvb/QyR0aUSpKJO27KAhJ6R9RxbsRQIv8DqkCgYEAxidsnQ1aEkV2L3RoIL1rk4xqWDYH73a2B0iDHBJnPulSACb+dV8/57I95K3kpqC+zPpvuRblzkwAfjXexPm8VZt9bM0g686dp+wKriE5zkozTtbT/AaEZioahaLPa8CN5HLsyfuUWhXdWYjg70Drc0M0lhkqR1dMYq7muJCSMBkCgYAyRGd9q28/DJsi4SRDvOVhqEzhe93Or5pceCL3TR36DyEKy4F+vCRk+tDABLgL+kiKNNHbR+9IpErQOkMfiT23N90KjV8LhuO2xcFZ6ZkhwEIo49x8+17ToMyZqUiTPOwqdZ8XsVeeUOoPG5gsi82qmhpb93TzlwVq39VYXBqIsQKBgDEgUJ70ywAGTJf1MaZAmbaoTWx0CSa4dtkK64YV/WKiLCntqC92pgL+Jp5sz6yQqJJAkq5CdmKJTxb5lf5p/Ilq+To9qBwgf8LoX5mg4MQC8h8fVEtCwOkioElrX/AxXuPaFemlcjFY3QoCtIY9Ucw+2RIGlqXE9OIqPEpecGx4";
            string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsBMGWETjJpEA0fl8aHKbHy73/gGtWLzbJ0RGPzk06pLcBhM8GfZqWJS8JxGyT904Vkp++3aYp7MGuz/3gUWKA73MjUzlMpe/cDUus4yHZ6Wmd6m+VYdkgq0HjSdSIRUCkK7DgNj2WEYNp/BaPi1eNvhnAIAqJLrNz6ilG3QXXYSRLmulpcV2G73J7y8nUMOHAY0wvXQ4K2YS/upCj6MnzLQylIuLXrV3DYPj6Zl6NTdtQa4SiIEIAs63vlX5SySmglqblqKh6+liEh5/GH3bkaQZ8PpQ5wnkI52198oDeBD9ysb6rOYqbLd5pHFt7GvNprqLP7L6DTaX6Bd8K3QnMQIDAQAB";

            RSA rsa = RSAHelper.CreateRsaProviderFromPrivateKey(privateKey);
            byte[] data = Encoding.UTF8.GetBytes("123abc");
            byte[] signData = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            string signDataString = Convert.ToBase64String(signData);
            string dataString = Encoding.UTF8.GetString(data);

            signData = Convert.FromBase64String(signDataString);
            //data = Encoding.UTF8.GetBytes(dataString);

            RSA rsa2 = RSAHelper.CreateRsaProviderFromPublicKey(publicKey);
            bool verify = rsa2.VerifyData(data, signData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Console.WriteLine(verify);
        }

        private static void CertTest2()
        {
            string file = "cert.pfx";
            string pwd = "password";

            X509Certificate2 cert = new X509Certificate2(file, pwd);
            string a = Convert.ToBase64String(cert.PublicKey.EncodedKeyValue.RawData);

            string publicKey = cert.GetRSAPublicKey().ToCustomXmlString(false);

            string privateKey = cert.GetRSAPrivateKey().ToCustomXmlString(true);

            RSA rsa = RSA.Create();
            rsa.FromCustomXmlString(privateKey);
            byte[] data = Encoding.UTF8.GetBytes("123abc");
            byte[] signData = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            string signDataString = Convert.ToBase64String(signData);

            signData = Convert.FromBase64String(signDataString);

            RSA rsa2 = RSA.Create();
            rsa.FromCustomXmlString(publicKey);
            bool verify = rsa2.VerifyData(data, signData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
