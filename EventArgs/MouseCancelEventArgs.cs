using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public class MouseCancelEventArgs: MouseEventArgs
    {
        private Control control;
        private bool cancel;

        public MouseCancelEventArgs(Control control,MouseEventArgs e)
            : this(control,e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
        }

        public MouseCancelEventArgs(Control control, MouseButtons button, int clicks, int x, int y, int delta) 
            : base(button, clicks, x, y, delta)
        {
            this.control = control;
        }

        public Control ChildControl { get => control; }
        public bool Cancel { get => cancel; set => cancel = value; }

    }
}
