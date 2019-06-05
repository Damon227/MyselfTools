using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace CQ.Foundation.AspNetCore.Filters
{
    public class UnduplicateOptions : IOptions<UnduplicateOptions>
    {
        /// <summary>
        ///     重复提交时间间隔，单位：秒。
        /// </summary>
        public int DuplicateInterval { get; set; } = 10;

        UnduplicateOptions IOptions<UnduplicateOptions>.Value => this;
    }
}
