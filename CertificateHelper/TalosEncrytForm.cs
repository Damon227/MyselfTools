using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateHelper
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

            RSA rsa = RSAHelper.CreateRsaProviderFromPrivateKey(privateKey);

            string temp = GenerateToken(privateKey);

            byte[] data = Encoding.UTF8.GetBytes(temp);
            byte[] signData = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            string signDataString = Convert.ToBase64String(signData);
            string token = $"{temp},{signDataString}";

            MessageBox.Show(token);
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
            long ticks = DateTimeOffset.UtcNow.UtcTicks - 621355968000000000;

            return ticks / 10000L;
        }
    }
}
