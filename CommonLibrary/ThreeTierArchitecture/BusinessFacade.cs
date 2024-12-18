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
        protected abstract T GetBusiness<T>() where T : class, IBusiness;
    }
}
