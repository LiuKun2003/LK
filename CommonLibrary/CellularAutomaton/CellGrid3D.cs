using System;
using System.Collections;
using System.Collections.Generic;

namespace LK
{
    /// <summary>
    /// 用于细胞自动机的三维网格。
    /// </summary>
    public class CellGrid3D<T> : IGrid3D<T>
    {
        private T[,,] _grid;
        private int _width;
        private int _height;
        private int _depth;

        public CellGrid3D() : this(0, 0, 0, null) { }

        public CellGrid3D(int width, int height, int depth) : this(width, height, depth, null) { }

        public CellGrid3D(int width, int height, int depth, Func<int, int, int, T> initializer)
        {
            _width = width;
            _height = height;
            _depth = depth;
            _grid = new T[_width, _height, _depth];

            if (initializer != null)
            {
                for (int x = 0; x < _width; x++)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        for (int z = 0; z < _depth; z++)
                        {
                            _grid[x, y, z] = initializer(x, y, z);
                        }
                    }
                }
            }
        }

        public T this[int x, int y, int z]
        {
            get => _grid[x, y, z];
            set => _grid[x, y, z] = value;
        }

        public int Width => _width;

        public int Height => _height;

        public int Depth => _depth;

        public IEnumerable<T> GetNeighbors(int x, int y, int z)
        {
            foreach (var neighbor in _grid.Surround(x, y, z))
            {
                yield return neighbor;
            }
        }

        public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)_grid.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _grid.GetEnumerator();
    }
}
