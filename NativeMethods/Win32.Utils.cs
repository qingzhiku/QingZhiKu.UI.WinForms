using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace System
{
    public partial class Win32
    {

        /// <summary>
        /// Win32常用公共程序工具
        /// </summary>
        public class Util
        {
            /// <summary>
            /// 取低位 X 坐标
            /// </summary>
            public static int LOWORD(IntPtr value)
            {
                return (int)value & 0xFFFF;
            }

            public static int LoWord(IntPtr value)
            {
                return (int)value & 0xFFFF;
            }

            /// <summary>
            /// 取低位 X 坐标
            /// </summary>
            public static int LOWORD(int n)
            {
                return n & 0xffff;
            }

            /// <summary>
            /// 取高位 Y 坐标
            /// </summary>
            public static int HIWORD(IntPtr value)
            {
                return (int)value >> 16;
            }
            
            public static int HiWord(IntPtr value)
            {
                return (int)value >> 16;
            }

            /// <summary>
            /// 取高位 Y 坐标
            /// </summary>
            public static int HIWORD(int n)
            {
                return (n >> 16) & 0xffff;
            }

            public static int MAKELONG(int low, int high)
            {
                return (high << 16) | (low & 0xffff);
            }

            public static IntPtr MAKELPARAM(int low, int high)
            {
                return (IntPtr)((high << 16) | (low & 0xffff));
            }

            public static int SignedHIWORD(IntPtr n)
            {
                return SignedHIWORD(unchecked((int)(long)n));
            }
            
            public static int SignedLOWORD(IntPtr n)
            {
                return SignedLOWORD(unchecked((int)(long)n));
            }

            public static int SignedHIWORD(int n)
            {
                int i = (int)(short)((n >> 16) & 0xffff);

                return i;
            }

            public static int SignedLOWORD(int n)
            {
                int i = (int)(short)(n & 0xFFFF);

                return i;
            }

            /// <summary>
            /// MouseMove时，使用左键移动窗体，可以在控件上实现移动窗体
            /// </summary>
            public static void MoveWindow(IntPtr hwnd)
            {
                ReleaseCapture();
                SendMessage(hwnd, WM_NCLBUTTONDOWN, NCHITTEST_Result.HTCAPTION, 0);
            }

            /// <summary>
            /// 移动窗体，这种只能在窗体上实现
            /// </summary>
            public static void MoveWindow(HandleRef hwnd)
            {
                SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)WMSYSCOMMAND_WParam.SC_MOUSEMOVE, IntPtr.Zero);
                SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero);
            }

            public static void ShowSystemMenu(IntPtr handle, Point screenLocation)
            {
                const uint TPM_RETURNCMD = 0x0100;
                const uint TPM_LEFTBUTTON = 0x0;

                if (handle == IntPtr.Zero)
                {
                    return;
                }

                IntPtr hmenu = GetSystemMenu(handle, false);

                uint cmd = TrackPopupMenuEx(hmenu, TPM_LEFTBUTTON | TPM_RETURNCMD, screenLocation.X, screenLocation.Y, handle, IntPtr.Zero);
                if (0 != cmd)
                {
                    PostMessage(handle, Win32.WM_SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
                }
            }

            /// <summary>
            /// Color转整数
            /// </summary>
            public static int RGB(Color color)
            {
                return color.ToArgb() & 0xffffff;
            }

            /// <summary>
            /// 整数转Color
            /// </summary>
            public static Color RGB(int color)
            {
                byte redColor = (byte)((0x000000FF & color) >> 0);
                byte greenColor = (byte)((0x0000FF00 & color) >> 8);
                byte blueColor = (byte)((0x00FF0000 & color) >> 16);
                //byte alphaColor = (byte)((0xFF000000 & color) >> 24);
                return Color.FromArgb(redColor, greenColor, blueColor);
            }

            /// <summary>
            /// 整数转Color
            /// </summary>
            public static Color RGB(uint color)
            {
                byte redColor = (byte)((0x000000FF & color) >> 0);
                byte greenColor = (byte)((0x0000FF00 & color) >> 8);
                byte blueColor = (byte)((0x00FF0000 & color) >> 16);
                //byte alphaColor = (byte)((0xFF000000 & color) >> 24);
                return Color.FromArgb(redColor, greenColor, blueColor);
            }


            #region 获取当前窗口状态

            /// <summary>
            /// 隐藏窗口并通过启动到另一个窗口
            /// </summary>
            public const int SW_HIDE = 0;
            public const int SW_NORMAL = 1;
            /// <summary>
            /// 激活窗口并将其显示为图标
            /// </summary>
            public const int SW_SHOWMINIMIZED = 2;
            /// <summary>
            /// 激活窗口并将其显示为最大化窗口的大小
            /// </summary>
            public const int SW_SHOWMAXIMIZED = 3;
            /// <summary>
            /// 最小化指定的窗口和激活在系统的顶级窗口列表
            /// </summary>
            public const int SW_MAXIMIZE = 3;
            /// <summary>
            /// 显示在其最近大小和位置的窗口。当前活动的窗口保持有效。
            /// </summary>
            public const int SW_SHOWNOACTIVATE = 4;
            /// <summary>
            /// 激活窗口并显示在其当前大小和位置
            /// </summary>
            public const int SW_SHOW = 5;
            /// <summary>
            /// 最小化指定的窗口并激活 z 顺序中的下一个顶级窗口
            /// </summary>
            public const int SW_MINIMIZE = 6;
            /// <summary>
            /// 显示窗口作为图标。当前活动的窗口保持有效
            /// </summary>
            public const int SW_SHOWMINNOACTIVE = 7;
            /// <summary>
            /// 以显示在其当前状态的窗口。当前活动的窗口保持有效
            /// </summary>
            public const int SW_SHOWNA = 8;
            /// <summary>
            /// 激活并显示窗口。如果窗口处于最小化或最大化，窗口还原为其原始大小和位置 (和 SW_SHOWNORMAL相同)
            /// </summary>
            public const int SW_RESTORE = 9;
            public const int SW_MAX = 10;

            /// <summary>
            /// 获取窗体显示的状态
            /// </summary>
            public static FormWindowState GetWindowState(Form form)
            {
                WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                wp.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
                GetWindowPlacement(form.Handle, ref wp);

                FormWindowState oldState = form.WindowState;

                switch (wp.showCmd)
                {
                    case SW_RESTORE:
                    case SW_NORMAL:
                    case SW_SHOW:
                    case SW_SHOWNA:
                    case SW_SHOWNOACTIVATE:
                        oldState = FormWindowState.Normal;
                        break;
                    case SW_SHOWMAXIMIZED:
                        oldState = FormWindowState.Maximized;
                        break;
                    case SW_SHOWMINIMIZED:
                    case SW_MINIMIZE:
                    case SW_SHOWMINNOACTIVE:
                        oldState = FormWindowState.Minimized;
                        break;
                    case SW_HIDE:
                        break;
                    default:
                        break;
                }

                return oldState;
            }

            #endregion

            /// <summary>
            /// 设置窗体的圆角矩形
            /// </summary>
            public static void SetFormRoundRectRgn(IntPtr handle,Rectangle bounds, int rgnRadius)
            {
                IntPtr hRgn = IntPtr.Zero;
                hRgn = CreateRoundRectRgn(0, 0, bounds.Width + 1, bounds.Height + 1, rgnRadius, rgnRadius);
                SetWindowRgn(handle, hRgn, true);
                DeleteObject(hRgn);
            }

            /// <summary>
            /// 绘制圆角矩形
            /// </summary>
            public static void DrawRoundRect(Graphics g, Size size, int rgnRadius,Color bgColor,Color bdColor)
            {
                IntPtr hDC = g.GetHdc();
                IntPtr thePen = Win32.CreatePen(0, 1, (uint)Win32.Util.RGB(bdColor));
                IntPtr theBrush = Win32.CreateSolidBrush((uint)Win32.Util.RGB(bgColor));
                IntPtr oldPen = Win32.SelectObject(hDC, thePen);
                IntPtr oldBrush = Win32.SelectObject(hDC, theBrush);

                Win32.RoundRect(hDC, 0, 0, size.Width, size.Height, rgnRadius, rgnRadius);

                Win32.SelectObject(hDC, oldPen);
                Win32.DeleteObject(thePen);
                Win32.SelectObject(hDC, oldBrush);
                Win32.DeleteObject(theBrush);

                g.ReleaseHdc(hDC);
            }

            /// <summary>
            /// 绘制圆角边框
            /// </summary>
            public static void DrawRoundRect(Graphics g, Size size, int rgnRadius, Color bdColor)
            {
                ControlPaint.DrawBorder(g, new Rectangle(Point.Empty, size), bdColor, ButtonBorderStyle.Solid);
            }

            /// <summary>
            /// 任务栏显示进度条
            /// </summary>
            public static void ShowTaskBarProgress(IntPtr handle, int max, int value)
            {
                if (handle == IntPtr.Zero)
                    return;

                IntPtr hwnd = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
                if (hwnd == IntPtr.Zero)
                    return;

                hwnd = Win32.FindWindowEx(hwnd, IntPtr.Zero, "TrayNotifyWnd", null);
                if (hwnd == IntPtr.Zero)
                    return;

                hwnd = Win32.FindWindowEx(hwnd, IntPtr.Zero, "SysPager", null);
                if (hwnd == IntPtr.Zero)
                    return;

                hwnd = Win32.FindWindowEx(hwnd, IntPtr.Zero, "ToolbarWindow32", null);
                if (hwnd == IntPtr.Zero)
                    return;

                hwnd = Win32.FindWindowEx(hwnd, IntPtr.Zero, "msctls_progress32", null);
                if (hwnd == IntPtr.Zero)
                    return;

                Win32.SendMessage(hwnd, 0x410, max, value);
            }

            public const int XBUTTON1 = 0x0001;
            public const int XBUTTON2 = 0x0002;
            /// <summary>
            /// 获取XButton的值
            /// </summary>
            public static MouseButtons GetXButton(int wparam)
            {
                switch (wparam)
                {
                    case XBUTTON1:
                        return MouseButtons.XButton1;
                    case XBUTTON2:
                        return MouseButtons.XButton2;
                }
                return MouseButtons.None;
            }

            private static int wmMouseEnterMessage = -1;

            /// <summary>
            /// 获取鼠标进入消息
            /// </summary>
            public static int WM_MOUSEENTER
            {
                get
                {
                    if (wmMouseEnterMessage == -1)
                    {
                        wmMouseEnterMessage = Win32.RegisterWindowMessage("WinFormsMouseEnter");
                    }
                    return wmMouseEnterMessage;
                }
            }

            




            /// <summary>
            /// 检测窗口是否隐藏，win8以上
            /// </summary>
            /// <param name="hwnd"></param>
            /// <returns></returns>
            public static bool IsWindowCloaked(IntPtr hwnd)
            {
                int dwCloaked = 0;

                if (DwmGetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CLOAKED, ref dwCloaked, sizeof(int)) == 0)
                {
                    return dwCloaked != 0;
                }

                return false;
            }




        }


    }
}
