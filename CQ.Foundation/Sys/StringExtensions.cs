// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation
// File             : StringExtensions.cs
// Updated          : 2017-11-14 1:08 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CQ.Foundation.Static;
using Newtonsoft.Json;

namespace CQ.Foundation.Sys
{
    /// <summary>
    ///     <see cref="System.String" /> 的扩展类。
    /// </summary>
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    public static class StringExtensions
    {
        /// <summary>
        ///     将 <see cref="System.String" /> 转换为指定的类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <typeparam name="TValue">指定的转换类型。</typeparam>
        /// <returns>转换后指定类型的值。</returns>
        public static TValue As<TValue>(this string s, TValue defaultValue = default(TValue))
        {
            try
            {
                TypeConverter converter1 = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter1.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter1.ConvertFrom(s);
                }
                TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(string));
                if (converter2.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter2.ConvertTo(s, typeof(TValue));
                }
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Boolean" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static bool AsBoolean(this string s, bool defaultValue = false)
        {
            return !bool.TryParse(s, out bool result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.DateTime" /> 类型，如果字符串为 <c>null</c>
        ///     或者转换失败，则返回 <see cref="System.DateTime.UtcNow" />。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的值。</returns>
        public static DateTime AsDateTime(this string s)
        {
            return AsDateTime(s, DateTime.UtcNow);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.DateTime" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static DateTime AsDateTime(this string s, DateTime defaultValue)
        {
            return !DateTime.TryParse(s, out DateTime result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.DateTimeOffset" /> 类型，如果字符串为 <c>null</c>
        ///     或者转换失败，则返回 <see cref="System.DateTimeOffset.UtcNow" />。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="timeZoneString">包含时区信息的字符串。</param>
        /// <returns>转换后的值。</returns>
        public static DateTimeOffset AsDateTimeOffset(this string s, string timeZoneString = "+08:00")
        {
            timeZoneString = timeZoneString ?? string.Empty;

            return AsDateTimeOffset(s, DateTime.UtcNow, timeZoneString);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.DateTimeOffset" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <param name="timeZoneString">包含时区信息的字符串。</param>
        /// <returns>转换后的值。</returns>
        public static DateTimeOffset AsDateTimeOffset(this string s, DateTimeOffset defaultValue, string timeZoneString = "+08:00")
        {
            timeZoneString = timeZoneString ?? string.Empty;

            return !DateTimeOffset.TryParse(s + timeZoneString, out DateTimeOffset result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Decimal" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static decimal AsDecimal(this string s, decimal defaultValue = 0m)
        {
            return !decimal.TryParse(s, out decimal result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Double" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static double AsDouble(this string s, double defaultValue = 0d)
        {
            return !double.TryParse(s, out double result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Single" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static float AsFloat(this string s, float defaultValue = 0.0f)
        {
            return !float.TryParse(s, out float result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Guid" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="format">下列说明符之一，指示当转换 input 时要使用的确切格式：“N”、“D”、“B”、“P”或“X”。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static Guid AsGuid(this string s, string format = "N", Guid? defaultValue = null)
        {
            return !Guid.TryParseExact(s, format, out Guid result) ? defaultValue.GetValueOrDefault(Guid.Empty) : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int32" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static int AsInt(this string s, int defaultValue = 0)
        {
            return !int.TryParse(s, out int result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int16" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static short AsInt16(this string s, short defaultValue = 0)
        {
            return !short.TryParse(s, out short result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int32" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static int AsInt32(this string s, int defaultValue = 0)
        {
            return !int.TryParse(s, out int result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int64" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static long AsInt64(this string s, long defaultValue = 0)
        {
            return !long.TryParse(s, out long result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int64" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static long AsLong(this string s, long defaultValue = 0L)
        {
            return !long.TryParse(s, out long result) ? defaultValue : result;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Single" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则返回指定的默认值。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="defaultValue">如果 <paramref name="s" /> 为 <c>null</c> 或者转换失败，返回的默认值。</param>
        /// <returns>转换后的值。</returns>
        public static float AsSingle(this string s, float defaultValue = 0.0f)
        {
            return !float.TryParse(s, out float result) ? defaultValue : result;
        }

        /// <summary>
        ///     连接 <see cref="System.String" /> 的两个指定实例。
        /// </summary>
        /// <param name="source">要连接的第一个字符串。</param>
        /// <param name="target">要连接的第一个字符串。</param>
        /// <returns>连接后的字符串</returns>
        public static string Concat(this string source, string target)
        {
            return string.Concat(source, target);
        }

        /// <summary>
        ///     使用 <paramref name="comparison" /> 规则判断指定的字符子串 <paramref name="input" /> 是否出现在
        ///     <paramref
        ///         name="source" />
        ///     中。
        /// </summary>
        /// <param name="source">要监测的字符串。</param>
        /// <param name="input">指定的字符子串。</param>
        /// <param name="comparison">指定搜索规则的枚举值之一。</param>
        /// <returns>如果包含指定的字符子串，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="source" /> 为 <c>null</c>。
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="input" /> 为 <c>null</c> 或者空字符串。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="comparison" /> 不是有效的 <see cref="T:System.StringComparison" /> 值。
        /// </exception>
        public static bool Contains(this string source, string input, StringComparison comparison)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (input.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(input));
            }

            return source.IndexOf(input, comparison) >= 0;
        }

        /// <summary>
        ///     使用 <see cref="System.StringComparison.Ordinal" /> 规则判断指定的字符子串 <paramref name="input" />
        ///     是否出现在 <paramref name="source" /> 中。
        /// </summary>
        /// <param name="source">要监测的字符串。</param>
        /// <param name="input">指定的字符子串。</param>
        /// <returns>如果包含指定的字符子串，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="source" /> 为 <c>null</c>。
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="input" /> 为 <c>null</c> 或者空字符串。
        /// </exception>
        public static bool Contains(this string source, string input)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (input.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(input));
            }

            return source.IndexOf(input, StringComparison.Ordinal) >= 0;
        }

        /// <summary>
        ///     使用 BASE36 算法将指定字符串中的所有字符解码为一个 <see cref="System.Int64" />。
        /// </summary>
        /// <param name="s">包含要解码的字符的字符串。</param>
        /// <returns>包含对指定的字符串进行解码结果的 <see cref="T:System.long" />。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="s" /> 包含非 BASE36 范围内的字符。</exception>
        public static long DecodeByBase36(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            IList<char> reversed = s.ToUpperInvariant().Reverse().ToList();
            long result = 0;
            int pos = 0;
            for (int i = 0; i < reversed.Count; i++)
            {
                int index = Constants.Base36Characters.IndexOf(reversed[i]);
                if (index < 0)
                {
                    throw new ArgumentException(nameof(s));
                }
                result += index * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }

        /// <summary>
        ///     用于调用 <see cref="System.String.Format(string, object[])" /> 的扩展方法。将指定字符串中的一个或多个格式项替换为对应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">需要格式化的字符串。</param>
        /// <param name="args">要设置格式的对象。</param>
        /// <returns><paramref name="format" /> 的副本，其中的一个或多个格式项已替换为 <paramref name="args" /> 的字符串表示形式。</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="format" /> 或 <paramref name="args" /> 为 <c>null</c>。
        /// </exception>
        /// <exception cref="System.FormatException"><paramref name="format" /> 无效。</exception>
        public static string FormatWith(this string format, params object[] args)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return string.Format(format, args);
        }

        /// <summary>
        ///     用于调用 <see cref="System.String.Format(IFormatProvider, string, object[])" /> 的扩展方法。将指定字符串中的一个或多个格式项替换为对应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">需要格式化的字符串。</param>
        /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
        /// <param name="args">要设置格式的对象。</param>
        /// <returns><paramref name="format" /> 的副本，其中的一个或多个格式项已替换为 <paramref name="args" /> 的字符串表示形式。</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="format" /> 或 <paramref name="args" /> 为 null。
        /// </exception>
        /// <exception cref="System.FormatException"><paramref name="format" /> 无效。</exception>
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return string.Format(provider, format, args);
        }
        

        /// <summary>
        ///     将指定的JSON字符串反序列化为 <typeparamref name="T" /> 的实例。 如果 <paramref name="s" /> 是 <c>null</c>
        ///     或者空字符串, 将返回 <typeparamref name="T" /> 的默认值 。
        /// </summary>
        /// <typeparam name="T">反序列化的目标类型。</typeparam>
        /// <param name="s">指定的JSON字符串。</param>
        /// <returns>
        ///     <typeparamref name="T" /> 的实例。 如果 <paramref name="s" /> 是 <c>null</c> 或者空字符串, 则返回
        ///     <typeparamref name="T" /> 的默认值 。
        /// </returns>
        public static T FromJson<T>(this string s)
        {
            return s.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        ///     将指定的JSON字符串反序列化为 <typeparamref name="T" /> 的实例。 如果 <paramref name="s" /> 是 <c>null</c>
        ///     或者空字符串, 将返回 <typeparamref name="T" /> 的默认值 。
        /// </summary>
        /// <typeparam name="T">反序列化的目标类型。</typeparam>
        /// <param name="s">指定的JSON字符串。</param>
        /// <param name="settings">反序列化的配置类。</param>
        /// <returns>
        ///     <typeparamref name="T" /> 的实例。 如果 <paramref name="s" /> 是 <c>null</c> 或者空字符串, 则返回
        ///     <typeparamref name="T" /> 的默认值 。
        /// </returns>
        public static T FromJson<T>(this string s, JsonSerializerSettings settings)
        {
            return s.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(s, settings);
        }

        /// <summary>
        ///     使用 <see cref="System.Text.Encoding.ASCII" /> 将指定字符串中的所有字符编码为一个字节序列。
        /// </summary>
        /// <param name="s">包含要编码的字符的字符串。</param>
        /// <returns>包含对指定的字符集进行编码结果的 <see cref="T:System.Byte[]" />。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="T:System.Text.DecoderFallbackException">
        ///     发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－
        ///     <see
        ///         cref="P:System.Text.Encoding.DecoderFallback" />
        ///     设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。
        /// </exception>
        public static byte[] GetBytesOfASCII(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Encoding.ASCII.GetBytes(s);
        }

        /// <summary>
        ///     使用 Base64 算法将指定字符串中的所有字符编码为一个字节序列。
        /// </summary>
        /// <param name="s">包含要编码的字符的字符串。</param>
        /// <returns>包含对指定的字符集进行编码结果的 <see cref="T:System.Byte[]" />。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="T:System.FormatException">
        ///     <paramref name="s" /> 格式错误或者包含非 Base64字符。
        /// </exception>
        public static byte[] GetBytesOfBase64(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Convert.FromBase64String(s);
        }

        /// <summary>
        ///     使用指定的编码将指定字符串中的所有字符编码为一个字节序列。
        /// </summary>
        /// <param name="s">包含要编码的字符的字符串。</param>
        /// <param name="encodingName">
        ///     指定编码的代码页名称。
        ///     <see
        ///         href="https://msdn.microsoft.com/zh-cn/library/system.text.encoding.webname(v=vs.110).aspx">
        ///         WebName
        ///     </see>
        ///     属性返回的值是有效的。可能的值都在
        ///     <see
        ///         href="https://msdn.microsoft.com/zh-cn/library/system.text.encoding(v=vs.110).aspx">
        ///         Encoding
        ///     </see>
        ///     类主题中出现的表的“名称”一列中列了出来。
        /// </param>
        /// <returns>包含对指定的字符集进行编码结果的 <see cref="T:System.Byte[]" />。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="encodingName" /> 不是有效的代码页名称。
        /// </exception>
        /// <exception cref="T:System.Text.DecoderFallbackException">
        ///     发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－
        ///     <see
        ///         cref="P:System.Text.Encoding.DecoderFallback" />
        ///     设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。
        /// </exception>
        public static byte[] GetBytesOfEncoding(this string s, string encodingName)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (encodingName.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(encodingName));
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            return encoding.GetBytes(s);
        }

        /// <summary>
        ///     使用 <see cref="System.Text.Encoding.Unicode" /> 将指定字符串中的所有字符编码为一个字节序列。
        /// </summary>
        /// <param name="s">包含要编码的字符的字符串。</param>
        /// <returns>包含对指定的字符集进行编码结果的 <see cref="T:System.Byte[]" />。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="T:System.Text.DecoderFallbackException">
        ///     发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－
        ///     <see
        ///         cref="P:System.Text.Encoding.DecoderFallback" />
        ///     设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。
        /// </exception>
        public static byte[] GetBytesOfUnicode(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Encoding.Unicode.GetBytes(s);
        }

        /// <summary>
        ///     使用 <see cref="System.Text.Encoding.UTF8" /> 将指定字符串中的所有字符编码为一个字节序列。
        /// </summary>
        /// <param name="s">包含要编码的字符的字符串。</param>
        /// <returns>包含对指定的字符集进行编码结果的 <see cref="T:System.Byte[]" />。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="T:System.Text.DecoderFallbackException">
        ///     发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－
        ///     <see
        ///         cref="P:System.Text.Encoding.DecoderFallback" />
        ///     设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。
        /// </exception>
        public static byte[] GetBytesOfUTF8(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        ///     从 <paramref name="s" /> 的第一个字符开始向后截取指定数量字符的子字符串。如果需要的数量大于 <paramref name="s" /> 的长度，则直接返回
        ///     <paramref name="s" /> 。
        /// </summary>
        /// <param name="s">被截取的原字符串。</param>
        /// <param name="count">截取的字符数量。</param>
        /// <returns>截取后的子字符串。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static string GetFirst(this string s, int count = 1)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return SubString(s, 0, count);
        }

        /// <summary>
        ///     使用 基于<see cref="System.Security.Cryptography.SHA256" /> 的算法计算指定字符串的哈希值，且该哈希值为 <see cref="int" />。
        /// </summary>
        /// <param name="s">指定字符串。</param>
        /// <returns>指定字符串的哈希值。</returns>
        public static int GetIntHash(this string s)
        {
            SHA256 sha = SHA256.Create(); // This is one implementation of the abstract class SHA1.
            int hash = 0;
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(s);
                byte[] result = sha.ComputeHash(data);
                for (int i = 0; i < result.Length; i += 4)
                {
                    int tmp = (result[i] << 24) | (result[i + 1] << 16) | (result[i + 2] << 8) | result[i + 3];
                    hash = hash ^ tmp;
                }
            }
            finally
            {
                sha.Dispose();
            }
            return hash;
        }

        /// <summary>
        ///     从 <paramref name="s" /> 的末尾向前截取指定数量字符的子字符串。如果需要的数量大于 <paramref name="s" /> 的长度，则直接返回
        ///     <paramref name="s" /> 。
        /// </summary>
        /// <param name="s">被截取的原字符串。</param>
        /// <param name="count">截取的字符数量。</param>
        /// <returns>截取后的子字符串。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static string GetLast(this string s, int count = 1)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int start = s.Length - count;
            if (start < 0)
            {
                start = 0;
            }
            return SubString(s, start, count);
        }

        /// <summary>
        ///     使用 <see cref="System.Security.Cryptography.MD5" /> 算法计算指定字符串的哈希值。
        /// </summary>
        /// <param name="s">指定字符串。</param>
        /// <returns>指定字符串的哈希值。</returns>
        public static string GetMD5Hash(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(s.GetBytesOfUTF8());
            return hashBytes.ToPrintableString();
        }

        /// <summary>
        ///     使用 <see cref="System.Security.Cryptography.SHA256" /> 算法计算指定字符串的哈希值。
        /// </summary>
        /// <param name="s">指定字符串。</param>
        /// <returns>指定字符串的哈希值。</returns>
        public static string GetSHA256Hash(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(s.GetBytesOfUTF8());
            return hashBytes.ToPrintableString();
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为指定的类型。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <typeparam name="TValue">转换的目标类型。</typeparam>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool Is<TValue>(this string s)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
            try
            {
                if (s != null)
                {
                    if (!converter.CanConvertFrom(null, s.GetType()))
                    {
                        return false;
                    }
                }
                // ReSharper disable once AssignNullToNotNullAttribute
                converter.ConvertFrom(null, CultureInfo.CurrentCulture, s);
                return true;
            }
            catch
            {
                // ignored
            }
            return false;
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Boolean" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsBool(this string s)
        {
            return bool.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <paramref name="s" /> 是否是合法的手机号。
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <returns>如果是合法的手机号，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static bool IsCellphone(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s.Match(RegexConstants.CellphoneRegex);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.DateTime" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsDateTime(this string s)
        {
            return DateTime.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.DateTimeOffset" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsDateTimeOffset(this string s)
        {
            return DateTimeOffset.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Decimal" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsDecimal(this string s)
        {
            return decimal.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Double" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsDouble(this string s)
        {
            return double.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <paramref name="s" /> 是否是合法的电子邮箱。
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <returns>如果是合法的电子邮箱，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static bool IsEmail(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Match(s, RegexConstants.EmailRegex);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Single" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsFloat(this string s)
        {
            return float.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Guid" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <param name="format">下列说明符之一，指示当转换 input 时要使用的确切格式：“N”、“D”、“B”、“P”或“X”。</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsGuid(this string s, string format = "N")
        {
            return Guid.TryParseExact(s, format, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Int32" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsInt(this string s)
        {
            return int.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Int16" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsInt16(this string s)
        {
            return short.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Int32" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsInt32(this string s)
        {
            return int.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="System.Int64" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsInt64(this string s)
        {
            return long.TryParse(s, out _);
        }

        /// <summary>
        ///     指示 <paramref name="s" /> 是否是合法的IP地址。
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <returns>如果是合法的IP地址，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static bool IsIPAddress(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Match(s, RegexConstants.IPAddressRegex);
        }

        /// <summary>
        ///     指示 <see cref="System.String" /> 是否可以转换为 <see cref="long" />。
        /// </summary>
        /// <param name="s">需要检查的字符串</param>
        /// <returns>如果可以转换为指定的类型，返回 <c>true</c>；否则返回 <c>false</c> 。</returns>
        public static bool IsLong(this string s)
        {
            return long.TryParse(s, out _);
        }

        /// <summary>
        ///     判断指定的字符串是否是 <c>null</c> 或者 <see cref="System.String.Empty" />。
        /// </summary>
        /// <param name="s">要测试的字符串。</param>
        /// <returns>如果 <paramref name="s" /> 字符串不是 <c>null</c> 并且不是空字符串，则为 <c>true</c>；否则为 <c>false</c>。</returns>
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        ///     判断指定的字符串是否是 <c>null</c>、 <see cref="System.String.Empty" /> 或者仅由空白字符组成。
        /// </summary>
        /// <param name="s">要测试的字符串。</param>
        /// <returns>
        ///     如果 <paramref name="s" /> 字符串为不是 <c>null</c>、也不是空字符串并且不是仅由空白字符组成，则为 <c>true</c>；否则为 <c>false</c>。
        /// </returns>
        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        ///     判断指定的字符串是否是 <c>null</c> 或者 <see cref="System.String.Empty" />。
        /// </summary>
        /// <param name="s">要测试的字符串。</param>
        /// <returns>如果 <paramref name="s" /> 字符串为 <c>null</c> 或者空字符串，则为 <c>true</c>；否则为 <c>false</c>。</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        ///     判断指定的字符串是否是 <c>null</c>、 <see cref="System.String.Empty" /> 或者仅由空白字符组成。
        /// </summary>
        /// <param name="s">要测试的字符串。</param>
        /// <returns>
        ///     如果 <paramref name="s" /> 字符串为 <c>null</c>、空字符串或者仅由空白字符组成，则为 <c>true</c>；否则为 <c>false</c>。
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        ///     指示 <paramref name="s" /> 在指定的字符串中是否找到了匹配项。
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <param name="regex">进行匹配的正则表达式。</param>
        /// <returns>如果正则表达式找到匹配项 ，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="s" /> 或者 <paramref name="regex" /> 为 <c>null</c>。
        /// </exception>
        public static bool Match(this string s, Regex regex)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (regex == null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return s.IsNotNullOrEmpty() && regex.IsMatch(s);
        }

        /// <summary>
        ///     从字符串中移除指定的字符子串。
        /// </summary>
        /// <param name="s">原字符串。</param>
        /// <param name="target">需要移除的字符子串。</param>
        /// <returns>移除了指定的字符子串后的字符串。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="target" /> 为 <c>null</c>。</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="target" /> 为 <c>null</c>。</exception>
        public static string Remove(this string s, string target)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (target.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(target));
            }

            return s.Replace(target, "");
        }

        /// <summary>
        ///     将 PascalCase 风格的字符串为以单词为单位使用空格分隔开。
        /// </summary>
        /// <example>
        ///     "ThisIsPascalCase".SeparatePascalCase(); // returns "This Is Pascal Case"
        /// </example>
        /// <param name="s">PascalCase 风格的字符串。</param>
        /// <returns>分隔后的字符串。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static string SeparatePascalCase(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return Regex.Replace(s, "([A-Z])", " $1").Trim();
        }

        /// <summary>
        ///     更安全截取子字符串的方法。与 <see cref="F:System.String.Substring()" /> 不同，字符串指定部分的长度不足需要获取的子字符串的长度时， 该方法不会报错，而是返回尽量截取出的长度的部分。
        /// </summary>
        /// <param name="s">原字符串。</param>
        /// <param name="start">此实例中子字符串的起始字符位置（从零开始）。</param>
        /// <param name="count">子字符串中的字符数。</param>
        /// <returns>与此实例中在 length 处开头、长度为 startIndex 的子字符串等效的一个字符串。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        public static string SubString(this string s, int start, int count = 1)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (start >= s.Length || start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return s.Length - count - start < 0 ? s.Substring(start) : s.Substring(start, count);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Boolean" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="s" /> 为 <c>null</c>。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static bool ToBool(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return bool.Parse(s);
        }

        /// <summary>
        ///     将指定的字符串转换为 camelCase 风格
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        public static string ToCamelCase(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            int len = s.Length;
            char[] newValue = new char[len];
            bool firstPart = true;

            for (int i = 0; i < len; ++i)
            {
                char c0 = s[i];
                char c1 = i < len - 1 ? s[i + 1] : 'A';
                bool c0IsUpper = c0 >= 'A' && c0 <= 'Z';
                bool c1IsUpper = c1 >= 'A' && c1 <= 'Z';

                if (firstPart && c0IsUpper && (c1IsUpper || i == 0))
                {
                    c0 = (char)(c0 + Constants.LowerCaseOffset);
                }
                else
                {
                    firstPart = false;
                }

                newValue[i] = c0;
            }

            return new string(newValue);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.DateTime" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="s" /> 为 <c>null</c> 或者空字符串。
        /// </exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static DateTime ToDateTime(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return DateTime.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.DateTimeOffset" /> 类型，如果字符串为 <c>null</c>、空字符串或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="timeZoneString">包含时区信息的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="s" /> 或者 <paramref name="timeZoneString" /> 为 <c>null</c> 或者空字符串。
        /// </exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static DateTimeOffset ToDateTimeOffset(this string s, string timeZoneString = "+08:00")
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            if (timeZoneString.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(timeZoneString));
            }

            return DateTimeOffset.Parse(s + timeZoneString);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Decimal" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static decimal ToDecimal(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return decimal.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Double" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static double ToDouble(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return double.Parse(s);
        }

        /// <summary>
        ///     如果指定的字符串是 <c>null</c> 或者空字符串，则返回 <see cref="System.String.Empty" />。
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <returns>
        ///     如果指定的字符串是 <c>null</c> 或者空字符串，则返回 <see cref="System.String.Empty" />；否则返回原字符串 <paramref name="s" />。
        /// </returns>
        public static string ToEmptyIfNull(this string s)
        {
            return s.IsNullOrEmpty() ? string.Empty : s;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Single" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static float ToFloat(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return float.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Guid" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <param name="format">下列说明符之一，指示当转换 input 时要使用的确切格式：“N”、“D”、“B”、“P”或“X”。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static Guid ToGuid(this string s, string format = "N")
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return Guid.ParseExact(s, format);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int32" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static int ToInt(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return int.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int16" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static short ToInt16(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return short.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int32" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static int ToInt32(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return int.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int64" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static long ToInt64(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return long.Parse(s);
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Int64" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static long ToLong(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return long.Parse(s);
        }

        /// <summary>
        ///     如果指定的字符串是 <c>null</c> 或者空字符串，则返回 <c>null</c>。
        /// </summary>
        /// <param name="s">指定的字符串。</param>
        /// <returns>如果指定的字符串是 <c>null</c> 或者空字符串，则返回 <c>null</c>；否则返回原字符串 <paramref name="s" />。</returns>
        public static string ToNullIfEmpty(this string s)
        {
            return s.IsNullOrEmpty() ? null : s;
        }

        /// <summary>
        ///     将 <see cref="System.String" /> 转换为 <see cref="System.Single" /> 类型，如果字符串为 <c>null</c> 或者转换失败，则抛出异常。
        /// </summary>
        /// <param name="s">包含要转换的值的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="System.ArgumentException"><paramref name="s" /> 为 <c>null</c> 或者空字符串。</exception>
        /// <exception cref="System.FormatException"><paramref name="s" /> 不是可以转换的合法格式。</exception>
        public static float ToSingle(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(s));
            }

            return float.Parse(s);
        }
    }
}