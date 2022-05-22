using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

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
                param.Style |= Win32.WS_SYSMENU;
                //}
                
                //if (MaximizeBox)
                //{
                    param.Style |= Win32.WS_MAXIMIZEBOX;
                //}
                //if (MinimizeBox)
                //{
                    param.Style |= Win32.WS_MINIMIZEBOX;
                //}

                //param.Style |= Win32.WS_SIZEBOX;
                //param.Style |= BitConverter.ToInt32(BitConverter.GetBytes(Win32.WS_POPUP));

                //param.ExStyle |= Win32.WS_EX_COMPOSITED;

                //param.ClassStyle &= ~Win32.CS_NOCLOSE;
                param.ClassStyle |= Win32.CS_VREDRAW;
                param.ClassStyle |= Win32.CS_DBLCLKS;

                // 防止因窗体控件太多出现闪烁，原理主窗口不绘制子窗口背景，由子窗口自己绘制
                // 一个按钮也是窗口
                param.ExStyle |= Win32.WS_CLIPCHILDREN;

            }

            return param;
        }

        /// <summary>
        /// 重新计算窗口位置
        /// </summary>
        protected virtual void OnWM_NCCALCSIZE(Win32.RECT gap, ref Message m)
        {
            if (gap.IsEmpty) return;
            
            // 客户区向上提6px，覆盖控制区
            if (m.WParam.ToInt32() == 1)
            {
                Win32.NCCALCSIZE_PARAMS nccsp = (Win32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam,
                    typeof(Win32.NCCALCSIZE_PARAMS));

                nccsp.rcNewWindow.Top -= gap.Top;

                if (!_aeroEnabled)
                {
                    nccsp.rcNewWindow.Left -= gap.Left;
                    nccsp.rcNewWindow.Right += gap.Right;
                    nccsp.rcNewWindow.Bottom += gap.Bottom;
                }

                Marshal.StructureToPtr(nccsp, m.LParam, false);
            }
        }

        /// <summary>
        /// 当窗口的大小或位置即将更改时，发送到窗口。 应用程序可以使用此消息替代窗口的默认最大化大小和位置，或者默认的最小或最大跟踪大小
        /// </summary>
        /// <param name="m"></param>
        protected virtual void OnWM_GETMINMAXINFO(Win32.RECT gap, ref Message m)
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
        protected virtual void OnWM_CREATE(ref Message m)
        {
            
        }

        /// <summary>
        /// 窗体激活
        /// </summary>
        protected virtual void OnWM_ACTIVATE(ref Message m)
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
        /// 尺寸更改与窗口状态
        /// </summary>
        protected virtual void OnWM_SIZE(ref Message m)
        {
            //switch ((int)m.WParam)
            //{
            //    // 窗口最大化
            //    case Win32.WMSIZE_WParam.SIZE_MAXIMIZED:
            //        //break;
            //    // 窗口最小化
            //    case Win32.WMSIZE_WParam.SIZE_MINIMIZED:
            //        break;
            //    // 窗口恢复
            //    case Win32.WMSIZE_WParam.SIZE_RESTORED:
            //        //if (WindowState == FormWindowState.Normal)
            //        //{
            //        //    _restoredWindowBounds = Bounds;
            //        //}
            //        break;
            //}

            if (DesignMode || !IsHandleCreated || WindowState == FormWindowState.Minimized) return;

            if (!_aeroEnabled)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, 0);
                    base.Padding = new Padding(0);
                }
                else
                {
                    Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, CornerRadius);
                    base.Padding = new Padding(2);
                }
            }

            OnCalculateResizeBorderThickness();
        }

        /// <summary>
        /// 窗口大小状态开始改变
        /// </summary>
        protected virtual void OnWM_ENTERSIZEMOVE(ref Message m)
        {
            if (!DesignMode && WindowState == FormWindowState.Normal)
            {
                _restoredWindowBounds = Bounds;
            }
        }

        /// <summary>
        /// 窗口大小状态改变结束
        /// </summary>
        protected virtual void OnWM_EXITSIZEMOVE(ref Message m)
        {
            if (!DesignMode && WindowState == FormWindowState.Normal)
            {
                _restoredWindowBounds = Bounds;
            }
        }

        /// <summary>
        /// 指定鼠标划过位置区域
        /// </summary>
        protected virtual void OnWM_NCHITTEST(ref Message m)
        {
            // 获取一个值，该值指示窗体是否可以不受限制地使用所有窗口和用户输入事件
            if (this.IsRestrictedWindow)
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTCLIENT;
                return;
            }

            Point pt = this.PointToClient(new Point(Win32.Util.LOWORD(m.LParam), Win32.Util.HIWORD(m.LParam)));

            // 点击标题栏
            if (_captionRectangle.Contains(pt))
            {
                if(Control.MouseButtons != MouseButtons.Right)
                {
                    m.Result = (IntPtr)Win32.NCHITTEST_Return.HTCAPTION;
                }
                
                return;
            }

            // 点击客户区
            if (this._virtualClientRectangle.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTCLIENT;
                return;
            }

            if (WindowState != FormWindowState.Normal) return;

            // 上拉框
            if (this._marginRectangle.TopRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTTOP;
                return;
            }

            // 下拉框
            if (this._marginRectangle.BottomRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTBOTTOM;
                return;
            }

            // 左拉框
            if (this._marginRectangle.LeftRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTLEFT;
                return;
            }

            // 右拉框
            if (this._marginRectangle.RightRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTRIGHT;
                return;
            }

            // 左上角
            if (this._angleRectangle.TopLeftRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTTOPLEFT;
                return;
            }

            // 右上角
            if (this._angleRectangle.TopRightRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTTOPRIGHT;
                return;
            }

            // 左下角
            if (this._angleRectangle.BottomLeftRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTBOTTOMLEFT;
                return;
            }

            // 右下角
            if (this._angleRectangle.BottomRightRect.Contains(pt))
            {
                m.Result = (IntPtr)Win32.NCHITTEST_Return.HTBOTTOMRIGHT;
                return;
            }
        }

        /// <summary>
        /// 当用户从 “窗口 ”菜单中选择命令时，窗口会收到此消息， (以前称为系统或控件菜单) ，或者当用户选择最大化按钮、最小化按钮、还原按钮或关闭按钮时
        /// </summary>
        protected virtual void OnWM_SYSCOMMAND(ref Message m)
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
        protected virtual void OnWM_COMPACTING(ref Message m)
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
        protected virtual void OnCheckAeroEnabled()
        {
            _aeroEnabled = false;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; 
                Win32.DwmIsCompositionEnabled(ref enabled);
                _aeroEnabled = (enabled == 1) ? true : false;
            }
        }

        /// <summary>
        /// 获取要调整的边界
        /// </summary>
        protected virtual void OnGetNoneTitleBarWindowAdjustGap()
        {
            //var size = this.Size - this.ClientSize;
            var size = SizeFromClientSize(ClientSize) - this.ClientSize;

            int v_gap = size.Height / 2 - (_aeroEnabled ? SystemInformation.BorderSize.Width : 0) + (size.Height % 2 == 0 ? 0 : 1);
            int h_gap = size.Width / 2 - (_aeroEnabled ? SystemInformation.BorderSize.Height : 0) + (size.Width % 2 == 0 ? 0 : 1);
            int taskbar_height = SystemInformation.VirtualScreen.Height - SystemInformation.WorkingArea.Height; 

            _noneTitleBarWindowAdjustGap.Top = v_gap;

            if (_aeroEnabled) return;

            _noneTitleBarWindowAdjustGap.Left = h_gap;
            _noneTitleBarWindowAdjustGap.Right = h_gap;
            _noneTitleBarWindowAdjustGap.Bottom = v_gap;
        }

        /// <summary>
        /// 计算窗口调整大小边界的厚度
        /// </summary>
        protected virtual void OnCalculateResizeBorderThickness()
        {
            _marginRectangle = MarginRectangle.Empty;
            _angleRectangle = AngleRectangle.Empty;
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
                _angleRectangle.TopLeftRect = new Rectangle(
                    0, 0, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);
                
                _angleRectangle.TopRightRect = new Rectangle(
                    Width - SystemInformation.CaptionHeight, 
                    0, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);
                
                _angleRectangle.BottomLeftRect = new Rectangle(
                    0, 
                    Height - SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight, 
                    SystemInformation.CaptionHeight);
                
                _angleRectangle.BottomRightRect = new Rectangle(
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
                _angleRectangle.TopLeftRect = new Rectangle(
                    0, 0,
                    SystemInformation.CaptionHeight,
                    _noneTitleBarWindowAdjustGap.Top);
                
                _angleRectangle.TopRightRect = new Rectangle(
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
        protected virtual void OnWM_PAINT(ref Message m)
        {
            if (!IsHandleCreated || DesignMode || _aeroEnabled || this.WindowState != FormWindowState.Normal)
                return;
            var g = Graphics.FromHwnd(this.Handle);
            Win32.Util.DrawRoundRect(g, this.ClientSize, CornerRadius, BackColor, GripDarkColor);
            g?.Dispose();
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected virtual void OnWmMouseDown(ref Message m, MouseButtons button, int clicks)
        {
            
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        protected virtual void OnWmMouseUp(ref Message m, MouseButtons button, int clicks)
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
        protected virtual void OnWmMouseWheel(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected virtual void OnWmMouseMove(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标悬浮
        /// </summary>
        protected virtual void OnWmMouseHover(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标离开
        /// </summary>
        protected virtual void OnWmMouseLeave(ref Message m)
        {

        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        protected virtual void OnWmMouseEnter(ref Message m)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnWmImeStartComposition(ref Message m)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnWmImeEndComposition(ref Message m)
        {
            
        }

    }
}
