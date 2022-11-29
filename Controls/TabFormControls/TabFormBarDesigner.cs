using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Actions;
using System.ComponentModel;
using System.Design;
using System.Collections;

namespace System.Windows.Forms.Design
{
    public class TabFormBarDesigner : PanelBaseDesigner
    {
        protected class TabFormSwitcherDesignerActionList : DesignerActionList
        {
            private TabFormBarDesigner owner;

            [TypeConverter(typeof(ReferenceConverter))]
            public Control SelectedObject
            {
                get
                {
                    return (owner?.Component as TabFormBar)?.SelectedObject;
                }
                set
                {
                    if (value != null)
                    {
                        if (value.GetType() != typeof(TabFormControl))
                        {
                            Console.WriteLine($"{DateTime.Now} : Failed to set the value of the property SelectedObject of the object TabFormSwitcher {value}");
                            return;
                        }
                    }

                    GetPropertyByName("SelectedObject").SetValue(owner?.Component, value);

                    //owner?.RefreshSmartTag();
                }
            }

            public TabFormSwitcherDesignerActionList(TabFormBarDesigner designer) :
                base(designer.Component)
            {
                owner = designer;
            }

            public void Add()
            {
                var tfcdesigner = owner.GetTabFormControlDesigner();
                if (tfcdesigner == null)
                {
                    return;
                }

                tfcdesigner?.OnAdd(tfcdesigner, EventArgs.Empty);
            }

            public void Remove()
            {
                var tfcdesigner = owner.GetTabFormControlDesigner();
                if (tfcdesigner == null)
                {
                    return;
                }

                tfcdesigner?.OnRemove(tfcdesigner, EventArgs.Empty);
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();

                //designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("CatAppearance")));
                //designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("CatInformation")));

                designerActionItemCollection.Add(
                    new DesignerActionPropertyItem("SelectedObject",
                        SR.GetString("TabFormSwitcherSelectedObject")
                        //,SR.GetString("CatAppearance"),
                        //"Selected TabFormControl"
                    ));

                if (SelectedObject is not TabFormControl tabFormControl)
                {
                    return designerActionItemCollection;
                }

                designerActionItemCollection.Add(new DesignerActionMethodItem(this,
                    "Add",
                    SR.GetString("TabControlAdd"),
                    SR.GetString("CatAppearance"),
                    "Add TabFormPage",
                    false
                ));

                if (tabFormControl.TabCount > 0)
                {
                    designerActionItemCollection.Add(new DesignerActionMethodItem(this,
                        "Remove",
                        SR.GetString("TabControlRemove"),
                        SR.GetString("CatAppearance"),
                        "Remove TabFormPage",
                        false
                    ));
                }

                return designerActionItemCollection;
            }

            private PropertyDescriptor GetPropertyByName(String propName)
            {
                var prop = TypeDescriptor.GetProperties(owner.Component)[propName];
                if (null == prop)
                    throw new ArgumentException(
                         "Matching ColorLabel property not found!",
                          propName);
                else
                    return prop;
            }

        }

        private bool disableDrawGrid;
        private DesignerActionListCollection? actionLists;
        protected Pen BorderPen
        {
            get
            {
                Color color = (((double)Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(Control.BackColor) : ControlPaint.Dark(Control.BackColor));
                Pen pen = new Pen(color);
                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new TabFormSwitcherDesignerActionList(this));
                }
                return actionLists;
            }
        }

        public override TabFormBar Control
        {
            get
            {
                if (base.Control is TabFormBar control)
                {
                    return control;
                }

                return null;
            }
        }

        public TabFormBarDesigner()
        {
            base.AutoResizeHandles = true;
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            base.AutoResizeHandles = true;

            var selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
                selectionService.SelectionChanged += OnSelectionChanged;
            }

            var componentChangeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (componentChangeService != null)
            {
                componentChangeService.ComponentChanged += OnComponentChanged;
            }
        }

        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            base.BehaviorService.SyncSelection();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                if (selectionService != null)
                {
                    selectionService.SelectionChanged -= OnSelectionChanged;
                }
                var componentChangeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (componentChangeService != null)
                {
                    componentChangeService.ComponentChanged -= OnComponentChanged;
                }
            }

            base.Dispose(disposing);
        }

        private void OnComponentChanged(object? sender, ComponentChangedEventArgs e)
        {
            RefreshSmartTag();
        }


        public virtual TabFormControlDesigner TabFormControlDesigner
        {
            get
            {
                return GetTabFormControlDesigner();
            }
        }


        protected virtual void DrawBorder(Graphics graphics)
        {
            //Control control = Control;
            //Rectangle clientRectangle = control.ClientRectangle;
            //Color color = ((!((double)control.BackColor.GetBrightness() < 0.5)) ? ControlPaint.Dark(control.BackColor) : ControlPaint.Light(control.BackColor));
            //Pen pen = new Pen(color);
            //pen.DashStyle = DashStyle.Dash;
            //clientRectangle.Width--;
            //clientRectangle.Height--;
            //graphics.DrawRectangle(pen, clientRectangle);
            //pen.Dispose();

            Pen borderPen = BorderPen;
            Rectangle clientRectangle = Control.ClientRectangle;
            clientRectangle.Width--;
            clientRectangle.Height--;
            graphics.DrawRectangle(borderPen, clientRectangle);
            borderPen.Dispose();
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            try
            {
                disableDrawGrid = true;
                TabFormBar pictureBox = (TabFormBar)base.Component;
                //if (pictureBox.BorderStyle == BorderStyle.None)
                //{
                    DrawBorder(pe.Graphics);
                //}
                base.OnPaintAdornments(pe);

                Console.WriteLine($"TabFormControlDesigner : {TabFormControlDesigner}");
            }
            finally
            {
                disableDrawGrid = false;
            }
        }

        private TabFormControlDesigner GetTabFormControlDesigner()
        {
            TabFormControlDesigner? result = null;
            if (Control != null && Control.SelectedObject != null)
            {
                var designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (designerHost != null)
                {
                    result = designerHost.GetDesigner((IComponent)Control.SelectedObject) as TabFormControlDesigner;
                }
            }
            return result;
        }

        private void RefreshSmartTag()
        {
            (GetService(typeof(DesignerActionUIService)) as DesignerActionUIService)?.Refresh(base.Component);
        }



    }
}
