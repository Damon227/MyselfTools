using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DamonHelper.sys;

namespace DamonHelper
{
    public partial class TalosEncrytForm : Form
    {
        public TalosEncrytForm()
        {
            InitializeComponent();
        }

        private void btn_TalosEncryt_GenerateToken_Dev_Click(object sender, EventArgs e)
        {
            string privateKey = "MIIEpAIBAAKCAQEApxUB9rN/U19i5SY8mPhnJncp096StLY27cCQvaIZxalldgPkq//8Y1gF/BBkhrXVK6P4dOZ3GA98aC3GQ0oczfrR0xYDwoFcrFiM9EWRMg1mbuTweYjr3F9gKMYqKmsDIw5kwjtXy/EJD5k1VL4YZLNvoI2OcFL2ofZugH050OqGbCQvUr3O8DuiTd2uy3G/UinlFG8TnuDLO0kB4HI2h1cqyuGLwNH3nTQBVRsXQnszEg33qZ12UveRFl01bPwp7LS2bAoTxpexXH4p21fG2I1Qc0+HhMQYmEsqoxMI+PWFPhcWNjif4ffP0jOTgv5Xj+7tOvM2uG1MyL2X39wqdwIDAQABAoIBABdPHCEwGmplq2ofeE246HRaec3dG6df/PAUhhIb3ATnechJ3mwhFV3rSO5zf6q2vEtPH+7Nu2idDx8zikNsl/HOdNjswHqGnzNLU+K8v8xKnUiJjNjPrA6S3wXhML8OgyIFXzN6TD3J3cM80vlZtGp8CUMjQcSpdk8oY3MZGpM/+CxXGQRCJg1Y1EjWRg4Zta/v9Ew7H7jjO8BYHmZGqLLYRCU3hhKbcSZIBrzSVMWrkYf28I3YnzJqC7Sypcz6935gGWYPP++7FEL1F19a7oh4eHoQBONyY8zvUjIPleOfqQ3uFpjrAD7lYQH6VMyy7EGTFPsVFovTQa+S38i0n1ECgYEA2e70KYpLpe07lwEvLzlmSZFGpuqFqcQkY0EvG+SmrjGudLfW3xCkCJPu91P4YXpVnRrM+FNZLA9cfpqQsVn0VnxWr8D8sa2aulez0ngdh7dN0+Amndw30S44+0+mS/WpTYdlmIIlraYEZd1UKvPQt4jC6KsmXZ94gRL3dCTQmtECgYEAxEQzKaCKjIT5pHbxAscWkWUOtgWtXS3uGjbDJbb1u+pRK1WhZnJEJQTNrJro+dmXNYmOGrtRmlkTNAHBXgTGlPJ7JKNHMbIDRRKTQYfHG9fa43ScojxfCIMt1M9C3ss1IGfdTREuSwVIKKewuXu7CTvTcVUuG3vBXl6uyDvHMscCgYEAzHIfueyUJRfVxEaHJk05ySAxYJs4Q1fPLxHSiN9LArV2zOY9/EtXJvjowDuVEpU6RcUDrp46VFwg0GBeBxK1ZoEEraJlLKYJNnDaMEMgqNXsfCyW7ZbPAjzTledVwYK3jhTL2XOWtz28eHdW42iRT+xLtQwCVOgqmyPY4qs6y1ECgYEAuHVIiIUD8hE/9xx6Yxvtz/RkoRow13RFZYm2WwnbZiNQ6iNur7QQ66HaE7D76WZhQMXpMqJ+jUvOSoCj1dMzN+W1areyP7iIbjCJCXus3DRA2qlMVQXcY0m2l128AVyGr9AuIzJUidmK+fqwk4MARCtgDBH8ZYQoNqNSLYpRKx0CgYAjVg9hllYvNbfBQL12Dl8PqPPxXzIJhocvyhzcYG1ZmR/dSmpNtqDK1xwV3Q3KDwzsg7en9wzHRiOvNSe2RDyszhnbENpDgbA966CowIxBbuY4BCBxpVVe38l+lQA3ACMKMn8uDqa1wgkDoNNrRJeOvbIJ/C6ucyL1pdYXtGdFuA==";

            string token = GenerateToken(privateKey);

            txb_TalosEncryt_DevToken.Text = token;
        }

