using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;
using Microsoft.VisualBasic.Devices;

namespace System.Windows.Forms
{
    public class DrawTabItemEventArgs : DrawItemEventArgs
    {
        private Rectangle bounds;
        private Rectangle clientBounds;
        private Rectangle textBounds;
        private Rectangle imageBounds;
        private Rectangle buttonBounds;

        public DrawTabItemEventArgs(
            Graphics graphics, 
            Font? font, 
            Rectangle bound, 
            int index, 
            DrawItemState state) 
            : base(graphics, font, bound, index, state)
        {
            bounds = bound;
            ClientBounds = bound;
            TextBounds = bound;
        }

        /// <summary>
        /// TabFormSwitcher DispalyBounds
        /// </summary>
        public new Rectangle Bounds 
        { get => bounds; set => bounds = value; }

        /// <summary>
        /// Tab Item Bounds
        /// </summary>
        public Rectangle ClientBounds { get => clientBounds; set => clientBounds = value; }

        /// <summary>
        /// Tab Item Text Bounds
        /// </summary>
        public Rectangle TextBounds { get => textBounds; set => textBounds = value; }

        /// <summary>
        /// Tab Item Image Bounds
        /// </summary>
        public Rectangle ImageBounds { get => imageBounds; set => imageBounds = value; }

        /// <summary>
        /// Tab Item Button Bounds
        /// </summary>
        public Rectangle ButtonBounds { get => buttonBounds; set => buttonBounds = value; }


    }

}
