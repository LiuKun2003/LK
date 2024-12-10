using System;
using System.Collections.Concurrent;

namespace LK
{
    /// <summary>
    /// 单例收集器，此类实现单例模式并且保存其他类型作为唯一实例
    /// </summary>
    internal sealed class SingletonCollector
    {
        private readonly static Lazy<SingletonCollector> m_instance = new Lazy<SingletonCollector>(() =>
        {
            SingletonCollector collector = new SingletonCollector();
            collector.Initialize();
            return collector;
        });

        private SingletonCollector() { }

        /// <summary>
        /// 获取<see cref="SingletonCollector"></see>唯一实例。
        /// </summary>
        internal static SingletonCollector Instance => m_instance.Value;

        private ConcurrentDictionary<Type, IController> m_Controllers;
        private ConcurrentDictionary<Type, IModel> m_Models;

        /// <summary>
        /// 初始化单例收集器。
        /// </summary>
        private void Initialize()
        {
            m_Controllers = new ConcurrentDictionary<Type, IController>();
            m_Models = new ConcurrentDictionary<Type, IModel>();
        }

        /// <summary>
        /// 获取保存的控制器类型实例。
        /// </summary>
        /// <returns>控制器的实例</returns>
        internal T GetController<T>() where T : class, IController, new()
        {
            return m_Controllers.GetOrAdd(typeof(T), (type) =>
            {
                T res = new T();
                return res;
            }) as T;
        }

        /// <summary>
        /// 获取保存的数据模型类型实例。
        /// </summary>
        /// <returns>数据模型的实例</returns>
        internal T GetModel<T>() where T : class, IModel, new()
        {
            return m_Models.GetOrAdd(typeof(T), (type) =>
            {
                T res = new T();
                return res;
            }) as T;
        }
    }
}
