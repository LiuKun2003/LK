namespace LK
{
    /// <summary>
    /// 表示一个控制器
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// 获取数据模型
        /// </summary>
	    public T GetModel<T>() where T : class, IModel, new();
    }
}
