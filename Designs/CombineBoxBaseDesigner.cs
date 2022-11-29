using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Actions;
using Microsoft.DotNet.DesignTools.Designers.Behaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Design
{
    public class CombineBoxBaseDesigner: ControlDesigner
    {
        //private DesignerActionListCollection? actionLists;

        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules selectionRules = base.SelectionRules;
                return selectionRules & ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
            }
        }

        public override IList<SnapLine> SnapLines
        {
            get
            {
                IList<SnapLine> arrayList = base.SnapLines/* as ArrayList*/;
                int textBaseline = DesignerUtils.GetTextBaseline(Control, ContentAlignment.TopLeft);
                BorderStyle borderStyle = BorderStyle.Fixed3D;
                var propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["BorderStyle"];
                if (propertyDescriptor != null)
                {
                    borderStyle = (BorderStyle)propertyDescriptor.GetValue(base.Component);
                }
                textBaseline = ((borderStyle != 0) ? (textBaseline + 2) : (textBaseline - 1));
                arrayList.Add(new SnapLine(SnapLineType.Baseline, textBaseline, SnapLinePriority.Medium));
                return arrayList;
            }
        }

        //public override DesignerActionListCollection ActionLists
        //{
        //    get
        //    {
        //        if (actionLists == null)
        //        {
        //            actionLists = new DesignerActionListCollection();
        //            actionLists.Add(new NavigationPanelActionList(this));
        //        }
        //        return actionLists;
        //    }
        //}

        public CombineBoxBaseDesigner()
        { 
            base.AutoResizeHandles = true; 
        }



    }

    //internal class NavigationPanelActionList : DesignerActionList
    //{
    //    private NavigationPanelDesigner navigationPanelDesigner;

    //    public NavigationPanelActionList(NavigationPanelDesigner designer)
    //        : base(designer.Component)
    //    {
    //        this.navigationPanelDesigner = designer;
    //    }

    //    public override DesignerActionItemCollection GetSortedActionItems()
    //    {
    //        DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();

    //        return designerActionItemCollection;
    //    }


    //}

}
