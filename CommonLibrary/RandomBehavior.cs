using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LK
{
    /// <summary>
    /// 提供关于随机行为相关的功能
    /// </summary>
    public static class RandomBehavior
    {
        /// <summary>
        /// 根据给定的概率来返回不同的值。
        /// </summary>
        /// <param name="value">命中的概率。</param>
        /// <returns>如果命中概率，则返回true。</returns>
        public static bool Probability(double value)
        {
            double randomInt = RandomNumberGenerator.GetInt32(0, int.MaxValue);
            double randomValue = randomInt / int.MaxValue;
            return randomValue < value;
        }

        /// <summary>
        /// 从一组选项中随机选择一个项。此方法的时间复杂度为O(n)。
        /// </summary>
        /// <param name="choices">给定的一组选项。</param>
        /// <returns>包含在给定的选项中的一个随机的项。</returns>
        public static T RandomItem<T>(this ICollection<T> choices)
        {
            if (choices.Count <= 0)
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            int i = RandomNumberGenerator.GetInt32(0, choices.Count);
            foreach (T item in choices)
            {
                if (i == 0)
                {
                    return item;
                }
                i--;
            }
            throw new Exception($"The enumerable number of {nameof(choices)} does not agree with the Count Property.");
        }

        /// <summary>
        /// 从一组选项中随机选择一个项。
        /// </summary>
        /// <param name="choices">给定的一组选项。</param>
        /// <returns>包含在给定的选项中的一个随机的项。</returns>
        public static T RandomItem<T>(this IList<T> choices)
        {
            if (choices.Count <= 0)
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            return choices[RandomNumberGenerator.GetInt32(0, choices.Count)];
        }

        /// <summary>
        /// 从一组选项中随机选择一个项。
        /// </summary>
        /// <param name="choices">给定的一组选项。</param>
        /// <returns>包含在给定的选项中的一个随机的项。</returns>
        public static T RandomItem<T>(this ReadOnlySpan<T> choices)
        {
            if (choices.IsEmpty)
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            return choices[RandomNumberGenerator.GetInt32(0, choices.Length)];
        }

        /// <summary>
        /// 从一个字符串中随机选择一个字符。
        /// </summary>
        /// <param name="choices">给定的字符串。</param>
        /// <returns>包含在给定的字符串中的一个随机的字符。</returns>
        public static char RandomItem(this string choices)
        {
            if (string.IsNullOrEmpty(choices))
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            return choices[RandomNumberGenerator.GetInt32(0, choices.Length)];
        }

        /// <summary>
        /// 从一组选项中随机选择项填充一个新的集合到指定数量。
        /// </summary>
        /// <param name="choices">给定的一组选项。</param>
        /// <param name="count">要填充的数量。</param>
        /// <returns>一个新的集合，由原集合中的项构成。</returns>
        public static ICollection<T> RandomItems<T>(this ICollection<T> choices, int count)
        {
            if (choices.Count <= 0)
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            List<T> list = new List<T>(choices);
            List<T> res = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                res[i] = list[RandomNumberGenerator.GetInt32(0, list.Count)];
            }
            return res;
        }

        /// <summary>
        /// 从一组选项中随机选择项填充一个新的列表到指定数量。
        /// </summary>
        /// <param name="choices">给定的一组选项。</param>
        /// <param name="count">要填充的数量。</param>
        /// <returns>一个新的列表，由原列表中的项构成。</returns>
        public static IList<T> RandomItems<T>(this IList<T> choices, int count)
        {
            if (choices.Count <= 0)
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            List<T> res = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                res[i] = choices[RandomNumberGenerator.GetInt32(0, choices.Count)];
            }
            return res;
        }

        /// <summary>
        /// 从一组选项中随机选择项填充一个新的数组到指定数量。
        /// </summary>
        /// <param name="choices">给定的一组选项。</param>
        /// <param name="count">要填充的数量。</param>
        /// <returns>一个新的数组，由原数组中的项构成。</returns>
        public static Span<T> RandomItem<T>(this ReadOnlySpan<T> choices, int count)
        {
            if (choices.IsEmpty)
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            List<T> res = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                res[i] = choices[RandomNumberGenerator.GetInt32(0, choices.Length)];
            }
            return res.ToArray();
        }

        /// <summary>
        /// 使用StringBuilder的阈值
        /// </summary>
        private const int ThresholdUsingStringBuilder = 8000;
        /// <summary>
        /// 从一个字符串中随机选择字符填充一个新的字符串到指定数量。
        /// </summary>
        /// <param name="choices">给定的字符串。</param>
        /// <param name="count">要填充的数量。</param>
        /// <returns>一个新的字符串，由原字符串中的字符构成。</returns>
        public static string RandomItems(this string choices, int count)
        {
            if (string.IsNullOrEmpty(choices))
            {
                throw new ArgumentException($"{nameof(choices)} is empty.");
            }

            string res = string.Empty;
            if (count <= ThresholdUsingStringBuilder)
            {
                for (int i = 0; i < count; i++)
                {
                    res += choices[RandomNumberGenerator.GetInt32(0, choices.Length)];
                }
            }
            else
            {
                StringBuilder builder = new StringBuilder(count);
                for (int i = 0; i < count; i++)
                {
                    builder.Append(choices[RandomNumberGenerator.GetInt32(0, choices.Length)]);
                }
                res = builder.ToString();
            }
            return res;
        }
    }
}
