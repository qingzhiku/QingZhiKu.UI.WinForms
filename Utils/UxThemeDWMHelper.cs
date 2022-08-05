using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class UxThemeDWMHelper
    {


        public static Color WindowGlassColor
        {
            get
            {
                Color _windowGlassColor;
                

                bool isOpaque;
                uint color;
                Win32.DwmGetColorizationColor(out color, out isOpaque);
                color |= isOpaque ? 0xFF000000 : 0;

                _windowGlassColor = Win32.Util.ColorFromArgbDword(color);


                return _windowGlassColor;
            }
        }



    }
}
