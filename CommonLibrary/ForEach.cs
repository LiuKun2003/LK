using System;
using System.Collections.Generic;

namespace LK
{
    /// <summary>
    /// 提供与遍历相关的功能。
    /// </summary>
    public static class ForEach
    {
        /// <summary>
        /// 获取一个二维下标的邻域的下标。
        /// </summary>
        /// <returns>包含了邻域下标的迭代器。</returns>
        public static IEnumerable<(int, int)> Surround(int x, int y, int xLimit, int yLimit)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        int m = x + i;
                        if (m < 0 || m >= xLimit) continue;
                        int n = y + j;
                        if (n < 0 || n >= yLimit) continue;
                        yield return (m, n);
                    }
                }
            }
        }

        /// <summary>
        /// 获取二维数组给定下标的邻域下标。
        /// </summary>
        /// <returns>包含了邻域下标的迭代器。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> Surround<T>(this T[,] array, int x, int y)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int width = array.GetLength(0);
            int height = array.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        int m = x + i;
                        if (m < 0 || m >= width) continue;
                        int n = y + j;
                        if (n < 0 || n >= height) continue;
                        yield return array[m, n];
                    }
                }
            }
        }

        /// <summary>
        /// 获取一个三维下标的邻域的下标。
        /// </summary>
        /// <returns>包含了邻域下标的迭代器。</returns>
        public static IEnumerable<(int, int, int)> Surround(int x, int y, int z, int xLimit, int yLimit, int zLimit)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (!(i == 0 && j == 0 && k == 0))
                        {
                            int m = x + i;
                            if (m < 0 || m >= xLimit) continue;
                            int n = y + j;
                            if (n < 0 || n >= yLimit) continue;
                            int u = z + k;
                            if (u < 0 || n >= zLimit) continue;
                            yield return (m, n, u);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取三维数组给定下标的邻域下标。
        /// </summary>
        /// <returns>包含了邻域下标的迭代器。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> Surround<T>(this T[,,] array, int x, int y, int z)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int width = array.GetLength(0);
            int height = array.GetLength(1);
            int depth = array.GetLength(2);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (!(i == 0 && j == 0 && k == 0))
                        {
                            int m = x + i;
                            if (m < 0 || m >= width) continue;
                            int n = y + j;
                            if (n < 0 || n >= height) continue;
                            int u = z + k;
                            if (u < 0 || n >= depth) continue;
                            yield return array[m, n, u];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定长的下标。
        /// </summary>
        /// <returns>包含了下标的迭代器。</returns>
        public static IEnumerable<int> Range(int xLimit)
        {
            for (int x = 0; x < xLimit; x++)
            {
                yield return x;
            }
        }

        /// <summary>
        /// 获取数组的下标。
        /// </summary>
        /// <returns>包含了下标的迭代器。</returns>
        public static IEnumerable<int> Range<T>(this T[] array)
        {
            for (int x = 0; x < array.Length; x++)
            {
                yield return x;
            }
        }

        /// <summary>
        /// 获取指定长宽的二维下标。
        /// </summary>
        /// <returns>包含了下标的迭代器。</returns>
        public static IEnumerable<(int, int)> Range2D(int xLimit, int yLimit)
        {
            for (int x = 0; x < xLimit; x++)
            {
                for (int y = 0; y < yLimit; y++)
                {
                    yield return (x, y);
                }
            }
        }

        /// <summary>
        /// 获取数组的下标。
        /// </summary>
        /// <returns>包含了下标的迭代器。</returns>
        public static IEnumerable<(int, int)> Range2D<T>(this T[,] array)
        {
            int xlimit = array.GetLength(0);
            int ylimit = array.GetLength(1);
            for (int x = 0; x < xlimit; x++)
            {
                for (int y = 0; y < ylimit; y++)
                {
                    yield return (x, y);
                }
            }
        }

        /// <summary>
        /// 获取指定长宽高的三维下标。
        /// </summary>
        /// <returns>包含了下标的迭代器。</returns>
        public static IEnumerable<(int, int, int)> Range3D(int xLimit, int yLimit, int zLimit)
        {
            for (int x = 0; x < xLimit; x++)
            {
                for (int y = 0; y < yLimit; y++)
                {
                    for (int z = 0; z < zLimit; z++)
                    {
                        yield return (x, y, z);
                    }
                }
            }
        }

        /// <summary>
        /// 获取数组的下标。
        /// </summary>
        /// <returns>包含了下标的迭代器。</returns>
        public static IEnumerable<(int, int, int)> Range3D<T>(this T[,,] array)
        {
            int xlimit = array.GetLength(0);
            int ylimit = array.GetLength(1);
            int zlimit = array.GetLength(2);
            for (int x = 0; x < xlimit; x++)
            {
                for (int y = 0; y < ylimit; y++)
                {
                    for (int z = 0; z < zlimit; z++)
                    {
                        yield return (x, y, z);
                    }
                }
            }
        }

        /// <summary>
        /// 使用指定的步长进行迭代。
        /// </summary>
        public static IEnumerable<T> Step<T>(this IEnumerable<T> enumerable, int step)
        {
            int counter = 0;
            foreach (T item in enumerable)
            {
                if (counter == step || counter == 0)
                {
                    counter = 0;
                    yield return item;
                }
                counter++;
            }
        }

        /// <summary>
        /// 使用指定的开始位置进行迭代。
        /// </summary>
        public static IEnumerable<T> Start<T>(this IEnumerable<T> enumerable, int start)
        {
            int counter = 0;
            foreach (T item in enumerable)
            {
                if (counter >= start)
                {
                    yield return item;
                }
                else
                {
                    counter++;
                }
            }
        }
    }
}
