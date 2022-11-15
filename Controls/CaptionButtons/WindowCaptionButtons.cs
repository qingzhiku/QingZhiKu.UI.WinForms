using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Layout;
using System.Drawing;

namespace System.Windows.Forms
{
    //[ToolboxItem(false)]
    public class WindowCaptionButtons : Control, ICaptionButtonWrap
    {
        private Size _captionButtonSize /*= SystemInformation.CaptionButtonSize*/;
        private Size _captionIconSize/* = SystemInformation.SmallIconSize*/;
        private WindowMonitor? _windowMonitor ;

        public virtual Form Owner
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AnchorStyles Anchor { get=>base.Anchor; set=> base.Anchor = value; }

        public new ControlCollection Controls
        {
            private get
            {
                return base.Controls;
            }
            set
            {

            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CaptionButtonSize
        {
            get
            {
                return _captionButtonSize;
            }
            set
            {
                _captionButtonSize = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CaptionIconSize {
            get
            {
                return _captionIconSize;
            }
            set
            {
                _captionIconSize = value;
            }
        }

        public int Count
        {
            get
            {
                // default has close button
                int count = 1;

                if(Owner != null)
                {
                    if (Owner.MinimizeBox)
                    {
                        count++;
                    }

                    if (Owner.MaximizeBox)
                    {
                        count++;
                    }

                    //if (Owner.HelpButton)
                    //{
                    //    count++;
                    //}
                }

                return count;
            }
        }
        
        public virtual Color DownBackColor
        {
            get
            {
                Color dc;

                if (NativeMethodHelper.CheckAeroEnabled())
                {
                    dc = NativeMethodHelper.GetColorizationColor();
                }
                else
                {
                    dc = NativeMethodHelper.GetAccentColor();
                }

                return ControlPaint.Light(dc);
            }
        }

        public virtual Color IconColor
        {
            get
            {
                Color ic = SystemColors.WindowFrame;
                return ic;
            }
        }

        public virtual Color DownIconColor
        {
            get
            {
                Color ic = SystemColors.Window;
                return Color.White;
            }
        }

        public bool WindowNCActived
        {
            get
            {
                bool act = true;

                if(Owner is IWindowNCSatuts)
                {
                    act = ((IWindowNCSatuts)Owner).WindowNCActived;
                }
                
                return act;
            }
        }

        public FormWindowState PrevisionWindowState
        {
            get
            {
                var state = FormWindowState.Normal;

                if (Owner is IWindowNCSatuts)
                {
                    state = ((IWindowNCSatuts)Owner).PrevisionWindowState;
                }
                else if(Owner is Form)
                {
                    state = Owner.GetWindowState();
                }

                return state;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get => base.Text; set => base.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Name { get=>base.Name; set => base.Name = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size { get => base.Size; set => base.Size = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point Location { get => base.Location; set => base.Location = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int TabIndex { get => base.TabIndex; set => base.TabIndex = value; }

        public WindowCaptionButtons()
            : base()
        {
            Anchor = AnchorStyles.Right | AnchorStyles.Top;

            if (_captionButtonSize == Size.Empty)
                _captionButtonSize = new Size(44, 28);
            if (_captionIconSize == Size.Empty)
                _captionIconSize = new Size(10, 10);

            //if(_windowMonitor == null)
            //{
            //    _windowMonitor = new WindowMonitor();
            //}
        }

        protected override void OnCreateControl()
        {
            SetStyle(
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            base.OnCreateControl();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (_windowMonitor != null)
            {
                OnReCreateWindowMonitor();
            }

            base.OnParentChanged(e);
        }

        protected override void OnParentVisibleChanged(EventArgs e)
        {
            //if (DesignMode)
            //{
            //    return;
            //}

            OnAddSystemButtons(EventArgs.Empty);

            OnReCreateWindowMonitor();

            base.OnParentVisibleChanged(e);
        }

        protected virtual void OnReCreateWindowMonitor() 
        {
            if (Owner != null)
            {
                if (_windowMonitor != null)
                {
                    if (_windowMonitor.Handle != Owner.Handle)
                    {
                        _windowMonitor.ReleaseHandle();
                        _windowMonitor.AssignHandle(Owner.Handle);
                    }
                }
                else
                {
                    _windowMonitor =/* _windowMonitor ?? */new WindowMonitor(this);
                    //_windowMonitor.ReleaseHandle();
                    _windowMonitor.AssignHandle(Owner.Handle);
                }

            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            OnInvalidateChildControl(e);
        }

        protected virtual void OnParentSizeChanged(EventArgs e)
        {
            if (Owner != null)
            {
                foreach (Control control in this.Controls)
                {
                    var cb = ((WindowCaptionButton)control).CaptionButton;

                    if (cb == CaptionButton.Maximize || cb == CaptionButton.Restore)
                    {
                        control.Invalidate();
                    }
                }
            }
        }

        protected virtual void OnAddSystemButtons(EventArgs e) 
        {
            if (Parent == null || Owner == null)
                return;
            
            //if (!DesignMode)
            //{

            Parent.SuspendLayout();

            try {
                this.Controls.Add(new WindowCaptionButton(new CaptionButtonRender(this, CaptionButton.Close)) { Dock = DockStyle.Left, Width = _captionButtonSize.Width });
                if (Owner.MaximizeBox)
                    this.Controls.Add(new WindowCaptionButton(new CaptionButtonRender(this, CaptionButton.Maximize)) { Dock = DockStyle.Left, Width = _captionButtonSize.Width });
                if (Owner.MinimizeBox)
                    this.Controls.Add(new WindowCaptionButton(new CaptionButtonRender(this, CaptionButton.Minimize)) { Dock = DockStyle.Left, Width = _captionButtonSize.Width });

                OnInvalidateChildControl(e);
                SendToBack();
            }
            finally
            {
                Parent.ResumeLayout();
            }

            
            //}
        }

        protected virtual void OnInvalidateChildControl(EventArgs e)
        {
            //if (Owner != null)
            //{
            //    if(this.BackColor != Owner.BackColor)
            //    {
            //        this.BackColor = Owner.BackColor;
            //    }

            //    foreach (Control control in this.Controls)
            //    {
            //        control.Invalidate();
            //    }
            //}
            if (Parent != null)
            {
                if (this.BackColor != Parent.BackColor)
                {
                    this.BackColor = Parent.BackColor;
                }

                foreach (Control control in this.Controls)
                {
                    control.Invalidate();
                }
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            
           //Location = new Point(Owner.ClientSize.Width - Width, 1);

            if(Parent != null)
            {
                Location = new Point(Parent.ClientSize.Width - Width - Parent.Padding.Right, Parent.Padding.Top);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Height = _captionButtonSize.Height;
            Width = _captionButtonSize.Width * Count;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e); 
            
            Height = _captionButtonSize.Height;
            Width = _captionButtonSize.Width * Count;

            //Location = new Point(Owner.ClientSize.Width - Width, 1);
            if (Parent != null)
            {
                Location = new Point(Parent.ClientSize.Width - Width, Parent.Padding.Top);
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            Height = _captionButtonSize.Height;
            Width = _captionButtonSize.Width * Count;

            //Location = new Point(Owner.ClientSize.Width - Width, 1);
            if (Parent != null)
            {
                Location = new Point(Parent.ClientSize.Width - Width, Parent.Padding.Top);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Controls.Clear();
                _windowMonitor?.ReleaseHandle();
                _windowMonitor?.DestroyHandle();
            }
        }


        public class WindowCaptionButton : Control
        {
            private ICaptionButtonRender _captionButtonRender;
            private Win32.TRACKMOUSEEVENT? trackMouseEvent;

            public virtual Form Owner
            {
                get
                {
                    //                    IContainerControl icc = GetContainerControl();

                    //                    if (icc != null && icc is Form from1)
                    //                    {
                    //                        return from1;
                    //                    }

                    //                    if (Parent != null && Parent is Form from2)
                    //                    {
                    //                        return from2;
                    //                    }

                    //#pragma warning disable CS8603 // 可能返回 null 引用。
                    //                    return null;
                    //#pragma warning restore CS8603 // 可能返回 null 引用。
                    return FindForm();
                }
            }

            public CaptionButton CaptionButton { 
                get
                {
                    return _captionButtonRender.CaptionButton;
                }
            }

            public ICaptionButtonRender CaptionButtonRender
            { 
                get {
                    return _captionButtonRender;
                }
            }

            protected override CreateParams CreateParams {
                get
                {
                    var cp = base.CreateParams;

                    cp.Style |= Win32.WS_MINIMIZEBOX;
                    cp.Style |= Win32.WS_MAXIMIZEBOX;

                    return cp;
                }
            }

            public WindowCaptionButton(ICaptionButtonRender captionButtonRender)
            {
                DoubleBuffered = true;
                _captionButtonRender = captionButtonRender;
            }

            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                base.OnPaintBackground(pevent);
                _captionButtonRender.DrawButtonBackColor(pevent.Graphics);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                _captionButtonRender.DrawButton(e.Graphics);
            }

            protected override void OnMouseHover(EventArgs e)
            {
                base.OnMouseHover(e);
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case Win32.WM_MOUSEHOVER:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Hover;
                        Invalidate();
                        break;
                    case Win32.WM_MOUSELEAVE:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Normal;
                        Invalidate();
                        break;
                    case Win32.WM_LBUTTONDOWN:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Down;
                        Invalidate();
                        break;
                    case Win32.WM_LBUTTONUP:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Up;
                        break;
                    case Win32.WM_MOUSEMOVE:
                        //_captionButtonRender.ButtonMouseState = ButtonMouseState.Down;
                        //Invalidate();
                        return;
                    case Win32.WM_NCHITTEST:
                        var result = _captionButtonRender.NCHITTEST();

                        if(result.ToInt32() == Win32.NCHITTEST_Result.HTCLIENT)
                        {
                            base.WndProc(ref m);
                        }
                        else
                        {
                            m.Result = _captionButtonRender.NCHITTEST();
                        }
                        
                        break;
                    case Win32.WM_NCLBUTTONDOWN:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Down;
                        Invalidate();
                        break;
                    case Win32.WM_NCLBUTTONUP:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Up;
                        Invalidate();

                        var point = PointToClient(new Point(Win32.Util.LOWORD(m.LParam), Win32.Util.HIWORD(m.LParam)));
                        _captionButtonRender.OnMouseUp(new MouseEventArgs(MouseButtons.Left,1, point.X, point.Y, 0));
                        break;
                    case Win32.WM_NCMOUSEMOVE:
                        ResetNCMouseEventArgs();
                        //ResetMouseEventArgs();
                        break;
                    case Win32.WM_NCMOUSELEAVE:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Normal;
                        Invalidate();
                        break;
                    case Win32.WM_NCMOUSEHOVER:
                        _captionButtonRender.ButtonMouseState = ButtonMouseState.Hover;
                        Invalidate();
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }


            protected virtual void ResetNCMouseEventArgs()
            {
                if (trackMouseEvent == null)
                {
                    trackMouseEvent = new Win32.TRACKMOUSEEVENT();
                    trackMouseEvent.dwFlags = Win32.TME_HOVER | Win32.TME_LEAVE | Win32.TME_NONCLIENT;
                    trackMouseEvent.hwndTrack = Handle;
                }

                Win32.TrackMouseEvent(trackMouseEvent);
            }

        }

        public class WindowMonitor : NativeWindow
        {
            private WindowCaptionButtons windowCaptionButtons;

            public WindowMonitor(WindowCaptionButtons captionButtons)
            {
                windowCaptionButtons = captionButtons;
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                switch (m.Msg)
                {
                    case Win32.WM_NCACTIVATE:
                        windowCaptionButtons.OnInvalidateChildControl(EventArgs.Empty);
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

                        windowCaptionButtons.OnParentSizeChanged(EventArgs.Empty);
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
                windowCaptionButtons = null;
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            }

            
        }




    }


}
