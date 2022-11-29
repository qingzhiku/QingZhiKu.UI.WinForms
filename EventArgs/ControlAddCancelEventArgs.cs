using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public class ControlCancelEventArgs : CancelEventArgs
    {
        private Control control;

        public ControlCancelEventArgs(Control control,bool cancel) 
            : base(cancel)
        {
            this.control = control;
        }

        public Control Control { 
            get => control;  
        }

    }
}
