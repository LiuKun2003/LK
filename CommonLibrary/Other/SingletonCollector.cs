using System;
using System.Collections.Concurrent;

namespace LK
{
    /// <summary>
    /// 单例收集器，此类实现单例模式并且保存其他类型作为唯一实例
    /// </summary>
    public sealed class SingletonCollector
    {
        private readonly static Lazy<SingletonCollector> s_instance = new Lazy<SingletonCollector>(() =>
        {
            SingletonCollector collector = new SingletonCollector();
            collector.Initialize();
            return collector;
        });

        private SingletonCollector() { }

        /// <summary>
        /// 获取<see cref="SingletonCollector"></see>唯一实例。
        /// </summary>
        public static SingletonCollector Instance => s_instance.Value;

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
        public T GetController<T>() where T : class, new()
        {
            return _objects.GetOrAdd(typeof(T), (type) =>
            {
                T res = new T();
                return res;
            }) as T;
        }
    }
}
