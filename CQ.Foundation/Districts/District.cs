using System;
using System.Collections.Generic;
using System.Text;

namespace CQ.Foundation.Districts
{
    /// <summary>
    ///     中国行政区实体信息。
    /// </summary>
    public class District
    {
        /// <summary>
        ///     主键。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     区域名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     父键。
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        ///     区域名称第一个汉字首字母。
        /// </summary>
        public string Initial { get; set; }

        /// <summary>
        ///     区域名称汉字首字母拼接。
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        ///     区域名称拼音。
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        ///     区域类别，市、省、区、县等。
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        ///     区域名称编码。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     省市编码。
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        ///     排序。
        /// </summary>
        public int Order { get; set; }
    }
}
