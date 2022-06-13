using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(Win32.RECT lpRect, Point point);

        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);

        #endregion

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

        #region dwmapi

        [DllImport("dwmapi.dll")]
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

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref uint attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

        /// <summary>
        /// 返回当前的Aero背景色以及它是不透明还是透明。要设置颜色，请使用DwmpSetColorization函数
        /// </summary>
        /// <param name="ColorizationColor"></param>
        /// <param name="ColorizationOpaqueBlend"></param>
        [DllImport("dwmapi.dll")]
        public static extern void DwmGetColorizationColor(out uint ColorizationColor, [MarshalAs(UnmanagedType.Bool)] out bool ColorizationOpaqueBlend);

        //public static Color GetColorizationColor(out bool ColorizationOpaqueBlend)
        //{
        //    uint colorizationColor;
        //    DwmGetColorizationColor(out colorizationColor, out ColorizationOpaqueBlend);
        //    return Color.FromArgb((int)(colorizationColor & 0xffffff));
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
        
        [DllImport("dwmapi.dll"/*, EntryPoint = "#127", PreserveSig = false*/)]
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
        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

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
        [DllImport("dwmapi.dll", PreserveSig = false)]
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
        [DllImport("dwmapi")]
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
            /* https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute */

        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF
        {
            public byte A;
            public byte R;
            public byte G;
            public byte B;
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out COLORREF pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll", EntryPoint = "#104")]
        public static extern int DwmpSetColorization(UInt32 ColorizationColor, bool ColorizationOpaqueBlend, UInt32 Opacity);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out Size size);

        [DllImport("dwmapi.dll", SetLastError = true)]
        public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
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


        [DllImport("dwmapi.dll")]
        static extern int DwmUnregisterThumbnail(IntPtr thumb);

        
        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public RECT rcDestination;
            public RECT rcSource;
            public byte opacity;
            public int fVisible;
            public int fSourceClientAreaOnly;
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumbnail, ref DWM_THUMBNAIL_PROPERTIES props);




        

        #endregion




    }
}
