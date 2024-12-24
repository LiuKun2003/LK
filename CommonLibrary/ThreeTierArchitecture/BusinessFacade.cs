using System;
using System.Collections.Generic;
using System.Text;

namespace LK
{
    /// <summary>
    /// 业务逻辑外观基类
    /// </summary>
    public abstract class BusinessFacadeBase
    {
        /// <summary>
        /// 获取指定类型的业务逻辑。
        /// </summary>
        /// <typeparam name="T">业务逻辑的类型</typeparam>
        /// <returns>指定类型的实例。</returns>
        protected abstract T GetBusiness<T>() where T : class, IBusiness;
    }
}
