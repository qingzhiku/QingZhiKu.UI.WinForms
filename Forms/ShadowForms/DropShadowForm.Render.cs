using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    public partial class DropShadowForm
    {


        /// <summary>
        /// 减少闪烁更新窗体样式
        /// </summary>
        protected virtual void UpdateStylesToReduceFlicker()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);


            // 强制分配样式重新应用到控件上
            UpdateStyles();
        }

        /// <summary>
        /// 获取外部区域
        /// </summary>
        protected virtual void UpdateMarginRectangle()
        {
            this.MarginRectangle = MarginRectangle.Empty;

            if (Owner == null)
                return;
            if (!Owner.IsHandleCreated)
                return;
            if (Owner.GetWindowState() == FormWindowState.Maximized)
                return;

            int radius = CornerRound / 2 + ShadowSpread;

            // 宽度或高度+BorderWidth，覆盖边框
            this.MarginRectangle = new MarginRectangle()
            {
                // edge
                TopRect = new Rectangle(radius, 0, Width - radius * 2 - BorderWidth, ShadowSpread + BorderWidth),
                LeftRect = new Rectangle(0, radius, ShadowSpread + BorderWidth, Height - radius * 2 - BorderWidth),
                RightRect = new Rectangle(Width - ShadowSpread - BorderWidth, radius, ShadowSpread + BorderWidth, Height - radius * 2 - BorderWidth),
                BottomRect = new Rectangle(radius, Height - ShadowSpread - BorderWidth, Width - radius * 2 - BorderWidth, ShadowSpread + BorderWidth),

                // angle
                TopLeftRect = new Rectangle(0, 0, radius, radius),
                TopRightRect = new Rectangle(Width - radius - BorderWidth, 0, radius, radius),
                BottomLeftRect = new Rectangle(0, Height - radius - BorderWidth, radius, radius),
                BottomRightRect = new Rectangle(Width - radius - BorderWidth, Height - radius - BorderWidth, radius, radius)
            };
        }

        /// <summary>
        /// 更换阴影图片
        /// </summary>
        protected virtual void UpdateShadowBitmap(int innerWidth, int innerHeight)
        {
            UpdateMarginRectangle();

            var bitmap = CreateShadowBitmap(innerWidth, innerHeight);

            SetDropShadowBitmap(bitmap);
        }

        /// <summary>
        /// 创建阴影图片
        /// </summary>
        protected virtual Bitmap CreateShadowBitmap(int innerWidth, int innerHeight)
        {
            if (Owner == null || Math.Min(Width, Height) == 0)
                return new Bitmap(1, 1);

            var bitmap = new Bitmap(Width, Height);

            if (ShadowSpread <= 0)
                return bitmap;
            if (_masterForm != null && _masterForm.GetWindowState() == FormWindowState.Maximized)
                return bitmap;

            //if (ShadowSpread > 0)
            //{
            // 奇数时校正，则半径应该减1
            int widthCorrection = /*Owner.Width*/innerWidth % 2 == 1 ? 0 : 1;
            int heightCorrection = /*Owner.Height*/innerHeight % 2 == 1 ? 0 : 1;

            using (GraphicsPath path = DrawingHelper.CreateRoundRectanglePath(new Rectangle(new Point(ShadowSpread, ShadowSpread),
                    new Size(/*Owner.Width*/innerWidth - widthCorrection, /*Owner.Height*/innerHeight - heightCorrection)), CornerRound))
            {

                //var region = DrawingHelper.CreateRoundRectangle(new Rectangle(new Point(ShadowSpread, ShadowSpread),
                //    new Size(Owner.Width - widthCorrection, Owner.Height - heightCorrection)), CornerRound);

                var borderColor = _ncACTIVATE ? SystemColors.ControlDark : ControlPaint.LightLight(SystemColors.ControlDark);

                using (Pen pen = new Pen(Color.FromArgb(255, /*borderColor*/BorderColor), BorderWidth) { Alignment = PenAlignment.Center, DashCap = DashCap.Round })
                {
                    var bitmargin = new MarginRectangle()
                    {
                        // 四边
                        LeftRect = new Rectangle(MarginRectangle.LeftRect.Location, new Size(MarginRectangle.TopLeftRect.Width, MarginRectangle.LeftRect.Size.Height)),
                        TopRect = new Rectangle(MarginRectangle.TopRect.Location, new Size(MarginRectangle.TopRect.Width, MarginRectangle.TopLeftRect.Height)),
                        RightRect = new Rectangle(new Point(MarginRectangle.RightRect.Location.X - MarginRectangle.TopRightRect.Width + MarginRectangle.RightRect.Width, MarginRectangle.RightRect.Location.Y), new Size(MarginRectangle.TopRightRect.Width, MarginRectangle.RightRect.Height)),
                        BottomRect = new Rectangle(new Point(MarginRectangle.BottomRect.Location.X, MarginRectangle.BottomRect.Location.Y - MarginRectangle.BottomLeftRect.Height + MarginRectangle.BottomRect.Height), new Size(MarginRectangle.BottomRect.Width, MarginRectangle.BottomLeftRect.Height)),

                        // 角
                        TopLeftRect = new Rectangle(MarginRectangle.TopLeftRect.Location, new Size(MarginRectangle.TopLeftRect.Size.Width * 2, MarginRectangle.TopLeftRect.Size.Height * 2)),
                        TopRightRect = new Rectangle(new Point(MarginRectangle.TopRightRect.Location.X - MarginRectangle.TopRightRect.Size.Width, MarginRectangle.TopRightRect.Location.Y), new Size(MarginRectangle.TopRightRect.Size.Width * 2, MarginRectangle.TopRightRect.Size.Height * 2)),
                        BottomLeftRect = new Rectangle(new Point(MarginRectangle.BottomLeftRect.Location.X, MarginRectangle.BottomLeftRect.Location.Y - MarginRectangle.BottomLeftRect.Size.Height), new Size(MarginRectangle.BottomLeftRect.Size.Width * 2, MarginRectangle.BottomLeftRect.Size.Height * 2)),
                        BottomRightRect = new Rectangle(new Point(MarginRectangle.BottomRightRect.Location.X - MarginRectangle.BottomRightRect.Size.Width, MarginRectangle.BottomRightRect.Location.Y - MarginRectangle.BottomRightRect.Size.Height), new Size(MarginRectangle.BottomRightRect.Size.Width * 2, MarginRectangle.BottomRightRect.Size.Height * 2))
                    };

                    bitmap = DrawingHelper.DrawGradientShadowToBitmapEdge(
                                    backbitmap: bitmap,
                                    marginRectangle: bitmargin,
                                    shadowColor: ShadowColor);

                    //bitmap = BitmapEx.GaussianBlur(bitmap,5,5);

                    bitmap = DrawingHelper.Clip(backbitmap: bitmap, innerPath: path);

                    bitmap = DrawingHelper.DrawPath(backbitmap: bitmap, borderPen: pen, innerPath: path);

                    //using (Graphics g = Graphics.FromImage(bitmap))
                    //{
                    //    // 边界
                    //    //g.DrawRectangles(Pens.Red, MarginRectangle.ToEdgeArray());

                    //    // 角落
                    //    //g.DrawRectangles(Pens.Green, MarginRectangle.ToAngleArray());

                    //    //// 完整
                    //    //g.DrawRectangle(Pens.Blue, Bounds);

                    //    //// 阴影
                    //    //g.DrawRectangles(Pens.SpringGreen, bitmargin.ToEdgeArray());
                    //}

                    //pen.Dispose();
                    //}

                    //bitmap.Save("bg.png", Drawing.Imaging.ImageFormat.Png);


                }

            }


            return bitmap;
        }

        private static Bitmap? oldbitmap = null;
        /// <summary>
        /// 设置窗体阴影图片
        /// </summary>
        protected virtual void SetDropShadowBitmap(Bitmap shadowBitmap)
        {
            if (oldbitmap != null && oldbitmap != shadowBitmap)
            {
                oldbitmap.Dispose();
            }
            oldbitmap = shadowBitmap;

            if (!Bitmap.IsCanonicalPixelFormat(shadowBitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(shadowBitmap.PixelFormat))
                throw new ApplicationException("图片一定要32位的，并且带Alhpa通道");
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.PointF topLoc = new Win32.PointF(Left, Top);
                Win32.SizeF bitMapSize = new Win32.SizeF(shadowBitmap.Width, shadowBitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.PointF srcLoc = new Win32.PointF(0, 0);

                hBitmap = shadowBitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = Byte.Parse("255"); // 相当于启用Alpha通道
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0,
                    ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }





    }
}
