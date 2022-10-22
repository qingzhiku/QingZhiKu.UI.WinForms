using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;

namespace System.Windows.Forms
{
    public class TabFormControlEventArgs : TabControlEventArgs
    {
        private ITabFormPage tabPage;

        public TabFormControlEventArgs(
            ITabFormPage tabPage,
            int tabPageIndex,
            TabControlAction action) : base(null, tabPageIndex, action)
        {
            this.tabPage = tabPage;
        }

        public new ITabFormPage TabPage
        {
            get
            {
                return tabPage;
            }
        }

    }

    
}
