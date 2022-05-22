using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class EnumEx
    {
        /// <summary>
        /// 是否包含当前枚举值
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool HasFlag(this Enum type, Enum flag)
        {
            if (flag == null)
            {
                throw new ArgumentNullException("flag");
            }

            int ntype = Convert.ToInt32(type);
            int nflag = Convert.ToInt32(flag);

            return (ntype & nflag) == nflag;
        }

        
        
    }
}
