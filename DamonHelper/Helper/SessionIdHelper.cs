// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : SessionIdHelper.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Net.Http;
using System.Windows.Forms;
using DamonHelper.sys;
using DamonHelper.Settings;
using Newtonsoft.Json.Linq;

namespace DamonHelper.Helper
{
    public static class SessionIdHelper
    {
        private static string s_sessionId;
        private static DateTimeOffset s_expiryTime;

        public static string SessionId
        {
            get
            {
                if (string.IsNullOrEmpty(s_sessionId) || s_expiryTime < Time.Now)
                {
                    s_sessionId = GetSessionId();
                    s_expiryTime = Time.Now.AddHours(10);
                }

                return s_sessionId;
            }
        }

        private static string GetSessionId()
        {
            var content0 = new
            {
                tenancyId = Config.TenancyId,
                loginInfoAccount = Config.LoginAccount,
                password = Config.Password
            };

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Config.TalosBaseAddress) };

            HttpResponseMessage response0 = httpClient.PostAsJsonAsync("api/PasswordLogin/Account/Login", content0).Result;
            if (!response0.IsSuccessStatusCode)
            {
                MessageBox.Show("管理员登录失败");
                return null;
            }

            string responseString0 = response0.Content.ReadAsStringAsync().Result;
            JObject obj0 = JObject.Parse(responseString0);
            if (!(bool)obj0["succeeded"])
            {
                MessageBox.Show("管理员登录失败" + obj0["message"]);
                return null;
            }

            string sessionId = obj0["headers"]["x-KC-SID"].ToString();
            return sessionId;
        }
    }
}