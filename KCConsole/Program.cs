// ***********************************************************************
// Solution         : MyselfTools
// Project          : KCConsole
// File             : Program.cs
// Updated          : 2018-05-28 15:59
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KCConsole
{
    internal class Program
    {
        private static readonly string s_succeedPath = Directory.GetCurrentDirectory() + "/succeed.txt";
        private static readonly string s_failedPath = Directory.GetCurrentDirectory() + "/failed.txt";

        private static readonly string s_dataPath = @"C:\Users\yuanchengman\Desktop\源涞国际61个未确认合同后台确认_0913.txt";
        //private static readonly string s_dataPath = @"C:\Users\yuanchengman\Desktop\imya.txt";

        private static void Main(string[] args)
        {
            //Console.WriteLine("开始确认租约");

            //string[] data = File.ReadAllLines(s_dataPath);
            //foreach (string s in data)
            //{
            //    string[] a = s.Split(",");
            //    ConfirmContract(a[0], a[1], a[2], a[3], a[4]).Wait();
            //}

            //Console.WriteLine("结束确认租约");

            // 补件迁移
            //PictureMigrate pictureMigrate = new PictureMigrate();
            //pictureMigrate.MigrateAsync().Wait();

            ReadJson.UpdateJson();
            ReadJson.AddZhuanyi();

            Console.ReadLine();
        }

        /// <summary>
        ///     确认租约
        /// </summary>
        private static async Task ConfirmContract(string userId, string contractId, string contactName, string contactCellphone, string contactRelation)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("https://kc-fengniaowu-talos.kolibre.credit");

            var content0 = new
            {
                TenancyId = "28342C66E8EE408E9070452799CC8F6E",
                LoginInfoAccount = "10100000000",
                Password = "KC@2018"
            };

            HttpResponseMessage response0 = await httpClient.PostAsJsonAsync("api/PasswordLogin/Account/Login", content0);
            if (!response0.IsSuccessStatusCode)
            {
                Console.WriteLine("管理员登录失败");
                return;
            }

            string responseString0 = await response0.Content.ReadAsStringAsync();
            JObject obj0 = JObject.Parse(responseString0);
            if (!(bool)obj0["succeeded"])
            {
                Console.WriteLine("管理员登录失败，" + responseString0);
                return;
            }

            httpClient.DefaultRequestHeaders.Add("X-KC-SID", obj0["headers"]["x-KC-SID"].ToString());

            // 获取 UserId
            var content1 = new
            {
                Id = userId
            };


            HttpResponseMessage response1 = await httpClient.PostAsJsonAsync("api/AuthorizationCodeLoginProvider/BuildAuthorizationCode", content1);
            if (!response1.IsSuccessStatusCode)
            {
                Console.WriteLine("获取 userid 失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string responseString1 = await response1.Content.ReadAsStringAsync();
            JObject obj1 = JObject.Parse(responseString1);
            if (!(bool)obj1["succeeded"])
            {
                Console.WriteLine("获取 userid 失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string authCode = obj1["data"]["authCodeId"].ToString() + obj1["data"]["token"];

            // 租客登录
            var content2 = new
            {
                AuthCode = authCode
            };

            HttpResponseMessage response2 = await httpClient.PostAsJsonAsync("api/AuthorizationCodeLoginProvider/Login", content2);
            if (!response2.IsSuccessStatusCode)
            {
                Console.WriteLine("租客登录失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string responseString2 = await response2.Content.ReadAsStringAsync();
            JObject obj2 = JObject.Parse(responseString2);
            if (!(bool)obj2["succeeded"])
            {
                Console.WriteLine("租客登录失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string sessionId = obj2["headers"]["x-KC-SID"].ToString();

            // 创建合同确认信息
            var content3 = new
            {
                ContractId = contractId
            };

            httpClient.DefaultRequestHeaders.Remove("X-KC-SID");
            httpClient.DefaultRequestHeaders.Add("X-KC-SID", sessionId);

            HttpResponseMessage response3 = await httpClient.PostAsJsonAsync("api/Contract/CreateConfirmInfo", content3);
            if (!response3.IsSuccessStatusCode)
            {
                Console.WriteLine("创建合同确认信息失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string responseString3 = await response3.Content.ReadAsStringAsync();
            JObject obj3 = JObject.Parse(responseString3);
            if (!(bool)obj3["succeeded"])
            {
                Console.WriteLine("创建合同确认信息失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string contractConfirmInfoId = obj3["data"]["contractConfirmInfoId"].ToString();
            string facePhoto = obj3["data"]["credentialFacePhoto"].ToString();
            string backPhoto = obj3["data"]["credentialBackPhoto"].ToString();
            string selfiePhoto = obj3["data"]["selfiePhoto"].ToString();
            JToken contractPictures = obj3["data"]["contractPictures"];
            List<string> contractPhoto = null;
            if (contractPictures != null)
            {
                contractPhoto = JsonConvert.DeserializeObject<List<string>>(obj3["data"]["contractPictures"].ToString());
            }

            // 确认合同
            var content4 = new
            {
                ContractConfirmInfoId = contractConfirmInfoId,
                credentialFacePhoto = facePhoto,
                credentialBackPhoto = backPhoto,
                SelfiePhoto = selfiePhoto,
                ContractPictures = contractPhoto,
                ContactInfo = new[]
                {
                    new
                    {
                        RealName = contactName,
                        Cellphone = contactCellphone,
                        Relationship = contactRelation
                    }
                }
            };

            HttpResponseMessage response4 = await httpClient.PostAsJsonAsync("api/Contract/ConfirmContract", content4);
            if (!response4.IsSuccessStatusCode)
            {
                Console.WriteLine("合同确认失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            string responseString4 = await response4.Content.ReadAsStringAsync();
            JObject obj4 = JObject.Parse(responseString4);
            if (!(bool)obj4["succeeded"])
            {
                Console.WriteLine("合同确认失败" + contractId);
                File.AppendAllText(s_failedPath, contractId + "\r\n");
                return;
            }

            Console.WriteLine("合同确认成功" + contractId);
            File.AppendAllText(s_succeedPath, contractId + "\r\n");

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}