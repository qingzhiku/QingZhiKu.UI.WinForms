using System;
using System.Collections.Generic;
using System.Text;

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

        /// <summary>
        /// 矩形中心点位置
        /// </summary>
        public static Point Center(this Rectangle rect)
        {
            var a = rect.Width % 2 > 0 ? 1 : 0;
            var b = rect.Height % 2 > 0 ? 1 : 0;

            return new Point(rect.Left + rect.Width / 2 + a, rect.Top + rect.Height / 2 + b);
        }






    }
    


    
}
