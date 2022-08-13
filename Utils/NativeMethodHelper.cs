using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class NativeMethodHelper
    {
        public static Color GetColorizationColor()
        {
            bool opaque;
            return Win32.Util.GetColorizationColor(out opaque);
        }

        public static Color GetAccentColor()
        {
            return Win32.Util.GetThemeColor();
        }

        public static bool CheckAeroEnabled()
        {
            return Win32.Util.CheckAeroEnabled();
        }

        public static FormWindowState GetWindowState(Form form)
        {
            var currentws = FormWindowState.Normal;

            if (Win32.Util.IsFormMaximized(form))
            {
                currentws = FormWindowState.Maximized;
            }

            if (Win32.Util.IsFormMinimized(form))
            {
                currentws = FormWindowState.Minimized;
            }

            return currentws;
        }

        public static FormWindowState GetWindowState(IntPtr handle)
        {
            var winstate = FormWindowState.Normal;

            // 获取当前窗口的样式
            uint style = (uint)Win32.GetWindowLong(handle, Win32.GWL_STYLE);

            if((style &= Win32.WS_MAXIMIZE) != 0)
            {
                winstate = FormWindowState.Maximized;
            }

            if ((style &= Win32.WS_MINIMIZE) != 0)
            {
                winstate = FormWindowState.Minimized;
            }

            return winstate;
        }

        public static Padding GetRealWindowBorders(CreateParams createParams)
        {
            Win32.RECT rect = new Win32.RECT();

            // 默认
            rect.Left = 0;
            rect.Right = 0;
            rect.Top = 0;
            rect.Bottom = 0;

            // 调用AdjustWindowRectEx矩形计算边框
            Win32.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);

            return new Padding(-rect.Left, -rect.Top, rect.Right, rect.Bottom);
        }

        public static Rectangle GetRealWindowRectangle(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                Win32.RECT windowRect = new Win32.RECT();
                Win32.GetWindowRect(handle, ref windowRect);

                return new Rectangle(0, 0,
                                     windowRect.Right - windowRect.Left,
                                     windowRect.Bottom - windowRect.Top);
            }
            else
                return Rectangle.Empty;
        }

        /// <summary>
        /// Gets the real client rectangle of the list.
        /// </summary>
        /// <param name="handle">Window handle of the control.</param>
        public static Rectangle GetRealClientRectangle(IntPtr handle)
        {
            Win32.RECT windowRect;
            Win32.GetClientRect(handle, out windowRect);

            return new Rectangle(0, 0,
                                 windowRect.Right - windowRect.Left,
                                 windowRect.Bottom - windowRect.Top);
        }

        public static void PostWindowPosMessage(IntPtr handle)
        {
            Win32.Util.PostWMNCCALCSIZEMessage(handle);
        }

        public static void SetWindowPos(IntPtr handle)
        {
            Win32.SetWindowPos(
                handle, 
                IntPtr.Zero, 
                0, 0, 0, 0,
            Win32.SWP_SHOWWINDOW | Win32.SWP_NOMOVE | Win32.SWP_NOSIZE | Win32.SWP_FRAMECHANGED);
        }

        public static bool Network
        {
            get
            {
                return (Win32.GetSystemMetrics(Win32.SM_NETWORK) & 0x00000001) != 0;
            }
        }

        public static bool Is64BitProcess
        {
            get
            {
#if WIN32
                    return false;
#else
                return true;
#endif
            }
        }

#if !FEATURE_PAL
        public static bool Is64BitOperatingSystem
        {
            [System.Security.SecuritySafeCritical]
            get
            {
#if WIN32
                    bool isWow64; // WinXP SP2+ and Win2k3 SP1+
                    return Win32Native.DoesWin32MethodExist(Win32Native.KERNEL32, "IsWow64Process")
                        && Win32Native.IsWow64Process(Win32Native.GetCurrentProcess(), out isWow64)
                        && isWow64;
#else
                // 64-bit programs run only on 64-bit
                //<
                return true;
#endif
            }
        }
#endif




    }

}
