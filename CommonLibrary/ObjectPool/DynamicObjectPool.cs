using System;
using System.Collections.Generic;

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
                return _maxRetained;
            }
        }

        /// <summary>
        /// 获取对象池中空闲可用的实例数
        /// </summary>
        public int Count
        {
            get
            {
                return _stack.Count;
            }
        }

        private const int DefaultRetained = 4;
        private const int DefaultSizeFactor = 2;
        private readonly Stack<T> _stack;
        private readonly Queue<int> _depthPerRound;
        private readonly IPooledObjectPolicy<T> _policy;
        private int _maxRetained;
        private int _currentDepth;
        private int _counter;

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

            _policy = policy;
            _maxRetained = maximumRetained;
            _stack = new Stack<T>(maximumRetained);
            _depthPerRound = new Queue<int>();
            _currentDepth = 0;
            //m_counter = 0;

            for (int i = 0; i < DefaultSizeFactor; i++)
            {
                _depthPerRound.Enqueue(maximumRetained);
            }
        }

        public T Get()
        {
            T res;
            if (_stack.Count > 0)
            {
                res = _stack.Pop();
            }
            else
            {
                res = _policy.Create();
            }

            _currentDepth += 1;
            TryResize();

            return res;
        }

        public void Return(T obj)
        {
            if (_stack.Contains(obj))
            {
                throw new ArgumentException("The pool already contains the specified reference.", nameof(obj));
            }

            if (!_policy.Return(obj))
            {
                _policy.Destroy(obj);
            }
            else
            {
                _stack.Push(obj);
            }

            _depthPerRound.Enqueue(_currentDepth);
            _depthPerRound.Dequeue();
            TryResize();
        }

        /// <summary>
        /// 尝试调整池的大小
        /// </summary>
        public void TryResize()
        {
            _counter += 1;
            if (_counter >= _maxRetained)
            {
                _counter = 0;

                float newSize = 0;
                foreach (float depth in _depthPerRound)
                {
                    newSize += depth / _depthPerRound.Count;
                }

                float offset = Math.Abs(newSize - _maxRetained);

                if (offset > _maxRetained * 0.10f)
                {
                    _maxRetained = (int)Math.Ceiling(newSize);
                }

                while (_stack.Count > _maxRetained)
                {
                    T obj = _stack.Pop();
                    _policy.Destroy(obj);
                }
            }
        }
    }
}
