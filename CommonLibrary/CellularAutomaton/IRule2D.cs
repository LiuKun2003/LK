namespace LK
{
    public interface IRule2D<T>
    {
        public void ApplyRule(IGrid2D<T> grid);
    }
}
