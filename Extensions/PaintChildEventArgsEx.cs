using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.NavigationTextBox;

namespace System.Windows.Forms
{
    internal static class PaintChildEventArgsEx
    {

        public static NavigationTextBox.INavigationButton? GetNavigationButton(this PaintCancelEventArgs paintChild)
        {
            return paintChild.ChildControl as NavigationTextBox.INavigationButton;
        }


    }

}
