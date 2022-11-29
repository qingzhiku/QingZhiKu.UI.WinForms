using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.ButtonTextBox;

namespace System.Windows.Forms
{
    [Designer(typeof(System.Windows.Forms.Design.CombineBoxBaseDesigner), typeof(IDesigner))]
    public class ButtonTextBox : CombineBoxBase /*ContainerControlBase*/
    {
        internal TextEdit? textEdit;
        internal AlignmentButton? leftButton;
        internal AlignmentButton? rightButton;
        List<AlignmentButton> leftbuttons = new List<AlignmentButton>();
        List<AlignmentButton> rightbuttons = new List<AlignmentButton>();

        private const int DefaultWheelScrollLinesPerPage = 1;
        private const int DefaultButtonsWidth = 16;
        private const int DefaultControlWidth = 120;
        private const int ThemedBorderWidth = 1; // width of custom border we draw when themed
        private const BorderStyle DefaultBorderStyle = BorderStyle.Fixed3D;
        private static readonly bool DefaultInterceptArrowKeys = true;
        private const LeftRightAlignment DefaultUpDownAlign = LeftRightAlignment.Right;
        private const int DefaultTimerInterval = 500;

        private bool interceptArrowKeys = DefaultInterceptArrowKeys;
        private bool userEdit = false;
        private bool changingText = false;
        private ControlState controlState = ControlState.Normal;

        private Color focusColor;

        //private int leftButtonWidth = 0;
        //private int rightButtonWidth = 0;

        private BorderStyle borderStyle = DefaultBorderStyle;
        internal int defaultButtonsWidth = DefaultButtonsWidth;
        private LeftRightAlignment upDownAlign = DefaultUpDownAlign;

        public ButtonTextBox()
            :base()
        {
            //if (System.Windows.Forms.DpiHelper.IsScalingRequired)
            //{
            //    defaultButtonsWidth = LogicalToDeviceUnits(16);
            //}

            focusColor = Color.Blue;

            //navTextEdit = new NavTextEdit(this);
            //navTextEdit.BorderStyle = BorderStyle.None;
            //navTextEdit.AutoSize = false;
            //navTextEdit.Index = 2;
            //navTextEdit.TextAlign = HorizontalAlignment.Left;
            //navTextEdit.KeyDown += OnTextBoxKeyDown;
            //navTextEdit.KeyPress += OnTextBoxKeyPress;
            //navTextEdit.TextChanged += OnTextBoxTextChanged;
            //navTextEdit.GotFocus += OnTextBoxGotFocus;
            //navTextEdit.LostFocus += OnTextBoxLostFocus;
            //navTextEdit.Resize += OnTextBoxResize;

            //navigationLeftButton = new NavigationButton(this, LeftRightAlignment.Left);
            //navigationLeftButton.Width = SystemInformation.CaptionButtonSize.Width;
            ////navigationLeftButton.Visible = true;
            //navigationRightButton = new NavigationButton(this, LeftRightAlignment.Right);
            //navigationRightButton.Width = SystemInformation.CaptionButtonSize.Width;
            ////navigationRightButton.Visible = true;

            //base.Controls.AddRange(new Control[1] { navTextEdit/*, navigationLeftButton, navigationRightButton*/ });

            Padding = new Padding(0, 5,0,5);
            Font = new Font(Font.FontFamily, 11.0f);

            SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.FixedHeight, value: true);
            SetStyle(ControlStyles.StandardClick, value: false);
            SetStyle(ControlStyles.UseTextForAccessibility, value: false);

            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //UpdateStyles();

            //SetStyle(ControlStyles.FixedHeight, true);
            //SetStyle(ControlStyles.UserPaint | 
            //    ControlStyles.StandardClick | 
            //    ControlStyles.StandardDoubleClick | 
            //    ControlStyles.UseTextForAccessibility, value: false);

        }

        internal class TextEdit : TextBox,IControl
        {
            private ButtonTextBox parent;
            private bool doubleClickFired = false;
            private ControlState state = ControlState.Normal;

            internal TextEdit(ButtonTextBox parent) : base()
            {

                SetStyle(ControlStyles.FixedHeight |
                         ControlStyles.FixedWidth, true);

                SetStyle(ControlStyles.Selectable, false);

                this.parent = parent;
            }

            public override string Text
            {
                get
                {
                    return base.Text;
                }
                set
                {
                    bool valueChanged = (value != base.Text);
                    base.Text = value;
                    if (valueChanged)
                    {
                        AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
                    }
                }
            }

