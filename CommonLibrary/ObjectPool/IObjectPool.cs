namespace LK
{
    /// <summary>
    /// ��ʾһ�������
    /// </summary>
    /// <typeparam name="T">���������</typeparam>
    public interface IObjectPool<T>
    {
        /// <summary>
        /// �����һ��������ã���ӳ��л�ȡһ�����󣬷��򴴽�һ������
        /// </summary>
        /// <returns>һ��<see cref="T"></see></returns>
        public T Get();

        /// <summary>
        /// �����󷵻ص����С�
        /// </summary>
        /// <param name="obj">Ҫ��ӵ����еĶ���</param>
        public void Return(T obj);
    }
}
