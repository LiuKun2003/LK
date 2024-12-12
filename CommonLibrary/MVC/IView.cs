namespace LK
{
    /// <summary>
    /// 表示一个视图
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 获取控制器
        /// </summary>
        public T GetController<T>() where T : class, IController, new();
    }
}
