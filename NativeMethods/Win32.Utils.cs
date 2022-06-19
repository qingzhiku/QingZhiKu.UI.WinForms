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
            /// 检查窗户是否隐形，win8以上
            /// </summary>
            /// <param name="hwnd"></param>
            /// <returns></returns>
            public static bool IsWindowCloaked(IntPtr hwnd)
            {
                int dwCloaked = 0;

                if (DwmGetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CLOAKED, out dwCloaked, sizeof(int)) == 0)
                {
                    return dwCloaked != 0;
                }

                return false;
            }

            /// <summary>
            /// 获取系统着色
            /// </summary>
            /// <param name="ColorizationOpaqueBlend"></param>
            /// <returns></returns>
            public static Color GetColorizationColor(out bool ColorizationOpaqueBlend)
            {
                uint colorizationColor;
                DwmGetColorizationColor(out colorizationColor, out ColorizationOpaqueBlend);
                return Color.FromArgb((int)(colorizationColor & 0xffffffff));
            }

            /// <summary>
            /// 获取DWM呈现状态
            /// </summary>
            /// <param name="hWnd"></param>
            /// <returns></returns>
            public static bool GetNCRenderingEnabled(IntPtr hWnd)
            {
                int isNCRenderingEnabled = 0;
                var result = DwmGetWindowAttribute(hWnd,
                    (int)DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_ENABLED,
                    ref isNCRenderingEnabled,
                    sizeof(int));

                //if (result != 0)
                //{
                    var messageException = MessageFromHResult(result);
                //}

                return messageException == null && isNCRenderingEnabled >= 0;
            }

            /// <summary>
            /// 检索屏幕空间中的扩展框架边界矩形
            /// </summary>
            public static RECT GetDWMExtendedFrameBounds(IntPtr hWnd)
            {
                RECT extendedFrameBounds = RECT.Empty;
                var result = DwmGetWindowAttribute(hWnd,
                    /*(int)*/DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS,
                    out extendedFrameBounds,
                    Marshal.SizeOf(typeof(RECT)));

                //var messageException = MessageFromHResult(result);

                return extendedFrameBounds;
            }

            /// <summary>
            /// 检索窗口相对空间中标题按钮区域的边界。检索到的值的类型为 RECT。如果窗口最小化或对用户不可见，则检索到的 RECT 的值是未定义的。您应该检查检索到的 RECT 是否包含可以使用的边界，如果没有，则可以断定窗口已最小化或不可见
            /// </summary>
            public static RECT GetDWMCaptionButtonBounds(IntPtr hWnd)
            {
                RECT captionButtonBounds = RECT.Empty;
                var result = DwmGetWindowAttribute(hWnd,
                    /*(int)*/DWMWINDOWATTRIBUTE.DWMWA_CAPTION_BUTTON_BOUNDS,
                    out captionButtonBounds,
                    Marshal.SizeOf(typeof(RECT)));

                //var messageException = MessageFromHResult(result);
                
                return captionButtonBounds;
            }

            public static void EnableBlurBehind(IntPtr hwnd)
            {
                //HRESULT hr = S_OK;

                // Create and populate the blur-behind structure.
                DWM_BLURBEHIND bb = DWM_BLURBEHIND.Empty;

                // Specify blur-behind and blur region.
                bb.dwFlags = DWM_BB.Enable;
                bb.fEnable = true;
                bb.hRgnBlur = IntPtr.Zero;

                // Enable blur-behind.
                var hr = DwmEnableBlurBehindWindow(hwnd, ref bb);
                //if (SUCCEEDED(hr))
                //{
                //    // ...
                //}
                //return hr;
            }


            /// <summary>
            /// 获得一个 HRESULT 的说明
            /// </summary>
            public static string? MessageFromHResult(int hr)
            {
                return Marshal.GetExceptionForHR(hr)?.Message;
            }


            /// <summary>
            /// 沉浸式启动选择背景
            /// </summary>
            /// <returns></returns>
            public static Color GetAccentColor()
            {
                var userColorSet = GetImmersiveUserColorSetPreference(false, false);
                var colorType = GetImmersiveColorTypeFromName(Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground"));
                var colorSetEx = GetImmersiveColorFromColorSetEx((uint)userColorSet, colorType, false, 0);
                return ConvertDWordColorToRGB(colorSetEx);
            }

            private static Color ConvertDWordColorToRGB(uint colorSetEx)
            {
                byte redColor = (byte)((0x000000FF & colorSetEx) >> 0);
                byte greenColor = (byte)((0x0000FF00 & colorSetEx) >> 8);
                byte blueColor = (byte)((0x00FF0000 & colorSetEx) >> 16);
                //byte alphaColor = (byte)((0xFF000000 & colorSetEx) >> 24);
                return Color.FromArgb(redColor, greenColor, blueColor);
            }

            /// <summary>
            /// 获取屏幕某个像素的颜色
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static Color GetPixelColor(int x, int y)
            {
                IntPtr hdc = Win32.GetDC(IntPtr.Zero);
                uint pixel = Win32.GetPixel(hdc, x, y);
                Win32.ReleaseDC(IntPtr.Zero, hdc);
                return Color.FromArgb((int)(pixel & 0xffffff));
            }

            /// <summary>
            /// 获取窗体某个像素的颜色
            /// </summary>
            /// <param name="hwnd"></param>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static Color GetPixelColor(IntPtr hwnd, int x, int y)
            {
                IntPtr hdc = Win32.GetDC(hwnd);
                uint pixel = Win32.GetPixel(hdc, x, y);
                Win32.ReleaseDC(IntPtr.Zero, hdc);
                return Color.FromArgb((int)(pixel & 0xffffff));
            }


            public static void PostWMNCCALCSIZEMessage(IntPtr hWnd)
            {
                Win32.RECT rcClient = Win32.RECT.Empty;
                Win32.GetWindowRect(hWnd, ref rcClient);

                // Inform the application of the frame change.
                var result = Win32.SetWindowPos(hWnd,
                             IntPtr.Zero,
                             rcClient.Left, rcClient.Top,
                             rcClient.Right - rcClient.Left,
                             rcClient.Bottom - rcClient.Top,
                             SWP_FRAMECHANGED);
            }

            public static Rectangle GetWindowRect(IntPtr hWnd)
            {
                Win32.RECT rcClient = Win32.RECT.Empty;
                Win32.GetWindowRect(hWnd, ref rcClient);

                return new Rectangle(rcClient.Left, rcClient.Top, rcClient.Right - rcClient.Left, rcClient.Bottom - rcClient.Top);
            }


            public static void GetWindowColorizationColor(bool opaque)
            {
                DWM_COLORIZATION_PARAMS temp = new DWM_COLORIZATION_PARAMS();
                Win32.DwmGetColorizationParameters(out temp);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(temp.clrColor.ToString());
                sb.AppendLine(temp.clrAfterGlow.ToString());
                sb.AppendLine(temp.nIntensity.ToString());
                sb.AppendLine(temp.clrAfterGlowBalance.ToString());
                sb.AppendLine(temp.clrBlurBalance.ToString());
                sb.AppendLine(temp.clrGlassReflectionIntensity.ToString());
                sb.AppendLine(temp.fOpaque.ToString());
                MessageBox.Show(sb.ToString());
            }



        }


    }
}
