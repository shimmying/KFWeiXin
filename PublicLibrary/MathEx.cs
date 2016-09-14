using System;

namespace KFWeiXin.PublicLibrary
{
    
    public static class MathEx
    {
        /// <summary>
        /// 判断某个数是否为底数的幂
        /// </summary>
        /// <param name="data">数</param>
        /// <param name="baseNumber">底数</param>
        /// <returns>如果是底数的幂，返回true；否则返回false。</returns>
        public static bool IsPowerOfBase(int data, int baseNumber)
        {
            bool result = false;
            if (baseNumber < 0)
                throw new ArgumentException("底数必须大于等于0。", "baseNumber");
            else if (baseNumber == 0)
                result = (data == 0);
            else if (baseNumber == 1)
                result = (data == 1);
            else
            {
                decimal temp = data;
                while (temp >= 1)
                {
                    if (temp == 1)
                    {
                        result = true;
                        break;
                    }
                    temp = temp / baseNumber;
                }
            }
            return result;
        }
    }
}