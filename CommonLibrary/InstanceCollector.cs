﻿using System;
using System.Collections.Concurrent;

namespace LK.CommonLibrary
{
    /// <summary>
    /// 实例收集器，此类实现单例模式并且保存其他类型实例作为唯一实例。
    /// </summary>
    public sealed class InstanceCollector
    {
        private static readonly Lazy<InstanceCollector> s_instance = new Lazy<InstanceCollector>(() =>
        {
            InstanceCollector collector = new InstanceCollector();
            collector.Initialize();
            return collector;
        });

        private InstanceCollector() { }

        /// <summary>
        /// 获取<see cref="InstanceCollector"></see>唯一实例。
        /// </summary>
        public static InstanceCollector Instance => s_instance.Value;

        private ConcurrentDictionary<Type, object> _objects;

        /// <summary>
        /// 初始化单例收集器。
        /// </summary>
        private void Initialize()
        {
            _objects = new ConcurrentDictionary<Type, object>();
        }

        /// <summary>
        /// 获取保存的类型的实例。
        /// </summary>
        public T GetInstance<T>(Func<T> valueFactory) where T : class
        {
            return _objects.GetOrAdd(typeof(T), valueFactory) as T;
        }
    }
}
