using System.Security.Cryptography;

namespace LK.CommonLibrary
{
    public static partial class RandomBehavior
    {
        /// <summary>
        /// 根据给定的概率来返回不同的值。
        /// </summary>
        /// <param name="probability">命中的概率。</param>
        /// <returns>如果命中概率，则返回true。</returns>
        public static bool Hit(double probability)
        {
            if (probability < 0)
            {
                return false;
            }
            else if (probability > 1)
            {
                return true;
            }
            else
            {
                double randomInt = RandomNumberGenerator.GetInt32(0, int.MaxValue);
                double randomValue = randomInt / int.MaxValue;
                return randomValue < probability;
            }
        }
    }
}
