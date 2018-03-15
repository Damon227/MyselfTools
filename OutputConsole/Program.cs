// ***********************************************************************
// Solution         : MyselfTools
// Project          : OutputConsole
// File             : Program.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace OutputConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = @"D:\蜂鸟屋源码导出";
            string outputPath = "d:\\1.txt";

            DirectoryInfo info = new DirectoryInfo(path);

            StringBuilder sb = new StringBuilder();
            Output(info, sb);
            
            File.AppendAllText(outputPath, sb.ToString());

            Console.ReadLine();
        }

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        private static void Output(DirectoryInfo info, StringBuilder sb)
        {
            if (info.Exists)
            {
                FileInfo[] files = info.GetFiles();

                foreach (FileInfo fileInfo in files)
                {
                    if (fileInfo.Extension == ".cs")
                    {
                        sb.Append(File.ReadAllText(fileInfo.FullName));
                        sb.Append("\r\n\r\n");
                    }
                }

                DirectoryInfo[] directoryInfos = info.GetDirectories();
                for (int i = 0; i < directoryInfos.Length; i++)
                {
                    if (directoryInfos[i].Name == "bin" || directoryInfos[i].Name == "obj" || directoryInfos[i].Name == "Properties")
                    {
                        continue;
                    }

                    Output(directoryInfos[i], sb);
                }
            }
        }
    }
}