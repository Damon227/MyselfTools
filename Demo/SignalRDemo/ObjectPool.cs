// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation
// File             : ObjectPool.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SignalRDemo
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentStack<T> _objects;
        private readonly Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objects = new ConcurrentStack<T>();
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
        }

        public T GetObject()
        {
            if (_objects.TryPop(out T item))
            {
                return item;
            }

            return _objectGenerator();
        }

        public void PutObject(T item)
        {
            _objects.Push(item);
        }

        public void Using(Action<T> action)
        {
            T t = GetObject();
            try
            {
                action(t);
            }
            finally
            {
                PutObject(t);
            }
        }

        public async Task UsingAsync(Func<T, Task> func)
        {
            T t = GetObject();
            try
            {
                await func(t);
            }
            finally
            {
                PutObject(t);
            }
        }

        public async Task<TResult> UsingAsync<TResult>(Func<T, Task<TResult>> func)
        {
            T t = GetObject();
            try
            {
                TResult result = await func(t);
                return result;
            }
            finally
            {
                PutObject(t);
            }
        }
    }
}