            internal ControlState ControlState
            {
                get
                {
                    return state;
                }
            }

            protected override AccessibleObject CreateAccessibilityInstance()
            {
                return new NavTextEditAccessibleObject(this, parent);
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    state = ControlState.Highlight;
                }

                if (e.Clicks == 2 && e.Button == MouseButtons.Left)
                {
                    doubleClickFired = true;
                }
                parent.OnMouseDown(parent.TranslateMouseEvent(this, e));
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (ClientRectangle.Contains(e.Location))
                    {
                        state = ControlState.Highlight;
                    }
                    else
                    {
                        state = ControlState.Focus;
                    }
                }

                Point pt = new Point(e.X, e.Y);
                pt = PointToScreen(pt);

                MouseEventArgs me = parent.TranslateMouseEvent(this, e);
                if (e.Button == MouseButtons.Left)
                {
                    if (Win32.WindowFromPoint(pt.X, pt.Y) == Handle)
                    {
                        if (!doubleClickFired)
                        {
                            parent.OnClick(me);
                            parent.OnMouseClick(me);
                        }
                        else
                        {
                            doubleClickFired = false;
                            parent.OnDoubleClick(me);
                            parent.OnMouseDoubleClick(me);
                        }
                    }
                    doubleClickFired = false;
                }

                parent.OnMouseUp(me);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                state = ControlState.Highlight;
                parent.OnMouseEnter(e);
                base.OnMouseEnter(e);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                if (state == ControlState.Highlight && Focused)
                {
                    state = ControlState.Focus;
                }
                else if (state == ControlState.Focus)
                {
                    state = ControlState.Focus;
                }
                else
                {
                    state = ControlState.Normal;
                }

