namespace LK
{
    /// <summary>
    /// 表示由规则和网格构成的三维细胞自动机。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CellularAutomaton3D<T> : ICellularAutomaton
    {
        private IGrid3D<T> _grid;
        private IRule3D<T> _rule;

        public CellularAutomaton3D(IGrid3D<T> grid, IRule3D<T> rule)
        {
            _grid = grid;
            _rule = rule;
        }

        public void Update()
        {
            _rule.ApplyRule(_grid);
        }
    }

}
