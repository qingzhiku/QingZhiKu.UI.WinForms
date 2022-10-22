using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms.Layout
{
    internal class TabFormControlLayout : LayoutEngine
    {
        public TabFormControlLayout()
            :base()
        {
        }

        public override void InitLayout(object child, BoundsSpecified specified)
        {
            base.InitLayout(child, specified);
        }

        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            var parent = container as TabFormControl;

            if(parent != null)
            {
                // Use DisplayRectangle
                Rectangle parentDisplayRectangle = parent.DisplayRectangle;
                Point nextControlLocation = parentDisplayRectangle.Location;

                var tabhd = parent.TabHeader;
                //bool hadth = parent.HasTabHeader();

                int width = parentDisplayRectangle.Width - parent.Padding.Horizontal;
                int headerHeight = tabhd ==null ? 0 : parent.MinHeaderHeight;

                //MessageBox.Show($"tabhd:{tabhd}");

                // TabFormHeader
                if (tabhd != null)
                {
                    nextControlLocation.Offset(parent.Padding.Left, parent.Padding.Top);
                    headerHeight = Math.Max(tabhd.Height, headerHeight);

                    if (tabhd.AutoSize)
                    {
                        tabhd.Size = tabhd.GetPreferredSize(parentDisplayRectangle.Size);
                    }

                    tabhd.Location = nextControlLocation;
                    tabhd.Size = new Size(width, headerHeight);

                    nextControlLocation.X = parentDisplayRectangle.X;
                    nextControlLocation.Y += headerHeight;
                }

                var selTab = parent.SelectedTab;

                // Selected
                if (selTab != null)
                {
                    nextControlLocation.Offset(parent.Padding.Left, tabhd == null ? parent.Padding.Top : -1);

                    selTab.Location = nextControlLocation;
                    selTab.Size = new Size(width,
                        parent.DisplayRectangle.Height - headerHeight - parent.Padding.Vertical);
                    
                }

                //Console.WriteLine($"selTab:{selTab?.Focused};tabhd:{tabhd?.Focused}");

                //foreach (Control c in parent.Controls)
                //{
                //    if (!c.Visible)
                //    {
                //        continue;
                //    }

                //    // Respect the margin
                //    nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);

                //if (c.AutoSize)
                //{
                //    c.Size = c.GetPreferredSize(parentDisplayRectangle.Size);
                //}

                //    // Set the location of the control.
                //    c.Location = nextControlLocation;

                //    nextControlLocation.X = parentDisplayRectangle.X;
                //    nextControlLocation.Y += c.Height + c.Margin.Bottom;
                //}

            }

            //return base.Layout(container, layoutEventArgs);
            return false;
        }


    }
}
