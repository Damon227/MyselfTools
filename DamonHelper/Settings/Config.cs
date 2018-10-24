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
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace DamonHelper.Settings
{
    public static class Config
    {
        public static string TalosDbConnectionString { get; set; }

        public static string TalosBaseAddress { get; set; }

        public static bool InitConfig(string env)
        {
            // 读取json配置
            string jsonFile = $"appsettings.{env.ToLower()}.json";
            try
            {
                string jsonContent = File.ReadAllText(jsonFile);
                JObject json = JObject.Parse(jsonContent);

                TalosDbConnectionString = json["TalosDbConnectionString"].ToString();
                TalosBaseAddress = json["TalosBaseAddress"].ToString();
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