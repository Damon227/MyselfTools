// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : CustomEventArgs.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace DamonHelper.Helper
{
    /// <summary>
    ///     自定义事件结构
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        public string Text { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}