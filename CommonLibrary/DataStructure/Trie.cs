using System;
using System.Collections.Generic;
using System.Linq;

namespace LK.CommonLibrary.DataStructure
{
    /// <summary>
    /// 前缀树
    /// </summary>
    /// <typeparam name="T">前缀树的词</typeparam>
    public class Trie<T>
    {
        private readonly TrieNode _root;

        /// <summary>
        /// 初始化一个新的前缀树。
        /// </summary>
        public Trie()
        {
            _root = new TrieNode();
        }

        /// <summary>
        /// 获取存储的词的数量。
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 添加一个指定的词。
        /// </summary>
        /// <param name="word">要添加的词。</param>
        public void Add(IEnumerable<T> word)
        {
            TrieNode current = _root;
            foreach (T i in word)
            {
                if (!current.Children.ContainsKey(i))
                {
                    current.Children[i] = new TrieNode();
                }
                current = current.Children[i];
            }
            if (current.IsEnd == false)
            {
                current.IsEnd = true;
                Count++;
            }
        }

        /// <summary>
        /// 移除一个指定的词。
        /// </summary>
        /// <param name="word">要移除的前缀。</param>
        /// <returns>如果成功移除词，则返回true。</returns>
        public bool Remove(IEnumerable<T> word)
        {
            TrieNode current = _root;
            foreach (T i in word)
            {
                if (current.Children.ContainsKey(i))
                {
                    current = current.Children[i];
                }
                else
                {
                    return false;
                }
            }

            if (current.IsEnd)
            {
                current.IsEnd = false;
                Count--;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 搜索是否存在指定的词。
        /// </summary>
        /// <param name="word">要搜索的词。</param>
        /// <returns>如果存在指定词，则返回true。</returns>
        public bool Search(IEnumerable<T> word)
        {
            TrieNode current = _root;
            foreach (T i in word)
            {
                if (current.Children.ContainsKey(i))
                {
                    current = current.Children[i];
                }
                else
                {
                    return false;
                }
            }
            return current.IsEnd;
        }

        /*    /// <summary>
            /// 搜索是否存在前缀包含指定的前缀。
            /// </summary>
            /// <param name="prefix"></param>
            /// <returns>如果存在前缀包含指定的前缀，则返回true。</returns>
            public bool StartsWith(IEnumerable<T> prefix)
            {
                TrieNode current = _root;
                foreach (T i in prefix)
                {
                    if (current.Children.ContainsKey(i))
                    {
                        current = current.Children[i];
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }*/

        /// <summary>
        /// 获取所有以指定前缀开始的词。
        /// </summary>
        /// <param name="prefix">要搜索的前缀。</param>
        public T[][] StartsWith(IEnumerable<T> prefix)
        {
            TrieNode current = _root;

            // 遍历前缀，找到前缀对应的最后一个节点
            foreach (T i in prefix)
            {
                if (!current.Children.ContainsKey(i))
                {
                    return Array.Empty<T[]>();
                }
                current = current.Children[i];
            }

            // 使用栈来遍历字典树，收集所有以prefix为前缀的前缀
            Stack<TrieNode> nodeStack = new Stack<TrieNode>();
            Stack<List<T>> wordStack = new Stack<List<T>>();
            List<T[]> words = new List<T[]>();
            nodeStack.Push(current);
            wordStack.Push(new List<T>(prefix));

            while (nodeStack.Count > 0)
            {
                TrieNode node = nodeStack.Pop();
                List<T> word = wordStack.Pop();

                // 如果当前节点是词的结束，则添加词
                if (node.IsEnd)
                {
                    words.Add(word.ToArray());
                }

                // 遍历子节点，并将当前词和子节点压入栈中
                foreach (KeyValuePair<T, Trie<T>.TrieNode> child in node.Children)
                {
                    nodeStack.Push(child.Value);
                    List<T> newWord = new List<T>(word)
                    {
                        child.Key
                    };
                    wordStack.Push(newWord);
                }
            }

            return words.ToArray();
        }

        /// <summary>
        /// 获取<see cref="Trie{T}"/>中最长公共前缀
        /// </summary>
        public T[] LongestCommonPrefix()
        {
            TrieNode current = _root;
            List<T> lcp = new List<T>();

            while (current.Children.Count == 1 && !current.IsEnd)
            {
                foreach (KeyValuePair<T, Trie<T>.TrieNode> pair in current.Children)
                {
                    current = pair.Value;
                    lcp.Add(pair.Key);
                    break;
                }
            }

            return lcp.ToArray();
        }

        /// <summary>
        /// 修剪<see cref="Trie{T}"/>中不被任何前缀使用的节点。
        /// </summary>
        public void TrimExcess()
        {
            PruneNode(_root);
        }

        private void PruneNode(TrieNode node)
        {
            foreach (T child in node.Children.Keys.ToList())
            {
                PruneNode(node.Children[child]);
                if (!node.Children[child].IsEnd && node.Children[child].Children.Count == 0)
                {
                    node.Children.Remove(child);
                }
            }
        }

        private class TrieNode
        {
            public TrieNode() : this(false) { }

            public TrieNode(bool isEnd)
            {
                IsEnd = isEnd;
                Children = new Dictionary<T, TrieNode>();
            }

            public Dictionary<T, TrieNode> Children { get; }

            public bool IsEnd { get; set; }
        }
    }
}
