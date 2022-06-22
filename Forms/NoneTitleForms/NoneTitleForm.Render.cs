using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.Win32;

namespace System.Windows.Forms
{
    public partial class NoneTitleForm
    {
        /// <summary>
        /// 调整样式
        /// </summary>
        protected virtual void AdjustStyles()
        {
            if (!DesignMode)
            {
                // 使用双缓存
                this.SetStyle(
                ControlStyles.DoubleBuffer |                                     // 双缓冲
               ControlStyles.UserPaint |                                          // 自绘
               ControlStyles.AllPaintingInWmPaint, true);                 // 禁止擦除背景

                this.SetStyle(
                    ControlStyles.OptimizedDoubleBuffer |                      // 双缓冲
                    ControlStyles.ResizeRedraw /*|                                   // 自绘
                    ControlStyles.SupportsTransparentBackColor*/, true);   // 透明边界

                //this.SetStyle(
                //    ControlStyles.EnableNotifyMessage |
                //    ControlStyles.StandardDoubleClick |
                //    ControlStyles.Selectable |
                //    ControlStyles.StandardClick |
                //    ControlStyles.ContainerControl, true);

                UpdateStyles();
            }
        }

        /// <summary>
        /// 更新窗体信息
        /// </summary>
        /// <param name="param"></param>
        protected virtual CreateParams UpdateCreateParams(CreateParams param)
        {
            if (!DesignMode)
            {
                param.Style &= ~Win32.WS_CAPTION;
                //param.Style &= ~Win32.WS_SYSMENU;
                //param.Style &= ~Win32.WS_MINIMIZEBOX;
                //param.Style &= ~Win32.WS_MAXIMIZEBOX;
                //param.ExStyle &= ~Win32.WS_EX_WINDOWEDGE;

                if (_aeroEnabled)
                {
                    param.Style |= Win32.WS_BORDER;
                    param.Style |= Win32.WS_CLIPSIBLINGS;
                }
                else
                {
                    param.Style &= ~Win32.WS_BORDER;
                    param.Style &= ~Win32.CS_DROPSHADOW;
                }

                //if (ControlBox)
                //{
                param.Style |= Win32.WS_SYSMENU; // 明确要求标题栏支持通过 Win + ← / Win + → 捕捉
                //}

                //if (MaximizeBox)
                //{
                param.Style |= Win32.WS_MAXIMIZEBOX;  // 添加最大化按钮，支持鼠标拖动最大化 到屏幕顶部
                //}
                //if (MinimizeBox)
                //{
                param.Style |= Win32.WS_MINIMIZEBOX; // 添加最小化按钮，支持点击任务栏图标最小化
                //}

                param.Style |= Win32.WS_SIZEBOX;  // 标准可调整大小窗口所需
                //param.Style |= BitConverter.ToInt32(BitConverter.GetBytes(Win32.WS_POPUP));
                param.Style |= Win32.WS_VISIBLE; // 使窗口在创建后可见（不重要）

                //param.ExStyle |= Win32.WS_EX_COMPOSITED;
                // 防止因窗体控件太多出现闪烁，原理主窗口不绘制子窗口背景，由子窗口自己绘制
                // 一个按钮也是窗口
                param.ExStyle |= Win32.WS_CLIPCHILDREN;
                param.ExStyle |= Win32.WS_EX_APPWINDOW;

                // 不激活窗口
                //param.ExStyle |= Win32.WS_EX_NOACTIVATE;

                //param.ClassStyle &= ~Win32.CS_NOCLOSE;
                param.ClassStyle |= Win32.CS_VREDRAW;
                param.ClassStyle |= Win32.CS_DBLCLKS;

            }

            return param;
        }

