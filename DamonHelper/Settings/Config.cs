// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : Config.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DamonHelper.Settings
{
    public static class Config
    {
        public static string Environment { get; set; }

        public static string TalosDbConnectionString { get; set; }

        public static string TalosBaseAddress { get; set; }

        public static string TenancyId { get; set; }

        public static string LoginAccount { get; set; }

        public static string Password { get; set; }

        public static bool InitConfig(string env)
        {
            Environment = env;

            // 读取json配置
            string jsonFile = $"DamonHelper.appsettings.{env.ToLower()}.json";
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
                Stream sm = asm.GetManifestResourceStream(jsonFile);
                StreamReader sr = new StreamReader(sm);
                JObject json = JObject.Load(new JsonTextReader(sr));

                TalosDbConnectionString = json["TalosDbConnectionString"].ToString();
                TalosBaseAddress = json["TalosBaseAddress"].ToString();
                TenancyId = json["TenancyId"].ToString();
                LoginAccount = json["LoginAccount"].ToString();
                Password = json["Password"].ToString();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("程序启动失败，DirectoryNotFoundException");
                return false;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("程序启动失败，FileNotFoundException");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("程序启动失败，" + e.Message);
                return false;
            }

            return true;
        }
    }
}