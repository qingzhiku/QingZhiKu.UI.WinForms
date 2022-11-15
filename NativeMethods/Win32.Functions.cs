using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Win32
    {
        #region gdi32.dll
        

        /// <summary>
        /// 创建圆角区域eg.Region.FromHrgn(0, 0, Width, Height,20,20)
        /// </summary>
        /// <returns>句柄</returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect,     // x-coordinate of upper-left corner
                int nTopRect,      // y-coordinate of upper-left corner
                int nRightRect,    // x-coordinate of lower-right corner
                int nBottomRect,   // y-coordinate of lower-right corner
                int nWidthEllipse, // width of ellipse
                int nHeightEllipse // height of ellipse
         );

        [DllImport(ExternDll.Gdi32/*, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto*/)]
        public static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);

        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(Win32.RECT lpRect, Point point);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RoundRect(HandleRef hDC, int left, int top, int right, int bottom, int width, int height);

        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true)]
        private static extern IntPtr IntCreatePen(int nStyle, int nWidth, int crColor);

        public static IntPtr CreatePen(int nStyle, int nWidth, int crColor)
        {
            return IntCreatePen(nStyle, nWidth, crColor);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(HandleRef hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);

        [StructLayout(LayoutKind.Sequential)]
        public struct BlurParameters
        {
            internal float Radius;
            internal bool ExpandEdges;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SharpenParams
        {
            internal float Radius;
            internal float Amount;
        }

        internal enum PaletteType               // GDI+1.1还可以针对一副图像获取某种特殊的调色
        {
            PaletteTypeCustom = 0,
            PaletteTypeOptimal = 1,
            PaletteTypeFixedBW = 2,
            PaletteTypeFixedHalftone8 = 3,
            PaletteTypeFixedHalftone27 = 4,
            PaletteTypeFixedHalftone64 = 5,
            PaletteTypeFixedHalftone125 = 6,
            PaletteTypeFixedHalftone216 = 7,
            PaletteTypeFixedHalftone252 = 8,
            PaletteTypeFixedHalftone256 = 9
        };

        internal enum DitherType                    // 这个主要用于将真彩色图像转换为索引图像，并尽量减低颜色损失
        {
            DitherTypeNone = 0,
            DitherTypeSolid = 1,
            DitherTypeOrdered4x4 = 2,
            DitherTypeOrdered8x8 = 3,
            DitherTypeOrdered16x16 = 4,
            DitherTypeOrdered91x91 = 5,
            DitherTypeSpiral4x4 = 6,
            DitherTypeSpiral8x8 = 7,
            DitherTypeDualSpiral4x4 = 8,
            DitherTypeDualSpiral8x8 = 9,
            DitherTypeErrorDiffusion = 10
        }

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTextMetrics(HandleRef hdc, Win32.TEXTMETRIC tm);

        [StructLayout(LayoutKind.Sequential)]
        public struct DTTOPTS
        {
            public int dwSize;
            public int dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public Win32.PointF ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct BITMAPFILEHEADER
        {
            public ushort bfType;
            public uint bfSize;
            public ushort bfReserved1;
            public ushort bfReserved2;
            public uint bfOffBits;
        }

        public enum BitmapCompressionMode : uint
        {
            BI_RGB = 0,
            BI_RLE8 = 1,
            BI_RLE4 = 2,
            BI_BITFIELDS = 3,
            BI_JPEG = 4,
            BI_PNG = 5
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public BitmapCompressionMode biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;

            public void Init()
            {
                biSize = (uint)Marshal.SizeOf(this);
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            /// <summary>
            /// A BITMAPINFOHEADER structure that contains information about the dimensions of color format.
            /// </summary>
            public BITMAPINFOHEADER bmiHeader;

            /// <summary>
            /// An array of RGBQUAD. The elements of the array that make up the color table.
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
            public RGBQUAD[] bmiColors;
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi, uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);


        #endregion


        #region gdiplus.dll

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipCreateEffect(Guid guid, out IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipDeleteEffect(IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipGetEffectParameterSize(IntPtr effect, out uint size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipSetEffectParameters(IntPtr effect, IntPtr parameters, uint size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipGetEffectParameters(IntPtr effect, ref uint size, IntPtr parameters);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipBitmapApplyEffect(IntPtr bitmap, IntPtr effect, ref Rectangle rectOfInterest, bool useAuxData, IntPtr auxData, int auxDataSize);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipBitmapCreateApplyEffect(ref IntPtr SrcBitmap, int numInputs, IntPtr effect, ref Rectangle rectOfInterest, ref Rectangle outputRect, out IntPtr outputBitmap, bool useAuxData, IntPtr auxData, int auxDataSize);


        // 这个函数我在C#下已经调用成功
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipInitializePalette(IntPtr palette, int palettetype, int optimalColors, int useTransparentColor, int bitmap);

        // 该函数一致不成功，不过我在VB6下调用很简单，也很成功，主要是结构体的问题。
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int GdipBitmapConvertFormat(IntPtr bitmap, int pixelFormat, int dithertype, int palettetype, IntPtr palette, float alphaThresholdPercent);


        #endregion

        #region DwmApi

        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        public enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }

        [DllImport("DwmApi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("DwmApi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref uint attrValue, int attrSize);

        [DllImport("DwmApi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("DwmApi.dll")]
        public static extern int DwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref IntPtr result);

        [DllImport("dwmapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmFlush();

        /// <summary>
        /// 返回当前的Aero背景色以及它是不透明还是透明。要设置颜色，请使用DwmpSetColorization函数
        /// </summary>
        /// <param name="ColorizationColor"></param>
        /// <param name="ColorizationOpaqueBlend"></param>
        [DllImport("DwmApi.dll")]
        public static extern void DwmGetColorizationColor(out uint ColorizationColor, [MarshalAs(UnmanagedType.Bool)] out bool ColorizationOpaqueBlend);

        //public static Color GetColorizationColor(out bool ColorizationOpaqueBlend)
        //{
        //    uint colorizationColor;
        //    DwmGetColorizationColor(out colorizationColor, out ColorizationOpaqueBlend);
        //    return Color.FromArgb((int)(colorizationColor & 0xffffffff));
        //}

        public struct DWM_COLORIZATION_PARAMS
        {
            public uint clrColor;
            public uint clrAfterGlow;
            public uint nIntensity;
            public uint clrAfterGlowBalance;
            public uint clrBlurBalance;
            public uint clrGlassReflectionIntensity;
            public bool fOpaque;
        }
        
        [DllImport("DwmApi.dll", EntryPoint = "#127", PreserveSig = false)]
        private static extern void DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS parameters);

        //public void getParameters()
        //{
        //    DWM_COLORIZATION_PARAMS temp = new DWM_COLORIZATION_PARAMS();
        //    DwmGetColorizationParameters(out temp);
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(temp.clrColor.ToString());
        //    sb.AppendLine(temp.clrAfterGlow.ToString());
        //    sb.AppendLine(temp.nIntensity.ToString());
        //    sb.AppendLine(temp.clrAfterGlowBalance.ToString());
        //    sb.AppendLine(temp.clrBlurBalance.ToString());
        //    sb.AppendLine(temp.clrGlassReflectionIntensity.ToString());
        //    sb.AppendLine(temp.fOpaque.ToString());
        //    MessageBox.Show(sb.ToString());
        //}


        [Flags]
        public enum DWM_BB
        {
            Enable = 1,
            BlurRegion = 2,
            TransitionMaximized = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND
        {
            public static readonly DWM_BLURBEHIND Empty;

            public DWM_BB dwFlags;
            public bool fEnable;
            public IntPtr hRgnBlur;
            public bool fTransitionOnMaximized;

            public DWM_BLURBEHIND(bool enabled)
            {
                fEnable = enabled ? true : false;
                hRgnBlur = IntPtr.Zero;
                fTransitionOnMaximized = false;
                dwFlags = DWM_BB.Enable;
            }

            public System.Drawing.Region Region
            {
                get { return System.Drawing.Region.FromHrgn(hRgnBlur); }
            }

            public bool TransitionOnMaximized
            {
                get { return fTransitionOnMaximized; }
                set
                {
                    fTransitionOnMaximized = value ? true : false;
                    dwFlags |= DWM_BB.TransitionMaximized;
                }
            }

            public void SetRegion(System.Drawing.Graphics graphics, System.Drawing.Region region)
            {
                hRgnBlur = region.GetHrgn(graphics);
                dwFlags |= DWM_BB.BlurRegion;
            }
        }

        /// <summary>
        /// 此函数在窗口的给定区域中的空气动力学后面模糊
        /// </summary>
        [DllImport("DwmApi.dll")]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

        [Flags]
        public enum CompositionAction : uint
        {
            /// <summary>
            /// To enable DWM composition
            /// </summary>
            DWM_EC_DISABLECOMPOSITION = 0,
            /// <summary>
            /// To disable composition.
            /// </summary>
            DWM_EC_ENABLECOMPOSITION = 1
        }

        /// <summary>
        /// 禁用Vista和window 7上的aero效果。
        /// </summary>
        [DllImport("DwmApi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(CompositionAction uCompositionAction);

        /// <summary>
        /// Used by DWM_TIMING_INFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UNSIGNED_RATIO
        {
            public UInt32 uiNumerator;
            public UInt32 uiDenominator;
        }

        /// <summary>
        /// Specifies Desktop Window Manager (DWM) composition timing information.
        /// Used by the DwmGetCompositionTimingInfo function.
        /// </summary>
        /// <remark>
        /// It's necessary to get rid of padding (by setting Pack = 1)
        /// The member sbSize must be set to (uint)Marshal.SizeOf(typeof(DWM_TIMING_INFO))
        /// before calling DwmGetCompositionTimingInfo function else it won't work.
        /// </remark>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DWM_TIMING_INFO
        {
            public UInt32 cbSize;
            public UNSIGNED_RATIO rateRefresh;
            public UInt64 qpcRefreshPeriod;
            public UNSIGNED_RATIO rateCompose;
            public UInt64 qpcVBlank;
            public UInt64 cRefresh;
            public UInt32 cDXRefresh;
            public UInt64 qpcCompose;
            public UInt64 cFrame;
            public UInt32 cDXPresent;
            public UInt64 cRefreshFrame;
            public UInt64 cFrameSubmitted;
            public UInt32 cDXPresentSubmitted;
            public UInt64 cFrameConfirmed;
            public UInt32 cDXPresentConfirmed;
            public UInt64 cRefreshConfirmed;
            public UInt32 cDXRefreshConfirmed;
            public UInt64 cFramesLate;
            public UInt32 cFramesOutstanding;
            public UInt64 cFrameDisplayed;
            public UInt64 qpcFrameDisplayed;
            public UInt64 cRefreshFrameDisplayed;
            public UInt64 cFrameComplete;
            public UInt64 qpcFrameComplete;
            public UInt64 cFramePending;
            public UInt64 qpcFramePending;
            public UInt64 cFramesDisplayed;
            public UInt64 cFramesComplete;
            public UInt64 cFramesPending;
            public UInt64 cFramesAvailable;
            public UInt64 cFramesDropped;
            public UInt64 cFramesMissed;
            public UInt64 cRefreshNextDisplayed;
            public UInt64 cRefreshNextPresented;
            public UInt64 cRefreshesDisplayed;
            public UInt64 cRefreshesPresented;
            public UInt64 cRefreshStarted;
            public UInt64 cPixelsReceived;
            public UInt64 cPixelsDrawn;
            public UInt64 cBuffersEmpty;
        }

        /// <summary>
        /// 检索DWM的当前合成计时信息
        /// </summary>
        [DllImport("DwmApi.dll")]
        public static extern uint DwmGetCompositionTimingInfo(IntPtr hwnd, ref DWM_TIMING_INFO pTimingInfo);

        /// <summary>
        ///  DwmGetWindowAttribute 和 DwmSetWindowAttribute 函数使用的选项
        /// </summary>
        public enum DWMWINDOWATTRIBUTE : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED = 14,
            DWMWA_FREEZE_REPRESENTATION,
            /* extra */

            DWMWA_PASSIVE_UPDATE_MODE,
            DWMWA_USE_HOSTBACKDROPBRUSH,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR,
            DWMWA_CAPTION_COLOR,
            DWMWA_TEXT_COLOR,
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
            DWMWA_SYSTEMBACKDROP_TYPE,
            DWMWA_LAST
            /* https://docs.microsoft.com/en-us/windows/win32/api/DwmApi/ne-DwmApi-dwmwindowattribute */

        }

        [DllImport("DwmApi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out int pvAttribute, int cbAttribute);

        [DllImport("DwmApi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out bool pvAttribute, int cbAttribute);

        [DllImport("DwmApi.dll")]
        public static extern int DwmGetWindowAttribute(
            IntPtr hwnd,
            int dwAttributeToGet, //DWMWA_* values
            ref int pvAttributeValue,
            int cbAttribute);

        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF
        {
            public byte A;
            public byte R;
            public byte G;
            public byte B;
        }

        [DllImport("DwmApi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out COLORREF pvAttribute, int cbAttribute);

        [DllImport("DwmApi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("DwmApi.dll", EntryPoint = "#104")]
        public static extern int DwmpSetColorization(UInt32 ColorizationColor, bool ColorizationOpaqueBlend, UInt32 Opacity);

        [DllImport("DwmApi.dll", PreserveSig = false)]
        public static extern void DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out Size size);

        [DllImport("DwmApi.dll", SetLastError = true)]
        public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("DwmApi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        //public void SetParameters(object sender, EventArgs e)
        //{
        //    DWM_COLORIZATION_PARAMS temp = new DWM_COLORIZATION_PARAMS();
        //    temp.clrColor = 1802811644;
        //    temp.clrAfterGlow = 1802811644;
        //    temp.nIntensity = 8;
        //    temp.clrAfterGlowBalance = 43;
        //    temp.clrBlurBalance = 49;
        //    temp.clrGlassReflectionIntensity = 50;
        //    temp.fOpaque = false;

        //    DwmSetColorizationParameters(ref temp, false);
        //}


        [DllImport("DwmApi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr thumb);

        
        //[StructLayout(LayoutKind.Sequential)]
        //public struct DWM_THUMBNAIL_PROPERTIES
        //{
        //    public int dwFlags;
        //    public RECT rcDestination;
        //    public RECT rcSource;
        //    public byte opacity;
        //    public int fVisible;
        //    public int fSourceClientAreaOnly;
        //}

        [DllImport("DwmApi.dll", PreserveSig = false)]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumbnail, ref DWM_THUMBNAIL_PROPERTIES props);


        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(bool bEnable);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out int pcrColorization,  [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern IntPtr DwmRegisterThumbnail( IntPtr dest, IntPtr source);

        //[DllImport("dwmapi.dll", PreserveSig = false)]
        //public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUpdateThumbnailProperties( IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

        //[DllImport("dwmapi.dll", PreserveSig = false)]
        //public static extern void DwmQueryThumbnailSourceSize(  IntPtr hThumbnail, out Size size);



        #endregion

        #region uxtheme

        [DllImport("uxtheme.dll", EntryPoint = "#95")]
        public static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);
       
        [DllImport("uxtheme.dll", EntryPoint = "#96")]
        public static extern uint GetImmersiveColorTypeFromName(IntPtr pName);
        
        [DllImport("uxtheme.dll", EntryPoint = "#98")]
        public static extern int GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

        // Theming/Visual Styles
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsAppThemed();

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeAppProperties();

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void SetThemeAppProperties(int Flags);

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int CloseThemeData(IntPtr hTheme);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsThemePartDefined(IntPtr hTheme, int iPartId, int iStateId);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int partId, int stateId, [In] Win32.COMRECT pRect, [In] Win32.COMRECT pClipRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeEdge(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [In] Win32.COMRECT pDestRect, int uEdge, int uFlags, [Out] Win32.COMRECT pContentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeParentBackground(IntPtr hwnd, IntPtr hdc, [In] Win32.COMRECT prc);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, int dwTextFlags2, [In] Win32.COMRECT pRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBackgroundContentRect(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [In] Win32.COMRECT pBoundingRect, [Out] Win32.COMRECT pContentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBackgroundExtent(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [In] Win32.COMRECT pContentRect, [Out] Win32.COMRECT pExtentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBackgroundRegion(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [In] Win32.COMRECT pRect, ref IntPtr pRegion);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeBool(IntPtr hTheme, int iPartId, int iStateId, int iPropId, ref bool pfVal);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, ref int pColor);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeEnumValue(IntPtr hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeFilename(IntPtr hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszThemeFilename, int cchMaxBuffChars);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public LOGFONT()
            {
            }
            public LOGFONT(LOGFONT lf)
            {
                //Debug.Assert(lf != null, "lf is null");

                this.lfHeight = lf.lfHeight;
                this.lfWidth = lf.lfWidth;
                this.lfEscapement = lf.lfEscapement;
                this.lfOrientation = lf.lfOrientation;
                this.lfWeight = lf.lfWeight;
                this.lfItalic = lf.lfItalic;
                this.lfUnderline = lf.lfUnderline;
                this.lfStrikeOut = lf.lfStrikeOut;
                this.lfCharSet = lf.lfCharSet;
                this.lfOutPrecision = lf.lfOutPrecision;
                this.lfClipPrecision = lf.lfClipPrecision;
                this.lfQuality = lf.lfQuality;
                this.lfPitchAndFamily = lf.lfPitchAndFamily;
                this.lfFaceName = lf.lfFaceName;
            }
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;
        }

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeFont(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId,  Win32.LOGFONT pFont);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeInt(IntPtr hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemePartSize(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [In] Win32.COMRECT prc, System.Windows.Forms.VisualStyles.ThemeSizeType eSize, [Out] Win32.SizeF psz);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemePosition(IntPtr hTheme, int iPartId, int iStateId, int iPropId, [Out] Win32.PointF pPoint);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeMargins(IntPtr hTheme, IntPtr hDC, int iPartId, int iStateId, int iPropId, ref Win32.MARGINS margins);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeString(IntPtr hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszBuff, int cchMaxBuffChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeDocumentationProperty([MarshalAs(UnmanagedType.LPWStr)] string pszThemeName, [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyName, StringBuilder pszValueBuff, int cchMaxValChars);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeTextExtent(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, [In] Win32.COMRECT pBoundingRect, [Out] Win32.COMRECT pExtentRect);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeTextMetrics(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref System.Windows.Forms.VisualStyles.TextMetrics ptm);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTSTRUCT
        {
            public int x;
            public int y;
            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int HitTestThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int dwOptions, [In] Win32.COMRECT pRect, HandleRef hrgn, [In] Win32.POINTSTRUCT ptTest, ref int pwHitTestCode);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetThemeSysBool(IntPtr hTheme, int iBoolId);
        [DllImport(ExternDll.Uxtheme, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetThemeSysInt(IntPtr hTheme, int iIntId, ref int piValue);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowTheme(IntPtr hWnd, string subAppName, string subIdList);

        [DllImport("uxtheme", ExactSpelling = true)]
        public extern static Int32 DrawThemeEdge(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pDestRect, uint egde, uint flags, out RECT pRect);

        [DllImport("uxtheme", ExactSpelling = true)]
        public extern static Int32 DrawThemeEdge(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pDestRect, uint egde, uint flags, int pRect);

        #endregion

        

    }
}
