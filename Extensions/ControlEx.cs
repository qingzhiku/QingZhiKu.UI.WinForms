using System;
using System.Collections.Generic;
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

    }
}
