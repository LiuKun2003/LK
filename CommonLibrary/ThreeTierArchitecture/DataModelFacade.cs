using System;
using System.Collections.Generic;
using System.Text;

namespace LK
{
    /// <summary>
    /// 数据模型外观基类
    /// </summary>
    public abstract class DataModelFacadeBase
    {
        protected abstract T GetDataModel<T>() where T : class, IDataModel;
    }
}
