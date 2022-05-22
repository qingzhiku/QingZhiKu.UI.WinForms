using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Drawing
{
    public static class RectangleEx
    {
        /// <summary>
        /// 矩形右上角点位置
        /// </summary>
        public static Point RTLocation(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Y);
        }

        /// <summary>
        /// 矩形左下角点位置
        /// </summary>
        public static Point LBLocation(this Rectangle rect)
        {
            return new Point(rect.X, rect.Bottom);
        }

        /// <summary>
        /// 矩形右下角点位置
        /// </summary>
        public static Point RBLocation(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }
    }
    


    
}
