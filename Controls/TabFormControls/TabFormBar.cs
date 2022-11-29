using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.TabFormControl;

namespace System.Windows.Forms
{
    [DefaultProperty("SelectedObject")]
    [Designer(typeof(System.Windows.Forms.Design.TabFormBarDesigner), typeof(IDesigner))]
    public class TabFormBar : PanelBase
    {
        //internal class SelectedObjectConverter : ReferenceConverter
        //{
        //    public SelectedObjectConverter() : base(typeof(IComponent))
        //    {
        //    }
        //}

        internal class TabFormHeader : ControlBase
        {

        }

        private TabFormHeader tabFormHeader = null;

        private TabFormControl? currentSelectedObject = null;
        private int minTabWith = 20;
        private int maxTabWith = 80;
        private int tabRadius = 3;


        public TabFormBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        [TypeConverter(typeof(ReferenceConverter))]
        public Control SelectedObject
        {
            get
            {
                return currentSelectedObject;
            }
            set
            {
                if (value is not TabFormControl formControl/* || value != null*/)
                {
                    Console.WriteLine($"{DateTime.Now} : Failed to set the value of the property SelectedObject of the object TabFormSwitcher {value}");
                    
                    if(value == null)
                    {
                        currentSelectedObject = null;
                        Invalidate();
                    }
                    
                    return;
                }

                currentSelectedObject = formControl;

                Invalidate();
            }
        }

        internal IList<ITabFormPage> TabPages
        {
            get
            {
                if(SelectedObject is not TabFormControl control)
                {
                    return new List<ITabFormPage>();
                }

                return control.TabPages;
            }
        }

        internal int SelectedIndex
        {
            get
            {
                //int index = -1;

                //if(SelectedObject is TabFormControl control)
                //{
                //    return control.SelectedIndex;
                //}

                //return index;
                return currentSelectedObject?.SelectedIndex ?? -1;
            }
        }

        public int MinTabWith
        {
            get
            {
                return minTabWith;
            }
            set
            {
                minTabWith = value;
            }
        }

        public int MaxTabWith
        {
            get
            {
                return maxTabWith;
            }
            set
            {
                maxTabWith = value;
            }
        }

        public int TabRadius
        {
            get
            {
                return tabRadius;
            }
            set
            {
                tabRadius = value;
            }
        }

        public Drawing.ContentAlignment ImageAlign
        {
            get;set;
        }

        public TabAlignment TabAlignment
        {
            get; set;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case Win32.WM_PAINT:
                    WmPaintTabItem(ref m);
                    break;
            }
        }

        private void WmPaintTabItem(ref Message m)
        {
            using (Graphics graphics = Graphics.FromHwnd(m.HWnd))
            {
                OnPaintBackground(new PaintEventArgs(graphics, ClientRectangle));

                if (SelectedObject == null || TabPages == null)
                {
                    return;
                }

                if (TabPages.Count > 0)
                {
                    for (int index = 0; index < TabPages.Count; index++)
                    {
                        DrawTabItemEventArgs tabargs = new DrawTabItemEventArgs(graphics, Font, ClientRectangle, index, DrawItemState.Default);
                        SetTabItemBoundCoreInternel(tabargs);

                        OnDrawItem(tabargs);
                    }
                }
            }

        }

        protected virtual void OnDrawItem(DrawTabItemEventArgs e)
        {
            var g = e.Graphics;

            if (Application.RenderWithVisualStyles)
            {
                VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Normal);

                if(SelectedIndex >= 0 && e.Index == SelectedIndex)
                {
                    visualStyleRenderer.SetParameters(VisualStyleElement.Tab.TabItem.Hot);
                }

                visualStyleRenderer.DrawBackground(e.Graphics, e.ClientBounds);
                visualStyleRenderer.DrawEdge(e.Graphics, e.ClientBounds, Edges.Right, EdgeStyle.Bump, EdgeEffects.Mono);
                visualStyleRenderer.DrawText(e.Graphics, e.TextBounds, Text);
            }
            else
            {
                //ControlPaint.DrawButton(e.Graphics, e.Bounds, ButtonState.Normal);
                //g?.DrawString(Name, e.Font, Brushes.Black, e.Bounds);
            }

                

            
        }

        private void SetTabItemBoundCoreInternel(DrawTabItemEventArgs e)
        {
            int tabwidth = GetTabWidth();

            // Bounds
            e.Bounds = new Rectangle(
                e.Index * tabwidth + Padding.Left, 
                0, 
                tabwidth, 
                ClientSize.Height);

            // ClientBounds
            e.ClientBounds = new Rectangle(
                 e.Index * tabwidth + Padding.Left,
                 tabRadius,
                 tabwidth,
                 ClientSize.Height - tabRadius *2);

            // TextBounds
            TextMetrics textMetrics = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Normal).GetTextMetrics(e.Graphics);
            int topPadding = (ClientSize.Height - textMetrics.Height) / 2;

            e.TextBounds = new Rectangle(
                 e.Index * tabwidth + Padding.Left + 3,
                 topPadding,
                 tabwidth - 6,
                 textMetrics.Height);

            OnSetTabItemBoundCore(e);
        }

        protected virtual void OnSetTabItemBoundCore(DrawTabItemEventArgs e)
        {
            
        }

        private int GetTabWidth()
        {
            int width = (ClientSize.Width - Padding.Horizontal) / TabPages.Count;
            width = Math.Min(width, maxTabWith);
            width = Math.Max(width, minTabWith);

            return width;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            pevent.Graphics.Clear(BackColor);
        }

        protected override IControl[] SeedControsl()
        {
            return Array.Empty<IControl>();
        }

        protected override void PositionControls()
        {
            
        }



    }
}
