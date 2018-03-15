// ***********************************************************************
// Solution         : MyselfTools
// Project          : ExcelConsole
// File             : Program.cs
// Updated          : 2018-03-07 10:17
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Floor");

            foreach (DataColumn column in dataTable.Columns)
            {
                Console.WriteLine(column.ColumnName);
            }
            
            DataTable dt = ReadExcelFile("D:\\damon\\room1.xlsx");

            string connectionString = "Data Source=kc-fengniaowu-dev.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Dev-Local;Integrated Security=False;User ID=KC;Password=V245ZGxbEhn3Sakk;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (transaction = conn.BeginTransaction())
                    {
                        using (SqlBulkCopy copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                        {
                            copy.DestinationTableName = "dbo.[KC.Fengniaowu.Talos.Rooms1]";

                            copy.ColumnMappings.Add("ActorId", "ActorId");
                            copy.ColumnMappings.Add("RoomId", "RoomId");
                            copy.ColumnMappings.Add("ApartmentId", "ApartmentId");
                            copy.ColumnMappings.Add("Floor", "Floor");
                            copy.ColumnMappings.Add("Price", "Price");
                            copy.ColumnMappings.Add("RoomNumber", "RoomNumber");
                            copy.ColumnMappings.Add("Enabled", "Enabled");
                            copy.ColumnMappings.Add("CreateTime", "CreateTime");
                            copy.ColumnMappings.Add("UpdateTime", "UpdateTime");
                            copy.ColumnMappings.Add("Data", "Data");

                            copy.WriteToServer(dt);
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception)
            {
                transaction?.Rollback();

                throw;
            }

            Console.ReadLine();
        }

        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        private static DataTable ReadExcelFile(string fileName)
        {
            IWorkbook workbook;

            string extension = Path.GetExtension(fileName);
            FileStream fs = File.OpenRead(fileName);
            if (extension.Equals(".xls"))
            {
                //把xls文件中的数据写入wk中
                workbook = new HSSFWorkbook(fs);
            }
            else
            {
                //把xlsx文件中的数据写入wk中
                workbook = new XSSFWorkbook(fileName);
            }

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);

            DataTable dt = new DataTable(sheet.SheetName);

            // write header row
            IRow headerRow = sheet.GetRow(0);
            foreach (ICell headerCell in headerRow)
            {
                dt.Columns.Add(headerCell.ToString());
            }

            dt.Columns.Add("Enabled");
            dt.Columns.Add("CreateTime");
            dt.Columns.Add("UpdateTime");
            dt.Columns.Add("Data");

            // write the rest
            int rowIndex = 0;
            foreach (IRow row in sheet)
            {
                // skip header row
                if (rowIndex++ == 0)
                {
                    continue;
                }

                DataRow dr = dt.NewRow();
                dr.ItemArray = row.Cells.ToArray();

                dr["ActorId"] = row.Cells[0];
                dr["RoomId"] = row.Cells[1];
                dr["ApartmentId"] = row.Cells[2];
                dr["Floor"] = row.Cells[3];
                dr["Price"] = row.Cells[4];
                dr["RoomNumber"] = row.Cells[5];
                dr["Enabled"] = true;
                dr["CreateTime"] = DateTimeOffset.Now;
                dr["UpdateTime"] = DateTimeOffset.Now;
                dr["Data"] = "{}";

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}