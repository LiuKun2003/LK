using System;
using System.Collections.Generic;

namespace LK.CommonLibrary.DataStructure
{
    /// <summary>
    /// 一个实现了发布-订阅者模式的事件通道。
    /// </summary>
    /// <typeparam name="T">订阅的主题</typeparam>
    public class EventChannel<T>
    {
        private readonly Dictionary<T, Trie> _subscriptions;

        /// <summary>
        /// 初始化一个新的事件通道。
        /// </summary>
        public EventChannel()
        {
            _subscriptions = new Dictionary<T, Trie>();
        }

        #region Subscribe
        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe(T topic, Action action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1>(T topic, Action<TArg1> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2>(T topic, Action<TArg1, TArg2> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2, TArg3>(T topic, Action<TArg1, TArg2, TArg3> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2, TArg3, TArg4>(T topic, Action<TArg1, TArg2, TArg3, TArg4> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2, TArg3, TArg4, TArg5>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> action) => InsertDelegate(topic, action);

        /// <summary>
        /// 在指定主题下订阅消息。
        /// </summary>
        public void Subscribe<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> action) => InsertDelegate(topic, action);
        #endregion

        #region Unsubscribe
        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe(T topic, Action action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1>(T topic, Action<TArg1> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2>(T topic, Action<TArg1, TArg2> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2, TArg3>(T topic, Action<TArg1, TArg2, TArg3> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2, TArg3, TArg4>(T topic, Action<TArg1, TArg2, TArg3, TArg4> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2, TArg3, TArg4, TArg5>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> action) => RemoveDelegate(topic, action);

        /// <summary>
        /// 在指定的主题下取消订阅。
        /// </summary>
        public bool Unsubscribe<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(T topic, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> action) => RemoveDelegate(topic, action);
        #endregion

        #region Publish
        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish(T topic) => InvokeDelegate(topic, new Type[] { });

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1>(T topic, TArg1 arg1) => InvokeDelegate(topic, new Type[] { typeof(TArg1) }, arg1);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2>(T topic, TArg1 arg1, TArg2 arg2) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2) }, arg1, arg2);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2, TArg3>(T topic, TArg1 arg1, TArg2 arg2, TArg3 arg3) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3) }, arg1, arg2, arg3);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2, TArg3, TArg4>(T topic, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4) }, arg1, arg2, arg3, arg4);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2, TArg3, TArg4, TArg5>(T topic, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5) }, arg1, arg2, arg3, arg4, arg5);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(T topic, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6) }, arg1, arg2, arg3, arg4, arg5, arg6);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(T topic, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7) }, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        /// <summary>
        /// 触发特定主题下订阅的事件。
        /// </summary>
        public void Publish<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(T topic, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) => InvokeDelegate(topic, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8) }, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        /*public void Publish(T topic, params object[] args)
        {
            if (_subscriptions.TryGetValue(topic, out Trie trie))
            {
                Type[] types = new Type[args.Length];
                foreach (int i in Enumerable.Range(0, args.Length))
                {
                    types[i] = args[i].GetType();
                }

                if (trie.Search(types, out HashSet<Delegate> delegates))
                {
                    foreach (var @delegate in delegates)
                    {
                        @delegate.DynamicInvoke(args);
                    }
                }
            }
        }*/
        #endregion

        /// <summary>
        /// 调整<see cref="EventChannel{T}"/>的容量到合适的值。
        /// </summary>
        public void TrimExcess()
        {
            foreach (var keyValuePair in _subscriptions)
            {
                keyValuePair.Value.TrimExcess();
            }
        }

        private void InsertDelegate(T topic, Delegate @delegate)
        {
            if (!_subscriptions.ContainsKey(topic))
            {
                _subscriptions.Add(topic, new Trie());
            }
            _subscriptions[topic].Insert(@delegate);
        }

        private bool RemoveDelegate(T topic, Delegate @delegate)
        {
            if (_subscriptions.ContainsKey(topic))
            {
                return _subscriptions[topic].Remove(@delegate);
            }
            return false;
        }

        private void InvokeDelegate(T topic, Type[] types, params object[] args)
        {
            if (_subscriptions.TryGetValue(topic, out Trie trie))
            {
                if (trie.Search(types, out HashSet<Delegate> delegates))
                {
                    foreach (var @delegate in delegates)
                    {
                        @delegate.DynamicInvoke(args);
                    }
                }
            }
        }

        /// <summary>
        /// 类似字典树的数据结构。
        /// </summary>
        private class Trie
        {
            private readonly TrieNode _root;

            public Trie()
            {
                _root = new TrieNode();
            }

            /// <summary>
            /// 插入一个委托，字典序为委托的参数类型从左到右。
            /// </summary>
            public void Insert(Delegate @delegate)
            {
                TrieNode current = _root;

                foreach (var parameter in @delegate.Method.GetParameters())
                {
                    Type type = parameter.ParameterType;
                    if (!current.Children.ContainsKey(type))
                    {
                        current.Children.Add(type, new TrieNode());
                    }
                    current = current.Children[type];
                }
                current.Delegates.Add(@delegate);
            }

            /// <summary>
            /// 移除一个委托，字典序为委托的参数类型从左到右。
            /// </summary>
            public bool Remove(Delegate @delegate)
            {
                TrieNode current = _root;
                foreach (var parameter in @delegate.Method.GetParameters())
                {
                    Type type = parameter.ParameterType;
                    if (!current.Children.ContainsKey(type))
                    {
                        return false;
                    }
                    current = current.Children[type];
                }
                if (current.IsEnd)
                {
                    return current.Delegates.Remove(@delegate);
                }
                return false;
            }

            /// <summary>
            /// 查找指定的路径下的所有委托。
            /// </summary>
            public bool Search(IEnumerable<Type> types, out HashSet<Delegate> delegates)
            {
                TrieNode current = _root;
                delegates = null;
                foreach (var type in types)
                {
                    if (!current.Children.ContainsKey(type))
                    {
                        return false;
                    }
                    current = current.Children[type];
                }
                if (current.IsEnd)
                {
                    delegates = current.Delegates;
                    return true;
                }
                return false;
            }

            /// <summary>
            /// 使用深度优先搜索来清理树中多余的枝叶。
            /// </summary>
            public void TrimExcess()
            {
                bool res = false;
                foreach (var keyValuePair in _root.Children)
                {
                    res |= DFS(keyValuePair.Value);
                }
                if (!res)
                {
                    _root.Children.Clear();
                }
            }

            private bool DFS(TrieNode root)
            {
                bool branch = false;
                foreach (var keyValuePair in root.Children)
                {
                    branch |= DFS(keyValuePair.Value);
                }
                if (!branch)
                {
                    root.Children.Clear();
                }
                branch |= root.IsEnd;
                if (!branch)
                {
                    root.Clean();
                }
                return branch;
            }
        }

        /// <summary>
        /// 树节点。
        /// </summary>
        private class TrieNode
        {
            private Dictionary<Type, TrieNode> _children;
            private HashSet<Delegate> _delegates;

            public Dictionary<Type, TrieNode> Children => _children;

            public HashSet<Delegate> Delegates => _delegates;

            public bool IsEnd => !(Delegates.Count == 0);

            public TrieNode()
            {
                _children = new Dictionary<Type, TrieNode>();
                _delegates = new HashSet<Delegate>();
            }

            /// <summary>
            /// 清理节点。
            /// </summary>
            public void Clean()
            {
                _children = null;
                _delegates = null;
            }
        }
    }
}
