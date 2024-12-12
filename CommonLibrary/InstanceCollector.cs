using System;
using System.Collections.Concurrent;

namespace LK
{
    /// <summary>
    /// 实例收集器，此类实现单例模式并且保存其他类型作为唯一实例
    /// </summary>
    public sealed class InstanceCollector
    {
        private readonly static Lazy<InstanceCollector> s_instance = new Lazy<InstanceCollector>(() =>
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
        public T GetInstance<T>() where T : class, new()
        {
            return _objects.GetOrAdd(typeof(T), (type) =>
            {
                T res = new T();
                return res;
            }) as T;
        }
    }
}
