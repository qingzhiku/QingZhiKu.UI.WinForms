using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class CaptionButtonRender : ICaptionButtonRender
    {
        private ICaptionButtonWrap _captionButtonWrap;
        private CaptionButton _captionButton;
        private ButtonMouseState _buttonMouseState;

        public CaptionButtonRender(ICaptionButtonWrap wrap, CaptionButton button)
        {
            _captionButtonWrap = wrap;
            _captionButton = button;
            _buttonMouseState = ButtonMouseState.Normal;
        }

        public ICaptionButtonWrap CaptionButtonWrap
        {
            get
            {
                return _captionButtonWrap;
            }
        }

        public CaptionButton CaptionButton
        {
            get
            {
                // special maxbox draw type
                if(_captionButton == CaptionButton.Restore || _captionButton == CaptionButton.Maximize)
                {
                    if (CaptionButtonWrap.PrevisionWindowState == FormWindowState.Maximized)
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
                var bcolor = _captionButtonWrap.BackColor;

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
                return _captionButtonWrap.DownBackColor;
            }
        }

        public Color IconColor
        {
            get
            {
                var color = _captionButtonWrap.IconColor;
                return color;
            }
        }

        public Color DownIconColor
        {
            get
            {
                var color = _captionButtonWrap.DownIconColor;
                return color;
            }
        }

        public bool WindowNCActived
        {
            get
            {
                return _captionButtonWrap.WindowNCActived;
            }
        }

        public Rectangle IconRectangle
        {
            get
            {
                var icons = CaptionButtonWrap.CaptionIconSize;

                if (CaptionButton == CaptionButton.Minimize)
                {
                    icons.Height = 1;
                }

                var size = CaptionButtonWrap.CaptionButtonSize - icons;

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
                return new Rectangle(c1,new Size(3, 3));
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
            if (_captionButtonWrap == null || _captionButtonWrap.Owner == null)
                return;
            if (!_captionButtonWrap.Owner.IsHandleCreated || !_captionButtonWrap.Owner.Visible)
                return;

            switch (CaptionButton)
            {
                case CaptionButton.Close:
                    _captionButtonWrap.Owner.Close();
                    break;
                case CaptionButton.Help:
                    break;
                case CaptionButton.Maximize:
                    Win32.ShowWindow(_captionButtonWrap.Owner.Handle, Win32.SW_MAXIMIZE);
                    break;
                case CaptionButton.Minimize:
                    Win32.ShowWindow(_captionButtonWrap.Owner.Handle, Win32.SW_MINIMIZE);
                    break;
                case CaptionButton.Restore:
                    Win32.ShowWindow(_captionButtonWrap.Owner.Handle, Win32.SW_NORMAL);
                    break;
            }
        }


    }
}
