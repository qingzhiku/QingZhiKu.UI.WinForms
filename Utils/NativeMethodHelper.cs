using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }

}
