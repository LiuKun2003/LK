using System;
using System.Collections.Generic;
using CommunityToolkit.Diagnostics;

namespace LK.CommonLibrary
{
    public static partial class ForEach
    {
        /// <summary>
        /// 使用指定的步长进行迭代。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/>的元素的类型。</typeparam>
        /// <param name="source">原迭代器。</param>
        /// <param name="step">指定的步长。</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>为null。</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="step"/>为负数。</exception>
        /// <returns>使用步长的新的迭代器。</returns>
        public static IEnumerable<TSource> Step<TSource>(this IEnumerable<TSource> source, int step)
        {
            Guard.IsNotNull(source, nameof(source));

            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(source));
            }
            if (step < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(step));
            }

            return StepEnumerable(source, step);
        }

        private static IEnumerable<TSource> StepEnumerable<TSource>(IEnumerable<TSource> source, int step)
        {
            int counter = 0;
            foreach (TSource element in source)
            {
                if (counter % step == 0)
                {
                    counter = 0;
                    yield return element;
                }
                counter++;
            }
        }
    }
}
