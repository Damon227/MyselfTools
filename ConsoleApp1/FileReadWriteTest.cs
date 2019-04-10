using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class FileReadWriteTest
    {
        public static void Work()
        {
            string filePath = @"D:\02-需求开发笔记\行政区域.txt";
            string writeFilePath = @"D:\02-需求开发笔记\行政区域2.txt";

            string[] contents = File.ReadAllLines(filePath);

            List<string> writeContents = new List<string>();
            for (int i = 0; i < contents.Length; i++)
            {
                string[] s = contents[i].Split(",");
                writeContents.Add($"{s[0]},{s[1]},{s[2]},{s[3]},{s[4]},{s[5]},{s[7]},{s[8]},{s[9]},{s[10]}");
            }

            File.AppendAllLines(writeFilePath, writeContents);
        }
    }
}