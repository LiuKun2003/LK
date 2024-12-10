namespace LK
{
    /// <summary>
    /// 表示用于管理共用对象的策略。
    /// </summary>
    /// <typeparam name="T">对象的类型</typeparam>
    public interface IPooledObjectPolicy<T>
    {
        /// <summary>
        /// 创建<see cref="T"></see>
        /// </summary>
        /// <returns>创建的实例</returns>
        public T Create();

        /// <summary>
        /// 在将对象返回到池时运行一些处理。可用于重置对象的状态，并指示是否应将对象返回到池中。
        /// </summary>
        /// <param name="obj">要返回到池中的对象。</param>
        /// <returns>若对象可以返回池中，则为 true；反之，则为 false。</returns>
        public bool Return(T obj);

        /// <summary>
        /// 在池需要销毁对象时运行一些处理。
        /// </summary>
        /// <param name="obj">要销毁的对象实例</param>
        public void Destroy(T obj);
    }
}