                base.OnMouseLeave(e);
                parent.OnMouseLeave(e);
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                switch (m.Msg)
                {
                    case Win32.WM_CONTEXTMENU:
                        WMContextMenu(ref m);
                        break;
                    case Win32.WM_PAINT:
                    case Win32.WM_CTLCOLOREDIT:
                        parent.WndProc(ref m);
                        break;
                }
            }

            internal virtual void WMContextMenu(ref Message m)
            {
                object[] ojb = new object[] { m, this };
                // VSWhidbey 521337: want to make the SourceControl to be the UpDownBase, not the Edit.
                if (ContextMenuStrip != null)
                {
                    ojb = new object[] { m, parent };
                }

                var methods = this.GetMethod("WmContextMenu", false);

                if(methods != null)
                {
                    foreach (var item in methods)
                    {
                        var parames = item.GetParameters();
                        if(parames == null)
                            continue;
                        
                        if(parames.Length != 2)
                            continue;

                        var adjust = parames.Any(o => o.ParameterType == typeof(Message)) && parames.Any(o => o.ParameterType == typeof(Control));

                        if (adjust)
                        {
                            item.Invoke(this, ojb);
                            break;
                        }
                    }
                }
            }

            protected override void OnKeyUp(KeyEventArgs e)
            {
                parent.OnKeyUp(e);
            }

            protected override void OnGotFocus(EventArgs e)
            {
                parent.ActiveControl = this;
                parent.InvokeGotFocus(parent, e);
            }

            protected override void OnLostFocus(EventArgs e)
            {
                state = ControlState.Normal;
                parent.InvokeLostFocus(parent, e);
            }

            protected override void OnEnabledChanged(EventArgs e)
            {
                if (Enabled)
                {
                    state = ControlState.Normal;
                }
                else
                {
                    state = ControlState.Disabled;
                }
                base.OnEnabledChanged(e);
            }

            internal class NavTextEditAccessibleObject : ControlAccessibleObject
            {
                ButtonTextBox parent;

                public NavTextEditAccessibleObject(TextEdit owner, ButtonTextBox parent) : base(owner)
                {
                    this.parent = parent;
                }

                public override string? Name
                {
                    get
                    {
                        return parent?.AccessibilityObject?.Name;
                    }
                    set
                    {
                        if (null != parent?.AccessibilityObject)
                            parent.AccessibilityObject.Name = value;
                    }
                }

                public override string? KeyboardShortcut
                {
                    get
                    {
                        return parent?.AccessibilityObject?.KeyboardShortcut;
                    }
                }


            }

        }

        internal interface IAlignmentButton: IControl
        {
            LeftRightAlignment ButtonAlignment { get; set; }
        }

        internal protected class AlignmentButton : ControlBase, IAlignmentButton
        {
            private ButtonTextBox parent;

            private bool doubleClickFired = false;

            internal class NavigationButtonAccessibleObject : ControlAccessibleObject
            {

                public NavigationButtonAccessibleObject(AlignmentButton owner) : base(owner)
                {
                }

                public override string? Name
                {
                    get
                    {
                        return base.Name;
                    }
                    set
                    {
                        base.Name = value;
                    }
                }

                public override AccessibleRole Role
                {
                    get
                    {
                        AccessibleRole role = Owner.AccessibleRole;
                        if (role != AccessibleRole.Default)
                        {
                            return role;
                        }
                        return AccessibleRole.ButtonMenu;
                    }
                }



            }

            internal AlignmentButton(ButtonTextBox parent,LeftRightAlignment leftRightAlignment)
                : base()
            {
                SetStyle(ControlStyles.Opaque | ControlStyles.FixedHeight |
                         ControlStyles.FixedWidth, true);

                SetStyle(ControlStyles.Selectable, false);

                base.TabStop = false;
                ButtonAlignment = leftRightAlignment;
                this.parent = parent;
            }


            public LeftRightAlignment ButtonAlignment
            {
                get;
                set;
            }

            public new bool TabStop
            {
                get { return base.TabStop; }
                set { }
            }

            protected override AccessibleObject CreateAccessibilityInstance()
            {
                return new NavigationButtonAccessibleObject(this);
            }

            protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
            {
                base.SetBoundsCore(x, y, width, height, specified);

                if (IsHandleCreated && parent != null && Visible)
                {
                    parent.Invalidate();
                }
            }

            protected override void SetVisibleCore(bool value)
            {
                base.SetVisibleCore(value);

                if (IsHandleCreated && parent != null)
                {
                    parent.Invalidate();
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                this.parent.Focus();
                
                //if (/*!parent.ValidationCancelled && */e.Button == MouseButtons.Left)
                //{
                //    BeginButtonPress(e);
                //}
                if (e.Clicks == 2 && e.Button == MouseButtons.Left)
                {
                    doubleClickFired = true;
                }

                MouseCancelEventArgs mce = new MouseCancelEventArgs(this, e);
                parent.OnMouseDown(mce);
                if (mce.Cancel)
                    return;
                
                parent.OnMouseDown(parent.TranslateMouseEvent(this, e));
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {

                //// If the mouse is captured by the buttons (i.e. an updown button
                //// was pushed, and the mouse button has not yet been released),
                //// determine the new state of the buttons depending on where
                //// the mouse pointer has moved.

                //if (Capture)
                //{

                //    // Determine button area

                //    Rectangle rect = ClientRectangle;
                //    rect.Height /= 2;

                //    if (captured == ButtonID.Down)
                //    {
                //        rect.Y += rect.Height;
                //    }

                //    // Test if the mouse has moved outside the button area

                //    if (rect.Contains(e.X, e.Y))
                //    {

                //        // Inside button
                //        // Repush the button if necessary

                //        if (pushed != captured)
                //        {

                //            // Restart the timer
                //            StartTimer();

                //            pushed = captured;
                //            Invalidate();
                //        }

                //    }
                //    else
                //    {

                //        // Outside button
                //        // Retain the capture, but pop the button up whilst
                //        // the mouse remains outside the button and the
                //        // mouse button remains pressed.

                //        if (pushed != ButtonID.None)
                //        {

                //            // Stop the timer for updown events
                //            StopTimer();

                //            pushed = ButtonID.None;
                //            Invalidate();
                //        }
                //    }
                //}

                ////Logic for seeing which button is Hot if any
                //Rectangle rectUp = ClientRectangle, rectDown = ClientRectangle;
                //rectUp.Height /= 2;
                //rectDown.Y += rectDown.Height / 2;

                ////Check if the mouse is on the upper or lower button. Note that it could be in neither.
                //if (rectUp.Contains(e.X, e.Y))
                //{
                //    mouseOver = ButtonID.Up;
                //    Invalidate();
                //}
                //else if (rectDown.Contains(e.X, e.Y))
                //{
                //    mouseOver = ButtonID.Down;
                //    Invalidate();
                //}

                //// At no stage should a button be pushed, and the mouse
                //// not captured.
                //Debug.Assert(!(pushed != ButtonID.None && captured == ButtonID.None),
                //             "Invalid button pushed/captured combination");

                MouseCancelEventArgs mce = new MouseCancelEventArgs(this, e);
                parent.OnMouseMove(mce);
                if (mce.Cancel)
                    return;

                parent.OnMouseMove(parent.TranslateMouseEvent(this, e));
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                //if (!parent.ValidationCancelled && e.Button == MouseButtons.Left)
                //{
                //    EndButtonPress();
                //}

                Point pt = new Point(e.X, e.Y);
                pt = PointToScreen(pt);

                MouseCancelEventArgs mce = new MouseCancelEventArgs(this, e);
                MouseEventArgs me = parent.TranslateMouseEvent(this, e);
                if (e.Button == MouseButtons.Left)
                {
                    if (/*!parent.ValidationCancelled && */Win32.WindowFromPoint(pt.X, pt.Y) == Handle)
                    {
                        if (!doubleClickFired)
                        {
                            parent.OnClick(mce);
                            if (!mce.Cancel)
                            {
                                this.parent.OnClick(me);
                            }
                        }
                        else
                        {
                            doubleClickFired = false; 
                            parent.OnDoubleClick(mce);
                            if (!mce.Cancel)
                            {
                                this.parent.OnDoubleClick(me);
                            }
                            else
                            {
                                mce.Cancel = false;
                            }

                            parent.OnMouseDoubleClick(mce);
                            if (!mce.Cancel)
                            {
                                this.parent.OnMouseDoubleClick(me);
                            }
                        }
                    }
                    doubleClickFired = false;
                }

                parent.OnMouseUp(me);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                //mouseOver = ButtonID.None;
                Invalidate();

                ControlCancelEventArgs mce = new ControlCancelEventArgs(this, false);
                parent.OnMouseLeave(mce);
                if (mce.Cancel)
                    return;

                parent.OnMouseLeave(e);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                //base.OnPaint(e);

                PaintCancelEventArgs cancelEventArgs = new PaintCancelEventArgs(this, e);
                parent.OnChildPaint(cancelEventArgs);

                if (cancelEventArgs.Cancel)
                    return;

                e.Graphics.Clear(parent.BackColor);

                if (Application.RenderWithVisualStyles)
                {
                    VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);

                    //if (!Enabled)
                    //{
                    //    vsr.SetParameters(VisualStyleElement.Button.UserButton.Disabled);
                    //}

                    vsr.DrawBackground(e.Graphics, new Rectangle(Point.Empty,Size));

                }


                
            }





            //internal enum ButtonID
            //{
            //    None = 0,
            //    Up = 1,
            //    Down = 2,
            //}
        }

        public override bool Focused
        {
            get
            {
                bool focused = base.Focused;

                if (textEdit != null)
                {
                    focused = focused || textEdit.Focused;
                }

                return focused;
            }
        }

        public override Color BackColor
        {
            get
            {
                if (textEdit != null)
                {
                    return textEdit.BackColor;
                }

                return base.BackColor;
            }
            set
            {
                base.BackColor = value; // Don't remove this or you will break serialization. See VSWhidbey #517574

                if (textEdit != null)
                {
                    textEdit.BackColor = value;
                }

                Invalidate(); // VSWhidbey #335074
            }
        }

        public virtual Color FocusColor
        {
            get
            {
                return focusColor;
            }
            set
            {
                this.focusColor = value; // Don't remove this or you will break serialization. See VSWhidbey #517574
                //navTextEdit.BackColor = value;
                //Invalidate(); // VSWhidbey #335074
            }
        }

        public override Color ForeColor
        {
            get
            {
                return textEdit?.ForeColor??base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                textEdit.ForeColor = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
                this.textEdit.ContextMenuStrip = value;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.Style &= (~Win32.WS_BORDER);
                if (!Application.RenderWithVisualStyles)
                {
                    switch (borderStyle)
                    {
                        case BorderStyle.Fixed3D:
                            cp.ExStyle |= Win32.WS_EX_CLIENTEDGE;
                            break;
                        case BorderStyle.FixedSingle:
                            cp.Style |= Win32.WS_BORDER;
                            break;
                    }
                }
                return cp;
            }
        }

        //protected NavigationButton NavigationLeftButton
        //{ 
        //    get => navigationLeftButton;
        //}

        //protected NavigationButton NavigationRightButton
        //{ 
        //    get => navigationRightButton; 
        //}

        [
        SRCategory(SR.CatAppearance),
        DefaultValue(BorderStyle.Fixed3D),
        //DispId(Win32.ActiveX.DISPID_BORDERSTYLE),
        //SRDescription(SR.UpDownBaseBorderStyleDescr)
        ]
        public BorderStyle BorderStyle
        {
            get
            {
                return borderStyle;
            }

            set
            {
                //valid values are 0x0 to 0x2
                if (!ClientUtils.IsEnumValid(value, (int)value, (int)BorderStyle.None, (int)BorderStyle.Fixed3D))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
                }

                if (borderStyle != value)
                {
                    borderStyle = value;
                    RecreateHandle();
                }
            }
        }

        //[
        //Localizable(true),
        //SRCategory(SR.CatAppearance),
        //DefaultValue(LeftRightAlignment.Right),
        ////SRDescription(SR.UpDownBaseAlignmentDescr)
        //]
        //public LeftRightAlignment UpDownAlign
        //{

        //    get
        //    {
        //        return upDownAlign;
        //    }

        //    set
        //    {
        //        //valid values are 0x0 to 0x1
        //        if (!ClientUtils.IsEnumValid(value, (int)value, (int)LeftRightAlignment.Left, (int)LeftRightAlignment.Right))
        //        {
        //            throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
        //        }

        //        if (upDownAlign != value)
        //        {

        //            upDownAlign = value;
        //            PositionControls();
        //            Invalidate();
        //        }
        //    }
        //}

        [
        //SRCategory(SR.CatLayout),
        Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        //SRDescription(SR.UpDownBasePreferredHeightDescr)
        ]
        public override int PreferredHeight
        {
            get
            {
                int height = FontHeight;

                // Adjust for the border style
                if (borderStyle != BorderStyle.None)
                {
                    height += SystemInformation.BorderSize.Height * 4 /*+ 3*/;
                }
                else
                {
                    //height += 3;
                }

                height += Padding.Vertical;

                return height;
            }
        }

        public int LeftButtonWidth
        {
            get
            {
                int w = 0;
                foreach (var item in leftbuttons)
                {
                    if (item.Visible)
                    {
                        w += item.Width;    
                    }
                }

                return w;
            }
        }

        public int RightButtonWidth
        {
            get
            {
                int w = 0;
                foreach (var item in rightbuttons)
                {
                    if (item.Visible)
                    {
                        w += item.Width;
                    }
                }

                return w;
                //return rightButtonWidth;
            }
        }

        [
        SRCategory(SR.CatBehavior),
        DefaultValue(false),
        //SRDescription(SR.UpDownBaseReadOnlyDescr)
        ]
        public bool ReadOnly
        {

            get
            {
                return textEdit.ReadOnly;
            }

            set
            {
                textEdit.ReadOnly = value;
            }
        }

        protected bool UserEdit
        {
            get
            {
                return userEdit;
            }

            set
            {
                userEdit = value;
            }
        }

        protected bool ChangingText
        {
            get
            {
                return changingText;
            }

            set
            {
                changingText = value;
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(DefaultControlWidth, PreferredHeight);
            }
        }

        protected override IControl[] SeedControsl()
        {
            textEdit = new TextEdit(this);
            textEdit.BorderStyle = BorderStyle.None;
            textEdit.AutoSize = false;
            textEdit.TextAlign = HorizontalAlignment.Left;
            textEdit.KeyDown += OnTextBoxKeyDown;
            textEdit.KeyPress += OnTextBoxKeyPress;
            textEdit.TextChanged += OnTextBoxTextChanged;
            textEdit.GotFocus += OnTextBoxGotFocus;
            textEdit.LostFocus += OnTextBoxLostFocus;
            textEdit.Resize += OnTextBoxResize;

            leftButton = new AlignmentButton(this, LeftRightAlignment.Left);
            leftButton.Width = SystemInformation.CaptionButtonSize.Width;
            //navigationLeftButton.Visible = true;
            rightButton = new AlignmentButton(this, LeftRightAlignment.Right);
            rightButton.Width = SystemInformation.CaptionButtonSize.Width;
            //navigationRightButton.Visible = true;

            return new IControl[3] { textEdit, leftButton, rightButton };
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            PositionControls();
            //SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
        }

        //protected override void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
        //{
        //    if (pref.Category == UserPreferenceCategory.Locale)
        //    {
        //        //UpdateEditText();
        //    }
        //}

        //protected override MouseEventArgs TranslateMouseEvent(Control child, MouseEventArgs e)
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

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            controlState = ControlState.Highlight;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (controlState == ControlState.Highlight && Focused)
            {
                controlState = ControlState.Focus;
            }
            else if (controlState == ControlState.Focus)
            {
                controlState = ControlState.Focus;
            }
            else
            {
                controlState = ControlState.Normal;
            }

            base.OnMouseLeave(e);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                controlState = ControlState.Highlight;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(e.Location))
                {
                    controlState = ControlState.Highlight;
                }
                else
                {
                    controlState = ControlState.Focus;
                }
            }
            base.OnMouseUp(e);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is AlignmentButton button)
            {
                if (button.ButtonAlignment == LeftRightAlignment.Left)
                {
                    leftbuttons.Add(button);
                    //leftButtonWidth += button.Width;
                    leftbuttons.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));
                }
                else
                {
                    rightbuttons.Add(button);
                    //rightButtonWidth += button.Width;
                    rightbuttons.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));
                }
            }

            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (e.Control is AlignmentButton button)
            {
                if (button.ButtonAlignment == LeftRightAlignment.Left)
                {
                    leftbuttons.Remove(button);
                    //leftButtonWidth -= button.Width;
                    leftbuttons.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));
                }
                else
                {
                    rightbuttons.Remove(button);
                    //rightButtonWidth -= button.Width;
                    rightbuttons.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));
                }
            }

            base.OnControlRemoved(e);
        }

      

        //protected override void OnLayout(LayoutEventArgs e)
        //{
        //    PositionControls();
        //    base.OnLayout(e);
        //}

        protected override void PositionControls()
        {
            Rectangle textEditBounds = Rectangle.Empty,
                      upDownButtonsBounds = Rectangle.Empty;

            Rectangle clientArea = new Rectangle(Point.Empty, ClientSize);
            int totalClientWidth = clientArea.Width;
            bool themed = Application.RenderWithVisualStyles;
            BorderStyle borderStyle = BorderStyle;
            
            // determine how much to squish in - Fixed3d and FixedSingle have 2PX border
            int borderWidth = (borderStyle == BorderStyle.None) ? 0 : 2;
            clientArea.Inflate(-borderWidth, -borderWidth);

            int leftw = clientArea.X;

            foreach (var item in leftbuttons)
            {
                if (item.Visible)
                {
                    item.Bounds = new Rectangle(
                        leftw,
                        clientArea.Y,
                        item.Width,
                        clientArea.Height
                        );
                    leftw += item.Width;
                }
            }

            // Reposition and resize the upDownEdit control
            //
            if (textEdit != null)
            {
                textEditBounds.Location = new Point(LeftButtonWidth + clientArea.X, clientArea.Y+Padding.Top);
                textEditBounds.Size = new Size(clientArea.Width - LeftButtonWidth - RightButtonWidth, clientArea.Height- Padding.Vertical);
            }

            // Reposition and resize the updown buttons
            //
            //if (upDownButtons != null)
            //{
            //    int borderFixup = (themed) ? 1 : 2;
            //    if (borderStyle == BorderStyle.None)
            //    {
            //        borderFixup = 0;
            //    }
            //    upDownButtonsBounds = new Rectangle(/*x*/clientArea.Right - defaultButtonsWidth + borderFixup,
            //                                        /*y*/clientArea.Top - borderFixup,
            //                                        /*w*/defaultButtonsWidth,
            //                                        /*h*/clientArea.Height + (borderFixup * 2));
            //}

            // Right to left translation
            //LeftRightAlignment updownAlign = UpDownAlign;
            //updownAlign = RtlTranslateLeftRight(updownAlign);

            // left/right updown align translation
            //if (updownAlign == LeftRightAlignment.Left)
            //{
            //    // if the buttons are aligned to the left, swap position of text box/buttons
            //    upDownButtonsBounds.X = totalClientWidth - upDownButtonsBounds.Right;
            //    navTextEditBounds.X = totalClientWidth - navTextEditBounds.Right;
            //}

            // apply locations
            if (textEdit != null)
            {
                textEdit.Bounds = textEditBounds;
            }
            //if (upDownButtons != null)
            //{
            //    upDownButtons.Bounds = upDownButtonsBounds;
            //    upDownButtons.Invalidate();
            //}

            int lrightw = clientArea.Width - RightButtonWidth + clientArea.X;

            foreach (var item in rightbuttons)
            {
                if (item.Visible)
                {
                    item.Bounds = new Rectangle(
                    lrightw,
                    clientArea.Y,
                    item.Width,
                    clientArea.Height
                    );
                    lrightw += item.Width;

                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);

            //Rectangle editBounds = navTextEdit.Bounds;
            //if (Application.RenderWithVisualStyles)
            //{
            //    if (borderStyle != BorderStyle.None)
            //    {
            //        Rectangle bounds = ClientRectangle;
            //        Rectangle clipBounds = e.ClipRectangle;

            //        //Draw a themed textbox-like border, which is what the spin control does
            //        VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Assist);
            //        int border = ThemedBorderWidth;
            //        Rectangle clipLeft = new Rectangle(bounds.Left, bounds.Top, border, bounds.Height);
            //        Rectangle clipTop = new Rectangle(bounds.Left, bounds.Top, bounds.Width, border);
            //        Rectangle clipRight = new Rectangle(bounds.Right - border, bounds.Top, border, bounds.Height);
            //        Rectangle clipBottom = new Rectangle(bounds.Left, bounds.Bottom - border, bounds.Width, border);
            //        clipLeft.Intersect(clipBounds);
            //        clipTop.Intersect(clipBounds);
            //        clipRight.Intersect(clipBounds);
            //        clipBottom.Intersect(clipBounds);

            //        vsr.DrawBackground(e.Graphics, bounds, clipLeft/*, Handle*/);
            //        vsr.DrawBackground(e.Graphics, bounds, clipTop/*, Handle*/);
            //        vsr.DrawBackground(e.Graphics, bounds, clipRight/*, Handle*/);
            //        vsr.DrawBackground(e.Graphics, bounds, clipBottom/*, Handle*/);

            //        if(hasFocus || Focused)
            //        using (Pen pen = new Pen(FocusColor))
            //        {
            //            e.Graphics.DrawRectangle(pen, bounds);
            //        }

            //        //// Draw rectangle around edit control with background color
            //        using (Pen pen = new Pen(BackColor))
            //        {
            //            Rectangle backRect = editBounds;
            //            backRect.X--;
            //            backRect.Y--;
            //            backRect.Width++;
            //            backRect.Height++;
            //            e.Graphics.DrawRectangle(pen, backRect);
            //        }

            //    }
            //}
            //else
            //{
            //    // Draw rectangle around edit control with background color
            //    using (Pen pen = new Pen(BackColor, Enabled ? 2 : 1))
            //    {
            //        Rectangle backRect = editBounds;
            //        backRect.Inflate(1, 1);
            //        if (!Enabled)
            //        {
            //            backRect.X--;
            //            backRect.Y--;
            //            backRect.Width++;
            //            backRect.Height++;
            //        }
            //        e.Graphics.DrawRectangle(pen, backRect);
            //    }
            //}

            //if (!Enabled && BorderStyle != BorderStyle.None && !navTextEdit.BackColor.IsEmpty)
            //{
            //    //draws a grayed rectangled around the upDownEdit, since otherwise we will have a white
            //    //border around the upDownEdit, which is inconsistent with Windows' behavior
            //    //we only want to do this when BackColor is not serialized, since otherwise
            //    //we should display the backcolor instead of the usual grayed textbox.
            //    editBounds.Inflate(1, 1);
            //    ControlPaint.DrawBorder(e.Graphics, editBounds, SystemColors.Control, ButtonBorderStyle.Solid);
            //}



        }

        protected override void OnChildPaint(PaintCancelEventArgs e)
        {

        }

        protected virtual void OnMouseDown(MouseCancelEventArgs e)
        {
        }

        protected virtual void OnMouseMove(MouseCancelEventArgs e)
        {
        }

        protected virtual void OnMouseUp(MouseCancelEventArgs e)
        {
        }

        protected virtual void OnMouseLeave(ControlCancelEventArgs e)
        {

        }

        protected virtual void OnTextBoxKeyDown(object? source, KeyEventArgs e)
        {
            this.OnKeyDown(e);
            //if (interceptArrowKeys)
            //{

            //    // Intercept up arrow
            //    if (e.KeyData == Keys.Up)
            //    {
            //        UpButton();
            //        e.Handled = true;
            //    }

            //    // Intercept down arrow
            //    else if (e.KeyData == Keys.Down)
            //    {
            //        DownButton();
            //        e.Handled = true;
            //    }
            //}

            // Perform text validation if ENTER is pressed
            //
            if (e.KeyCode == Keys.Return && UserEdit)
            {
                ValidateEditText();
            }
        }

        protected virtual void ValidateEditText()
        {
        }

        protected virtual void OnTextBoxKeyPress(object? source, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);

        }

        protected virtual void OnTextBoxResize(object? source, EventArgs e)
        {
            this.Height = PreferredHeight;
            PositionControls();
        }

        protected virtual void OnTextBoxGotFocus(object? source, EventArgs e)
        {

        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            //Invalidate();
        }

        protected virtual void OnTextBoxLostFocus(object? source, EventArgs e)
        {
            if (UserEdit)
            {
                ValidateEditText();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if(textEdit != null)
            {
                if (!textEdit.Focused)
                {
                    controlState = ControlState.Normal;
                }
            }
            else
            {
                controlState = ControlState.Normal;
            }
            
            base.OnLostFocus(e);

            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                controlState = ControlState.Normal;
            }
            else
            {
                controlState = ControlState.Disabled;
            }
            base.OnEnabledChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            // Clear the font height cache
            FontHeight = -1;

            Height = PreferredHeight;
            PositionControls();

            base.OnFontChanged(e);
        }

        protected virtual void OnTextBoxTextChanged(object? source, EventArgs e)
        {
            if (changingText)
            {
                Debug.Assert(UserEdit == false, "OnTextBoxTextChanged() - UserEdit == true");
                ChangingText = false;
            }
            else
            {
                UserEdit = true;
            }

            this.OnTextChanged(e);
            OnChanged(source, new EventArgs());
        }

        protected virtual void OnChanged(object? source, EventArgs e)
        {
        }

        //protected override void WndProc(ref Message m)
        //{//TextBox是由系统进程绘制，重载OnPaint方法将不起作用

        //    base.WndProc(ref m);
        //    if (m.Msg == Win32.WM_PAINT || m.Msg == Win32.WM_CTLCOLOREDIT)
        //    {
        //        WmPaint(ref m);
        //    }
        //}

        protected override void WmPaint(ref Message m)
        {
            //Console.WriteLine($"navTextEdit:{navTextEdit.ControlState}");
            //Console.WriteLine($"controlState:{controlState}");

            //if(navTextEdit!= null)
            //{
            //    navTextEdit.Text = $"nTE:{navTextEdit.ControlState};cS:{controlState}";
            //}

            Graphics g = Graphics.FromHwnd(base.Handle);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (!Enabled)
            {
                controlState = ControlState.Disabled;
            }

            switch (controlState)
            {
                case ControlState.Normal:
                    DrawNormalTextBox(g);
                    break;
                case ControlState.Highlight:
                    DrawHighLightTextBox(g);
                    break;
                case ControlState.Focus:
                    DrawFocusTextBox(g);
                    break;
                case ControlState.Disabled:
                    DrawDisabledTextBox(g);
                    break;
                default:
                    break;
            }

            //if (Text.Length == 0 && !string.IsNullOrEmpty(EmptyTextTip) && !Focused)
            //{
            //    TextRenderer.DrawText(g, EmptyTextTip, Font, ClientRectangle, EmptyTextTipColor, GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
            //}
        }

        private void DrawNormalTextBox(Graphics g)
        {
            using (Pen borderPen = new Pen(Color.LightGray))
            {
                g.DrawRectangle(
                    borderPen,
                    new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1));
            }
        }

        private void DrawHighLightTextBox(Graphics g)
        {
            using (Pen highLightPen = new Pen(Color.FromArgb(110, 205, 253)))
            {
                Rectangle drawRect = new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1);

                g.DrawRectangle(highLightPen, drawRect);

                //InnerRect
                drawRect.Inflate(-1, -1);
                highLightPen.Color = Color.FromArgb(73, 172, 231);
                g.DrawRectangle(highLightPen, drawRect);
            }
        }

        private void DrawFocusTextBox(Graphics g)
        {
            using (Pen focusedBorderPen = new Pen(Color.FromArgb(73, 172, 231)))
            {
                g.DrawRectangle(
                    focusedBorderPen,
                    new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1));
            }
        }

        private void DrawDisabledTextBox(Graphics g)
        {
            using (Pen disabledPen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(disabledPen,
                    new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1));
            }
        }

        



    }
}
