using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class NoneTitleForm : Form
    {
        private Win32.RECT _noneTitleBarWindowAdjustGap = Win32.RECT.Empty;
        private Rectangle _restoredWindowBounds = Rectangle.Empty;
        private int _cornerRadius = 7;

        private MarginRectangle _marginRectangle = MarginRectangle.Empty;
        private Rectangle _virtualClientRectangle = Rectangle.Empty;
        private Rectangle _captionRectangle = Rectangle.Empty;
        private bool _manulChangSize = false;
        
        private bool _aeroEnabled = false;
        private bool _ncACTIVATE = false;
        private bool _colorPrevalence = false;
        private Color _colorizationColor = Color.Empty;

        private UserPreferenceChangedEventHandler? UserPreferenceChanged;

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

        public new Rectangle Bounds
        {
            get => base.Bounds;
            set { 
                _manulChangSize = true;
                base.Bounds = value;
                _manulChangSize = false;
            }
        }

        public new Size Size
        {
            get => base.Size;
            set { 
                _manulChangSize = true;
                base.Size = value;
                _manulChangSize = false;
            }
        }

        public new int Height
        {
            get => base.Height;
            set { 
                _manulChangSize = true;
                base.Height = value;
                _manulChangSize = false;
            }
        }

        public new int Width
        {
            get => base.Width;
            set { 
                _manulChangSize = true;
                base.Width = value;
                _manulChangSize = false;
            }
        }
        
        public new Point Location
        {
            get => base.Location;
            set
            {
                _manulChangSize = true;
                base.Location = value;
                _manulChangSize = false;
            }
        }

        public new int Left
        {
            get => base.Left;
            set
            {
                _manulChangSize = true;
                base.Left = value;
                _manulChangSize = false;
            }
        }

        public new int Top
        {
            get => base.Top;
            set
            {
                _manulChangSize = true;
                base.Top = value;
                _manulChangSize = false;
            }
        }

        public new Rectangle DesktopBounds
        {
            get => base.DesktopBounds;
            set
            {
                _manulChangSize = true;
                base.DesktopBounds = value;
                _manulChangSize = false;
            }
        }

        public new Point DesktopLocation
        {
            get => base.DesktopLocation;
            set
            {
                _manulChangSize = true;
                base.DesktopLocation = value;
                _manulChangSize = false;
            }
        }

        public new Size ClientSize
        {
            get => base.ClientSize;
            set
            {
                _manulChangSize = true;
                base.ClientSize = value;
                _manulChangSize = false;
            }
        }

        public Color BorderColor
        {
            get
            {
                var borderColor = _ncACTIVATE ? SystemColors.WindowFrame : SystemColors.ControlDark;

                if (_aeroEnabled && _colorPrevalence && _ncACTIVATE)
                {
                    borderColor = _colorizationColor;
                }

                return borderColor;
            }
        }

        public bool Is64BitProcess => Environment.Is64BitProcess;
        public bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;

        public bool Network => SystemInformation.Network;

        public NoneTitleForm()
        {
            InitializeComponent();

            EnableMove = true;
            GripDarkColor = Color.FromArgb(184, 184, 184);
            base.BackColor = Color.FromArgb(240, 240, 240);
            base.DoubleBuffered = true;
            base.MinimizeBox = true;
            base.ResizeRedraw = true;

            if (DesignMode) return;

            AdjustStyles();

            _aeroEnabled = CheckAeroEnabled();

            OnGetSystemMetricsForDpi();

            OnGetNoneTitleBarWindowAdjustGap();

            OnCalculateResizeBorderThickness();

            AdjustAeroPadding();

            UserPreferenceChanged = new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
            SystemEvents.UserPreferenceChanged += UserPreferenceChanged;

            
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (DesignMode || _manulChangSize)
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
            else {
                int top = y, left = x, w = width, h = height;

                Size gap = SizeFromClientSize(ClientSize) - ClientSize;

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
            if (DesignMode /*|| _manulChangSize*/)
            {
                base.SetClientSizeCore(x, y);
            }
            else
            {
                _manulChangSize = false;
                //base.SetClientSizeCore(
                //x - _noneTitleBarWindowAdjustGap.Left,
                //y - _noneTitleBarWindowAdjustGap.Top);
                base.SetClientSizeCore(x, y);
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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.Clear(this.BackColor); 
        }
        
        public new void SetDesktopLocation(int x, int y)
        {
            base.SetDesktopLocation(x, y);
        }

        public new void SetDesktopBounds(int x, int y, int width, int height)
        {
            base.SetDesktopBounds(x, y, width, height);
        }

        protected new void UpdateBounds()
        {
            base.UpdateBounds();
        }

        protected new void UpdateBounds(int x, int y, int width, int height)
        {
            //_manulChangSize = true;
            base.UpdateBounds(x, y, width, height);
            //_manulChangSize = false;
        }

        /// <summary>
        /// all size change
        /// </summary>
        protected new void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
        {
            _manulChangSize = true;
            base.UpdateBounds(x, y, width, height, clientWidth, clientHeight);
            _manulChangSize = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.WM_NCACTIVATE)
            {
                if (m.WParam == IntPtr.Zero /*&& this.GetWindowState() != FormWindowState.Maximized*/) // 窗口失去焦点
                {
                    _ncACTIVATE = false;
                    //if (_aeroEnabled && !this.IsGreaterWin10())
                    //{
                    //    WM_NCACTIVATE(ref m);
                    //    m.Result = new IntPtr(1);
                    //    return;
                    //}
                }
                else // 窗口获得焦点
                {
                    _ncACTIVATE = true;
                    //m.Result = new IntPtr(0);
                }
            }

            base.WndProc(ref m);

            if (DesignMode || IsDisposed) return;

            switch (m.Msg)
            {
                case Win32.WM_NCCALCSIZE: //计算窗体尺寸时
                    WM_NCCALCSIZE(_noneTitleBarWindowAdjustGap, ref m);
                    break;
                case Win32.WM_GETMINMAXINFO:
                    WM_GETMINMAXINFO(_noneTitleBarWindowAdjustGap, ref m);
                    break;
                case Win32.WM_NCACTIVATE: // 非客户区有焦点和失去焦点时
                    WM_NCACTIVATE( ref m);
                    break;
                case Win32.WM_NCPAINT: //绘非客户区时
                    WM_NCPAINT(ref m);
                    break;
                case Win32.WM_NCHITTEST: // 窗口边框拖动
                    break;
                case Win32.WM_NCMOUSEMOVE:
                    break;
                case Win32.WM_NCLBUTTONDOWN:
                case Win32.WM_NCRBUTTONDOWN:
                case Win32.WM_NCMBUTTONDOWN:
                case Win32.WM_NCXBUTTONDOWN:
                    //WM_NcButtonDown(ref m);
                    break;
                case Win32.WM_NCLBUTTONUP:
                    break;
                case Win32.WM_NCDESTROY:
                    break;
                case Win32.WM_CANCELMODE:
                    break;
                case Win32.WM_ACTIVATE:
                    //WM_NCPAINT(ref m);
                    WM_ACTIVATE(ref m);
                    break;
                // 系统内存不足，通知释放不必要的内存
                case Win32.WM_COMPACTING:
                    WM_COMPACTING(ref m);
                    break;
                // 打印、绘制、背景等操作
                case Win32.WM_PRINTCLIENT:
                case Win32.WM_ERASEBKGND:
                case Win32.WM_PAINT:
                    WM_PAINT(ref m);
                    //WM_NCPAINT(ref m);
                    break;
                case Win32.WM_LBUTTONDBLCLK:
                    Wm_MouseDown(ref m, MouseButtons.Left, 2);
                    break;
                case Win32.WM_LBUTTONDOWN:
                    Wm_MouseDown(ref m, MouseButtons.Left, 1);
                    break;
                case Win32.WM_LBUTTONUP:
                    Wm_MouseUp(ref m, MouseButtons.Left, 1);
                    break;
                case Win32.WM_MBUTTONDBLCLK:
                    Wm_MouseDown(ref m, MouseButtons.Middle, 2);
                    break;
                case Win32.WM_MBUTTONDOWN:
                    Wm_MouseDown(ref m, MouseButtons.Middle, 1);
                    break;
                case Win32.WM_MBUTTONUP:
                    Wm_MouseUp(ref m, MouseButtons.Middle, 1);
                    break;
                case Win32.WM_RBUTTONDBLCLK:
                    Wm_MouseDown(ref m, MouseButtons.Right, 2);
                    break;
                case Win32.WM_RBUTTONDOWN:
                    Wm_MouseDown(ref m, MouseButtons.Right, 1);
                    break;
                case Win32.WM_RBUTTONUP:
                    Wm_MouseUp(ref m, MouseButtons.Right, 1);
                    break;
                case Win32.WM_XBUTTONDOWN:
                    Wm_MouseDown(ref m, Win32.Util.GetXButton(Win32.Util.HIWORD(m.WParam)), 1);
                    break;
                case Win32.WM_XBUTTONUP:
                    Wm_MouseUp(ref m, Win32.Util.GetXButton(Win32.Util.HIWORD(m.WParam)), 1);
                    break;
                case Win32.WM_XBUTTONDBLCLK:
                    Wm_MouseDown(ref m, Win32.Util.GetXButton(Win32.Util.HIWORD(m.WParam)), 2);
                    break;
                case Win32.WM_MOUSEWHEEL:
                    Wm_MouseWheel(ref m);
                    break;
                case Win32.WM_MOUSEMOVE:
                    Wm_MouseMove(ref m);
                    break;
                case Win32.WM_MOUSEHOVER:
                    Wm_MouseHover(ref m);
                    break;
                case Win32.WM_MOUSELEAVE:
                    Wm_MouseLeave(ref m);
                    break;
                case Win32.WM_EXITMENULOOP:
                    break;
                case Win32.WM_ENTERMENULOOP:
                    break;
                case Win32.WM_MENUCHAR:
                    break;
                case Win32.WM_CAPTURECHANGED:
                    break;                    
                case Win32.WM_IME_STARTCOMPOSITION:
                    Wm_Ime_StartComposition(ref m);
                    break;
                case Win32.WM_IME_ENDCOMPOSITION:
                    Wm_Ime_EndComposition(ref m);
                    break;
                // 响应系统Aero Glass的开启或关闭
                case Win32.WM_DWMCOMPOSITIONCHANGED: 
                    break;
                case Win32.WM_DWMNCRENDERINGCHANGED:
                    // 开启时会发送一次WM_DWMCOMPOSITIONCHANGED消息
                    bool isDWMrendering = m.WParam != IntPtr.Zero;
                    _aeroEnabled = CheckAeroEnabled();
                    AdjustAeroPadding();

                    // 通过win32 api获取系统主题色是否应用于标题栏
                    _colorPrevalence = RegistryHelper.DWMColorPrevalence;
                    bool opaque;
                    _colorizationColor = Win32.Util.GetColorizationColor(out opaque);
                    break;
                case Win32.WM_DWMCOLORIZATIONCOLORCHANGED:
                    // The color format of currColor is 0xAARRGGBB.
                    //uint currColor = (uint)m.WParam.ToInt64();
                    Color newColorizationColor = Color.FromArgb((int)m.WParam.ToInt64());
                    bool isBlendedWithOpacity = (m.LParam.ToInt64() != 0);

                    _colorPrevalence = RegistryHelper.DWMColorPrevalence;
                    _colorizationColor = newColorizationColor;
                    break;
                case Win32.WM_DWMWINDOWMAXIMIZEDCHANGE:
                    break;
                case Win32.WM_SENDICONICLIVEPREVIEWBITMAP:
                    break; 
                case Win32.WM_THEMECHANGED:
                    break;
            }
            
            if (m.Msg == Win32.Util.WM_MOUSEENTER)
            {
                Wm_MouseEnter(ref m);
            }

           
        }
        
        protected override void DefWndProc(ref Message m)
        {
            if (!DesignMode)
            {
                switch (m.Msg)
                {
                    // 热键
                    case Win32.WM_HOTKEY: 
                        break;
                    // Alt+F4
                    case Win32.WM_SYSCHAR:
                        break;
                    // 窗口显示
                    case Win32.WM_SHOWWINDOW:
                        break;
                    // 窗口创建                        
                    case Win32.WM_CREATE:
                        WM_CREATE(ref m);
                        break;
                    // 窗口位置将改变
                    case Win32.WM_WINDOWPOSCHANGING: 
                        //FormWindowState currentWindowState = Win32.Util.GetWindowState(this);
                        break;
                    // 窗口位置已改变
                    case Win32.WM_WINDOWPOSCHANGED:
                        break;
                    // 开始移动窗体
                    case Win32.WM_ENTERSIZEMOVE: 
                        WM_ENTERSIZEMOVE(ref m);
                        break;
                    // 结束移动窗体
                    case Win32.WM_EXITSIZEMOVE: 
                        WM_EXITSIZEMOVE(ref m);
                        break;
                    case Win32.WM_SIZING:
                        WM_SIZING(ref m);
                        break;
                    case Win32.WM_SIZE:
                        WM_SIZE(ref m);
                        break;
                    case Win32.WM_SYSCOMMAND:
                        WM_SYSCOMMAND(ref m);
                        break;
                    case Win32.WM_DPICHANGED:
                    case Win32.WM_GETDPISCALEDSIZE:
                        break;
                }
            }

            base.DefWndProc(ref m);

            if (DesignMode) return;

            switch (m.Msg)
            {
                case Win32.WM_NCHITTEST:
                    WM_NCHITTEST(ref m);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (!IsHandleCreated || DesignMode || _aeroEnabled || this.WindowState != FormWindowState.Normal)
            //    return;

            //var g = e.Graphics;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            ////g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            ////g.FillRectangles(Brushes.Red, _marginRectangle.ToArray());

            ////g.DrawRectangle(Pens.Brown, _virtualClientRectangle);
            ////ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
            ////                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset,
            ////                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset,
            ////                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset,
            ////                      Color.Black, SystemInformation.BorderSize.Width, ButtonBorderStyle.Inset);

            ////ControlPaint.DrawLockedFrame(e.Graphics, ClientRectangle, false);

            //var path = DrawingHelper.CreateRoundRectanglePath(Bounds, CornerRadius);
            //g.DrawPath(new Pen(GripDarkColor) { Alignment = PenAlignment.Center, DashCap = DashCap.Round }, path);
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
