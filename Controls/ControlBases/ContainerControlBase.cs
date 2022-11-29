using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace System.Windows.Forms
{
    public abstract class ContainerControlBase : ContainerControl, IFilterControl, INotifyControl, IControl
    {
        //public bool IsFilterControl => false;

        public Type[] FilterTypes => GetFilterTypes(EventArgs.Empty);

        public virtual Form Form
        {
            get
            {
                //                IContainerControl icc = GetContainerControl();

                //                if (icc != null && icc is Form from1)
                //                {
                //                    return from1;
                //                }

                //                if (Parent != null && Parent is Form from2)
                //                {
                //                    return from2;
                //                }

                //#pragma warning disable CS8603 // 可能返回 null 引用。
                //                return null;
                //#pragma warning restore CS8603 // 可能返回 null 引用。
                return FindForm();
            }
        }

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlBase.ControlCollection(this);
        }

        protected virtual Type[] GetFilterTypes(EventArgs e)
        {
            return Array.Empty<Type>();
        }

        protected virtual void OnChildPaint(PaintCancelEventArgs e)
        {

        }

        protected virtual void OnControlAdding(ControlCancelEventArgs e)
        {

        }

        void IFilterControl.OnControlAdding(ControlCancelEventArgs e)
        {
            OnControlAdding(e);
        }

        void INotifyControl.OnChildPaint(PaintCancelEventArgs e)
        {
            OnChildPaint(e);
        }

        protected override void WndProc(ref Message m)
        {
            //避免TextBox等是由系统进程绘制，重载OnPaint方法将不起作用
            //base.WndProc(ref m);
            //if (m.Msg == Win32.WM_PAINT || m.Msg == Win32.WM_CTLCOLOREDIT)
            //{
            //    WmPaint(ref m);
            //}

            switch (m.Msg)
            {
                case Win32.WM_PAINT:
                case Win32.WM_CTLCOLOREDIT:
                    if (Parent is INotifyControl parent)
                    {
                        PaintCancelEventArgs pcea = new PaintCancelEventArgs(this, Graphics.FromHwnd(base.Handle), Bounds);
                        parent.OnChildPaint(pcea);
                        if (pcea.Cancel)
                            return;
                    }
                    else
                    {
                        Parent.Invalidate();
                    }

                    base.WndProc(ref m);
                    WmPaint(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected virtual void WmPaint(ref Message m)
        {
            //if (Parent != null)
            //{
            //    //switch (m.Msg)
            //    //{
            //    //    case Win32.WM_PAINT:
            //    //    case Win32.WM_CTLCOLOREDIT:
            //            if (Parent is INotifyControl parent)
            //            {
            //                 parent.OnChildPaint(new PaintCancelEventArgs(this, Graphics.FromHwnd(base.Handle), Bounds));
            //            }
            //            else
            //            {
            //                Parent.Invalidate();
            //            }
            //    //        break;
            //    //}
            //}
            //Invalidate(true);
        }




    }
}
