using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Layout;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design;

namespace System.Windows.Forms
{
    //[ToolboxItem(false)]
    public class CaptionCommandBox : PanelBase
    {
        private Size _captionButtonSize /*= SystemInformation.CaptionButtonSize*/;
        private Size _iconSize/* = SystemInformation.SmallIconSize*/;
        private CommandButton? closeButton;
        private CommandButton? maximizeButton;
        private CommandButton? minimizeButton;

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override AnchorStyles Anchor { get=>base.Anchor; set=> base.Anchor = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CaptionButtonSize
        {
            get
            {
                if (_captionButtonSize == Size.Empty)
                    _captionButtonSize = new Size(44, 28);
                return _captionButtonSize;
            }
            set
            {
                _captionButtonSize = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size IconSize {
            get
            {
                if (_iconSize == Size.Empty)
                    _iconSize = new Size(10, 10);
                return _iconSize;
            }
            set
            {
                _iconSize = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public override int PreferredHeight
        {
            get
            {
                return Math.Max(Height, Math.Max(CaptionButtonSize.Height, IconSize.Height)) + 2;
            }
        }

        public CaptionCommandBox()
            //: base()
        {
            //Anchor = AnchorStyles.Right | AnchorStyles.Top;

            //if (_captionButtonSize == Size.Empty)
            //    _captionButtonSize = new Size(44, 28);
            //if (_captionIconSize == Size.Empty)
            //    _captionIconSize = new Size(10, 10);

            
        }

        protected override IControl[] SeedControsl()
        {
            closeButton = new CommandButton(new CaptionCommandButtonRender(this, CaptionButton.Close)) /*{ Dock = DockStyle.Left, Width = _captionButtonSize.Width }*/;
            maximizeButton = new CommandButton(new CaptionCommandButtonRender(this, CaptionButton.Maximize))/* { Dock = DockStyle.Left, Width = _captionButtonSize.Width }*/;
             minimizeButton = new CommandButton(new CaptionCommandButtonRender(this, CaptionButton.Minimize)) /*{ Dock = DockStyle.Left, Width = _captionButtonSize.Width }*/;

            //closeButton.Width = maximizeButton.Width = minimizeButton.Width = _captionButtonSize.Width;

            //this.Controls.Add(new WindowCaptionButton(new CaptionButtonRender(this, CaptionButton.Close)) { Dock = DockStyle.Left, Width = _captionButtonSize.Width });
            //if (Owner.MaximizeBox)
            //    this.Controls.Add(new WindowCaptionButton(new CaptionButtonRender(this, CaptionButton.Maximize)) { Dock = DockStyle.Left, Width = _captionButtonSize.Width });
            //if (Owner.MinimizeBox)
            //    this.Controls.Add(new WindowCaptionButton(new CaptionButtonRender(this, CaptionButton.Minimize)) { Dock = DockStyle.Left, Width = _captionButtonSize.Width });
            
            return new IControl[3] { closeButton, maximizeButton, minimizeButton };
        }

        protected override void PositionControls()
        {
            //Rectangle captionBounds = Rectangle.Empty;

            int left = 0, top = DesignMode?  1:0;
            int count = 0;

            if (null != minimizeButton && minimizeButton.Visible)
            {
                minimizeButton.Bounds = new Rectangle(
                    left,
                    top,
                    _captionButtonSize.Width,
                    _captionButtonSize.Height
                    );
                left += minimizeButton.Width;
                count++;
            }

            if (null != maximizeButton && maximizeButton.Visible)
            {
                maximizeButton.Bounds = new Rectangle(
                    left,
                    top,
                    _captionButtonSize.Width,
                    _captionButtonSize.Height
                    );
                left += maximizeButton.Width;
                count++;
            }

            if (null != closeButton)
            {
                closeButton.Bounds = new Rectangle(
                    left,
                    top,
                    _captionButtonSize.Width,
                    _captionButtonSize.Height
                    );
                //left += closeButton.Width;
                count++;
            }

            //captionBounds = new Rectangle(Location,new Size(_captionButtonSize.Width * count , _captionButtonSize.Height));

            //if (Parent != null)
            //{
            //    captionBounds.Location = new Point(Parent.ClientSize.Width - captionBounds.Width - Parent.Padding.Right-1, Parent.Padding.Top+1);
            //}

            //Height = _captionButtonSize.Height;
            //Width = _captionButtonSize.Width * count;

            Width = _captionButtonSize.Width * count + (DesignMode ? 1 : 0);

            //Bounds = captionBounds;

            //Invalidate(true);
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

            //CreateFormMonitor();
            //OnAddSystemButtons(EventArgs.Empty);
        }

        protected void ChangFormButtonVisiabled()
        {
            if (null == Form)
                return;

            if (null != maximizeButton && maximizeButton.Visible != Form.MaximizeBox)
            {
                maximizeButton.Visible = Form.MaximizeBox;
            }

            if (null != minimizeButton && minimizeButton.Visible != Form.MinimizeBox)
            {
                minimizeButton.Visible = Form.MinimizeBox;
            }
        }

        protected override void WmPaint(ref Message m)
        {
            ChangFormButtonVisiabled();
            base.WmPaint(ref m);
        }

        public class CommandButton : ControlBase,IControl
        {
            private CaptionCommandButtonRender _captionButtonRender;
            private Win32.TRACKMOUSEEVENT? trackMouseEvent;

            protected override CreateParams CreateParams {
                get
                {
                    var cp = base.CreateParams;

                    cp.Style |= Win32.WS_MINIMIZEBOX;
                    cp.Style |= Win32.WS_MAXIMIZEBOX;
                    //cp.ExStyle |= Win32.WS_EX_COMPOSITED;

                    return cp;
                }
            }

            public CommandButton(CaptionCommandButtonRender captionButtonRender)
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


        public class CaptionCommandButtonRender
        {
            private CaptionCommandBox _captionCommandBox;
            private CaptionButton _captionButton;
            private ButtonMouseState _buttonMouseState;

            public CaptionCommandButtonRender(CaptionCommandBox wrap, CaptionButton button)
            {
                _captionCommandBox = wrap;
                _captionButton = button;
                _buttonMouseState = ButtonMouseState.Normal;
            }

            public CaptionCommandBox CaptionCommandBox
            {
                get
                {
                    return _captionCommandBox;
                }
            }

            public CaptionButton CaptionButton
            {
                get
                {
                    // special maxbox draw type
                    if (_captionButton == CaptionButton.Restore || _captionButton == CaptionButton.Maximize)
                    {
                        if (CaptionCommandBox.PrevisionWindowState == FormWindowState.Maximized)
                        {
                            return CaptionButton.Restore;
                        }

                        return CaptionButton.Maximize;
                    }

                    return _captionButton;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ButtonMouseState ButtonMouseState
            {
                get
                {
                    return _buttonMouseState;
                }
                set
                {
                    _buttonMouseState = value;
                }
            }

            public Color BackColor
            {
                get
                {
                    var bcolor = _captionCommandBox.BackColor;

                    switch (ButtonMouseState)
                    {
                        case ButtonMouseState.Normal:
                            break;
                        case ButtonMouseState.Hover:
                            bcolor = ControlPaint.LightLight(bcolor);
                            break;
                        case ButtonMouseState.Down:
                            bcolor = DownBackColor;
                            break;
                        case ButtonMouseState.Up:
                            break;
                    }

                    //return _captionButtonWrap.BackColor;
                    return bcolor;
                }
            }

            public Color DownBackColor
            {
                get
                {
                    return _captionCommandBox.DownBackColor;
                }
            }

            public Color IconColor
            {
                get
                {
                    var color = _captionCommandBox.IconColor;
                    return color;
                }
            }

            public Color DownIconColor
            {
                get
                {
                    var color = _captionCommandBox.DownIconColor;
                    return color;
                }
            }

            public bool WindowNCActived
            {
                get
                {
                    return _captionCommandBox.WindowNCActived;
                }
            }

            public Rectangle IconRectangle
            {
                get
                {
                    var icons = CaptionCommandBox.IconSize;

                    if (CaptionButton == CaptionButton.Minimize)
                    {
                        icons.Height = 1;
                    }

                    var size = CaptionCommandBox.CaptionButtonSize - icons;

                    var left = size.Width / 2 + (size.Width % 2 > 0 ? 1 : 0);
                    var top = size.Height / 2 + (size.Height % 2 > 0 ? 1 : 0);

                    return new Rectangle(new Point(left, top), icons);
                }
            }

            public Rectangle IconCenterRectangle
            {
                get
                {
                    var c1 = IconRectangle.Center();
                    c1.Offset(-1, -1);
                    return new Rectangle(c1, new Size(3, 3));
                }
            }

            public Pen CoarsePen
            {
                get
                {
                    var color = IconColor;
                    switch (ButtonMouseState)
                    {
                        case ButtonMouseState.Down:
                            color = DownIconColor;
                            break;
                        default:
                            color = ControlPaint.LightLight(color);
                            if (!WindowNCActived)
                            {
                                color = ControlPaint.LightLight(color);
                            }
                            break;
                    }

                    return new Pen(color, 2f);
                }
            }

            public Pen FinePen
            {
                get
                {
                    var color = IconColor;
                    switch (ButtonMouseState)
                    {
                        case ButtonMouseState.Down:
                            color = DownIconColor;
                            break;
                        default:
                            if (!WindowNCActived)
                            {
                                color = ControlPaint.Light(color);
                            }
                            else
                            {
                                color = ControlPaint.DarkDark(color);
                            }
                            break;
                    }
                    return new Pen(color, 1);
                }
            }

            public Brush CoarseBrush
            {
                get
                {
                    var color = IconColor;
                    switch (ButtonMouseState)
                    {
                        case ButtonMouseState.Down:
                            color = DownIconColor;
                            break;
                        default:
                            color = ControlPaint.LightLight(color);
                            break;
                    }
                    return new SolidBrush(color);
                }
            }

            public Brush FineBrush
            {
                get
                {
                    var color = IconColor;
                    switch (ButtonMouseState)
                    {
                        case ButtonMouseState.Down:
                            color = DownIconColor;
                            break;
                        default:
                            if (!WindowNCActived)
                            {
                                color = ControlPaint.Light(color);
                            }
                            else
                            {
                                color = ControlPaint.Dark(color);
                            }
                            break;
                    }
                    return new SolidBrush(color);
                }
            }

            public void DrawButtonBackColor(Graphics graphics)
            {
                //switch (ButtonMouseState)
                //{
                //    case ButtonMouseState.Normal:
                //        graphics.Clear(BackColor);
                //        break;
                //    case ButtonMouseState.Hover:
                //        graphics.Clear(ControlPaint.LightLight(BackColor));
                //        break;
                //    case ButtonMouseState.Down:
                //        //graphics.Clear(SystemColors.ActiveCaption);
                //        graphics.Clear(DownBackColor);
                //        break;
                //    case ButtonMouseState.Up:
                //        break;
                //    default:
                //        graphics.Clear(BackColor);
                //        break;
                //}

                graphics.Clear(BackColor);
            }

            public IntPtr NCHITTEST()
            {
                int result = Win32.NCHITTEST_Result.HTCLIENT;

                switch (CaptionButton)
                {
                    case CaptionButton.Close:
                        result = Win32.NCHITTEST_Result.HTCLOSE;
                        break;
                    case CaptionButton.Restore:
                        result = Win32.NCHITTEST_Result.HTMAXBUTTON;
                        break;
                    case CaptionButton.Maximize:
                        result = Win32.NCHITTEST_Result.HTMAXBUTTON;
                        break;
                    case CaptionButton.Minimize:
                        result = Win32.NCHITTEST_Result.HTMINBUTTON;
                        break;
                    case CaptionButton.Help:
                        result = Win32.NCHITTEST_Result.HTHELP;
                        break;
                }

                return new IntPtr(result);
            }

            public void DrawButton(Graphics graphics)
            {
                switch (CaptionButton)
                {
                    case CaptionButton.Close:
                        DrawCloseButton(graphics);
                        break;
                    case CaptionButton.Restore:
                        DrawRestoreButton(graphics);
                        break;
                    case CaptionButton.Maximize:
                        DrawMaximizeButton(graphics);
                        break;
                    case CaptionButton.Minimize:
                        DrawMinimizeButton(graphics);
                        break;
                    case CaptionButton.Help:
                        DrawHelpButton(graphics);
                        break;
                }
            }

            protected virtual void DrawCloseButton(Graphics graphics)
            {
                var bounds = IconRectangle;

                if (ButtonMouseState != ButtonMouseState.Down)
                    graphics.DrawLines(CoarsePen, new Point[] { bounds.Location, bounds.RBLocation() });
                graphics.DrawLines(FinePen, new Point[] { bounds.Location, bounds.RBLocation() });

                if (ButtonMouseState != ButtonMouseState.Down)
                    graphics.DrawLines(CoarsePen, new Point[] { bounds.RTLocation(), bounds.LBLocation() });
                graphics.DrawLines(FinePen, new Point[] { bounds.RTLocation(), bounds.LBLocation() });

                //draw center dot
                graphics.FillRectangle(FineBrush, IconCenterRectangle);
            }

            protected virtual void DrawMaximizeButton(Graphics graphics)
            {
                var bounds = IconRectangle;
                
                //if (ButtonMouseState != ButtonMouseState.Down)
                //    graphics.DrawRectangle(CoarsePen, bounds);
                graphics.DrawRectangle(FinePen, bounds);
            }

            protected virtual void DrawMinimizeButton(Graphics graphics)
            {
                var bounds = IconRectangle;
                graphics.FillRectangle(FineBrush, bounds);
            }

            protected virtual void DrawRestoreButton(Graphics graphics)
            {
                //var coarsepen = new Pen(ControlPaint.LightLight(SystemColors.WindowFrame), 2);
                //var finepen = new Pen(ControlPaint.Dark(SystemColors.WindowFrame), 1);

                //var bounds = new Rectangle(3, 3, 10, 10);
                var bounds = IconRectangle;

                var bound2 = bounds;
                var bd = graphics.ClipBounds;
                bound2.Inflate(-1, -1);
                bound2.Offset(2, -1);
                //graphics.DrawRectangle(new Pen(ControlPaint.LightLight(coarsepen.Color),1.5f), bound2);
                graphics.DrawRectangle(FinePen, bound2);

                bound2 = bounds;
                bound2.Inflate(-1, -1);
                bound2.Offset(-1, 2);
                graphics.SetClip(bound2);
                graphics.Clear(BackColor);
                graphics.SetClip(bd);
                graphics.DrawRectangle(FinePen, bound2);
            }

            protected virtual void DrawHelpButton(Graphics graphics)
            {

            }

            public void OnMouseUp(MouseEventArgs args)
            {
                if (_captionCommandBox == null || _captionCommandBox.Form == null)
                    return;
                if (!_captionCommandBox.Form.IsHandleCreated || !_captionCommandBox.Form.Visible)
                    return;

                switch (CaptionButton)
                {
                    case CaptionButton.Close:
                        _captionCommandBox.Form.Close();
                        break;
                    case CaptionButton.Help:
                        break;
                    case CaptionButton.Maximize:
                        Win32.ShowWindow(_captionCommandBox.Form.Handle, Win32.SW_MAXIMIZE);
                        break;
                    case CaptionButton.Minimize:
                        Win32.ShowWindow(_captionCommandBox.Form.Handle, Win32.SW_MINIMIZE);
                        break;
                    case CaptionButton.Restore:
                        Win32.ShowWindow(_captionCommandBox.Form.Handle, Win32.SW_NORMAL);
                        break;
                }
            }


        }



    }

}
