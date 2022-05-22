using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class NoneTitleForm : Form
    {
        private Win32.RECT _noneTitleBarWindowAdjustGap = Win32.RECT.Empty;
        private Rectangle _restoredWindowBounds = Rectangle.Empty;
        private bool _aeroEnabled = false;
        private int _cornerRadius = 7;
        
        private MarginRectangle _marginRectangle = MarginRectangle.Empty;
        private AngleRectangle _angleRectangle = AngleRectangle.Empty;
        private Rectangle _virtualClientRectangle = Rectangle.Empty;
        private Rectangle _captionRectangle = Rectangle.Empty;

        [Description("启动全窗体标题栏效果"), Category("NoneTitle")]
        public bool EnableMove { get; set; }

        [Description("在系统内存过低时自动释放一次内存"), Category("NoneTitle")]
        public bool AutoCompacting { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("圆角半径，仅在Aero无效时使用"), Category("NoneTitle")]
        public int CornerRadius { 
            get=> _cornerRadius;
            internal set
            {
                if (DesignMode || !_aeroEnabled) return;

                value = Math.Min(Math.Max(value, 0), SystemInformation.CaptionHeight);
                if (IsHandleCreated && _cornerRadius != value)
                {
                    _cornerRadius = value;

                    Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, _cornerRadius);

                    OnCalculateResizeBorderThickness();

                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Description("Aero无效时，边框颜色"), Category("NoneTitle")]
        public Color GripDarkColor { get; set; }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("系统菜单"), Category("NoneTitle")]
        public SystemMenu? SystemMenu { get; private set; }

        public NoneTitleForm()
        {
            InitializeComponent();

            EnableMove = true;
            GripDarkColor = Color.FromArgb(184, 184, 184);
            base.BackColor = Color.FromArgb(240, 240, 240);
            base.DoubleBuffered = true;
            base.MinimizeBox = true;

            if (DesignMode) return;

            AdjustStyles();

            OnCheckAeroEnabled();
            
            OnGetNoneTitleBarWindowAdjustGap();

            OnCalculateResizeBorderThickness();

            

            //Controls.Add(new Button());
            //Controls.Add(new TextBox());
            ////Controls[0].Dock = DockStyle.Bottom;
            //Controls[1].Dock = DockStyle.Right;
            //Controls[1].Width = 200;
            //((TextBox)Controls[1]).Multiline = true;
            //Controls[0].BackColor = Color.Red;
            //((Button)Controls[0]).FlatStyle = FlatStyle.Flat;
            //Controls[0].Text = "测试";
            //Controls[0].Location = new Point(50, 50);
            //Controls[0].MouseDown += (sender, e) =>
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        MessageBox.Show($"SizeFromClientSize{SizeFromClientSize(ClientRectangle.Size)},Size{Size}, ClientSize{ClientSize},{_aeroEnabled}");
            //    }
            //};

        }

        protected override CreateParams CreateParams => UpdateCreateParams(base.CreateParams);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            
            if (DesignMode) return;
            
            _restoredWindowBounds = Bounds;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            SystemMenu = SystemMenuHelper.FromHandle(Handle);
            //SystemMenu?.AppendMenu(1056, "测试");
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (DesignMode)
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
            else {
                int top = y;
                int left = x;
                Size gap = SizeFromClientSize(Size) - Size;

                if (IsHandleCreated && WindowState == FormWindowState.Normal && _restoredWindowBounds != this.RestoreBounds)
                {
                    if (specified.HasFlag(BoundsSpecified.X))
                    {
                        left = Math.Max(_restoredWindowBounds.X, _noneTitleBarWindowAdjustGap.Left + _noneTitleBarWindowAdjustGap.Right + width - SystemInformation.WorkingArea.Width + gap.Width / 2);
                    }
                    if (specified.HasFlag(BoundsSpecified.Y))
                    {
                        top = Math.Min(Math.Max(_restoredWindowBounds.Y, 0), SystemInformation.WorkingArea.Height - _noneTitleBarWindowAdjustGap.Top - _noneTitleBarWindowAdjustGap.Bottom + gap.Height / 2);
                    }
                }

                base.SetBoundsCore(
                left, top,
                width - _noneTitleBarWindowAdjustGap.Left - _noneTitleBarWindowAdjustGap.Right,
                height - _noneTitleBarWindowAdjustGap.Top - _noneTitleBarWindowAdjustGap.Bottom,
                specified);
            }
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            if (DesignMode)
            {
                base.SetClientSizeCore(x, y);
            }
            else
            {
                base.SetClientSizeCore(
                x - _noneTitleBarWindowAdjustGap.Left,
                y - _noneTitleBarWindowAdjustGap.Top);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //if (DesignMode || !IsHandleCreated || WindowState == FormWindowState.Minimized) return;

            //if (!_aeroEnabled)
            //{
            //    if (WindowState == FormWindowState.Maximized)
            //    {
            //        Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, 0);
            //        base.Padding = new Padding(0);
            //    }
            //    else
            //    {
            //        Win32.Util.SetFormRoundRectRgn(this.Handle, this.Bounds, CornerRadius);
            //        base.Padding = new Padding(2);
            //    }
            //}

            //OnCalculateResizeBorderThickness();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (DesignMode) return;

            switch (m.Msg)
            {
                case Win32.WM_NCCALCSIZE:
                    OnWM_NCCALCSIZE(_noneTitleBarWindowAdjustGap, ref m);
                    break;
                case Win32.WM_GETMINMAXINFO:
                    OnWM_GETMINMAXINFO(_noneTitleBarWindowAdjustGap, ref m);
                    break;
                case Win32.WM_ACTIVATE:
                    //OnWM_ACTIVATE(ref m);
                    break;
                // 系统内存不足，通知释放不必要的内存
                case Win32.WM_COMPACTING:
                    OnWM_COMPACTING(ref m);
                    break;
                // 打印、绘制、背景等操作
                case Win32.WM_PRINTCLIENT:
                case Win32.WM_ERASEBKGND:
                case Win32.WM_PAINT:
                    OnWM_PAINT(ref m);
                    break;
                case Win32.WM_LBUTTONDBLCLK:
                    OnWmMouseDown(ref m, MouseButtons.Left, 2);
                    break;
                case Win32.WM_LBUTTONDOWN:
                    OnWmMouseDown(ref m, MouseButtons.Left, 1);
                    break;
                case Win32.WM_LBUTTONUP:
                    OnWmMouseUp(ref m, MouseButtons.Left, 1);
                    break;
                case Win32.WM_MBUTTONDBLCLK:
                    OnWmMouseDown(ref m, MouseButtons.Middle, 2);
                    break;
                case Win32.WM_MBUTTONDOWN:
                    OnWmMouseDown(ref m, MouseButtons.Middle, 1);
                    break;
                case Win32.WM_MBUTTONUP:
                    OnWmMouseUp(ref m, MouseButtons.Middle, 1);
                    break;
                case Win32.WM_RBUTTONDBLCLK:
                    OnWmMouseDown(ref m, MouseButtons.Right, 2);
                    break;
                case Win32.WM_RBUTTONDOWN:
                    OnWmMouseDown(ref m, MouseButtons.Right, 1);
                    break;
                case Win32.WM_RBUTTONUP:
                    OnWmMouseUp(ref m, MouseButtons.Right, 1);
                    break;
                case Win32.WM_XBUTTONDOWN:
                    OnWmMouseDown(ref m, Win32.Util.GetXButton(Win32.Util.HIWORD(m.WParam)), 1);
                    break;
                case Win32.WM_XBUTTONUP:
                    OnWmMouseUp(ref m, Win32.Util.GetXButton(Win32.Util.HIWORD(m.WParam)), 1);
                    break;
                case Win32.WM_XBUTTONDBLCLK:
                    OnWmMouseDown(ref m, Win32.Util.GetXButton(Win32.Util.HIWORD(m.WParam)), 2);
                    break;
                case Win32.WM_MOUSEWHEEL:
                    OnWmMouseWheel(ref m);
                    break;
                case Win32.WM_MOUSEMOVE:
                    OnWmMouseMove(ref m);
                    break;
                case Win32.WM_MOUSEHOVER:
                    OnWmMouseHover(ref m);
                    break;
                case Win32.WM_MOUSELEAVE:
                    OnWmMouseLeave(ref m);
                    break;
                case Win32.WM_IME_STARTCOMPOSITION:
                    OnWmImeStartComposition(ref m);
                    break;
                case Win32.WM_IME_ENDCOMPOSITION:
                    OnWmImeEndComposition(ref m);
                    break;
            }
            
            if (m.Msg == Win32.Util.WM_MOUSEENTER)
            {
                OnWmMouseEnter(ref m);
            }
        }
        
        protected override void DefWndProc(ref Message m)
        {
            if (!DesignMode)
            {
                switch (m.Msg)
                {
                    case Win32.WM_HOTKEY: // 热键
                        break;
                    case Win32.WM_NCHITTEST: // 窗口边框拖动
                        break;
                    case Win32.WM_SYSCHAR: // Alt+F4
                        break;
                    case Win32.WM_SHOWWINDOW: // 窗口显示
                        break;
                    case Win32.WM_CREATE: // 窗口创建
                        OnWM_CREATE(ref m);
                        break;
                    case Win32.WM_WINDOWPOSCHANGING: // 窗口位置将改变
                        //FormWindowState currentWindowState = Win32.Util.GetWindowState(this);
                        break;
                    case Win32.WM_WINDOWPOSCHANGED: // 窗口位置已改变
                        break;
                    case Win32.WM_ENTERSIZEMOVE: // 开始移动窗体
                        OnWM_ENTERSIZEMOVE(ref m);
                        break;
                    case Win32.WM_EXITSIZEMOVE: // 结束移动窗体
                        OnWM_EXITSIZEMOVE(ref m);
                        break;
                    case Win32.WM_SIZE:
                        OnWM_SIZE(ref m);
                        break;
                    case Win32.WM_SYSCOMMAND:
                        OnWM_SYSCOMMAND(ref m);
                        break;
                }
            }

            base.DefWndProc(ref m);

            if (DesignMode) return;

            switch (m.Msg)
            {
                case Win32.WM_NCHITTEST:
                    OnWM_NCHITTEST(ref m);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (DesignMode) return;

            //var g = e.Graphics;

            //g.FillRectangles(Brushes.Red, _marginRectangle.ToArray());

            //g.FillRectangles(Brushes.Blue, _angleRectangle.ToArray());

            //g.DrawRectangle(Pens.Brown, _virtualClientRectangle);
            //ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
            //                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset,
            //                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset,
            //                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset,
            //                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset);

            //ControlPaint.DrawLockedFrame(e.Graphics, ClientRectangle,false);

            // win32绘制圆角窗体边框
            
            //DrawRoundRect(e.Graphics);
        }

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        protected override void OnMenuStart(EventArgs e)
        {
            base.OnMenuStart(e);
        }

    }

    
}
