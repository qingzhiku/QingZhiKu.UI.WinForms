using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Drawing
{
    public static class ColorEx
    {
        /// <summary>
        /// Color转整数
        /// </summary>
        public static int ToInt(this Color color)
        {
            return color.ToArgb() & 0xffffff;
        }

        
    }
}
