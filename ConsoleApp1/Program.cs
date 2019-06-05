using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Foundation.Tools;
using KC.Foundation.Sys;

namespace ConsoleApp1
{
    class Program
    {
        private static void Main(string[] args)
        {
            //FileReadWriteTest.Work();

            //TeacherTest.Test();

            //MagicLockTest test = new MagicLockTest();
            //test.StartTest();

            //QueueTest.Peek();
            //QueueTest.Peek();
            //QueueTest.Peek();
            //QueueTest.Peek();

            //QueueTest.Dequeue();
            //QueueTest.Dequeue();
            //QueueTest.Dequeue();
            //QueueTest.Dequeue();

            CertTest();
            Console.ReadKey();
        }

        private static void CertTest()
        {
            //string privateKey = "MIIEowIBAAKCAQEAsBMGWETjJpEA0fl8aHKbHy73/gGtWLzbJ0RGPzk06pLcBhM8GfZqWJS8JxGyT904Vkp++3aYp7MGuz/3gUWKA73MjUzlMpe/cDUus4yHZ6Wmd6m+VYdkgq0HjSdSIRUCkK7DgNj2WEYNp/BaPi1eNvhnAIAqJLrNz6ilG3QXXYSRLmulpcV2G73J7y8nUMOHAY0wvXQ4K2YS/upCj6MnzLQylIuLXrV3DYPj6Zl6NTdtQa4SiIEIAs63vlX5SySmglqblqKh6+liEh5/GH3bkaQZ8PpQ5wnkI52198oDeBD9ysb6rOYqbLd5pHFt7GvNprqLP7L6DTaX6Bd8K3QnMQIDAQABAoIBAHecpP3H5mvnq5/5FSu7V2fqt2ul5gXXXiWhuvruOOV12OSDopuci4gbmmoMGo8ooEH2n6biXTfnZZZFETWPcP+zq0b+SuGAtvtaFZzBxaaCoMC/3l4p+PzQ4MViRefVZwW0w3k/arJy/InuvM9qcIt1Wju71WVYRcJhNbCdv9W1vKWnyzGzqUGz/TyNrSNIihfDVHDLLLazYIvEWkcca7RR0AHsRwA7UJ/grectF+ewwdy/YgYO761/Q1MpPVMPL4gH6QKyOSOHdz4S5AXjiOqdEj55wXSvjrkL+CcyfzzwiG81NIAHr2dFwUq9abuxSaa6V7zRxZRFQkvFdpnZGUECgYEA5kasSnkwbeNNzhsB8YtiTijWBuR8v/AxBDqIbhi8l1izHRaosfiECZ15i/+70gf8nMKweyOLCK7aQXpkN0GIRk46UIgZbMjrwIv4jAyn1C+Gmk3hwNT0Z6e4fnlX3OINSXAPmf33xefWtPEgYVVW7Q0KgW0wKTBNeC/MGmE80UkCgYEAw75QxWvrmCMzb4AX7MqBRVtlPZ2a2w8TIu6PR0eaIpN4tnqLLwbU6MauXELt2UcqJEiWaECnsbALBUeDDeiIilJi6GYevWAHwJ+IvcFwJ9AdWBofeBKtFc6JmugH+nxqlryzNGxpvb/QyR0aUSpKJO27KAhJ6R9RxbsRQIv8DqkCgYEAxidsnQ1aEkV2L3RoIL1rk4xqWDYH73a2B0iDHBJnPulSACb+dV8/57I95K3kpqC+zPpvuRblzkwAfjXexPm8VZt9bM0g686dp+wKriE5zkozTtbT/AaEZioahaLPa8CN5HLsyfuUWhXdWYjg70Drc0M0lhkqR1dMYq7muJCSMBkCgYAyRGd9q28/DJsi4SRDvOVhqEzhe93Or5pceCL3TR36DyEKy4F+vCRk+tDABLgL+kiKNNHbR+9IpErQOkMfiT23N90KjV8LhuO2xcFZ6ZkhwEIo49x8+17ToMyZqUiTPOwqdZ8XsVeeUOoPG5gsi82qmhpb93TzlwVq39VYXBqIsQKBgDEgUJ70ywAGTJf1MaZAmbaoTWx0CSa4dtkK64YV/WKiLCntqC92pgL+Jp5sz6yQqJJAkq5CdmKJTxb5lf5p/Ilq+To9qBwgf8LoX5mg4MQC8h8fVEtCwOkioElrX/AxXuPaFemlcjFY3QoCtIY9Ucw+2RIGlqXE9OIqPEpecGx4";
            //string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsBMGWETjJpEA0fl8aHKbHy73/gGtWLzbJ0RGPzk06pLcBhM8GfZqWJS8JxGyT904Vkp++3aYp7MGuz/3gUWKA73MjUzlMpe/cDUus4yHZ6Wmd6m+VYdkgq0HjSdSIRUCkK7DgNj2WEYNp/BaPi1eNvhnAIAqJLrNz6ilG3QXXYSRLmulpcV2G73J7y8nUMOHAY0wvXQ4K2YS/upCj6MnzLQylIuLXrV3DYPj6Zl6NTdtQa4SiIEIAs63vlX5SySmglqblqKh6+liEh5/GH3bkaQZ8PpQ5wnkI52198oDeBD9ysb6rOYqbLd5pHFt7GvNprqLP7L6DTaX6Bd8K3QnMQIDAQAB";

            // pro star.fengniaowu.com
            //string privateKey = "MIIEpAIBAAKCAQEA4G+D0try1mcCe1ezn1tUAu2D2rRj7fO6eSoRvbuzIps3TW9M4q/+n50+Lyx6Mvc0M7ySNGm2QWufCnQxFezHM0fnSZQpTe1GOZnqJAixKYey5LyZMP/sND44Nw9T8G9yr/Ycf2vAPQAAPvnb0rJ5yOLZhZWOUIB+L+o5Qmj9bII5aV8q94sjwhEhvhr33TMphmT4W+xL+p3f6WAWeKAm2SGTKYTC9M1mXf6Jqr7rsUnqQ4htfC7F9aitRPOYlm83XnCRoykOr2UuleFa2FtrW/XCDoKM0T6Z9Q7mb9T30HMcBGxNC8/OLt1U2q4vchX3BslBerPiePr23nbyHWIC7QIDAQABAoIBAQDdwDD+eAixoIvKNKgsTr9eOTErOUr3A/b92nw2/CsNiVEF+G9+Fyg8qs+TKp3+l1ODvtCyarSCRQSZ/p21LQLQ/z1d7L822LanGRVHzzLEduhhqpqt2F+YatV+SW3FRUiK5V/Y4izJ2KNlldgFgQRlYQZNP6yii6KiUwmKPVQQDW0gOzXvCB4g9vxBtqBmzNggGMYYgeXb+XTQMZ4fw8KsGsBVsED1sibmAtuwD15voVAKryLh+5Fe7QRXpv2YeUgYaawRonyxB62kfAx9seBa4d3sqdgjOhArsX8+BdEY58zhjhUZBfgcgIVkT0ea0DR4ZkLo2KrJPO++iAl8GC6hAoGBAPfG07QZyFubxtiXYeAnRzXN4BAZzeBW4O4HqOkMkyuCiJXbFdiQodyNaX2bTBx/fHQ+1AcpbDCdynrHbryJysBReWASKaFuv/TLHlWrEpcsh3GTZpJ4RA4QEunuEgijNMJRhRRcUJdyrEXIAaXpoLr2XvISSmDIMU4sBZy7h3tpAoGBAOfiYFzfAo0IZLrPqe44mGIxd/Fp1uqSGKulPcfM2SlnoapnbcdDyF3lFgDJC2p1sl9+0bhvyu6GJFfE3sLF3h3QVPjqa2AY5EPunPvdDhJsB3AEB/9veCKfPgF/gftk46Z/cd4YT7tfZ6EWcWS4xjRvelYd0ssWX6V3FwQfLe7lAoGBAKMpFrd+UuWgSuzNovIMa2QO0gFIPL4B99RGzv/H/BGmv3KO9m/JrgyiQlkoLpKFkNqrlMuyhz/saC05rr4Qrqa+AAM3cpqPZ3A3YR5grLlH5VjxC5ZgA9CO3SWWU+HENzXOoKerspZIOREqh4tNsT3shtE5IilhKrajXn67LpghAoGAb3YDN+ECiuQz9xAG8LlHljuNGf/0DID5Vthw3+95m0tzDEP3IsdUMr4BW5NH5353pSuCh5NFCm50XbQGF72gzNftYFDeGC0e3ACAeRiCwRwoXxGw/nmIV6wP1AyEYGDYhJnSFB2/haOQXoA5NV8T9vR265oeTvyMMS8TYynRnHkCgYAKnz63x6Nb5dKK0Ia9gWi0QVaK+1IcfmGOxhGSREt9yBgXbrPZZr+crhsm6z9HNU3QzHRGYiq88q2EiwafzQJk0W/kTyQ9LeeB65xNW9Zbx/r6vLUX4n/fPaamWFTkSGWmYar7olwdi9b5fLlP5Fx6hRH1GdzVIGXo5zntQLMPRA==";
            //string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4G+D0try1mcCe1ezn1tUAu2D2rRj7fO6eSoRvbuzIps3TW9M4q/+n50+Lyx6Mvc0M7ySNGm2QWufCnQxFezHM0fnSZQpTe1GOZnqJAixKYey5LyZMP/sND44Nw9T8G9yr/Ycf2vAPQAAPvnb0rJ5yOLZhZWOUIB+L+o5Qmj9bII5aV8q94sjwhEhvhr33TMphmT4W+xL+p3f6WAWeKAm2SGTKYTC9M1mXf6Jqr7rsUnqQ4htfC7F9aitRPOYlm83XnCRoykOr2UuleFa2FtrW/XCDoKM0T6Z9Q7mb9T30HMcBGxNC8/OLt1U2q4vchX3BslBerPiePr23nbyHWIC7QIDAQAB";

            // mp.fengniaowu.com
            string privateKey = "MIIEpQIBAAKCAQEAtNNUHWvhmk533OkSEdsZ//LpSmrfjHptuDv91pBlFGb6ErKyVgt5ZyYqJxviE9sHSmsWKGWvpPj6yXKZU0Vm0hqUtShYUw5SySZTXSl0l3d7v4UzXhpDDSNjCWAz7UKRbURdALq9Jq+mP6XMsDyiLSEE2TXHFCUafeimqLHd5zBguuRfHkLyzEZzwCyk9bxbVI6JRxmyGuJvtjMobGDRN0Sfs5fjL0fAFPJM8kWpi0qYcnnqNkikvZokEuhYWMnPvhoekav0MQSIrVwf/siT1Kyuv3DHQay27Oj0wrmYHB4BU7kVqaNaAotE0AX2rBN5/e/Mii9qNx1mzUwapAjEGwIDAQABAoIBAQCvirCP11kuOZr3JHRcTT2SUbjUe6gFchzty5+DGq5l/goq+LtT+NOkpVIqoVD6QZl0Tz01fnHjT6n2wn/CbQ+CTDetHjuQdck8aS5otIPxPxctOBxD8G7DZGUShwgh/ou7bbBzstVJpx9XjpeFR3eRJArW6vQWxGZRcPfJsNJhSd37pZMUFkJpCkfN5CpeMxqdEhjF4E79Fo9eVrSBwWQ9n3FF1HNlKoi5uCEN6SLxEnVecNQ4BpFh8fwEOSf4pbhSpQsTN0BkJO78vahTAGN+3c4ZuOZ4sVn5PzWXI/GGYCUwM/goued+eJizth4HVbcMJxA8q/DRKfraFHMozK6xAoGBANkgKV/ocqPy0DB7T0IR+AilkBIZyVACKP/Hr4yuixOF2VXjfzD/+/XwguDO0vEBYgUIuMyK7tiApV95U9fD4Tsdze9a/JxXUMvHr2MOYD0vuI7HlNPoaw8I9aN7WYUu5WVXe/tXT/1qOtFFHYjm3eepIgFTQN1lnoTE7WTZZwRNAoGBANUzXqgY2GK+6x4YxcH4Riy28S4APETYormq6A/yLzzS5/khmlxoXINIZW6sXCkCmYf9cyxDnWxwMHLYHutEwaBqEV28P5ziccaYgy8x/fjkw+qREnSqg1pqGRjnigPWE+gCSSgUfwbWx2GOHZeBzd6dRX3AB2MF5+9ym7SWYT4HAoGBAMn/lN6IWBdVMADax4r1QqdwuE9OtC5+Q/xqn1SnkfXwwZiZVNyWWqedQXW6lR7opXN/gMWyv7CmRrRDIRCMSL+B4/Q51ufwh9d+CBqE5l4mRnJ3FWvsP6W2NSkJ+efhEEOvF0ZnHaDqspu8zFRwTKRYgq1u8drHzqlAuIbph2DFAoGAEimJI8rSDD91ah/0IN0UpVSEYUXV3IuT+Tss/8bC7WyOJHQPYVXmo6x0L6RxSXEWQTZ/LEMqUwWPqwjsQWCfGhpPFmwmAyhrRt7RNiENILnkUsQ/DT9FZ1tofe2jnUYMdhRTxR1R4PRFEfN4Y+LD9fXx6I+GUZI8OvLJfu3cpNsCgYEAjW7u6rILrVSSFvnTScvhAp4TdTj9TcN6hwNUWLzmxyXMSKZkYJuTgEwYG2s3umf5LvIvK0EgNcz19iL2cmvG/XEWEZiSgD9yEFv4EErnCDVV7vA+gVYxMpJ0XuU5D1GHiMIrITvbvNtem9OQVqKKKenpxzIF8ptkqvq9RYXRkhQ=";
            string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtNNUHWvhmk533OkSEdsZ//LpSmrfjHptuDv91pBlFGb6ErKyVgt5ZyYqJxviE9sHSmsWKGWvpPj6yXKZU0Vm0hqUtShYUw5SySZTXSl0l3d7v4UzXhpDDSNjCWAz7UKRbURdALq9Jq+mP6XMsDyiLSEE2TXHFCUafeimqLHd5zBguuRfHkLyzEZzwCyk9bxbVI6JRxmyGuJvtjMobGDRN0Sfs5fjL0fAFPJM8kWpi0qYcnnqNkikvZokEuhYWMnPvhoekav0MQSIrVwf/siT1Kyuv3DHQay27Oj0wrmYHB4BU7kVqaNaAotE0AX2rBN5/e/Mii9qNx1mzUwapAjEGwIDAQAB";


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

        private static void Test3()
        {
            List<TestClass> list = new List<TestClass>();

            List<string> ids = list.FindAll(t => t.Id == "1").Select(t => t.Id).ToList();
        }
    }
}