        /// <summary>
        /// 重新计算窗口位置
        /// </summary>
        protected virtual void WM_NCCALCSIZE(Win32.RECT gap, ref Message m)
        {
            if (gap.IsEmpty) return;

            // 客户区向上提6px，覆盖控制区
            if (m.WParam.ToInt32() == 1)
            {
                Win32.NCCALCSIZE_PARAMS nccsp;
                
                //nccsp = (Win32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam,typeof(Win32.NCCALCSIZE_PARAMS));
                nccsp = (Win32.NCCALCSIZE_PARAMS)m.GetLParam(typeof(Win32.NCCALCSIZE_PARAMS));


                var correntgap = (this.GetWindowState() == FormWindowState.Maximized && !OSFeature.Feature.OnWin11() ? 1 : 0);

                nccsp.rcNewWindow.Top -= gap.Top +  correntgap;

                //if (!_aeroEnabled)
                //{
                nccsp.rcNewWindow.Left -= gap.Left;
                nccsp.rcNewWindow.Right += gap.Right;
                nccsp.rcNewWindow.Bottom += gap.Bottom;
                //}

                Marshal.StructureToPtr(nccsp, m.LParam, false);
                
                m.Result = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 当窗口的大小或位置即将更改时，发送到窗口。 应用程序可以使用此消息替代窗口的默认最大化大小和位置，或者默认的最小或最大跟踪大小
        /// </summary>
        /// <param name="m"></param>
        protected virtual void WM_GETMINMAXINFO(Win32.RECT gap, ref Message m)
        {
            if (this.IsRestrictedWindow) return;

            Win32.MINMAXINFO minmax = (Win32.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(Win32.MINMAXINFO));

            if (MaximumSize != Size.Empty)
            {
                minmax.ptMaxTrackSize = MaximumSize;
            }
            else
            {
                if (this.Parent == null)
                {
                    minmax.ptMaxPosition.X += gap.Left;
                    minmax.ptMaxPosition.Y += gap.Top;
                    minmax.ptMaxTrackSize.Height = SystemInformation.WorkingArea.Height + (this.Size - this.ClientSize).Height;

                    if (!_aeroEnabled)
                    {
                        minmax.ptMaxPosition.X = 0;
                        minmax.ptMaxTrackSize.Width = SystemInformation.WorkingArea.Width;
                    }
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

        /// <summary>
        /// 创建窗体
        /// </summary>
        protected virtual void WM_CREATE(ref Message m)
        {
            ReflushNC(m.HWnd);
        }

        /// <summary>
        /// 窗体激活
        /// </summary>
        protected virtual void WM_ACTIVATE(ref Message m)
        {
            // activation flag 
            int fActive = Win32.Util.LOWORD(m.WParam);   
            //// minimized flag
            //bool fMinimized = Convert.ToBoolean(Win32.Util.HIWORD(m.WParam));
            //// 上一个或下一个激活的窗口
            //IntPtr hwndPrevious = m.LParam;   
            switch (fActive)
            {
                // 通过鼠标单击激活了该窗口
                case Win32.WMACTIVATE_WParam.WA_CLICKACTIVE: 
                    //break;
                // 通过鼠标以外的工具（如键盘）激活了该窗口
                case Win32.WMACTIVATE_WParam.WA_ACTIVE: 
                    //break;
                // 取消该窗口的激活
                case Win32.WMACTIVATE_WParam.WA_INACTIVE: 
                    break;
            }

            
        }

        /// <summary>
        /// 尺寸正在改变
        /// </summary>
        protected virtual void WM_SIZING(ref Message m)
        {
            if (DesignMode) return;

        }

        /// <summary>
        /// 尺寸更改与窗口状态，发生在窗口大小改变前
        /// </summary>
        protected virtual void WM_SIZE(ref Message m)
        {
            if (DesignMode) return;
            
            switch ((int)m.WParam)
            {
                // 窗口最大化
                case Win32.WMSIZE_WParam.SIZE_MAXIMIZED:
                    OnStateChanged(this,EventArgs.Empty);
                    break;
                // 窗口最小化
                case Win32.WMSIZE_WParam.SIZE_MINIMIZED:
                    OnStateChanged(this, EventArgs.Empty);
                    break;
                // 窗口恢复
                case Win32.WMSIZE_WParam.SIZE_RESTORED:
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    _restoredWindowBounds = Bounds;
                    //}
                    OnStateChanged(this, EventArgs.Empty);
                    break;
            }
            
            if ( !IsHandleCreated || /*WindowState*/ this.GetWindowState() == FormWindowState.Minimized) return;

            if (!_aeroEnabled)
            {
                int nWidth = Win32.Util.LOWORD(m.LParam);
                int nHeight = Win32.Util.HIWORD(m.LParam);
                
                if (WindowState == FormWindowState.Maximized)
                {
                    //Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, 0);
                    //this.CreateRoundRectRegion(0);
                    this.Region = null;
                    base.Padding = new Padding(0);
                }
                else
                {
                    //Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, CornerRadius);
                    this.CreateRoundRectRegion(CornerRadius, nWidth, nHeight);
                    base.Padding = new Padding(2);
                }
            }

            //OnCalculateResizeBorderThickness();
        }

        /// <summary>
        /// 窗口大小状态开始改变
        /// </summary>
        protected virtual void WM_ENTERSIZEMOVE(ref Message m)
        {
            if (!DesignMode && WindowState == FormWindowState.Normal)
            {
                _restoredWindowBounds = Bounds;
            }
        }

        /// <summary>
        /// 窗口大小状态改变结束
        /// </summary>
        protected virtual void WM_EXITSIZEMOVE(ref Message m)
        {
            if (!DesignMode && WindowState == FormWindowState.Normal)
            {
                _restoredWindowBounds = Bounds;
            }
        }

        /// <summary>
        /// 指定鼠标划过位置区域
        /// </summary>
        protected virtual void WM_NCHITTEST(ref Message m)
        {
            // 获取一个值，该值指示窗体是否可以不受限制地使用所有窗口和用户输入事件
            if (this.IsRestrictedWindow)
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTCLIENT;
                return;
            }

            Point pt = this.PointToClient(new Point(Win32.Util.LOWORD(m.LParam), Win32.Util.HIWORD(m.LParam)));

            // 点击标题栏
            if (_captionRectangle.Contains(pt))
            {
                if(Control.MouseButtons != MouseButtons.Right)
                {
                    m.Result = (IntPtr)Win32.NCHITTEST_Result.HTCAPTION;
                }
                
                return;
            }

            // 点击客户区
            if (this._virtualClientRectangle.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTCLIENT;
                return;
            }

            if (WindowState != FormWindowState.Normal) return;

            // 上拉框
            if (this._marginRectangle.TopRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTTOP;
                return;
            }

            // 下拉框
            if (this._marginRectangle.BottomRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTBOTTOM;
                return;
            }

            // 左拉框
            if (this._marginRectangle.LeftRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTLEFT;
                return;
            }

            // 右拉框
            if (this._marginRectangle.RightRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTRIGHT;
                return;
            }

            // 左上角
            if (this._marginRectangle.TopLeftRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTTOPLEFT;
                return;
            }

            // 右上角
            if (this._marginRectangle.TopRightRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTTOPRIGHT;
                return;
            }

            // 左下角
            if (this._marginRectangle.BottomLeftRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTBOTTOMLEFT;
                return;
            }

            // 右下角
            if (this._marginRectangle.BottomRightRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Result.HTBOTTOMRIGHT;
                return;
            }
        }


        protected virtual void WM_NCACTIVATE(ref Message m)
        {

            //var g = Graphics.FromHwnd(m.HWnd);

            //var toprect = this.ClientRectangle;
            //toprect.Height = SystemInformation.BorderSize.Height;
            //g.FillRectangle(Brushes.Yellow, toprect); // new SolidBrush(ControlPaint.ContrastControlDark)
            ////Win32.Util.DrawRoundRect(g, this.ClientSize, CornerRadius, BackColor, GripDarkColor);
            //g?.Dispose();

            //ReflushNC(m.HWnd);
        }

        protected virtual void ReflushNC(IntPtr hWnd)
        {
            if (this.IsHandleCreated || !DesignMode)
            {
                //AdjustAeroPadding();
                //OnGetNoneTitleBarWindowAdjustGap();
                Win32.Util.PostWMNCCALCSIZEMessage(hWnd);
            }
        }

        protected virtual void WM_NCPAINT(ref Message m)
        {
            //if (IsRestrictedWindow || !IsHandleCreated || DesignMode || this.GetWindowState() != FormWindowState.Normal) return;

            //if (Win32.GetForegroundWindow() == this.Handle)
            //{

            //}
            //else
            //{

            //}

            //if (_noneTitleBarWindowAdjustGap.Top > 0)
            //{
            //    Rectangle tr = new Rectangle(0, 0, Width, _noneTitleBarWindowAdjustGap.Top);
            //    Invalidate(tr);
            //}

            //if (_noneTitleBarWindowAdjustGap.Bottom > 0)
            //{
            //    Rectangle br = new Rectangle(0, Height - _noneTitleBarWindowAdjustGap.Bottom, Width, _noneTitleBarWindowAdjustGap.Bottom);
            //    Invalidate(br);
            //}

            //if (_noneTitleBarWindowAdjustGap.Left > 0)
            //{
            //    Rectangle lr = new Rectangle(0, 0, _noneTitleBarWindowAdjustGap.Left, Height);
            //    Invalidate(lr);
            //}

            //if (_noneTitleBarWindowAdjustGap.Right > 0)
            //{
            //    Rectangle rr = new Rectangle(Width - _noneTitleBarWindowAdjustGap.Right, 0, _noneTitleBarWindowAdjustGap.Right, Height);
            //    Invalidate(rr);
            //}

            //var v = 2;
            //Win32.DwmSetWindowAttribute(this.Handle, 2, ref v, 4);    // 去掉vista或win7特效
            //Win32.MARGINS margins = new Win32.MARGINS()
            //{
            //    leftWidth = 0,
            //    rightWidth = 0,
            //    bottomHeight = 0,
            //    topHeight = 1
            //};
            //Win32.DwmExtendFrameIntoClientArea(this.Handle, ref margins);

            //m.Result = new IntPtr(1);


            //IntPtr hDC = Win32.GetWindowDC(m.HWnd);

            ////把DC转换为.NET的Graphics就可以很方便地使用Framework提供的绘图功能了 

            //Graphics gs = Graphics.FromHdc(hDC);

            ////gs.FillRectangle(new LinearGradientBrush(Bounds, Color.Pink, Color.Purple, LinearGradientMode.BackwardDiagonal), Bounds);

            //gs.FillRectangle(new SolidBrush(Color.Red), new Rectangle((int)gs.VisibleClipBounds.X, (int)gs.VisibleClipBounds.Y, (int)gs.VisibleClipBounds.Width, (int)gs.VisibleClipBounds.Height));

            ////gs.DrawRectangle(new Pen(Color.Red, 1), new  Rectangle((int)gs.VisibleClipBounds.X, (int)gs.VisibleClipBounds.Y, (int)gs.VisibleClipBounds.Width, (int)gs.VisibleClipBounds.Height));

            ////StringFormat strFmt = new StringFormat();

            ////strFmt.Alignment = StringAlignment.Center;

            ////strFmt.LineAlignment = StringAlignment.Center;

            ////gs.DrawString("√ ", this.Font, Brushes.BlanchedAlmond, Bounds, strFmt);

            //gs.Dispose();

            ////释放GDI资源 

            //Win32.ReleaseDC(m.HWnd, hDC);


            //if (Win32.GetForegroundWindow() == Handle)
            //{
            //    Width = Width + 1;
            //    Width = Width - 1;
            //}

            

            //var g = Graphics.FromHwnd(m.HWnd);

            //var toprect = this.ClientRectangle;
            //toprect.Height = SystemInformation.BorderSize.Height;
            //g.FillRectangle(new SolidBrush(ControlPaint.ContrastControlDark), toprect);
            ////Win32.Util.DrawRoundRect(g, this.ClientSize, CornerRadius, BackColor, GripDarkColor);
            //g?.Dispose();


        }

        /// <summary>
        /// 当用户从 “窗口 ”菜单中选择命令时，窗口会收到此消息， (以前称为系统或控件菜单) ，或者当用户选择最大化按钮、最小化按钮、还原按钮或关闭按钮时
        /// </summary>
        protected virtual void WM_SYSCOMMAND(ref Message m)
        {
            // 最小化、最大化并恢复表单时，请保持表单大小。因为表单的大小考虑了标题栏和边框的大小
            if (m.Msg == Win32.WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                
                switch (wParam)
                {
                    case Win32.WMSYSCOMMAND_WParam.SC_MAXIMIZE: // 最大化前
                        if (WindowState != FormWindowState.Minimized && _restoredWindowBounds != RestoreBounds)
                        {
                            _restoredWindowBounds = Bounds;
                        }
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_MINIMIZE: // 最小化前
                        if (WindowState != FormWindowState.Maximized && _restoredWindowBounds != RestoreBounds)
                        {
                            _restoredWindowBounds = Bounds;
                        }
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_RESTORE: // 恢复前
                        //if (!_restoredWindowBounds.IsEmpty)
                        //{
                        //    Size = _restoredWindowBounds.Size;
                        //}
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_CLOSE: // 关闭窗口
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_NEXTWINDOW: // 移动到下一个窗口
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_PREVWINDOW: // 移动到上一个窗口
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_SIZE: // 调整窗口的大小
                    case Win32.WMSYSCOMMAND_WParam.SC_MOVE: // 移动窗口
                        //if (WindowState == FormWindowState.Normal)
                        //{
                        //    _restoredWindowBounds = Bounds;
                        //}
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_TASKLIST: // 激活"开始"菜单菜单
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_CONTEXTHELP:
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_KEYMENU:
                        break;
                    case Win32.WMSYSCOMMAND_WParam.SC_VSCROLL: // 垂直滚动
                    case Win32.WMSYSCOMMAND_WParam.SC_HSCROLL: // 水平滚动
                    default:
                        break;
                }

                //m.Result = (IntPtr)1;
            }
        }

        /// <summary>
        /// 内存过低时，自动释放
        /// </summary>
        protected virtual void WM_COMPACTING(ref Message m)
        {
            if (AutoCompacting)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 查询释放启用了Aero效果
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckAeroEnabled()
        {
            bool aeroEnabled = false;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; 
                Win32.DwmIsCompositionEnabled(ref enabled);
                aeroEnabled = (enabled == 1) ? true : false;
            }

            return aeroEnabled;
        }

        protected virtual void AdjustAeroPadding()
        {
            if (!_aeroEnabled)
            {
                Padding = new Padding(Math.Max(Padding.Left, 1), Math.Max(Padding.Top, 1), Math.Max(Padding.Right, 1), Math.Max(Padding.Bottom, 1));
            }
            else
            {
                // 排除win11以上
                if (!OSFeature.Feature.OnWin11())
                {
                    Padding = new Padding(Math.Max(Padding.Left, 0), Math.Max(Padding.Top, 1), Math.Max(Padding.Right, 0), Math.Max(Padding.Bottom, 0));
                }
            }
        }

        /// <summary>
        /// 获取要调整的边界
        /// </summary>
        protected virtual void OnGetNoneTitleBarWindowAdjustGap()
        {
            //var size = this.Size - this.ClientSize;
            var size = SizeFromClientSize(ClientSize) - this.ClientSize;

            int v_gap = size.Height / 2 /*- (_aeroEnabled ? SystemInformation.BorderSize.Width : 0)*/ + (size.Height % 2 == 0 ? 0 : 1);
            int h_gap = size.Width / 2 /*- (_aeroEnabled ? SystemInformation.BorderSize.Height : 0)*/ + (size.Width % 2 == 0 ? 0 : 1);
            //int taskbar_height = SystemInformation.VirtualScreen.Height - SystemInformation.WorkingArea.Height; 

            _noneTitleBarWindowAdjustGap.Top = v_gap;

            if (_aeroEnabled) return;

            _noneTitleBarWindowAdjustGap.Left = h_gap;
            _noneTitleBarWindowAdjustGap.Right = h_gap;
            _noneTitleBarWindowAdjustGap.Bottom = v_gap;
        }

        /// <summary>
        /// 可以获取间隙，但是需要win10以上才能使用
        /// </summary>
        protected virtual void OnGetSystemMetricsForDpi()
        {
            //int dpi = this.DeviceDpi;

            ////_systemMetricsForDpi.CaptionHeight = Win32.GetSystemMetricsForDpi((int)Win32.SM_Index.SM_CYCAPTION, dpi);

            //int frame_x = Win32.GetSystemMetricsForDpi((int)Win32.SM_Index.SM_CXFRAME, dpi);
            //int frame_y = Win32.GetSystemMetricsForDpi((int)Win32.SM_Index.SM_CYFRAME, dpi);
            //int padding = Win32.GetSystemMetricsForDpi((int)Win32.SM_Index.SM_CXPADDEDBORDER, dpi);


        }

        /// <summary>
        /// 计算窗口调整大小边界的厚度
        /// </summary>
        protected virtual void OnCalculateResizeBorderThickness()
        {
            _marginRectangle = MarginRectangle.Empty;
            _virtualClientRectangle = this.ClientRectangle;
            _captionRectangle = Rectangle.Empty;

            if (EnableMove)
            {
                _captionRectangle.Width = this.ClientSize.Width;
                _captionRectangle.Height = SystemInformation.CaptionHeight;
            }

            if (this.WindowState == FormWindowState.Maximized)
                return;
            

            // 如果未检测到_aeroEnabled，每个边框都增加调整边框
            if (!_aeroEnabled)
            {
                // 边距
                _marginRectangle.LeftRect = new Rectangle(
                    0, 
                    SystemInformation.CaptionHeight,
                    Math.Max(_noneTitleBarWindowAdjustGap.Left, SystemInformation.HorizontalResizeBorderThickness),
                    Height - SystemInformation.CaptionHeight * 2);
                
                _marginRectangle.TopRect = new Rectangle(
                    SystemInformation.CaptionHeight,
                    0, 
                    Width - SystemInformation.CaptionHeight * 2,
                    Math.Max(_noneTitleBarWindowAdjustGap.Top, SystemInformation.VerticalResizeBorderThickness));
                
                _marginRectangle.RightRect = new Rectangle(
                    Width - Math.Max(_noneTitleBarWindowAdjustGap.Right, SystemInformation.HorizontalResizeBorderThickness),
                    SystemInformation.CaptionHeight,
                    Math.Max(_noneTitleBarWindowAdjustGap.Right, SystemInformation.HorizontalResizeBorderThickness), 
                    Height - SystemInformation.CaptionHeight * 2);
                
                _marginRectangle.BottomRect = new Rectangle(
                    SystemInformation.CaptionHeight, 
                    Height - Math.Max(_noneTitleBarWindowAdjustGap.Bottom, SystemInformation.VerticalResizeBorderThickness), 
                    Width - SystemInformation.CaptionHeight * 2,
                    Math.Max(_noneTitleBarWindowAdjustGap.Bottom, SystemInformation.VerticalResizeBorderThickness));

                // 角距
                _marginRectangle.TopLeftRect = new Rectangle(
                    0, 0, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);

                _marginRectangle.TopRightRect = new Rectangle(
                    Width - SystemInformation.CaptionHeight, 
                    0, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);

                _marginRectangle.BottomLeftRect = new Rectangle(
                    0, 
                    Height - SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);

                _marginRectangle.BottomRightRect = new Rectangle(
                    Width - SystemInformation.CaptionHeight, 
                    Height - SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);

                // 虚拟客户区
                _virtualClientRectangle = new Rectangle(
                    Math.Max(_noneTitleBarWindowAdjustGap.Left, SystemInformation.HorizontalResizeBorderThickness),
                    Math.Max(_noneTitleBarWindowAdjustGap.Top, SystemInformation.VerticalResizeBorderThickness),
                    Width - Math.Max(_noneTitleBarWindowAdjustGap.Left, SystemInformation.HorizontalResizeBorderThickness) - Math.Max(_noneTitleBarWindowAdjustGap.Right, SystemInformation.HorizontalResizeBorderThickness),
                    Height - Math.Max(_noneTitleBarWindowAdjustGap.Top, SystemInformation.VerticalResizeBorderThickness) - Math.Max(_noneTitleBarWindowAdjustGap.Bottom, SystemInformation.VerticalResizeBorderThickness));

                // 标题栏
                if (EnableMove)
                {
                    _captionRectangle = new Rectangle(
                        Math.Max(_noneTitleBarWindowAdjustGap.Left, SystemInformation.HorizontalResizeBorderThickness),
                        Math.Max(_noneTitleBarWindowAdjustGap.Top, SystemInformation.VerticalResizeBorderThickness),
                        Width - Math.Max(_noneTitleBarWindowAdjustGap.Left, SystemInformation.HorizontalResizeBorderThickness) - Math.Max(_noneTitleBarWindowAdjustGap.Right, SystemInformation.HorizontalResizeBorderThickness),
                        SystemInformation.CaptionHeight);
                }
            }
            else
            {
                // 边距
                _marginRectangle.TopRect = new Rectangle(
                    SystemInformation.CaptionHeight, 
                    0,
                    ClientSize.Width - SystemInformation.CaptionHeight * 2,
                    _noneTitleBarWindowAdjustGap.Top);

                // 角距
                _marginRectangle.TopLeftRect = new Rectangle(
                    0, 0,
                    SystemInformation.CaptionHeight,
                    _noneTitleBarWindowAdjustGap.Top);

                _marginRectangle.TopRightRect = new Rectangle(
                    ClientSize.Width - SystemInformation.CaptionHeight, 
                    0,
                    SystemInformation.CaptionHeight,
                    _noneTitleBarWindowAdjustGap.Top);

                // 虚拟客户区
                _virtualClientRectangle = new Rectangle(
                    CornerRadius,
                    _noneTitleBarWindowAdjustGap.Top,
                    ClientSize.Width - CornerRadius * 2,
                    ClientSize.Height - _noneTitleBarWindowAdjustGap.Top - CornerRadius);

                // 标题栏
                if (EnableMove)
                {
                    _captionRectangle.X = CornerRadius;
                    _captionRectangle.Y = _noneTitleBarWindowAdjustGap.Top;
                    _captionRectangle.Width = ClientSize.Width - CornerRadius * 2;
                    _captionRectangle.Height = SystemInformation.CaptionHeight;
                }
            }
        }

        /// <summary>
        /// 绘制边缘
        /// </summary>
        protected virtual void WM_PAINT(ref Message m)
        {
            //if (!IsHandleCreated || DesignMode || this.WindowState != FormWindowState.Normal)
            //    return;
            if (!IsHandleCreated || DesignMode || OSFeature.Feature.OnWin11() || this.WindowState != FormWindowState.Normal)
                return;

            //if (_aeroEnabled)
            //{
            //    var g = Graphics.FromHwnd(this.Handle);

            //    Win32.Util.DrawRoundRect(g, this.ClientSize, CornerRadius, BackColor, GripDarkColor);
            //    g?.Dispose();
            //}
            var g = Graphics.FromHwnd(m.HWnd);
            if (g == null) return;

            //g.CompositingQuality = CompositingQuality.HighQuality;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;


            var toprect = this.ClientRectangle;
            //var borderColor = _ncACTIVATE ? SystemColors.WindowFrame: SystemColors.ControlDark;

            if (_aeroEnabled /*&& !this.IsGreaterWin10()*/)
            {
                toprect.Height = SystemInformation.BorderSize.Height;
                g.FillRectangle(new SolidBrush(BorderColor), toprect);
            }
            
            if (!_aeroEnabled)
            {
                var path = DrawingHelper.CreateRoundRectanglePath(Bounds, CornerRadius);
                g.DrawPath(new Pen(BorderColor) { Alignment = PenAlignment.Center, DashCap = DashCap.Round }, path); // GripDarkColor
            }

            //Win32.Util.DrawRoundRect(g, this.ClientSize, CornerRadius, BackColor, GripDarkColor);
            g?.Dispose();
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected virtual void Wm_MouseDown(ref Message m, MouseButtons button, int clicks)
        {
            
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        protected virtual void Wm_MouseUp(ref Message m, MouseButtons button, int clicks)
        {
            if (!IsHandleCreated || DesignMode || button != MouseButtons.Right)
                return;
            
            var point = new Point(Win32.Util.LOWORD(m.LParam), Win32.Util.HIWORD(m.LParam));
            if (_captionRectangle.Contains(point))
            {
                //Win32.Util.ShowSystemMenu(this.Handle, PointToScreen(point));
                SystemMenu?.ShowSystemMenu(PointToScreen(point));
            }
        }

        /// <summary>
        /// 鼠标滚轮滚动
        /// </summary>
        protected virtual void Wm_MouseWheel(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected virtual void Wm_MouseMove(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标悬浮
        /// </summary>
        protected virtual void Wm_MouseHover(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标离开
        /// </summary>
        protected virtual void Wm_MouseLeave(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        protected virtual void Wm_MouseEnter(ref Message m)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        protected virtual void Wm_Ime_StartComposition(ref Message m)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void Wm_Ime_EndComposition(ref Message m)
        {
            
        }

        /// <summary>
        /// 窗体状态发生变化
        /// </summary>
        protected virtual void OnStateChanged(object sender, EventArgs e)
        {

        }


        protected virtual void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General || e.Category == UserPreferenceCategory.VisualStyle)
            {
                //  Windows Accent Color
                var themeColor = Win32.Util.GetAccentColor(); 
                var lightColor = ControlPaint.Light(themeColor);
                var darkColor = ControlPaint.Dark(themeColor);

                
            }

        }


        
    }
}
