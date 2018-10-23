// ***********************************************************************
// Solution         : MyselfTools
// Project          : Foundation.Tools
// File             : DateTimeOffsetExtentions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace Foundation.Tools
{
    public static class DateTimeOffsetExtentions
    {
        public static DateTimeOffset ToChinaTime(this DateTimeOffset time)
        {
            time = time.ToUniversalTime().AddHours(8);
            return new DateTimeOffset(time.DateTime, TimeSpan.FromHours(8));
        }
    }
}