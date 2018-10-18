// ***********************************************************************
// Solution         : MyselfTools
// Project          : KCConsole
// File             : PictureMigrate.cs
// Updated          : 2018-06-12 13:18
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using KC.Fengniaowu.Eos.ResourceStorage;
using KC.Foundation.Sys;
using KC.Foundation.Sys.Collections.Generic;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace KCConsole
{
    /// <summary>
    ///     补件照片迁移，蜂鸟屋老系统迁移到新系统
    /// </summary>
    public class PictureMigrate
    {
        private static readonly IOptions<ResourceStorageProviderOptions> _options = new ResourceStorageProviderOptions
        {
            BlobContainerName = "kc-report",
            StorageConnectionString = "BlobEndpoint=https://kolibrestore.blob.core.chinacloudapi.cn/;QueueEndpoint=https://kolibrestore.queue.core.chinacloudapi.cn/;TableEndpoint=https://kolibrestore.table.core.chinacloudapi.cn/;AccountName=kolibrestore;AccountKey=GWJk5prSz4W/FJseTcMucDF0MYld11Z+HW7q7D/Sd2Ex7ogs6cq7Rb67vweGfmjVJ3Xr3VeVoU2B22Afks6UWA=="
        };

        private readonly string _kolibreDbConnectionString = "Server=tcp:kolibresqlserver.database.chinacloudapi.cn,1433;Database=kolibreSQL;User ID=creditkolibre@kolibresqlserver;Password=Kolibre01!;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;MultipleActiveResultSets=True;";
        private readonly string _talosDbConnectionString = "Data Source=kc-fengniaowu-pro.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Pro;Integrated Security=False;User ID=KC;Password=FgQy20VM*feO!S7E;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly string _onlyExistInOldSystemLogFilePath = Directory.GetCurrentDirectory() + "\\existOldSystem.txt";
        private readonly string _blobNotExistFilePath = Directory.GetCurrentDirectory() + "\\blobNotExist.txt";
        private readonly string _succeedFilePath = Directory.GetCurrentDirectory() + "\\succeed.txt";
        private readonly string _exceptionFilePath = Directory.GetCurrentDirectory();
        private readonly string _appendedFilePath = Directory.GetCurrentDirectory() + "\\appended.txt";

        private readonly AzureBlobStorageProvider _blobStorageProvider = new AzureBlobStorageProvider(_options);

        public async Task MigrateAsync()
        {
            SqlConnection kolibreConnection = new SqlConnection(_kolibreDbConnectionString);
            SqlConnection talosConnection = new SqlConnection(_talosDbConnectionString);

            string sql1 = "select * from dbo.[Credit.Kolibre.CreditReference.Apartment.ApartmentReports] where Enable = 1";
            IEnumerable<dynamic> kolibreResult = await kolibreConnection.QueryAsync(sql1);

            // 循环老系统每个租约
            foreach (dynamic o in kolibreResult)
            {
                try
                {
                    // 去新系统查，是否有对应的ContractConfirm记录
                    string sql2 = $"select * from dbo.[KC.Fengniaowu.Talos.ContractConfirmInfos] where Enabled = 1 and ContractId = '{o.ApartmentLeaseId}'";
                    dynamic talosResult = await talosConnection.QueryFirstOrDefaultAsync<dynamic>(sql2);

                    // 只存在于老系统
                    if (talosResult == null)
                    {
                        File.AppendAllLines(_onlyExistInOldSystemLogFilePath, new List<string> { o.ApartmentLeaseId });
                        Console.WriteLine($"ContractId:{o.ApartmentLeaseId} only exist in old system.");
                        continue;
                    }

                    // 读取Blob文件，进行图片覆盖：替换合同照片、追加老系统补件时追加的照片

                    Stream stream = await _blobStorageProvider.GetResourceStreamAsync(new Uri(o.ContentUrl));
                    if (stream == null)
                    {
                        File.AppendAllLines(_blobNotExistFilePath, new List<string> { o.ApartmentLeaseId });
                        Console.WriteLine($"ContractId:{o.ApartmentLeaseId} blob file not exist.");
                        continue;
                    }

                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();

                    JObject obj = JObject.Parse(content);

                    // 个人证件照
                    //string facePicture = obj["CredentialFacePhotoUrl"].ToString();
                    //string backPicture = obj["CredentialBackPhotoUrl"].ToString();
                    //string selfiesPicture = JArray.Parse(obj["Selfies"].ToString()).FirstOrDefault()?.ToString();
                    // 合同照片
                    JArray contractPirctureArray = JArray.Parse(obj["ContractPictures"].ToString());
                    string contractPictures = contractPirctureArray.Join();
                    // 追加照片
                    List<string> appendedPictures = null;
                    if (obj.Property("AdditionalPictures") != null)
                    {
                        string p = obj["AdditionalPictures"].ToString();
                        if (p.IsNotNullOrEmpty() && p != "[]")
                        {
                            JArray array = JArray.Parse(p);
                            appendedPictures = new List<string>();
                            foreach (JToken token in array)
                            {
                                appendedPictures.Add(token.ToString());
                            }
                        }
                    }

                    Dictionary<string, string> dataDic = new Dictionary<string, string>();
                    if (appendedPictures != null)
                    {
                        dataDic["appendPicture"] = appendedPictures.Join();
                        File.AppendAllLines(_appendedFilePath, new List<string>{ o.ApartmentLeaseId });
                    }

                    string sql3 = $"Update dbo.[KC.Fengniaowu.Talos.ContractConfirmInfos] set ContractPictures = '{contractPictures}' , Data = '{dataDic.ToJson()}' " +
                                  $"where ContractConfirmInfoId = '{talosResult.ContractConfirmInfoId}'";

                    await talosConnection.ExecuteAsync(sql3);

                    File.AppendAllLines(_succeedFilePath, new List<string> { o.ApartmentLeaseId });
                    Console.WriteLine($"ContractId:{o.ApartmentLeaseId} migrate succeed.");
                }
                catch (Exception e)
                {
                    File.AppendAllText(_exceptionFilePath + $"\\{o.ApartmentLeaseId}.txt", e.ToString());
                    Console.WriteLine($"ContractId:{o.ApartmentLeaseId} has a exception.");
                }
            }

            Console.WriteLine("补件迁移完成");
        }
    }
}