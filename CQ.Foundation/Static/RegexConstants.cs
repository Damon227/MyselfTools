// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation
// File             : RegexConstants.cs
// Modified         : 2017-04-17  3:14 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Text.RegularExpressions;

#pragma warning disable 1591

namespace CQ.Foundation.Static
{
    public static class RegexConstants
    {
        public static readonly Regex BankCardRegex = new Regex(Constants.BankCardRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex CellphoneRegex = new Regex(Constants.CellphoneRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex ComplexPasswordRegex = new Regex(Constants.ComplexPasswordRegexString, RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex DateStringRegex = new Regex(Constants.DateRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex DateTimeRegex = new Regex(Constants.DateTimeRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex EmailRegex = new Regex(Constants.EmailRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex IDCardRegex = new Regex(Constants.IDCardRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex IPAddressRegex = new Regex(Constants.IPAddressRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex NumericPasswordRegex = new Regex(Constants.NumericPasswordRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex PositiveNumberRegex = new Regex(Constants.PositiveNumberRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex PositiveNumberWithZeroRegex = new Regex(Constants.PositiveNumberWithZeroRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex SimplePasswordRegex = new Regex(Constants.SimplePasswordRegexString, RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));

        public static readonly Regex UrlRegex = new Regex(Constants.UrlRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, TimeSpan.FromMinutes(2));
    }
}