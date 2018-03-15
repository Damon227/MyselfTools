// ***********************************************************************
// Solution         : MyselfTools
// Project          : EFCoreTest
// File             : Program.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Linq;
using System.Threading.Tasks;
using KC.Foundation.Data;
using KC.Foundation.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest
{
    internal class Program
    {
        private static string _connectionString = "Data Source=kc-fengniaowu-dev.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Dev-Local;Integrated Security=False;User ID=KC;Password=V245ZGxbEhn3Sakk;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static void Main(string[] args)
        {
            MyDbContext dbContext = new MyDbContext(_connectionString);

            City city = dbContext.ReadonlyQuery<City>().FirstOrDefault(t => t.CityName == "宁波");
            if (city != null)
            {
                city.UpdateTime = Time.Now;

                dbContext.Update(city);
                dbContext.SaveChanges();

                // 在同一个 DbContext 实例下,对同一条数据多次更新,如果不 Detached，后面更新会失败。
                dbContext.Entry(city).State = EntityState.Detached;
            }

            City city2 = dbContext.ReadonlyQuery<City>().FirstOrDefault(t => t.CityName == "宁波");
            if (city2 != null)
            {
                dbContext.Update(city2);
                dbContext.SaveChanges();
            }
        }
    }
}