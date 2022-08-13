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
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public bool IsEmpty => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

            public static readonly RECT Empty;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle rcSrc)
            {
                Left = rcSrc.Left;
                Top = rcSrc.Top;
                Right = rcSrc.Right;
                Bottom = rcSrc.Bottom;
            }


        }

        /// <summary>
        /// 包含应用程序在处理WM_NCCALCSIZE消息时可以使用这些信息来计算窗口工作区的大小、位置和有效内容
        /// <seealso href="https://docs.microsoft.com/zh-CN/windows/win32/api/winuser/ns-winuser-nccalcsize_params"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            /// <summary>
            /// 第一个矩形包含已移动或调整大小的窗口的新坐标，即它是建议的新窗口坐标
            /// </summary>
            public RECT rcNewWindow;
            /// <summary>
            /// 第二个包含窗口在移动或调整大小之前的坐标，即它是窗口在创建时的坐标
            /// </summary>
            public RECT rcOldWindow;
            /// <summary>
            /// 第三个包含窗口在移动窗口或调整大小之前窗口工作区的坐标,如果窗口是子窗口，则坐标相对于父窗口的工作区。如果窗口是顶级窗口，则坐标相对于屏幕原点
            /// </summary>
            public RECT rcOldClient;
            /// <summary>
            /// 指向 WINDOWPOS 结构的指针，该结构包含在移动窗口或调整窗口大小的操作中指定的大小和位置值
            /// </summary>
            public IntPtr lpPos;
        }

        /// <summary>
        /// 包含有关窗口最大大小和位置及其最小和最大跟踪大小的信息<see cref="https://docs.microsoft.com/zh-CN/windows/win32/api/winuser/ns-winuser-minmaxinfo"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            /// <summary>
            ///  保留;请勿使用
            /// </summary>
            public Point ptReserved;
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

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }


        /// <summary>
        /// 定义传递给应用程序的窗口过程的初始化参数。这些成员与 CreateWindowEx 函数的参数相同
        /// <seealso href="https://docs.microsoft.com/zh-CN/windows/win32/api/winuser/ns-winuser-createstructa"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CREATESTRUCTA
        {
            /// <summary>
            /// 包含可用于创建窗口的其他数据。如果窗口是作为调用 CreateWindow 或 CreateWindowEx 函数的结果而创建的，则此成员包含函数调用中指定的 lpParam 参数的值
            /// </summary>
            public object lpCreateParams;
            /// <summary>
            /// 拥有新窗口的模块的句柄
            /// </summary>
            public IntPtr hInstance;
            /// <summary>
            /// 要由新窗口使用的菜单的句柄
            /// </summary>
            public IntPtr hMenu;
            /// <summary>
            /// 父窗口的句柄（如果该窗口是子窗口）。如果该窗口是所有者，则此成员标识所有者窗口。如果窗口不是子窗口或拥有的窗口，则此成员为 NULL
            /// </summary>
            public IntPtr hwndParent;
            /// <summary>
            /// 新窗口的高度，以像素为单位
            /// </summary>
            public int cy;
            /// <summary>
            /// 新窗口的宽度，以像素为单位
            /// </summary>
            public int cx;
            /// <summary>
            /// 新窗口左上角的 y 坐标。如果新窗口是子窗口，则坐标相对于父窗口。否则，坐标是相对于屏幕原点的
            /// </summary>
            public int y;
            /// <summary>
            /// 新窗口左上角的 x 坐标。如果新窗口是子窗口，则坐标相对于父窗口。否则，坐标是相对于屏幕原点的
            /// </summary>
            public int x;
            /// <summary>
            /// 新窗口的样式。有关可能值的列表，请参见窗口样式
            /// </summary>
            public long style;
            /// <summary>
            /// 新窗口的名称
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpszName;
            /// <summary>
            /// 指向以 null 结尾的字符串或指定新窗口的类名的原子的指针
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpszClass;
            /// <summary>
            /// 新窗口的扩展窗口样式。有关可能值的列表，请参见扩展窗口样式
            /// </summary>
            public long dwExStyle;
        }

        /// <summary>
        /// 包含有关窗口在屏幕上的位置的信息
        /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-windowplacement"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            /// <summary>
            /// 结构的长度，以字节为单位。在调用 GetWindowPlacement 或 SetWindowPlacement 函数之前，请将此成员设置为
            /// </summary>
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public RECT rcNormalPosition;
            //public RECT rcDevice;
        }

        //public struct WINDOWPLACEMENT
        //{
        //    public int length;
        //    public int flags;
        //    public int showCmd;
        //    // ptMinPosition was a by-value POINT structure
        //    public int ptMinPosition_x;
        //    public int ptMinPosition_y;
        //    // ptMaxPosition was a by-value POINT structure
        //    public int ptMaxPosition_x;
        //    public int ptMaxPosition_y;
        //    // rcNormalPosition was a by-value RECT structure
        //    public int rcNormalPosition_left;
        //    public int rcNormalPosition_top;
        //    public int rcNormalPosition_right;
        //    public int rcNormalPosition_bottom;
        //}


        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }


        [StructLayout(LayoutKind.Sequential)]
        public class DWM_THUMBNAIL_PROPERTIES
        {
            public uint dwFlags;
            public RECT rcDestination;
            public RECT rcSource;
            public byte opacity;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fVisible;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fSourceClientAreaOnly;
            public const uint DWM_TNP_RECTDESTINATION = 0x00000001;
            public const uint DWM_TNP_RECTSOURCE = 0x00000002;
            public const uint DWM_TNP_OPACITY = 0x00000004;
            public const uint DWM_TNP_VISIBLE = 0x00000008;
            public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
        }

        //[StructLayout(LayoutKind.Sequential)]
        //public class MARGINS
        //{
        //    public int cxLeftWidth, cxRightWidth,
        //               cyTopHeight, cyBottomHeight;

        //    public MARGINS(int left, int top, int right, int bottom)
        //    {
        //        cxLeftWidth = left; cyTopHeight = top;
        //        cxRightWidth = right; cyBottomHeight = bottom;
        //    }
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public class DWM_BLURBEHIND
        //{
        //    public uint dwFlags;
        //    [MarshalAs(UnmanagedType.Bool)]
        //    public bool fEnable;
        //    public IntPtr hRegionBlur;
        //    [MarshalAs(UnmanagedType.Bool)]
        //    public bool fTransitionOnMaximized;

        //    public const uint DWM_BB_ENABLE = 0x00000001;
        //    public const uint DWM_BB_BLURREGION = 0x00000002;
        //    public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
        //}

        
        [StructLayout(LayoutKind.Sequential)]
        public struct TRACKMOUSEEVENTS
        {
            public uint cbSize;
            public uint dwFlags;
            public IntPtr hWnd;
            public uint dwHoverTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public PointF pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            private IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class IMAGELISTDRAWPARAMS
        {
            public int cbSize = Marshal.SizeOf(typeof(IMAGELISTDRAWPARAMS));
            public IntPtr himl = IntPtr.Zero;
            public int i = 0;
            public IntPtr hdcDst = IntPtr.Zero;
            public int x = 0;
            public int y = 0;
            public int cx = 0;
            public int cy = 0;
            public int xBitmap = 0;
            public int yBitmap = 0;
            public int rgbBk = 0;
            public int rgbFg = 0;
            public int fStyle = 0;
            public int dwRop = 0;
            public int fState = 0;
            public int Frame = 0;
            public int crEffect = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class IMAGEINFO
        {
            public IntPtr hbmImage = IntPtr.Zero;
            public IntPtr hbmMask = IntPtr.Zero;
            public int Unused1 = 0;
            public int Unused2 = 0;
            // rcImage was a by-value RECT structure
            public int rcImage_left = 0;
            public int rcImage_top = 0;
            public int rcImage_right = 0;
            public int rcImage_bottom = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TRACKMOUSEEVENT
        {
            public int cbSize = Marshal.SizeOf(typeof(TRACKMOUSEEVENT));
            public int dwFlags;
            public IntPtr hwndTrack;
            public int dwHoverTime = 100; // Never set this to field ZERO, or to HOVER_DEFAULT, ever!
        }


    }
    
}
