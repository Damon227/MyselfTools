using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CQ.Foundation.Districts
{
    /// <summary>
    ///     三级行政区域帮助类。
    /// </summary>
    public static class DistrictHelper
    {
        static DistrictHelper()
        {
            State = ReadFromFile();
        }

        /// <summary>
        ///     行政区域集合。
        /// </summary>
        public static List<District> State { get; }

        /// <summary>
        ///     获取省份列表，包含直辖市。
        /// </summary>
        public static List<District> GetProvinces()
        {
            return State.FindAll(t => t.ParentId == 0);
        }

        /// <summary>
        ///     通过父Id查询行政区域。
        /// </summary>
        /// <param name="parentId">父Id</param>
        public static List<District> GetDistrictsByParentId(int parentId)
        {
            return State.FindAll(t => t.ParentId == parentId);
        }

        private static List<District> ReadFromFile()
        {
            string districtFile = "CQ.Foundation.Districts.districts.txt";
            Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            Stream sm = asm.GetManifestResourceStream(districtFile);
            string txt = new StreamReader(sm).ReadToEnd();
            string[] contents = txt.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            List<District> districts = new List<District>();
            foreach (string content in contents)
            {
                string[] s = content.Split(',');
                districts.Add(new District
                {
                    Id = Convert.ToInt32(s[0]),
                    Name = s[1],
                    ParentId = Convert.ToInt32(s[2]),
                    Initial = s[3],
                    Initials = s[4],
                    Pinyin = s[5],
                    Extra = s[6],
                    Code = s[7],
                    AreaCode = s[8],
                    Order = Convert.ToInt32(s[9])
                });
            }

            return districts;
        }
    }
}
