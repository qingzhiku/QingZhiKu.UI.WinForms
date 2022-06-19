using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public partial class DropShadowForm
    {

        protected virtual void UpdateMasterStyles(object sender, EventArgs e)
        {
            if (sender is not Form form) return;

            //if (form.FormBorderStyle != FormBorderStyle.None)
            //form.FormBorderStyle = FormBorderStyle.None;
            form.FormBorderStyle = MasterFormBorderStyle;

            form.SetStyles(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer
                , true);

            form.SetDoubleBuffered(true);

            //if (form.ControlBox) 
            form.UpdateSysMenu();

            //form.UpdateStyle(Win32.WS_SIZEBOX);

            // 闪烁
            form.UpdateEXStyles(Win32.WS_EX_COMPOSITED);

            //if (form.WindowState == FormWindowState.Normal)
            //    form.CreateRoundRectRegion(CornerRound);

            form.BringToFront();
        }

        protected virtual void OnMasterResizeBegin(object sender, EventArgs e)
        {
            if (sender is not Form form) return;
            
        }

        protected virtual void OnMasterResizeEnd(object sender, EventArgs e)
        {
            if (sender is not Form form) return;
            
           
        }

        protected virtual void OnMasterResize(object sender, SizeEventArgs e)
        {
            if (sender is not Form form) return;

            //Size = new Size(form.Width + ShadowSpread * 2, form.Height + ShadowSpread * 2);
        }

        protected virtual void OnMasterLocationChanged(object sender, EventArgs e)
        {
            if (sender is not Form form) return;

            Size gap = form.Size - form.ClientSize;
            int left = gap.Width / 2;
            int top = gap.Height - left;

            var pos = new Point(form.Left - ShadowSpread + left, form.Top - ShadowSpread + top);

            if (pos != Location)
                Location = pos;
        }

        protected virtual void OnMasterSizeChanged(object sender, SizeEventArgs e)
        {
            if (sender is not Form form) return;
            
            OnMasterResize(sender,e);

            Size = new Size(e.Width + ShadowSpread * 2, e.Height + ShadowSpread * 2);

            if (form.GetWindowState() == FormWindowState.Normal)
            {
                UpdateShadowBitmap(e.Width, e.Height);

                Size gap = form.Size - form.ClientSize;
                int left = gap.Width / 2;
                int top = gap.Height - left;

                form.CreateRoundRectRegion(new Rectangle(left,top,e.Width, e.Height), CornerRound);

                //form.CreateRoundRectRegion(CornerRound, e.Width, e.Height);
                //form.Invalidate();
            }
            else  if (form.GetWindowState() == FormWindowState.Maximized)
            {
                UpdateShadowBitmap(e.Width, e.Height);
                form.Region = null;
                //form.CreateRoundRectRegion(0, form.Width,  form.Height);
            }

            //form.Invalidate();
        }

        protected virtual void OnMasterPaint(object sender, PaintEventArgs e)
        {
            if (sender is not Form form) return;
        }

        protected virtual void OnMasterClose() 
        {
            if (this.IsHandleCreated)
            {
                this.Close();
            }
        }

        protected virtual void OnMasterDestroyHandle()
        {
            if (this.IsHandleCreated)
            {
                this.DestroyHandle();
            }
        }

        protected virtual void OnMasterMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not Form form) return;
            
            if (this.MasterMoveable)
                form.DragMove();
        }

        protected virtual void OnMasterMouseDown(object sender, MouseEventArgs e)
        {
            if (sender is not Form form) return;

            //if (e.Button == MouseButtons.Left)
            //{
            //    if (this.MasterMoveable)
            //        form.Capture = true;
            //}
        }

        protected virtual void OnMasterMouseUp(object sender, MouseEventArgs e)
        {
            if (sender is not Form form) return;

            //if (e.Button == MouseButtons.Left)
            //{
            //    if (this.MasterMoveable)
            //        form.Capture = false;
            //}
        }
        
        protected virtual void OnMasterVisibleChanged(object sender, EventArgs e)
        {
            if (sender is not Form form) return;

            this.Visible = form.Visible;

            //Win32.AnimateWindow(form.Handle, 150, Win32.AW_VER_NEGATIVE);
        }

        protected virtual void OnMasterNCActivate(object sender, NCActivateEventArgs e)
        {
            if (sender is not Form form) return;

            _ncACTIVATE = e.Activate;

            UpdateShadowBitmap(form.ClientSize.Width, form.ClientSize.Height);
        }


        
    }
}