        private void btn_TalosEncryt_GenerateToken_Pro_Click(object sender, EventArgs e)
        {
            string privateKey = "MIIEowIBAAKCAQEAsBMGWETjJpEA0fl8aHKbHy73/gGtWLzbJ0RGPzk06pLcBhM8GfZqWJS8JxGyT904Vkp++3aYp7MGuz/3gUWKA73MjUzlMpe/cDUus4yHZ6Wmd6m+VYdkgq0HjSdSIRUCkK7DgNj2WEYNp/BaPi1eNvhnAIAqJLrNz6ilG3QXXYSRLmulpcV2G73J7y8nUMOHAY0wvXQ4K2YS/upCj6MnzLQylIuLXrV3DYPj6Zl6NTdtQa4SiIEIAs63vlX5SySmglqblqKh6+liEh5/GH3bkaQZ8PpQ5wnkI52198oDeBD9ysb6rOYqbLd5pHFt7GvNprqLP7L6DTaX6Bd8K3QnMQIDAQABAoIBAHecpP3H5mvnq5/5FSu7V2fqt2ul5gXXXiWhuvruOOV12OSDopuci4gbmmoMGo8ooEH2n6biXTfnZZZFETWPcP+zq0b+SuGAtvtaFZzBxaaCoMC/3l4p+PzQ4MViRefVZwW0w3k/arJy/InuvM9qcIt1Wju71WVYRcJhNbCdv9W1vKWnyzGzqUGz/TyNrSNIihfDVHDLLLazYIvEWkcca7RR0AHsRwA7UJ/grectF+ewwdy/YgYO761/Q1MpPVMPL4gH6QKyOSOHdz4S5AXjiOqdEj55wXSvjrkL+CcyfzzwiG81NIAHr2dFwUq9abuxSaa6V7zRxZRFQkvFdpnZGUECgYEA5kasSnkwbeNNzhsB8YtiTijWBuR8v/AxBDqIbhi8l1izHRaosfiECZ15i/+70gf8nMKweyOLCK7aQXpkN0GIRk46UIgZbMjrwIv4jAyn1C+Gmk3hwNT0Z6e4fnlX3OINSXAPmf33xefWtPEgYVVW7Q0KgW0wKTBNeC/MGmE80UkCgYEAw75QxWvrmCMzb4AX7MqBRVtlPZ2a2w8TIu6PR0eaIpN4tnqLLwbU6MauXELt2UcqJEiWaECnsbALBUeDDeiIilJi6GYevWAHwJ+IvcFwJ9AdWBofeBKtFc6JmugH+nxqlryzNGxpvb/QyR0aUSpKJO27KAhJ6R9RxbsRQIv8DqkCgYEAxidsnQ1aEkV2L3RoIL1rk4xqWDYH73a2B0iDHBJnPulSACb+dV8/57I95K3kpqC+zPpvuRblzkwAfjXexPm8VZt9bM0g686dp+wKriE5zkozTtbT/AaEZioahaLPa8CN5HLsyfuUWhXdWYjg70Drc0M0lhkqR1dMYq7muJCSMBkCgYAyRGd9q28/DJsi4SRDvOVhqEzhe93Or5pceCL3TR36DyEKy4F+vCRk+tDABLgL+kiKNNHbR+9IpErQOkMfiT23N90KjV8LhuO2xcFZ6ZkhwEIo49x8+17ToMyZqUiTPOwqdZ8XsVeeUOoPG5gsi82qmhpb93TzlwVq39VYXBqIsQKBgDEgUJ70ywAGTJf1MaZAmbaoTWx0CSa4dtkK64YV/WKiLCntqC92pgL+Jp5sz6yQqJJAkq5CdmKJTxb5lf5p/Ilq+To9qBwgf8LoX5mg4MQC8h8fVEtCwOkioElrX/AxXuPaFemlcjFY3QoCtIY9Ucw+2RIGlqXE9OIqPEpecGx4";

            string token = GenerateToken(privateKey);

            txb_TalosEncryt_ProToken.Text = token;
        }

        private static string GenerateToken(string privateKey)
        {
            string temp = $"{GetMillisecond()},{new Random().Next(100000, 999999)}";
            byte[] data = Encoding.UTF8.GetBytes(temp);

            RSA rsa = RSAHelper.CreateRsaProviderFromPrivateKey(privateKey);
            byte[] signatureData = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            string signature = Convert.ToBase64String(signatureData);

            return $"{temp},{signature}";
        }

        public static long GetMillisecond()
        {
            long ticks = Time.Now.UtcTicks - 621355968000000000;

            return ticks / 10000L;
        }

        private void TalosEncrytForm_Load(object sender, EventArgs e)
        {
            txb_TalosEncryt_DevToken.Text = null;
            txb_TalosEncryt_ProToken.Text = null;
        }
    }
}
