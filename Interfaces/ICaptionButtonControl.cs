using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public interface ICaptionButtonWrap : IWindowNCSatuts
    {
        Size CaptionButtonSize { get; set; }
        Size CaptionIconSize { get; set; }
        Color BackColor { get; }
        Color DownBackColor { get; }
        Color IconColor { get; }
        Color DownIconColor { get; }
        Form Owner { get; }
    }

    public interface ICaptionButtonRender
    {
        ICaptionButtonWrap CaptionButtonWrap { get; }
        CaptionButton CaptionButton { get; }
        ButtonMouseState ButtonMouseState { get; set; }
        void DrawButtonBackColor(Graphics graphics);
        void DrawButton(Graphics graphics);
        IntPtr NCHITTEST();
        void OnMouseUp(MouseEventArgs args);
    }

    public interface IWindowNCSatuts
    {
        FormWindowState PrevisionWindowState { get; }
        bool WindowNCActived { get; }
    }

}
