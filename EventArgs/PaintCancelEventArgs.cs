using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public class PaintCancelEventArgs : PaintEventArgs
    {
        private Control control;
        private bool cancel;
        
        public PaintCancelEventArgs(Control control, PaintEventArgs e)
           : this(control, e.Graphics, e.ClipRectangle)
        {
        }

        public PaintCancelEventArgs(Control control, Graphics graphics, Rectangle clipRect)
            : base(graphics, clipRect)
        {
            this.control = control;
        }

        public Control ChildControl { get => control;  }
        public bool Cancel { get => cancel; set => cancel = value; }
    }


   
}
