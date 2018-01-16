// ***********************************************************************
// Solution         : MyselfTools
// Project          : SFVersionTool
// File             : ActorInfo.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************


namespace SFVersionTool
{
    public class ActorInfo
    {
        public string ActorName { get; set; }

        public string CurrentPkgVersion { get; set; }

        public string CurrentCodeVersion { get; set; }

        public string CurrentConfigVersion { get; set; }

        public string NextPkgVersion { get; set; }

        public string NextCodeVersion { get; set; }

        public string NextConfigVersion { get; set; }
    }
}