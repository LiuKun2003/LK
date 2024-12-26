namespace LK.CommonLibrary
{
    /// <summary>
    /// 数据模型外观基类
    /// </summary>
    public abstract class DataModelFacadeBase
    {
        /// <summary>
        /// 获取指定类型的数据模型。
        /// </summary>
        /// <typeparam name="T">数据模型的类型。</typeparam>
        /// <returns>指定类型的实例。</returns>
        protected abstract T GetDataModel<T>() where T : class, IDataModel;
    }
}
