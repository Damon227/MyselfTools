// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : Config.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************


namespace DamonHelper.Config
{
    public static class Config
    {
        public const string TalosDbConnectionString_Pro = "Data Source=kc-fengniaowu-pro.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Pro;Integrated Security=False;User ID=KC;Password=FgQy20VM*feO!S7E;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public const string TalosDbConnectionString_Test = "Data Source=kc-fengniaowu-dev.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Test;Integrated Security=False;User ID=KC;Password=V245ZGxbEhn3Sakk;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public const string TalosDbConnectionString_Dev = "Data Source=kc-fengniaowu-dev.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Dev;Integrated Security=False;User ID=KC;Password=V245ZGxbEhn3Sakk;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public const string TalosBaseAddress = "https://kc-fengniaowu-talos.kolibre.credit/";
    }
}