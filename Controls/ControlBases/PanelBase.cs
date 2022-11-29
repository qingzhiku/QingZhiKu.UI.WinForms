using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.CombineBoxBase;

namespace System.Windows.Forms
{
    [Designer(typeof(System.Windows.Forms.Design.PanelBaseDesigner), typeof(IDesigner))]
    public abstract class PanelBase : ScrollableControl, IControl, IFilterControl, INotifyControl, CombineBoxBase.ICombineBox
    {
        #region Extend

        private FormMonitor? _formMonitor;
        private bool _ncACTIVATE = true;

        public Type[] FilterTypes => GetFilterTypes(EventArgs.Empty);

        public virtual Form Form
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
                if (null == _formMonitor && null != Form && !Form.IsHandleCreated)
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

                if (Form != null)
                {
                    state = Form.GetWindowState();
                }

                return state;
            }
        }

        protected PanelBase()
        {
            this.SetState2(STATE2_USEPREFERREDSIZECACHE, value: true); // 2048
            TabStop = false;
            SetStyle(ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint, value: false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);

            InitializationControls();
        }

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

        protected abstract IControl[] SeedControsl();

        protected override void OnLayout(LayoutEventArgs e)
        {
            PositionControls();
            base.OnLayout(e);
        }

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

        protected abstract void PositionControls();

        protected virtual void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
        {

        }

        protected virtual void OnChildPaint(PaintCancelEventArgs e)
        {

        }

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlBase.ControlCollection(this);
        }

        protected virtual void OnControlAdding(ControlCancelEventArgs e)
        {

        }

        protected virtual Type[] GetFilterTypes(EventArgs e)
        {
            return Array.Empty<Type>();
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
        #endregion

        #region Panel
        internal const int STATE2_USEPREFERREDSIZECACHE = 0x00000800;
        private BorderStyle borderStyle = System.Windows.Forms.BorderStyle.None;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        [Localizable(true)]
        //[SRCategory("CatLayout")]
        [Browsable(true)]
        [DefaultValue(AutoSizeMode.GrowOnly)]
        //[SRDescription("ControlAutoSizeModeDescr")]
        public virtual AutoSizeMode AutoSizeMode
        {
            get
            {
                return GetAutoSizeMode();
            }
            set
            {
                if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
                }
                if (GetAutoSizeMode() == value)
                {
                    return;
                }
                SetAutoSizeMode(value);
                if (Parent != null)
                {
                    if (Parent.LayoutEngine.GetType().FullName == "System.Windows.Forms.Layout.DefaultLayout")
                    {
                        Parent.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
                    }
                    LayoutTransaction.DoLayout(Parent, this, "AutoSize");
                }
            }
        }

        [SRDescription("PanelBorderStyleDescr")]
        [DefaultValue(BorderStyle.None)]
        [DispId(-504)]
        [SRCategory("CatAppearance")]
        public BorderStyle BorderStyle
        {
            get
            {
                return borderStyle;
            }
            set
            {
                if (borderStyle != value)
                {
                    if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
                    {
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
                    }
                    borderStyle = value;
                    UpdateStyles();
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 65536;
                createParams.ExStyle &= -513;
                createParams.Style &= -8388609;
                switch (borderStyle)
                {
                    case BorderStyle.Fixed3D:
                        createParams.ExStyle |= 512;
                        break;
                    case BorderStyle.FixedSingle:
                        createParams.Style |= 8388608;
                        break;
                }
                return createParams;
            }
        }

        [DefaultValue(false)]
        public new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [SRDescription("ControlOnAutoSizeChangedDescr")]
        [SRCategory("CatPropertyChanged")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler AutoSizeChanged
        {
            add
            {
                base.AutoSizeChanged += value;
            }
            remove
            {
                base.AutoSizeChanged -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event KeyEventHandler KeyUp
        {
            add
            {
                base.KeyUp += value;
            }
            remove
            {
                base.KeyUp -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event KeyEventHandler KeyDown
        {
            add
            {
                base.KeyDown += value;
            }
            remove
            {
                base.KeyDown -= value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event KeyPressEventHandler KeyPress
        {
            add
            {
                base.KeyPress += value;
            }
            remove
            {
                base.KeyPress -= value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TextChanged
        {
            add
            {
                base.TextChanged += value;
            }
            remove
            {
                base.TextChanged -= value;
            }
        }


        protected override void OnResize(EventArgs eventargs)
        {
            if (base.DesignMode && borderStyle == BorderStyle.None)
            {
                Invalidate();
            }
            base.OnResize(eventargs);
        }


        private static string StringFromBorderStyle(BorderStyle value)
        {
            Type borderStyleType = typeof(BorderStyle);
            return (ClientUtils.IsEnumValid(value, (int)value, (int)BorderStyle.None, (int)BorderStyle.Fixed3D)) ? (borderStyleType.ToString() + "." + value.ToString()) : "[Invalid BorderStyle]";
        }


        public override string ToString()
        {
            string s = base.ToString();
            return s + ", BorderStyle: " + StringFromBorderStyle(borderStyle);
        } 
        #endregion

    }
}
