// ***********************************************************************
// Solution         : MyselfTools
// Project          : Foundation.Tools
// File             : MagicLocker.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Concurrent;

namespace Foundation.Tools
{
    /// <summary>
    ///     魔法锁
    /// </summary>
    public class MagicLock
    {
        /// <summary>
        ///     线程安全的锁对象字典
        /// </summary>
        private readonly ConcurrentDictionary<string, object> _lockDic = new ConcurrentDictionary<string, object>();

        /// <summary>
        ///     获取锁对象，若key无对应的锁，则新增一个与key对应的锁，并返回。
        /// </summary>
        /// <param name="key">key</param>
        public object GetLock(string key)
        {
            return _lockDic.GetOrAdd(key, k => new object());
        }

        /// <summary>
        ///     移除key对应的锁。
        /// </summary>
        /// <param name="key">key</param>
        public void RemoveLock(string key)
        {
            _lockDic.TryRemove(key, out object _);
        }

        /// <summary>
        ///     锁住并运行lambda表达式
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="body">lambda表达式</param>
        public TResult Run<TResult>(string key, Func<TResult> body)
        {
            lock (GetLock(key))
            {
                return body();
            }
        }

        /// <summary>
        ///     锁住并运行lambda表达式
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="body">lambda表达式</param>
        public void Run(string key, Action body)
        {
            lock (GetLock(key))
            {
                body();
            }
        }
    }
}