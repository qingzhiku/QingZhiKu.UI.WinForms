using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Drawing;

namespace System.Windows.Forms
{
    [Designer(typeof(System.Windows.Forms.Design.CombineBoxBaseDesigner), typeof(IDesigner))]
    public abstract class CombineBoxBase : ContainerControlBase, CombineBoxBase.ICombineBox
    {
        private FormMonitor? _formMonitor;
        private bool _ncACTIVATE = true;

        //public class CombineButton: ControlBase
        //{

        //}

        public override Form Form
        {
            get
            {
                return FindForm();
            }
        }

        protected NativeWindow? FormMonitors
        {
            get
            {
                if(null == _formMonitor && null != Form && !Form.IsHandleCreated)
                {
                    CreateFormMonitor();
                }

                return _formMonitor;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int PreferredHeight
        {
            get
            {
                return SystemInformation.CaptionHeight;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int PreferredWidth
        {
            get
            {
                return SystemInformation.MenuButtonSize.Width;
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(PreferredWidth, PreferredHeight);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool WindowNCActived
        {
            get
            {
                return _ncACTIVATE;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormWindowState PrevisionWindowState
        {
            get
            {
                var state = FormWindowState.Normal;

                //if (Owner is IWindowNCSatuts)
                //{
                //    state = ((IWindowNCSatuts)Owner).PrevisionWindowState;
                //}
                //else if(Owner is Form)
                //{
                //    state = Owner.GetWindowState();
                //}
                if (Form != null)
                {
                    state = Form.GetWindowState();
                }

                return state;
            }
        }

        protected CombineBoxBase()
        {
            InitializationControls();
        }

        protected abstract IControl[] SeedControsl();

        protected virtual void InitializationControls()
        {
            var cbases = SeedControsl().Cast<Control>();

            if (cbases != null && cbases.Count() > 0)
                base.Controls.AddRange(cbases.ToArray());
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            PositionControls();
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
            CreateFormMonitor();
        }

        //protected override void OnCreateControl()
        //{
        //    base.OnCreateControl();
        //    //InitializationControls();
        //}

        protected override void OnLayout(LayoutEventArgs e)
        {
            PositionControls();
            base.OnLayout(e);
        }

        protected abstract void PositionControls();

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            CreateFormMonitor();
        }

        protected virtual void CreateFormMonitor()
        {
            if (Form != null && Form.IsHandleCreated)
            {
                if (_formMonitor != null)
                {
                    if (_formMonitor.Handle != Form.Handle)
                    {
                        _formMonitor.ReleaseHandle();
                        _formMonitor.AssignHandle(Form.Handle);
                    }
                }
                else
                {
                    _formMonitor =/* _windowMonitor ?? */new FormMonitor(this);
                    //_windowMonitor.ReleaseHandle();
                    _formMonitor.AssignHandle(Form.Handle);
                }
            }
        }

        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            DestroyFormMonitor();
        }

        protected virtual void DestroyFormMonitor()
        {
            _formMonitor?.ReleaseHandle();
            _formMonitor?.DestroyHandle();
            _formMonitor = null;
        }

        protected virtual void WM_NCACTIVATE(ref Message m)
        {
            // 非客户区窗口失去焦点
            if (m.WParam == IntPtr.Zero)
            {
                _ncACTIVATE = false;
            }
            // 非客户区窗口获得焦点
            else
            {
                _ncACTIVATE = true;
            }
        }

        protected virtual void OnParentSizeChanged(EventArgs empty)
        {
            Invalidate(true);
        }

        void ICombineBox.WM_NCACTIVATE(ref Message m)
        {
            WM_NCACTIVATE(ref m);
        }

        void ICombineBox.OnParentSizeChanged(EventArgs args)
        {
            OnParentSizeChanged(args);
        }

        protected virtual void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
        {
           
        }

        //protected virtual MouseEventArgs TranslateMouseEvent(Control child, MouseEventArgs e)
        //{
        //    if (child != null && IsHandleCreated)
        //    {
        //        // same control as PointToClient or PointToScreen, just
        //        // with two specific controls in mind.
        //        Win32.POINT point = new Win32.POINT(e.X, e.Y);
        //        Win32.MapWindowPoints(new HandleRef(child, child.Handle), new HandleRef(this, Handle), point, 1);
        //        return new MouseEventArgs(e.Button, e.Clicks, point.x, point.y, e.Delta);
        //    }
        //    return e;
        //}

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);

            PositionControls();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);

                DestroyFormMonitor();
            }
        }

        internal class FormMonitor : NativeWindow
        {
            private ICombineBox combinePanel;

            public FormMonitor(ICombineBox captionButtons)
            {
                combinePanel = captionButtons;
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                switch (m.Msg)
                {
                    case Win32.WM_NCACTIVATE:
                        combinePanel.WM_NCACTIVATE(ref m);
                        break;
                    case Win32.WM_SIZE:
                        //  method one
                        //if (Control.FromHandle(m.HWnd).IsForm())
                        //{
                        //    var winstate = NativeMethodHelper.GetWindowState(m.HWnd);
                        //}

                        // method two
                        //switch ((int)m.WParam)
                        //{
                        //    // 窗口最大化
                        //    case Win32.WMSIZE_WParam.SIZE_MAXIMIZED:
                        //        break;
                        //    // 窗口最小化
                        //    case Win32.WMSIZE_WParam.SIZE_MINIMIZED:
                        //        break;
                        //    // 窗口恢复
                        //    case Win32.WMSIZE_WParam.SIZE_RESTORED:
                        //        break;
                        //}

                        combinePanel.OnParentSizeChanged(EventArgs.Empty);
                        break;
                    //case Win32.WM_NCHITTEST:
                    //    windowCaptionButtons.OnInvalidateChildControl(EventArgs.Empty);
                    //    break;
                    //case Win32.WM_DESTROY: // when destroy old handle

                    //    break;
                    default:
                        break;
                }



            }

            public override void DestroyHandle()
            {
                base.DestroyHandle();
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
                combinePanel = null;
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            }


        }

        internal interface ICombineBox
        {
            void WM_NCACTIVATE(ref Message m);
            void OnParentSizeChanged(EventArgs args);
        }

        public enum ControlState
        {
            /// <summary>
            /// 正常状态
            /// </summary>
            Normal = 0,
            /// <summary>
            ///  /鼠标进入
            /// </summary>
            Highlight = 1,
            /// <summary>
            /// 鼠标按下
            /// </summary>
            Down = 2,
            /// <summary>
            /// 获得焦点
            /// </summary>
            Focus = 4,
            /// <summary>
            /// 控件禁止
            /// </summary>
            Disabled = 8
        }


    }

    


}
