using System.Collections.Generic;
using System;

namespace LK
{
    /// <summary>
    /// 随着使用情况，不断调整大小的对象池
    /// </summary>
    public class DynamicObjectPool<T> : IObjectPool<T>
    {
        /// <summary>
        /// 获取对象池可缓存的最大实例数
        /// </summary>
        public int MaximumRetained
        {
            get
            {
                return m_maxRetained;
            }
        }

        /// <summary>
        /// 获取对象池中空闲可用的实例数
        /// </summary>
        public int Count
        {
            get
            {
                return m_stack.Count;
            }
        }
        
        private const int DefaultRetained = 4;
        private const int DefaultSizeFactor = 2;
        private Stack<T> m_stack;
        private Queue<int> m_DepthPerRound;
        private IPooledObjectPolicy<T> m_policy;
        private int m_maxRetained;
        private int m_currentDepth;
        private int m_counter;

        /// <summary>
        /// 使用指定的池策略初始化一个新的动态池
        /// </summary>
        /// <param name="policy"></param>
        public DynamicObjectPool(IPooledObjectPolicy<T> policy) : this(policy, DefaultRetained)
        {
        }

        /// <summary>
        /// 使用指定的池策略和最大缓存限制初始化一个新的动态池
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="maximumRetained"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DynamicObjectPool(IPooledObjectPolicy<T> policy, int maximumRetained)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy), "Value cannot be null.");
            }

            if (maximumRetained < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumRetained), maximumRetained, "Non-negative number required.");
            }

            m_policy = policy;
            m_maxRetained = maximumRetained;
            m_stack = new Stack<T>(maximumRetained);
            m_DepthPerRound = new Queue<int>();
            m_currentDepth = 0;
            //m_counter = 0;

            for (int i = 0; i < DefaultSizeFactor; i++)
            {
                m_DepthPerRound.Enqueue(maximumRetained);
            }
        }

        public T Get()
        {
            T res;
            if (m_stack.Count > 0)
            {
                res = m_stack.Pop();
            }
            else
            {
                res = m_policy.Create();
            }

            m_currentDepth += 1;
            TryResize();

            return res;
        }

        public void Return(T obj)
        {
            if (m_stack.Contains(obj))
            {
                throw new ArgumentException("The pool already contains the specified reference.", nameof(obj));
            }

            if (!m_policy.Return(obj))
            {
                m_policy.Destroy(obj);
            }
            else
            {
                m_stack.Push(obj);
            }

            m_DepthPerRound.Enqueue(m_currentDepth);
            m_DepthPerRound.Dequeue();
            TryResize();
        }

        /// <summary>
        /// 尝试调整池的大小
        /// </summary>
        public void TryResize()
        {
            m_counter += 1;
            if (m_counter >= m_maxRetained)
            {
                m_counter = 0;

                float newSize = 0;
                foreach (float depth in m_DepthPerRound)
                {
                    newSize += depth / m_DepthPerRound.Count;
                }

                float offset = Math.Abs(newSize - m_maxRetained);

                if (offset > m_maxRetained * 0.10f)
                {
                    m_maxRetained = (int)Math.Ceiling(newSize);
                }

                while (m_stack.Count > m_maxRetained)
                {
                    T obj = m_stack.Pop();
                    m_policy.Destroy(obj);
                }
            }
        }
    }
}
