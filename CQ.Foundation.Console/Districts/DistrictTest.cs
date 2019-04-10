using CQ.Foundation.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQ.Foundation.Console.Districts
{
    public class DistrictTest
    {
        public static void StartTest()
        {
            List<District> districts = DistrictHelper.GetProvinces();
            foreach (District district in districts)
            {
                System.Console.WriteLine(district.Name + district.Extra);
                List<District> districts2 = DistrictHelper.GetDistrictsByParentId(district.Id);
                foreach (District district1 in districts2)
                {
                    System.Console.WriteLine(district.Name + district.Extra + " - " + district1.Name + district1.Extra);
                }
            }
        }
    }
}
