using System;
using System.Collections.Generic;

namespace LK
{
    /// <summary>
    /// 用于细胞自动机的二维网格。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CellGrid2D<T> : IGrid2D<T>
    {
        private T[,] _grid;
        private int _width;
        private int _height;

        public CellGrid2D() : this(0, 0, null) { }

        public CellGrid2D(int width, int height) : this(width, height, null) { }

        public CellGrid2D(int width, int height, Func<int, int, T> initializer)
        {
            _width = width;
            _height = height;
            _grid = new T[_width, _height];

            if (initializer != null)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        _grid[i, j] = initializer(i, j);
                    }
                }
            }
        }

        public T this[int x, int y]
        {
            get => _grid[x, y];
            set => _grid[x, y] = value;
        }

        public int Width => _width;

        public int Height => _height;

        public IEnumerable<T> GetNeighbors(int x, int y)
        {
            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        int m = x + i;
                        if (m < 0 || m >= _width) continue;
                        int n = y + j;
                        if (n < 0 || n >= _height) continue;
                        yield return _grid[m, n];
                    }
                }
            }
        }

        public int CountNeighbors(int x, int y, Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            int res = 0;
            foreach (T neighbor in GetNeighbors(x, y))
            {
                if (predicate(neighbor))
                {
                    res++;
                }
            }
            return res;
        }
    }

}
