using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
    /// <summary>
    /// 第四层，接收DWM信息变化窗体
    /// </summary>
    public class DWMThemeForm : CompactForm
    {
        private static bool _isThemedApp;
        private static bool _aeroEnabled;
        private static bool _colorPrevalence;
        private static Color _colorizationColor;
        private static Color _themeColor;

        private bool _ncACTIVATE;
        private int _cornerRadius = 7;
        private Padding _extendClientAreaIntoFrame = Padding.Empty;
        private WindowBorderStyle _formBorderStyle = WindowBorderStyle.Sizeable;
        private Padding _windowMinPadding;
        private ToolStripStatusLabelBorderSides _windowBorderSpecified;

        /// <summary>
        /// 是否使用了主题
        /// </summary>
        protected bool IsThemed
        {
            get
            {
                return _isThemedApp;
            }
        }

        /// <summary>
        /// DWM是否启用
        /// </summary>
        protected bool AeroEnable
        {
            get
            {
                return _aeroEnabled;
            }
        }

        /// <summary>
        /// 是否启用边框与标题栏强制着色
        /// </summary>
        protected bool ColorPrevalence
        {
            get
            {
                return _colorPrevalence;
            }
        }

        /// <summary>
        /// 边框与窗体着色颜色
        /// </summary>
        protected Color ColorizationColor
        {
            get
            {
                return _colorizationColor;
            }
        }

        /// <summary>
        /// 系统主题颜色
        /// </summary>
        protected Color ThemeColor
        {
            get
            {
                return _themeColor;
            }
        }

        /// <summary>
        /// 窗体边框颜色
        /// </summary>
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

        /// <summary>
        /// 窗体边框样式
        /// </summary>
        public new WindowBorderStyle FormBorderStyle
        {
            get
            {
                return _formBorderStyle;
            }
            set
            {
                _formBorderStyle = value;

                SetWindowBorderStyle(this,EventArgs.Empty);
            }
        }

        /// <summary>
        /// 窗体是否在激活状态
        /// </summary>
        public bool WindowNCActived
        {
            get
            {
                return _ncACTIVATE;
            }
        }

        /// <summary>
        /// 当前窗体边框垂直与水平方向的宽度
        /// </summary>
        public Size WindowBorderSize
        {
            get
            {
                var  size = Size.Empty;

                switch (base.FormBorderStyle)
                {
                    case System.Windows.Forms.FormBorderStyle.None:
                        break;
                    case System.Windows.Forms.FormBorderStyle.Fixed3D:
                        size = SystemInformation.Border3DSize;
                        break;
                    case System.Windows.Forms.FormBorderStyle.FixedToolWindow:
                        //break;
                    case System.Windows.Forms.FormBorderStyle.FixedDialog:
                        //break;
                    case System.Windows.Forms.FormBorderStyle.FixedSingle:
                        //size = SystemInformation.FixedFrameBorderSize;
                        //break;
                    case System.Windows.Forms.FormBorderStyle.Sizable:
                        //break;
                    case System.Windows.Forms.FormBorderStyle.SizableToolWindow:
                        size = SystemInformation.BorderSize;
                        break;
                    //default:
                    //    break;
                }


                return size;
            }
        }

        /// <summary>
        /// 当前窗体客户区到窗体边缘之间厚度
        /// </summary>
        public Padding WindowNCBorderThickness
        {
            get
            {
                //if (!IsHandleCreated)
                //    return Padding.Empty;
                

                return NativeMethodHelper.GetRealWindowBorders(this.CreateParams);
            }
        }

        /// <summary>
        /// 当前整个窗体尺寸
        /// </summary>
        public Rectangle WindowRectangle
        {
            get
            {
                if (!IsHandleCreated)
                    return Rectangle.Empty;

                return NativeMethodHelper.GetRealWindowRectangle(this.Handle);
            }
        }

        /// <summary>
        /// 当前窗体客户区尺寸
        /// </summary>
        public Rectangle WindowClientRectangle
        {
            get
            {
                if (!IsHandleCreated)
                    return Rectangle.Empty;

                return NativeMethodHelper.GetRealClientRectangle(this.Handle);
            }
        }

        /// <summary>
        /// 窗体中客户区扩展到非客户区的四周距离
        /// </summary>
        public Padding WindowExtendClientAreaIntoFrame
        {
            get
            {
                return _extendClientAreaIntoFrame;
            }
        }

        /// <summary>
        /// 要绘制窗体的哪些边
        /// </summary>
        protected ToolStripStatusLabelBorderSides WindowBorderSpecified
        {
            get
            {
                return _windowBorderSpecified;
            }
            set
            {
                _windowBorderSpecified = value;
            }
        }

        /// <summary>
        /// 窗体最小内边距
        /// </summary>
        protected Padding WindowMinPadding
        {
            get
            {
                return _windowMinPadding;
            }
        }

        /// <summary>
        /// 操作系统是否是Windows10以上
        /// </summary>
        public bool IsWindows10OrGreater
        {
            get {
                return OSFeature.Feature.IsWindows10OrGreater();
            }
        }

        static DWMThemeForm()
        {
            try
            {
                _aeroEnabled = NativeMethodHelper.CheckAeroEnabled();
                // 通过win32 api获取系统主题色是否应用于标题栏
                _colorPrevalence = RegistryHelper.DWMColorPrevalence;
                _colorizationColor = NativeMethodHelper.GetColorizationColor();

                _isThemedApp = (VisualStyleInformation.IsEnabledByUser && !string.IsNullOrEmpty(VisualStyleInformation.ColorScheme));
            }
            catch
            {
            }
        }

        public DWMThemeForm()
            :base()
        {
            base.FormBorderStyle = Forms.FormBorderStyle.Sizable;

            SetWindowBorderStyle(this, EventArgs.Empty);
        }

        protected override void WM_CREATE(ref Message m)
        {
            base.WM_CREATE(ref m);

            if (!DesignMode)
                NativeMethodHelper.SetWindowPos(m.HWnd);
        }

        protected override void WM_NCACTIVATE(ref Message m)
        {
            // 非客户区窗口失去焦点
            if (m.WParam == IntPtr.Zero) 
            {
                _ncACTIVATE = false;
            }
            // 非客户区窗口获得焦点
            else
            {
                _ncACTIVATE = true;
            }

            //base.OnActivated(EventArgs.Empty);

            base.WM_NCACTIVATE(ref m);

            // 防止默认边框
            if (/*!DesignMode && */_extendClientAreaIntoFrame != Padding.Empty)
            {
                // 未激活时窗体
                if (m.WParam == IntPtr.Zero)
                {
                    m.Result = new IntPtr(1);
                }
                // 激活窗体
                else
                {
                    m.Result = IntPtr.Zero;
                }

                //return;

                //Win32.MARGINS aRGINS = new Win32.MARGINS();

                ////if (PrevisionWindowState == FormWindowState.Normal)
                ////{
                //aRGINS.topHeight = 1;
                //aRGINS.leftWidth = 0;
                //aRGINS.bottomHeight = 0;
                //aRGINS.rightWidth = 0;
                ////}
                ////else
                ////{
                ////    aRGINS.topHeight = 0;
                ////    aRGINS.leftWidth = 0;
                ////    aRGINS.bottomHeight = 0;
                ////    aRGINS.rightWidth = 0;
                ////}

                //var hr = Win32.DwmExtendFrameIntoClientArea(m.HWnd, ref aRGINS);

                //IntPtr res = IntPtr.Zero;
                //Win32.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, ref res);
                //m.Result = res;

                //Win32.DwmFlush();

                //return;
            }

            if (!DesignMode)
            {
                NativeMethodHelper.SetWindowPos(m.HWnd);
                Invalidate(true);
            }
        }

        protected override void WM_WINDOWPOSCHANGING(ref Message m)
        {
            //AdjustPadding();
            base.WM_WINDOWPOSCHANGING(ref m);
        }

        protected override void WM_WINDOWPOSCHANGED(ref Message m)
        {
            base.WM_WINDOWPOSCHANGED(ref m);

            if (_extendClientAreaIntoFrame == Padding.Empty)
                return;
            if (!_aeroEnabled)
                return;

            //Win32.MARGINS aRGINS = new Win32.MARGINS();

            ////if (PrevisionWindowState == FormWindowState.Normal)
            ////{
            ////    aRGINS.topHeight = 1;
            ////    aRGINS.leftWidth = 0;
            ////    aRGINS.bottomHeight = 0;
            ////    aRGINS.rightWidth = 0;
            ////}
            ////else
            ////{
            ////    aRGINS.topHeight = 0;
            ////    aRGINS.leftWidth = 0;
            ////    aRGINS.bottomHeight = 0;
            ////    aRGINS.rightWidth = 0;
            ////}
            //aRGINS.topHeight = 1;
            //aRGINS.leftWidth = 0;
            //aRGINS.bottomHeight = 0;
            //aRGINS.rightWidth = 0;

            //var hr = Win32.DwmExtendFrameIntoClientArea(m.HWnd, ref aRGINS);
        }

        protected override void WM_DWMNCRENDERINGCHANGED(ref Message m)
        {
            // 非工作区策略发生变化DWM
            //bool isDWMrendering = m.WParam != IntPtr.Zero;

            _aeroEnabled = NativeMethodHelper.CheckAeroEnabled();
            _colorPrevalence = RegistryHelper.DWMColorPrevalence;
            _colorizationColor = NativeMethodHelper.GetColorizationColor();

            OnAeroEnableChanged(this, new AeroEventArgs(_aeroEnabled));

            base.WM_DWMNCRENDERINGCHANGED(ref m);
        }

        protected override void WM_DWMCOLORIZATIONCOLORCHANGED(ref Message m)
        {
            // https://docs.microsoft.com/zh-cn/archive/msdn-magazine/2007/april/aero-glass-create-special-effects-with-the-desktop-window-manager#S4
            Color newColorizationColor = Color.FromArgb((int)m.WParam.ToInt64());
            //bool isBlendedWithOpacity = (m.LParam.ToInt64() != 0);

            _colorPrevalence = RegistryHelper.DWMColorPrevalence;
            _colorizationColor = newColorizationColor;

            base.WM_DWMCOLORIZATIONCOLORCHANGED(ref m);
        }

        protected override void WM_DWMWINDOWMAXIMIZEDCHANGE(ref Message m)
        { 
            // 启动时会发出WM_DWMCOMPOSITIONCHANGED消息
            bool isDWMrendering = m.WParam != IntPtr.Zero;
            _aeroEnabled = NativeMethodHelper.CheckAeroEnabled();

            // 通过win32 api获取系统主题色是否应用于标题栏
            _colorPrevalence = RegistryHelper.DWMColorPrevalence;
            _colorizationColor = NativeMethodHelper.GetColorizationColor();

            base.WM_DWMWINDOWMAXIMIZEDCHANGE(ref m);

            SetWindowBorderGap(Padding.Empty);
        }

        /// <summary>
        /// 通知DWM起用或者禁用了
        /// </summary>
        protected override void WM_DWMCOMPOSITIONCHANGED(ref Message m)
        {
            base.WM_DWMCOMPOSITIONCHANGED(ref m);
        }

        protected virtual void OnAeroEnableChanged(object sender, AeroEventArgs args)
        {
            
        }

        protected virtual void SetWindowBorderStyle(object sender, EventArgs args)
        {
            switch (FormBorderStyle)
            {
                case WindowBorderStyle.None:
                    base.FormBorderStyle = Forms.FormBorderStyle.None;
                    break;
                case WindowBorderStyle.Sizeable:
                case WindowBorderStyle.Flat:
                    base.FormBorderStyle = Forms.FormBorderStyle.Sizable;
                    break;
                case WindowBorderStyle.Fixed:
                    base.FormBorderStyle = Forms.FormBorderStyle.FixedDialog;
                    break;
            }

            SetWindowBorderGap(Padding.Empty);
        }

        //private Padding GetWindowBorderThickness()
        //{
        //    Size gap = base.SizeFromClientSize(base.ClientSize) - base.ClientSize;
        //    Padding padding = NativeMethodHelper.GetRealWindowBorders(this.CreateParams); 

        //    return padding;
        //}

        private void SetWindowBorderGap(Padding gap)
        {
            _windowBorderSpecified = ToolStripStatusLabelBorderSides.None;
            SetWindowBorderAdjustGapCore(gap.Left,gap.Right,gap.Top,gap.Bottom);
            SetWindowMinPaddingCore(0,0,0,0);
            // 发起一次
            OnPaddingChanged(EventArgs.Empty);
        }

        //protected virtual void UpdateWindowBorderAdjustGap()
        //{
        //    Padding gap = Padding.Empty;
        //    SetWindowBorderAdjustGapCore(gap.Left, gap.Right, gap.Top, gap.Bottom);
        //}

        protected virtual void SetWindowBorderAdjustGapCore(int left, int right, int top, int bottom)
        {
            if (DesignMode)
            {
                _extendClientAreaIntoFrame = Padding.Empty;
                return;
            }

            //_extendClientAreaIntoFrame = new Padding(left,  top,  right,  bottom);
            _extendClientAreaIntoFrame.Left = left;
            _extendClientAreaIntoFrame.Right = right;
            _extendClientAreaIntoFrame.Top = top;
            _extendClientAreaIntoFrame.Bottom = bottom;
        }

        protected virtual void SetWindowMinPaddingCore(int minleft,int mintop,int minright,int minbottom)
        {
            _windowMinPadding = new Padding(minleft, mintop, minright, minbottom);
        }

        protected override void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General || e.Category == UserPreferenceCategory.VisualStyle)
            {
                //  Windows Accent Color
                var themeColor = Win32.Util.GetThemeColor();
                //var lightColor = ControlPaint.Light(themeColor);
                //var darkColor = ControlPaint.Dark(themeColor);
                _themeColor = themeColor;
            }
        }

        protected override void WM_ERASEBKGND(ref Message m)
        {
            // 防止背景重绘导致的抖动
            if (/*!DesignMode && */_extendClientAreaIntoFrame != Padding.Empty)
            {
                m.Result = new IntPtr(1);
                return;
            }

            base.WM_ERASEBKGND(ref m);
        }

        protected override void WM_NCPAINT(ref Message m)
        {
            // 防止透明闪烁,DWM阴影会消失
            //if (!DesignMode && _extendClientAreaIntoFrame != Padding.Empty)
            //{
            //    m.Result = IntPtr.Zero;
            //    return;
            //}

            base.WM_NCPAINT(ref m);

            //IntPtr hdc = Win32.GetWindowDC(m.HWnd);
            //if ((int)hdc != 0)
            //{
            //    using (Graphics gp = Graphics.FromHdc(hdc))
            //    {
            //        gp.DrawRectangle(
            //            new Pen(SystemBrushes.ControlDarkDark, 1),
            //            new Rectangle(
            //                0, 0,
            //                Width-14,
            //                Height-8
            //                ));

            //        gp.Flush();
            //    }
            //    Win32.ReleaseDC(m.HWnd, hdc);
            //}


            //Invalidate(true);
        }


        protected override void WM_NCUAHDRAWCAPTION(ref Message m)
        {
            if (/*!DesignMode && */_extendClientAreaIntoFrame != Padding.Empty)
            {
                m.Result = new IntPtr(1);// IntPtr.Zero;
                return;
            }

            base.WM_NCUAHDRAWCAPTION(ref m);
        }

        protected override void WM_NCUAHDRAWFRAME(ref Message m)
        {
            if (/*!DesignMode && */_extendClientAreaIntoFrame != Padding.Empty)
            {
                m.Result = new IntPtr(1);// IntPtr.Zero;
                return;
            }

            base.WM_NCUAHDRAWFRAME(ref m);
        }


        protected override void WM_PAINT(ref Message m)
        {
            base.WM_PAINT(ref m);

            //m.Result = IntPtr.Zero;

            IntPtr hdc = Win32.GetWindowDC(m.HWnd);
            if (hdc != IntPtr.Zero)
            {
                using (Graphics gp = Graphics.FromHdc(hdc))
                {
                    if (_windowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Left))
                    {
                        gp.DrawLine(
                        new Pen(BorderColor, WindowBorderSize.Width),
                        new Point(WindowNCBorderThickness.Left - _extendClientAreaIntoFrame.Left /*+ WindowBorderSize.Width*/, 0),
                        new Point(WindowNCBorderThickness.Left - _extendClientAreaIntoFrame.Left /*+ WindowBorderSize.Width*/, WindowRectangle.Height)
                        );
                    }

                    if (_windowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Top))
                    {
                        gp.DrawLine(
                        new Pen(BorderColor, WindowBorderSize.Height),
                        new Point(0, WindowNCBorderThickness.Top - _extendClientAreaIntoFrame.Top /*+ WindowBorderSize.Height*/),
                        new Point(WindowRectangle.Width, WindowNCBorderThickness.Top - _extendClientAreaIntoFrame.Top /*+ WindowBorderSize.Height*/)
                        );
                    }

                    if (_windowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Right))
                    {
                        gp.DrawLine(
                            new Pen(BorderColor, WindowBorderSize.Width),
                            new Point(WindowRectangle.Width - WindowNCBorderThickness.Right + _extendClientAreaIntoFrame.Right - WindowBorderSize.Width, 0),
                            new Point(WindowRectangle.Width - WindowNCBorderThickness.Right + _extendClientAreaIntoFrame.Right - WindowBorderSize.Width, WindowRectangle.Height)
                            );
                    }

                    if (_windowBorderSpecified.HasFlag(ToolStripStatusLabelBorderSides.Bottom))
                    {
                        gp.DrawLine(
                        new Pen(BorderColor, WindowBorderSize.Height),
                        new Point(0, WindowRectangle.Height - WindowNCBorderThickness.Bottom + _extendClientAreaIntoFrame.Bottom - WindowBorderSize.Height),
                        new Point(WindowRectangle.Width, WindowRectangle.Height - WindowNCBorderThickness.Bottom + _extendClientAreaIntoFrame.Bottom - WindowBorderSize.Height)
                        );
                    }


                    gp.Flush();
                }
                Win32.ReleaseDC(m.HWnd, hdc);
            }


        }

        private const int DTT_COMPOSITED = 8192;
        private const int DTT_GLOWSIZE = 2048;
        private const int DTT_TEXTCOLOR = 1;
        private void PainFrameEdge(IntPtr hWnd, IntPtr hdc)
        {
            Win32.RECT rECT = new Win32.RECT();
            Win32.GetWindowRect(hWnd, ref rECT);

            rECT.Left += 8;
            rECT.Right -= 8;
            rECT.Bottom -= 8;

            IntPtr hTheme = Win32.OpenThemeData(IntPtr.Zero, "CompositedWindow::Window");
            if (hTheme == IntPtr.Zero) return;

            IntPtr hdcPaint = Win32.CreateCompatibleDC(hdc);
            if (hdcPaint == IntPtr.Zero) return;

            //var wac = VisualStyleElement.Window.FrameBottom.Active.ClassName;
            //var vsr = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);
            //vsr.DrawEdge(Graphics.FromHwnd(this.Handle) ,
            //    new Rectangle(
            //        0,
            //        -1,
            //        rECT.Right - rECT.Left,
            //        rECT.Bottom - rECT.Top),
            //    Edges.Top, EdgeStyle.Etched, EdgeEffects.Flat);

            //Win32.OpenThemeData()

            //if (hdcPaint != IntPtr.Zero)
            //{

            int DIB_RGB_COLORS = 0,
        BI_BITFIELDS = 3,
        BI_RGB = 0,
        BITMAPINFO_MAX_COLORSIZE = 256,
        SPI_GETICONTITLELOGFONT = 0x001F,
        SPI_GETNONCLIENTMETRICS = 41,
        DEFAULT_GUI_FONT = 17,
        HOLLOW_BRUSH = 5;

            int cx = Win32.Util.RECTWIDTH(rECT);
            int cy = Win32.Util.RECTHEIGHT(rECT);

            Win32.BITMAPINFO dib = new Win32.BITMAPINFO();
            dib.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(Win32.BITMAPINFOHEADER));
            dib.bmiHeader.biWidth = cx;
            dib.bmiHeader.biHeight = -cy;
            dib.bmiHeader.biPlanes = 1;
            dib.bmiHeader.biBitCount = 32;// BIT_COUNT;
            dib.bmiHeader.biCompression = Win32.BitmapCompressionMode.BI_RGB; // BI_RGB;

            IntPtr ppvbit;
            IntPtr hbm = Win32.CreateDIBSection(hdc, ref dib, (uint)DIB_RGB_COLORS,out ppvbit, IntPtr.Zero,0);
            IntPtr hbmOld;

            //if (hbm != IntPtr.Zero)
            //{
                hbmOld = Win32.SelectObject(hdcPaint, hbm);

                // Create and select font
                IntPtr fontHandle = Font.ToHfont();
                Win32.SelectObject(hdcPaint, fontHandle);

            
                //Win32.SelectObject(hdcPaint, hbmOld);
            //}

            // Draw glowing text
            //VisualStyleRenderer renderer = new VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active);
            Win32.DTTOPTS dttOpts = new Win32.DTTOPTS();
            dttOpts.dwSize = Marshal.SizeOf(typeof(Win32.DTTOPTS));
            dttOpts.dwFlags = DTT_COMPOSITED | DTT_GLOWSIZE | DTT_TEXTCOLOR;
            dttOpts.iGlowSize = 15;

            const int SRCCOPY = 0x00CC0020;
            Win32.BitBlt(hdc, 0, 0, cx, cy, hdcPaint, 0, 0, SRCCOPY);


            Win32.DeleteObject(hbm);


            // https://csharp.hotexamples.com/examples/-/BITMAPINFO/-/php-bitmapinfo-class-examples.html


            //Win32.DeleteObject(hbm);
            Win32.DeleteDC(hdcPaint);
            //}
            Win32.CloseThemeData(hTheme);

        }


        void paint_caption(IntPtr hWnd, IntPtr hdc, int caption_height)
        {
            Win32.RECT rc;
            Win32.GetClientRect(hWnd, out rc);
            rc.Bottom = caption_height;

            int cx = Win32.Util.RECTWIDTH(rc);
            int cy = Win32.Util.RECTHEIGHT(rc);

            IntPtr memdc = Win32.CreateCompatibleDC(hdc);

            Win32.BITMAPINFO dib = new Win32.BITMAPINFO();
            dib.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(Win32.BITMAPINFOHEADER));
            dib.bmiHeader.biWidth = cx;
            dib.bmiHeader.biHeight = -cy;
            dib.bmiHeader.biPlanes = 1;
            dib.bmiHeader.biBitCount = 32;// BIT_COUNT;
            dib.bmiHeader.biCompression = Win32.BitmapCompressionMode.BI_RGB;

            IntPtr ppvbit;
            IntPtr hbmOld;
            IntPtr hbm = Win32.CreateDIBSection(hdc, ref dib, 0, out ppvbit, IntPtr.Zero, 0);

            hbmOld = Win32.SelectObject(memdc, hbm);

            //Note, GDI functions don't support alpha channel, they can't be used here
            //Use GDI+, BufferedPaint, or DrawThemeXXX functions
            
            IntPtr hTheme = Win32.OpenThemeData(Win32.GetDesktopWindow(), "Window"); // CompositedWindow::

            var active = VisualStyleElement.Window.FrameBottom.Active;

            Win32.DrawThemeEdge(hTheme,
                hbm, 
                active.Part, 
                active.State,
                new Win32.COMRECT(ClientRectangle),
                (int)EdgeStyle.Raised,
                (int)Edges.Bottom,
                new Win32.COMRECT(ClientRectangle));


            const int SRCCOPY = 0x00CC0020;
            Win32.BitBlt(hdc, 0, 0, cx, caption_height, memdc, 0, 0, SRCCOPY);
            Win32.SelectObject(memdc, hbmOld);
            Win32.DeleteObject(hbm);
            Win32.DeleteDC(memdc);
        }






    }
}
