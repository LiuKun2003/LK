using System;
using System.Collections.Generic;

namespace LK
{
    public interface IGrid2D<T>
    {
        public int Width { get; }
        public int Height { get; }
        public T this[int x, int y] { get; set; }
        public IEnumerable<T> GetNeighbors(int x, int y);
        public int CountNeighbors(int x, int y, Predicate<T> predicate);
    }
}
