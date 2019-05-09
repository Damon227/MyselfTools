using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamonHelper.sys
{
    public static class ListExtensions
    {
        /// <summary>
        ///     拆分List为多个指定大小的List的集合
        /// </summary>
        /// <param name="list">需要拆分的List</param>
        /// <param name="nSize">指定拆分出的List大小</param>
        /// <returns>多个指定大小的List的集合</returns>
        public static IEnumerable<List<T>> SplitToSmallList<T>(this List<T> list, int nSize = 30)
        {
            for (int i = 0; i < list.Count; i += nSize)
            {
                yield return list.GetRange(i, Math.Min(nSize, list.Count - i));
            }
        }
    }
}
