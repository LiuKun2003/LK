namespace LK
{
    /// <summary>
    /// 表示一个对象池
    /// </summary>
    /// <typeparam name="T">对象的类型</typeparam>
    public interface IObjectPool<T>
    {
        /// <summary>
        /// 如果有一个对象可用，则从池中获取一个对象，否则创建一个对象。
        /// </summary>
        /// <returns>一个<see cref="T"></see></returns>
        public T Get();

        /// <summary>
        /// 将对象返回到池中。
        /// </summary>
        /// <param name="obj">要添加到池中的对象。</param>
        public void Return(T obj);
    }
}
