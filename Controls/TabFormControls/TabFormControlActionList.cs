using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Actions;
using Microsoft.DotNet.DesignTools.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable CS8618
#pragma warning disable CS8600

namespace System.Windows.Forms.Design
{
    public class TabFormControlActionList : DesignerActionList
    {
        private DesignerActionUIService? designerActionUISvc;
        private TabFormControlDesigner owner;


        public TabFormControlActionList(TabFormControlDesigner designer) 
            : base(designer.Component)
        {
            owner = designer;
            designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        public void AddHeader()
        {
            owner?.OnAddHeader(owner, EventArgs.Empty);
        }

        public void RemoveHeader()
        {
            owner?.OnRemoveHeader(owner, EventArgs.Empty);
        }

        public void Add()
        {
            owner?.OnAdd(owner, EventArgs.Empty);
        }

        public void Remove()
        {
            owner?.OnRemove(owner, EventArgs.Empty);
        }


        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();

            //designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("CatAppearance")));
            //designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("CatInformation")));

            if (owner.Control.TabHeader == null)
            {
                designerActionItemCollection.Add(new DesignerActionMethodItem(this,
                        "AddHeader",
                        SR.GetString("TabControlAddHeader"),
                        SR.GetString("CatAppearance"),
                        "Add TabFormHeader",
                        false
                    ));
            }

            if (owner.Control.TabHeader != null)
            {
                designerActionItemCollection.Add(new DesignerActionMethodItem(this,
                    "RemoveHeader",
                    SR.GetString("TabControlRemoveHeader"),
                    SR.GetString("CatAppearance"),
                    "Remove TabFormHeader",
                    false
                ));
            }

            designerActionItemCollection.Add(new DesignerActionMethodItem(this,
                    "Add",
                    SR.GetString("TabControlAdd"),
                    SR.GetString("CatAppearance"),
                    "Add TabFormPage",
                    false
                ));

            if (owner.Control.TabCount > 0)
            {
                designerActionItemCollection.Add(new DesignerActionMethodItem(this,
                    "Remove",
                    SR.GetString("TabControlRemove"),
                    SR.GetString("CatAppearance"),
                    "Remove TabFormPage",
                    false
                ));
            }

            

            //designerActionItemCollection.Add(
            //    new DesignerActionPropertyItem(
            //        "TabHeaderVisiable",
            //        SR.GetString("TabHeaderDisplayName"),
            //        //SR.GetString("PropertiesCategoryName"), 
            //        //"Appearance",
            //        null,
            //        SR.GetString("TabHeaderDescription"))
            //    );

            //designerActionItemCollection.Add(new DesignerActionTextItem(SR.GetString("CatInformation"),
            //             SR.GetString("CatInformation")));

            return designerActionItemCollection;
        }

    }
}
