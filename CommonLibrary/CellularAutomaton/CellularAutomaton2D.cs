using System;
using System.Collections.Generic;
using System.Text;

namespace LK
{
    /// <summary>
    /// 表示由规则和网格构成的二维细胞自动机。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CellularAutomaton2D<T>
    {
        private IGrid2D<T> _grid;
        private IRule2D<T> _rule;

        public CellularAutomaton2D(IGrid2D<T> grid, IRule2D<T> rule)
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
