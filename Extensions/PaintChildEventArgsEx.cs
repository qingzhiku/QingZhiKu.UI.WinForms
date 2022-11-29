using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.ButtonTextBox;

namespace System.Windows.Forms
{
    internal static class PaintChildEventArgsEx
    {

        public static ButtonTextBox.IAlignmentButton? GetNavigationButton(this PaintCancelEventArgs paintChild)
        {
            return paintChild.ChildControl as ButtonTextBox.IAlignmentButton;
        }


    }

}
