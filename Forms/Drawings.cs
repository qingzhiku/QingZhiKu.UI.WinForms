/***************************************************************************************************
 * QingZhiKu 开源控件库
 ***************************************************************************************************
 * 文件名称: DrawingHelper.cs
 * 文件说明: 阴影窗体阴影绘图类
 * 起始日期：2022-5-5
 * 
 * 
 * 
 * 2022-5-5：绘制背景图片
 ***************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace System.Drawing
{
    /// <summary>
    /// 四边外部的区域
    /// </summary>
    public struct MarginRectangle : IDisposable
    {
        public Rectangle TopRect { get; set; }
        public Rectangle RightRect { get; set; }
        public Rectangle BottomRect { get; set; }
        public Rectangle LeftRect { get; set; }

        public static readonly MarginRectangle Empty;

        public Rectangle[] ToArray()
        {
            return new Rectangle[] { TopRect, RightRect, BottomRect, LeftRect };
        }

        public void Dispose()
        {
            this = MarginRectangle.Empty;
        }
    }

    /// <summary>
    /// 四角外部的区域
    /// </summary>
    public struct AngleRectangle : IDisposable
    {
        public Rectangle TopLeftRect { get; set; }
        public Rectangle TopRightRect { get; set; }
        public Rectangle BottomLeftRect { get; set; }
        public Rectangle BottomRightRect { get; set; }

        public static readonly AngleRectangle Empty;

        public Rectangle[] ToArray()
        {
            return new Rectangle[] { TopLeftRect, TopRightRect, BottomLeftRect, BottomRightRect };
        }

        public void Dispose()
        {
            this = AngleRectangle.Empty;
        }
    }

    /// <summary>
    /// 建立圆角路径的样式。
    /// </summary>
    public enum RoundStyle
    {
        /// <summary>
        /// 四个角都不是圆角
        /// </summary>
        None = 1,
        /// <summary>
        /// 四个角都为圆角
        /// </summary>
        All = 2,
        /// <summary>
        /// 左边两个角为圆角
        /// </summary>
        Left = 4,
        /// <summary>
        /// 右边两个角为圆角
        /// </summary>
        Right = 8,
        /// <summary>
        /// 上边两个角为圆角
        /// </summary>
        Top = 16,
        /// <summary>
        /// 下边两个角为圆角
        /// </summary>
        Bottom = 32,
        /// <summary>
        /// 左下角为圆角
        /// </summary>
        BottomLeft = 64,
        /// <summary>
        /// 右下角为圆角
        /// </summary>
        BottomRight = 128,
    }

   
    public class DrawingHelper
    {
        /// <summary>
        /// 建立带有圆角样式的路径
        /// </summary>
        /// <param name="rect">用来建立路径的矩形</param>
        /// <param name="_radius">圆角的大小</param>
        /// <param name="style">圆角的样式</param>
        /// <returns>建立的路径</returns>
        public static GraphicsPath CreateRoundRectanglePath(Rectangle rect, int radius, RoundStyle style = RoundStyle.All)
        {
            GraphicsPath path = new GraphicsPath();

            // 圆角半径为0时，不做圆角处理，直接用矩形做路径
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            // 圆角的半径不能超过矩形高度或宽度的一半
            radius = rect.Width / 2 < radius ? rect.Width / 2 : radius;
            radius = rect.Height / 2 < radius ? rect.Height / 2 : radius;

            // 如果矩形为奇数，则半径应该减1
            int radiusCorrection = rect.Width % 2 == 1 ? 0 : 1;
            switch (style)
            {
                case RoundStyle.None:
                    path.AddRectangle(rect);
                    break;
                case RoundStyle.All:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y, radius, radius, 270, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 0, 90);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 90, 90);
                    break;
                case RoundStyle.Left:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddLine(
                        rect.Right - radiusCorrection, rect.Y,
                        rect.Right - radiusCorrection, rect.Bottom - radiusCorrection);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 90, 90);
                    break;
                case RoundStyle.Right:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y, radius, radius, 270, 90);
                    path.AddArc(
                       rect.Right - radius - radiusCorrection,
                       rect.Bottom - radius - radiusCorrection,
                       radius, radius, 0, 90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    break;
                case RoundStyle.Top:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y, radius, radius, 270, 90);
                    path.AddLine(
                        rect.Right - radiusCorrection, rect.Bottom - radiusCorrection,
                        rect.X, rect.Bottom - radiusCorrection);
                    break;
                case RoundStyle.Bottom:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 0, 90);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 90, 90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
                case RoundStyle.BottomLeft:
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 90, 90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    path.AddLine(
                        rect.Right - radiusCorrection,
                        rect.Y,
                        rect.Right - radiusCorrection,
                        rect.Bottom - radiusCorrection);
                    break;
                case RoundStyle.BottomRight:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius, radius, 0, 90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
            }
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 建立带有圆角样式的区域
        /// </summary>
        public static Region CreateRoundRectangle(Rectangle rect, int radius)
        {
            return Region.FromHrgn(Win32.CreateRoundRectRgn(rect.X, rect.Y, rect.Width, rect.Height, radius, radius));
        }

        /// <summary>
        /// 绘制四周圆角阴影位图
        /// </summary>
        public static Bitmap DrawShadowBitmap(Bitmap backbitmap, MarginRectangle marginRectangle, AngleRectangle angleRectangle,
            Color shadowColor, Pen borderPen, GraphicsPath innerPath)
        {

            //var bitmap = new Bitmap(outsize.Width, outsize.Height);
            Graphics g = Graphics.FromImage(backbitmap);

            //必要设置，当ShadowSpread设置为1或2时，需要高质量绘制才能显示
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            #region 绘制四条边 
            LinearGradientBrush brush;
            // left 
            if (marginRectangle.LeftRect.Width > 0)
            {
                brush = new LinearGradientBrush(
                    marginRectangle.LeftRect.Location,
                    marginRectangle.LeftRect.RTLocation(),
                    Color.Transparent, shadowColor);
                g.FillRectangle(brush, marginRectangle.LeftRect);
            }

            // top 
            if (marginRectangle.TopRect.Height > 0)
            {
                brush = new LinearGradientBrush(
                    marginRectangle.TopRect.Location,
                    marginRectangle.TopRect.LBLocation(),
                    Color.Transparent, shadowColor);
                g.FillRectangle(brush, marginRectangle.TopRect);
            }

            // right  
            if (marginRectangle.RightRect.Width > 0)
            {
                brush = new LinearGradientBrush(
                    marginRectangle.RightRect.Location,
                    marginRectangle.RightRect.RTLocation(),
                    shadowColor, Color.Transparent);
                g.FillRectangle(brush, marginRectangle.RightRect);
            }

            // down  
            if (marginRectangle.BottomRect.Height > 0)
            {
                brush = new LinearGradientBrush(
                    marginRectangle.BottomRect.Location,
                    marginRectangle.BottomRect.LBLocation(),
                    shadowColor, Color.Transparent);
                g.FillRectangle(brush, marginRectangle.BottomRect);
            }
            #endregion

            #region  绘制四个角 
            // tl
            FillGradientPie(g, angleRectangle.TopLeftRect, 180, 90, shadowColor);

            // tr
            FillGradientPie(g, angleRectangle.TopRightRect, 270, 90, shadowColor);

            //// br
            FillGradientPie(g, angleRectangle.BottomRightRect, 0, 90, shadowColor);

            //// bl
            FillGradientPie(g, angleRectangle.BottomLeftRect, 90, 90, shadowColor);
            #endregion

            #region 删除内部主体区域像素

            g.SetClip(innerPath, CombineMode.Intersect);
            g.Clear(Color.Transparent);
            g.DrawPath(borderPen, innerPath);
            g.Dispose();

            #endregion

            return backbitmap;
        }

        /// <summary>
        /// 填充渐变饼形
        /// </summary>
        public static void FillGradientPie(Graphics g, Rectangle rec, int startAngle, int sweepAngle, Color shadowColor)
        {
            if (rec.Width <= 0 || rec.Height < 0) return;

            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(rec);

            PathGradientBrush pgb = new PathGradientBrush(gp);
            pgb.CenterColor = shadowColor;
            pgb.SurroundColors = new[] { Color.Transparent };
            pgb.CenterPoint = new PointF(rec.Left + rec.Width / 2, rec.Top + rec.Height / 2);

            g.FillPie(pgb, rec, startAngle, sweepAngle);
        }

        /// <summary>
        /// 绘制圆角矩形边框
        /// </summary>
        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int radius)
        {
            g.DrawArc(pen, rect.X, rect.Y, radius, radius, 180, 90);
            g.DrawLine(pen, rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            g.DrawArc(pen, rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            g.DrawLine(pen, rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            g.DrawArc(pen, rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            g.DrawLine(pen, rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            g.DrawArc(pen, rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            g.DrawLine(pen, rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);
        }


        

    }


    
}
