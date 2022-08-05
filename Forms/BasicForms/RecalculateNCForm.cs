using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 第五层，重新计算非客户区窗体
    /// </summary>
    public class RecalculateNCForm : DWMThemeForm
    {
        private bool windowPosChangedHack;
        //private Rectangle _windowMaxBounds;
        private Padding _newWindowGaps;
        private WindowMinMaxInfo _windowMinMaxInfo;

        public RecalculateNCForm()
            : base()
        {
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (windowPosChangedHack && WindowExtendClientAreaIntoFrame != Padding.Empty)
            {
                var gap = WindowExtendClientAreaIntoFrame;

                bool isneed = false;

                if (specified.HasFlag(BoundsSpecified.Y) && RestoreBounds.Top < 0)
                {
                    isneed = true;
                }

                base.SetBoundsCore(x, isneed ? Math.Max(y,0):y, width - gap.Horizontal, height - gap.Vertical, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            if (WindowExtendClientAreaIntoFrame == Padding.Empty)
            {
                base.SetClientSizeCore(x, y);
            }
            else
            {
                windowPosChangedHack = true;
                base.SetClientSizeCore(x, y);
                windowPosChangedHack = false;
            }
        }

        protected override void WM_WINDOWPOSCHANGED(ref Message m)
        {
            if (WindowExtendClientAreaIntoFrame != Padding.Empty)
            {
                windowPosChangedHack = true;
                base.WM_WINDOWPOSCHANGED(ref m);
                windowPosChangedHack = false;
            }
            else
            {
                base.WM_WINDOWPOSCHANGED(ref m);
            }
        }

        protected override void WM_CREATE(ref Message m)
        {
            base.WM_CREATE(ref m);

            if (DesignMode) return;
            if (!this.IsHandleCreated) return;

            NativeMethodHelper.PostWindowPosMessage(m.HWnd);
        }

        protected override void WM_NCCALCSIZE(ref Message m)
        {
            if (DesignMode /*|| IsDisposed*/)
            {
                base.WM_NCCALCSIZE(ref m);
                return;
            }

            if (m.WParam != IntPtr.Zero && m.Result == IntPtr.Zero)
            {
                var gap = WindowExtendClientAreaIntoFrame;

                if (gap != Padding.Empty)
                {
                    Win32.NCCALCSIZE_PARAMS nccsp = (Win32.NCCALCSIZE_PARAMS)m.GetLParam(typeof(Win32.NCCALCSIZE_PARAMS));

                    CalculateNewWindowGapCore(gap);

                    //if (PrevisionWindowState == FormWindowState.Maximized)
                    //{
                    //    gap.Top -= (WindowNCBorderThickness.Bottom - WindowBorderSize.Height);
                    //    //gap.Left = 0;
                    //    //gap.Right = 0;
                    //    //gap.Bottom = 0;
                    //}

                    //nccsp.rcNewWindow.Top -= gap.Top;
                    //nccsp.rcNewWindow.Left -= gap.Left;
                    //nccsp.rcNewWindow.Right += gap.Right;
                    //nccsp.rcNewWindow.Bottom += gap.Bottom;

                    nccsp.rcNewWindow.Top -= _newWindowGaps.Top;
                    nccsp.rcNewWindow.Left -= _newWindowGaps.Left;
                    nccsp.rcNewWindow.Right += _newWindowGaps.Right;
                    nccsp.rcNewWindow.Bottom += _newWindowGaps.Bottom;

                    nccsp.rcOldWindow = nccsp.rcNewWindow;

                    Marshal.StructureToPtr(nccsp, m.LParam, false);

                    m.Result = new IntPtr(0x400); //IntPtr.Zero; //
                }
                //  base.WM_NCCALCSIZE(ref m);
            }
            //  else
            //  {
            //      base.WM_NCCALCSIZE(ref m);
            //  }

            //  var gap = WindowExtendClientAreaIntoFrame;

            //  if (gap == Padding.Empty) return;
            //  if (m.WParam == IntPtr.Zero) return;

            //  Win32.NCCALCSIZE_PARAMS nccsp;
            //  nccsp = (Win32.NCCALCSIZE_PARAMS)m.GetLParam(typeof(Win32.NCCALCSIZE_PARAMS));

            //  var correntgap = (this.PrevisionWindowState == FormWindowState.Maximized && !OSFeature.Feature.OnWin11OrLarge() ? 1 : 0);
            //  nccsp.rcNewWindow.Top -= gap.Top /*+ correntgap*/;

            //  if (!_aeroEnabled)
            //  {
            //  nccsp.rcNewWindow.Left -= gap.Left;
            //  nccsp.rcNewWindow.Right += gap.Right;
            //  nccsp.rcNewWindow.Bottom += gap.Bottom;
            //  }

            //  nccsp.rcOldWindow = nccsp.rcNewWindow;
            //  Marshal.StructureToPtr(nccsp, m.LParam, false);
            //  m.Result = new IntPtr(0x400); // IntPtr.Zero;

            base.WM_NCCALCSIZE(ref m);
        }

        protected virtual void CalculateNewWindowGapCore(Padding newWindowGap)
        {
            _newWindowGaps = newWindowGap;
        }

        protected override void WM_GETMINMAXINFO(ref Message m)
        {
            base.WM_GETMINMAXINFO(ref m);

            if (IsRestrictedWindow || DesignMode) return;

            Win32.MINMAXINFO minmax = (Win32.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(Win32.MINMAXINFO));

            if (MaximumSize != Size.Empty)
            {
                minmax.ptMaxTrackSize = MaximumSize;
            }
            else
            {
                if (this.Parent == null)
                {
                    //var gap = WindowExtendClientAreaIntoFrame;

                    SetWindowMinMaxBoundsCore(minmax.ptMaxSize,
                        minmax.ptMaxPosition,
                        minmax.ptMinTrackSize,
                        minmax.ptMaxTrackSize);

                    minmax.ptMaxSize = _windowMinMaxInfo.ptMaxSize;
                    minmax.ptMaxPosition = _windowMinMaxInfo.ptMaxPosition;
                    minmax.ptMinTrackSize = _windowMinMaxInfo.ptMinTrackSize;
                    minmax.ptMaxTrackSize = _windowMinMaxInfo.ptMaxTrackSize;

                    //minmax.ptMaxPosition.X += _windowMaxBounds.Left/*gap.Left*/;
                    //minmax.ptMaxPosition.Y = _windowMaxBounds.Top/*0*/;// WindowBorderThickness.Top - WindowBorderThickness.Bottom; // gap.Bottom;
                    //minmax.ptMaxTrackSize.Width = _windowMaxBounds.Width;
                    //minmax.ptMaxTrackSize.Height = _windowMaxBounds.Height; // SystemInformation.WorkingArea.Height + (this.Size - this.ClientSize).Height;

                    //if (!AeroEnable)
                    //{
                    //    minmax.ptMaxPosition.X = 0;
                    //    minmax.ptMaxTrackSize.Width = SystemInformation.WorkingArea.Width;
                    //}
                }

                if (this.Parent != null)
                {
                    Rectangle rect = this.Parent.ClientRectangle;
                    minmax.ptMaxTrackSize = new Size(rect.Width, rect.Height);
                    minmax.ptMaxPosition = new Point(rect.X, rect.Y);
                    minmax.ptMaxSize = new Size(rect.Width, rect.Height);
                }
            }

            if (MinimumSize != Size.Empty)
            {
                minmax.ptMinTrackSize = MinimumSize;
            }
            else
            {
                minmax.ptMinTrackSize = SystemInformation.MinWindowTrackSize;
            }

            Marshal.StructureToPtr(minmax, m.LParam, false);
            m.Result = IntPtr.Zero;
        }

        protected virtual void SetWindowMinMaxBoundsCore(Size ptMaxSize, Point ptMaxPosition, Size ptMinTrackSize, Size ptMaxTrackSize)
        {
            _windowMinMaxInfo.ptMaxSize = ptMaxSize;
            _windowMinMaxInfo.ptMaxPosition = ptMaxPosition;
            _windowMinMaxInfo.ptMinTrackSize = ptMinTrackSize;
            _windowMinMaxInfo.ptMaxTrackSize = ptMaxTrackSize;
        }

        protected override void WM_NCHITTEST(ref Message m)
        {
            base.WM_NCHITTEST(ref m);

            WndProcNCHitTest(ref m);
        }

        protected virtual void WndProcNCHitTest(ref Message m)
        {
            Point point = new Point(Win32.Util.LOWORD(m.LParam), Win32.Util.HIWORD(m.LParam));
            point = PointToClient(point);

            if (PrevisionWindowState != FormWindowState.Normal)
            {
                if (PrevisionWindowState == FormWindowState.Maximized)
                {
                    if (point.Y < CaptionHeight)
                    {
                        m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTCAPTION);
                    }
                }

                return;
            }

            var thickness = WindowNCBorderThickness;
            var extendGap = WindowExtendClientAreaIntoFrame;
            var borderSize = WindowBorderSize;

            var hitEdge = new Padding(
                extendGap.Left,
                Math.Max(extendGap.Top - SystemInformation.CaptionHeight, thickness.Bottom),
                WindowRectangle.Width - thickness.Horizontal + extendGap.Left - borderSize.Width,
                WindowRectangle.Height - thickness.Vertical + extendGap.Top - borderSize.Height);

            #region Test
            //using (var g = Graphics.FromHwnd(m.HWnd))
            //{
            //    //g.Clear(BackColor);
            //    // left
            //    g.DrawLine(Pens.Red, new Point(hitEdge.Left, hitEdge.Top), new Point(hitEdge.Left, hitEdge.Bottom));

            //    // top
            //    g.DrawLine(Pens.Yellow, new Point(hitEdge.Left, hitEdge.Top), new Point(hitEdge.Right, hitEdge.Top));

            //    // right
            //    g.DrawLine(Pens.Red, new Point(hitEdge.Right, hitEdge.Top), new Point(hitEdge.Right, hitEdge.Bottom));

            //    // bottom
            //    g.DrawLine(Pens.DarkKhaki, new Point(hitEdge.Left, hitEdge.Bottom), new Point(hitEdge.Right, hitEdge.Bottom));

            //    //g.DrawRectangle(Pens.Red, new Rectangle(
            //    //    extendGap.Left,
            //    //    extendGap.Top - SystemInformation.CaptionHeight, 
            //    //    WindowRectangle.Width - thickness.Horizontal - borderSize.Width, 
            //    //    WindowRectangle.Height - thickness.Vertical + SystemInformation.CaptionHeight - borderSize.Height
            //    //    ));

            //    if (!lastPoint.Equals(Point.Empty))
            //    {
            //        g.DrawLine(Pens.PeachPuff, lastPoint, point);
            //    }

            //    g.DrawString($"{point}{WindowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Bottom)}", Font, Brushes.Black, WindowRectangle.Width/2, WindowRectangle.Height/2);

            //    g.Flush();
            //} 
            #endregion

            if (WindowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Top))
            {
                if (point.Y <= hitEdge.Top)
                {
                    if (point.X <= thickness.Left)
                    {
                        m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTTOPLEFT);
                        return;
                    }

                    if (point.X >= hitEdge.Right - thickness.Right)
                    {
                        m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTTOPRIGHT);
                        return;
                    }


                    m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTTOP);
                    return;
                }
            }

            if (WindowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Bottom))
            {
                if (point.Y >= hitEdge.Bottom)
                {
                    if (point.X <= thickness.Left)
                    {
                        m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTBOTTOMLEFT);
                        return;
                    }

                    if (point.X >= hitEdge.Right - thickness.Right)
                    {
                        m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTBOTTOMRIGHT);
                        return;
                    }

                    m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTBOTTOM);
                    return;
                }
            }

            if (WindowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Left))
            {
                if (point.X <= hitEdge.Left)
                {
                    m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTLEFT);
                    return;
                }
            }

            if (WindowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Right))
            {
                if (point.X >= hitEdge.Right)
                {
                    m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTRIGHT);
                    return;
                }
            }

            if(point.Y < CaptionHeight)
            {
                m.Result = new IntPtr((int)Win32.NCHITTEST_Result.HTCAPTION);
            }
        }

        protected override void WM_SIZE(ref Message m)
        {
            base.WM_SIZE(ref m);
            Invalidate();
        }

        private struct WindowMinMaxInfo
        {
            /// <summary>
            /// 窗口的最大化宽度（x 成员）和最大化高度（y 成员）。对于顶级窗口，此值基于主监视器的宽度
            /// </summary>
            public Size ptMaxSize;
            /// <summary>
            /// 最大化窗口左侧的位置（x 成员）和最大化窗口顶部的位置（y 成员）。对于顶级窗口，此值基于主监视器的位置
            /// </summary>
            public Point ptMaxPosition;
            /// <summary>
            /// 窗口的最小跟踪宽度（x 成员）和最小跟踪高度（y 成员）。可以通过编程方式从系统指标SM_CXMINTRACK和SM_CYMINTRACK中获取此值
            /// </summary>
            public Size ptMinTrackSize;
            /// <summary>
            /// 窗口的最大跟踪宽度（x 成员）和最大跟踪高度（y 成员）。此值基于虚拟屏幕的大小，可以通过编程方式从系统指标SM_CXMAXTRACK和SM_CYMAXTRACK
            /// </summary>
            public Size ptMaxTrackSize;
        }


    }
}
