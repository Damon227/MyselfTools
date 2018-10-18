// ***********************************************************************
// Solution         : MyselfTools
// Project          : KCConsole
// File             : ReadJson.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using KC.Foundation.Sys;

namespace KCConsole
{
    public static class ReadJson
    {
        private static readonly List<string> s_cities = new List<string>
        {
            "北京", "上海", "天津", "重庆"
        };

        private static readonly List<string> s_specials = new List<string>
        {
            "台湾", "港澳", "钓鱼岛"
        };

        public static void UpdateJson()
        {
            string filePath = @"D:\province.txt";
            string content = File.ReadAllText(filePath);
            content = content.Replace("\\", "");

            List<ProvinceCity> provinceCities = content.FromJson<List<ProvinceCity>>();
            for (int i = 0; i < provinceCities.Count; i++)
            {

                if (s_cities.Contains(provinceCities[i].ProvinceName))
                {
                    provinceCities[i].ProvinceName += "市";
                }
                else if(!s_specials.Contains(provinceCities[i].ProvinceName))
                {
                    provinceCities[i].ProvinceName += "省";
                }
            }

            File.AppendAllText(@"D:\province-01.txt", provinceCities.ToJson());
        }

        public static void AddZhuanyi()
        {
            string filePath = @"D:\province-01.txt";
            string content = File.ReadAllText(filePath);
            content = content.Replace("\"", "\\\"");

            File.AppendAllText(@"D:\province-02.txt", content);
        }
    }

    public class ProvinceCity
    {
        /// <summary>
        /// 省份编码
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 城镇编码
        /// </summary>
        public string CountyCode { get; set; }

        /// <summary>
        /// 城镇名称
        /// </summary>
        public string CountyName { get; set; }
    }
}