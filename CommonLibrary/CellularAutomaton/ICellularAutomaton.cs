namespace LK
{
    /// <summary>
    /// 表示一个细胞自动机
    /// </summary>
    public interface ICellularAutomaton
    {
        /// <summary>
        /// 更新细胞自动机的状态。
        /// </summary>
        public void Update();
    }
}
