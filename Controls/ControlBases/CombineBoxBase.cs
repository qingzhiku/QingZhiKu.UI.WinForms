using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    [Designer(typeof(System.Windows.Forms.Design.CombineBoxBaseDesigner), typeof(IDesigner))]
    public abstract class CombineBoxBase : ContainerControlBase
    {
        private FormMonitor? _formMonitor;

        //public class CombineButton: ControlBase
        //{
            
        //}

        public virtual Form Owner
        {
            get
            {
                return FindForm();
            }
        }

        internal FormMonitor? Monitor
        {
            get
            {
                if (Owner == null)
                    return null;

                if (!Owner.IsHandleCreated )
                    return null;

                if(_formMonitor == null || _formMonitor.Handle != Owner.Handle)
                {
                    _formMonitor = new FormMonitor(this);
                    _formMonitor.AssignHandle(Owner.Handle);
                }

                //if (_formMonitor != null && _formMonitor.Handle != Owner.Handle)
                //{
                //    _formMonitor = new FormMonitor(this);
                //    _formMonitor.AssignHandle(Owner.Handle);
                //}

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
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            PositionControls();
            base.OnLayout(e);
        }

        protected abstract void PositionControls();

        protected virtual void OnParentSizeChanged(EventArgs empty)
        {
            
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

                _formMonitor?.ReleaseHandle();
                _formMonitor?.DestroyHandle();
                _formMonitor = null;
            }
        }

        internal class FormMonitor : NativeWindow
        {
            private CombineBoxBase combinePanel;

            public FormMonitor(CombineBoxBase captionButtons)
            {
                combinePanel = captionButtons;
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                switch (m.Msg)
                {
                    //case Win32.WM_NCACTIVATE:
                    //    windowCaptionButtons.OnInvalidateChildControl(EventArgs.Empty);
                    //    break;
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
