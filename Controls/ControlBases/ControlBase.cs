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
    public abstract class ControlBase : Control, IFilterControl, INotifyControl, IControl
    {
        [ComVisible(false)]
        public new class ControlCollection : Control.ControlCollection
        {
            private Control owner;

            public ControlCollection(Control owner)
                : base(owner)
            {
                this.owner = owner;
            }

            public override void Add(Control value)
            {
                if (value == null)
                {
                    Console.WriteLine(new ArgumentNullException("value"));
                    return;
                }

                if (value is not IControl)
                {
                    Console.WriteLine(new ArgumentException("value"));
                    return;
                }

                var args = new ControlCancelEventArgs(value,false);

                if (owner is IFilterControl filterControl && filterControl.FilterTypes.Length > 0)
                {
                    if (filterControl.IsFilterControl)
                    {
                        bool isFilter = false;

                        foreach (var type in filterControl.FilterTypes)
                        {
                            if (value.GetType().IsAssignableTo(type))
                            {
                                isFilter = true;
                                break;
                            }
                        }

                        if (!isFilter)
                            return;
                    }

                    filterControl.OnControlAdding(args);

                    if (args.Cancel)
                        return;
                }

                base.Add(value);

                   
            }

        }
        
        public bool IsFilterControl => false;
        
        public Type[] FilterTypes => GetFilterTypes(EventArgs.Empty);

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
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
            //    if (Parent is INotifyControl parent)
            //    {
            //        parent.OnChildPaint(new PaintCancelEventArgs(this, Graphics.FromHwnd(base.Handle), Bounds));
            //    }
            //    else
            //    {
            //        Parent.Invalidate();
            //    }
            //    //break;
            //    //}
            //}
        }




    }

    public abstract class ContainerControlBase : ContainerControl, IFilterControl, INotifyControl, IControl
    {
        public bool IsFilterControl => false;

        public Type[] FilterTypes => GetFilterTypes(EventArgs.Empty);

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
        }

        


    }

    public interface IControl
    {
    }

    public interface INotifyControl : IControl
    {
        void OnChildPaint(PaintCancelEventArgs e);
    }

    public interface IFilterControl : INotifyControl
    {
        bool IsFilterControl { get; }

        Type[] FilterTypes { get; }

        void OnControlAdding(ControlCancelEventArgs e);
    }



}
