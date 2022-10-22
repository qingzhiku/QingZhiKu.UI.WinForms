using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;

namespace System.Windows.Forms
{
    public class TabFormControlCancelEventArgs : TabControlCancelEventArgs
    {
        private ITabFormPage tabPage;

        public TabFormControlCancelEventArgs(
            ITabFormPage tabPage,
            int tabPageIndex,
            bool cancel,
            TabControlAction action) : base(null, tabPageIndex, cancel, action)
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
