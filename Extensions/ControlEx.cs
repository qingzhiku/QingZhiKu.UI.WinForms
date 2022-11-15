using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    public static class ControlEx
    {
        public static bool IsForm(this Control control)
        {
            if (control is Form)
            {
                return true;
            }
            return false;
        }

        public static MouseEventArgs TranslateMouseEvent(this Control control,Control child, MouseEventArgs e)
        {
            if (child != null && control.IsHandleCreated)
            {
                // same control as PointToClient or PointToScreen, just
                // with two specific controls in mind.
                Win32.POINT point = new Win32.POINT(e.X, e.Y);
                Win32.MapWindowPoints(new HandleRef(child, child.Handle), new HandleRef(control, control.Handle), point, 1);
                return new MouseEventArgs(e.Button, e.Clicks, point.x, point.y, e.Delta);
            }
            return e;
        }




    }
}
