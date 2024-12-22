using System.Collections.Generic;

namespace LK
{
    public interface IGrid3D<T> : IEnumerable<T>
    {
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }
        public T this[int x, int y, int z] { get; set; }
        public IEnumerable<T> GetNeighbors(int x, int y, int z);
    }
}
