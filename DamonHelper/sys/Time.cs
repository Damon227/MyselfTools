// ***********************************************************************
// Solution         : MyselfTools
// Project          : Foundation.Tools
// File             : Time.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace DamonHelper.sys
{
    public class Time
    {
        public static DateTimeOffset Now => DateTimeOffset.UtcNow.ToChinaTime();

        public static DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}