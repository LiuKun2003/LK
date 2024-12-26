using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CommunityToolkit.Diagnostics;

namespace LK.CommonLibrary
{
    public static partial class RandomBehavior
    {
        /// <summary>
        /// 从序列指定的长度中随机选择一个元素。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/>的元素的类型。</typeparam>
        /// <param name="source">给定的一组选项。</param>
        /// <param name="length">指定的长度。</param>
        /// <returns>包含在给定的序列中的一个随机的元素。</returns>
        public static TSource RandomItem<TSource>(this IEnumerable<TSource> source, int length)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(source));
            }

            int randomIndex = RandomNumberGenerator.GetInt32(0, length);

            return source.ElementAt(randomIndex);
        }
    }
}
