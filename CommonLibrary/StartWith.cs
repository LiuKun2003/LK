using System;
using System.Collections.Generic;
using CommunityToolkit.Diagnostics;

namespace LK.CommonLibrary
{
    public static partial class ForEach
    {
        /// <summary>
        /// 使用指定的开始位置进行迭代。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/>的元素的类型。</typeparam>
        /// <param name="source">原迭代器。</param>
        /// <param name="start">开始位置。</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>为null。</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>为负数。</exception>
        /// <returns></returns>
        public static IEnumerable<TSource> StartWith<TSource>(this IEnumerable<TSource> source, int start)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(source));
            }
            if (start < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(start));
            }

            return StartEnumerable(source, start);
        }

        private static IEnumerable<TSource> StartEnumerable<TSource>(IEnumerable<TSource> source, int start)
        {
            int counter = 0;
            foreach (TSource element in source)
            {
                if (counter < start)
                {
                    counter++;
                }
                else
                {
                    yield return element;
                }
            }
        }
    }
}
