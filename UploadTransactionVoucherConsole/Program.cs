using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using Dapper;
using KC.Foundation;
using KC.Foundation.Sys;
using KC.Foundation.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Newtonsoft.Json.Linq;

namespace UploadTransactionVoucherConsole
{
    internal class Program
    {
        private static string s_path = @"D:\Work\Kolibre\Doc\蜂鸟屋管家\18年青年汇贷后收款数据\四月\";
        private static string s_prefix = "images";
        private static string s_connectionString = "Data Source=kc-fengniaowu-pro.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Pro;Integrated Security=False;User ID=KC;Password=FgQy20VM*feO!S7E;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static CloudBlobContainer _blobContainer;

        private static readonly List<string> s_photosContentTypes = new List<string>
        {
            ".png",
            ".jpeg",
            ".jpg",
            ".gif",
            ".tiff"
        };

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CloudStorageAccount account = CloudStorageAccount.Parse("BlobEndpoint=https://fengniaowudev.blob.core.chinacloudapi.cn/;QueueEndpoint=https://fengniaowudev.queue.core.chinacloudapi.cn/;TableEndpoint=https://fengniaowudev.table.core.chinacloudapi.cn/;AccountName=fengniaowudev;AccountKey=wxA2p+FMsAvKopxtAPQPUcqWO1Gw2Tw5KqBYlODfSrB53eGXln63HvSb+BeEIKlNzb6LUAMBOzsdSurtPLZ7Fg==");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            blobClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(3.Seconds(), 5);
            _blobContainer = blobClient.GetContainerReference("saas");
            _blobContainer.CreateIfNotExistsAsync().Wait();

            Console.WriteLine("读取图片中。。。。。。。。。。。。。");

            //1.获取所有的图片

            DirectoryInfo dir = new DirectoryInfo(s_path);
            System.IO.FileInfo[] fileInfo = dir.GetFiles();

            List<string> fileNames = new List<string>();

            List<string> uploadSuccess = new List<string>();

            List<string> uploadFailed = new List<string>();

            foreach (System.IO.FileInfo item in fileInfo)
            {
                if (s_photosContentTypes.Contains(item.Extension.ToLower()))
                {
                    string filename = item.Name.Substring(0, item.Name.IndexOf('.'));
                    fileNames.Add(filename);
                    int count = DoDatabase(filename, item.OpenRead());

                    if (count > 0)
                    {
                        uploadSuccess.Add(filename);
                    }
                    else
                    {
                        uploadFailed.Add(item.Name);
                    }
                }

            }

            //File.AppendAllLines(s_path + "1.txt", fileNames);

            File.AppendAllLines(s_path + "Failed" + Time.Now.ToUnixTimestamp() + ".txt", uploadFailed);

            File.AppendAllLines(s_path + "Success" + Time.Now.ToUnixTimestamp() + ".txt", uploadSuccess);

            Console.WriteLine("图片上传完毕。。。。。。。。。。。。。");

            Console.WriteLine("正在清除缓存。。。。。。。。。。。。。");

            //foreach (string liushuiId in uploadSuccess)
            //{
            //    Result result = ClearCacheAsync(liushuiId);

            //    if (!result.Succeeded)
            //    {
            //        Console.WriteLine(liushuiId + "清除缓存失败");
            //    }
            //    else
            //    {
            //        Console.WriteLine(liushuiId + "清除缓存成功");
            //    }
            //}

            Console.WriteLine("缓存清除完毕。。。。。。。。。。。。。");

            Console.ReadKey();
        }


        public static int DoDatabase(string filename, Stream file)
        {
            using (IDbConnection db = GetDbContext())
            {
                string sql = "SELECT COUNT(transactions.TransactionId) from dbo.[KC.Fengniaowu.Talos.Transactions] AS transactions WHERE transactions.TransactionId=@TransactionId AND  transactions.Enabled=1";
                int count = db.Query<int>(sql, new { TransactionId = filename }).Single();

                int result = 0;

                if (count > 0)
                {
                    //上传图片

                    string uploadUrl = UploadCarFiles(filename, file);

                    if (uploadUrl.IsNotNullOrEmpty())
                    {
                        sql = "UPDATE dbo.[KC.Fengniaowu.Talos.Transactions] SET TransactionVoucher=@TransactionVoucher WHERE TransactionId=@TransactionId";

                        result = db.Execute(sql, new { TransactionId = filename, TransactionVoucher = uploadUrl });
                    }
                }
                return result;
            }
        }

        public static Result ClearCacheAsync(string liushuiId)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://kc-fengniaowu-talos.kolibre.credit");
            httpClient.DefaultRequestHeaders.Add("X-KC-SID", "C+9CF61E0EC77E4A9FAA51016390FD677F");
            HttpResponseMessage response = httpClient.GetAsync("api/Transaction/ClearCache?transactionId=" + liushuiId).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failed("流水缓存清除失败", 0);
            }
            if (result.IsNullOrEmpty())
            {
                return Result.Failed("流水缓存清除失败，接口返回NULL OR EMPTY", 0);
            }
            JObject obj = JObject.Parse(result);
            if (!(bool)obj["succeeded"])
            {
                return Result.Failed("失败", 0);
            }

            return Result.Succeed(1, "成功");
        }



        private static string UploadCarFiles(string orderId, Stream fileStream)
        {
            string baseUrl = _blobContainer.ServiceClient.BaseUri.ToString();

            string fileName = Guid.NewGuid().ToString().Replace("-", string.Empty).Trim().ToUpper() + ".png";

            FileInfo fileInfo = SaveResourceAsync(fileStream, s_prefix, fileName, "Transaction");

            string url = fileInfo.Path.GetFirst() == "/" ? fileInfo.Path.Remove(0, 1) : fileInfo.Path;
            return Path.Combine(baseUrl, url);
        }

        /// <summary>
        ///     保存资源到Blob。
        /// </summary>
        /// <param name="file">资源。</param>
        /// <param name="prefixPath">资源路径。</param>
        /// <param name="fileName">资源完整名称，含路径。</param>
        /// <param name="contentType">资源类型。</param>
        /// <param name="kind">资源种类。</param>
        private static FileInfo SaveResourceAsync(Stream file, string prefixPath, string fileName, string contentType, string kind = "others")
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            string fullName = prefixPath.IsNullOrEmpty() ? fileName : prefixPath.Last() == '/' ? prefixPath + fileName : prefixPath + "/" + fileName;

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fullName); //public path
            blob.Properties.CacheControl = "no-cache";
            blob.Metadata["FileName"] = Convert.ToBase64String(fileName.GetBytesOfUTF8());
            blob.Metadata["Kind"] = kind;

            blob.UploadFromStreamAsync(file).Wait();
            blob.FetchAttributesAsync().Wait();

            return new FileInfo
            {
                Name = fileName,
                Kind = kind,
                Exists = true,
                IsDirectory = false,
                LastModified = blob.Properties.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow),
                Length = blob.Properties.Length,
                Path = blob.Uri.PathAndQuery,
                TempPath = null
            };
        }

        private static IDbConnection GetDbContext()
        {
            SqlConnection db = new SqlConnection(s_connectionString);
            return db;
        }
    }
}
