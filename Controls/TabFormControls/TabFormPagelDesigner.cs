using Microsoft.DotNet.DesignTools.Designers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable CS8618
#pragma warning disable CS8600

namespace System.Windows.Forms.Design
{
    public class TabFormPageDesigner : ControlDesigner
    {


        internal virtual void OnDragEnterInternal(DragEventArgs de)
        {
            OnDragEnter(de);
        }

        internal virtual void OnDragDropInternal(DragEventArgs de)
        {
            OnDragDrop(de);
        }

        internal virtual void OnDragLeaveInternal(EventArgs e)
        {
            OnDragLeave(e);
        }

        internal virtual void OnDragOverInternal(DragEventArgs de)
        {
            OnDragOver(de);
        }

        internal virtual void OnGiveFeedbackInternal(GiveFeedbackEventArgs e)
        {
            OnGiveFeedback(e);
        }



    }
}
