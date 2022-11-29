using Microsoft.DotNet.DesignTools.Designers;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DotNet.DesignTools.Designers.Actions;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
    public class PanelBaseDesigner: ScrollableControlDesigner
    {
        protected Pen BorderPen
        {
            get
            {
                Color color = (((double)Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(Control.BackColor) : ControlPaint.Dark(Control.BackColor));
                Pen pen = new Pen(color);
                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }

        

        public PanelBaseDesigner()
        {
            base.AutoResizeHandles = true;
        }

        protected virtual void DrawBorder(Graphics graphics)
        {
            PanelBase panel = (PanelBase)base.Component;
            if (panel != null && panel.Visible)
            {
                Pen borderPen = BorderPen;
                Rectangle clientRectangle = Control.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                graphics.DrawRectangle(borderPen, clientRectangle);
                borderPen.Dispose();
            }
        }


        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            PanelBase panel = (PanelBase)base.Component;
            if (panel.BorderStyle == BorderStyle.None)
            {
                DrawBorder(pe.Graphics);
            }
            base.OnPaintAdornments(pe);
        }



    }


}
