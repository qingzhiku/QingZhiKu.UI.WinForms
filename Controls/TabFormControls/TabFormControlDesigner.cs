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
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable CS8618
#pragma warning disable CS8600

namespace System.Windows.Forms.Design
{
    public class TabFormControlDesigner : ParentControlDesigner
    {
        private bool tabControlSelected;

        private DesignerVerbCollection verbs;
        private DesignerActionListCollection actionLists;

        private DesignerVerb removeVerb;
        private DesignerVerb addHeaderVerb;
        private DesignerVerb removeHeaderVerb;

        private bool disableDrawGrid;

        //private int persistedSelectedIndex;

        private bool addingOnInitialize;

        private bool forwardOnDrag;

        //protected override bool AllowControlLasso => false;

        //protected override bool DrawGrid
        //{
        //    get
        //    {
        //        if (disableDrawGrid)
        //        {
        //            return false;
        //        }
        //        return base.DrawGrid;
        //    }
        //}

        //public override bool ParticipatesWithSnapLines
        //{
        //    get
        //    {
        //        if (!forwardOnDrag)
        //        {
        //            return false;
        //        }
        //        return GetSelectedTabPageDesigner()?.ParticipatesWithSnapLines ?? true;
        //    }
        //}

        public override TabFormControl Control
        {
            get
            {
                if(base.Control is TabFormControl control)
                {
                    return control;
                }

                return null;
            }
        }

        internal static ITabFormPage GetTabPageOfComponent(TabFormControl parent, object comp)
        {
            if (!(comp is Control))
            {
                return null;
            }
            for (Control control = (Control)comp; control != null; control = control.Parent)
            {
                if (control is ITabFormPage tabPage && tabPage.Parent == parent)
                {
                    Console.WriteLine($"{DateTime.Now} : TabFormControlDesigner  get TabFromPage of component {control}");
                    return tabPage;
                }
            }
            return null;
        }


        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            base.AutoResizeHandles = true;

            ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
            if (selectionService != null)
            {
                selectionService.SelectionChanged += OnSelectionChanged;
            }

            IComponentChangeService componentChangeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (componentChangeService != null)
            {
                componentChangeService.ComponentChanged += OnComponentChanged;
            }

            var tabControl = component as TabFormControl;
            base.BehaviorService.SyncSelection();
            if (tabControl != null)
            {
                //tabControl.SelectedIndexChanged += OnTabSelectedIndexChanged;
                tabControl.GotFocus += OnGotFocus;
                //tabControl.RightToLeftLayoutChanged += OnRightToLeftLayoutChanged;
                //tabControl.ControlAdded += OnControlAdded;
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
                if (selectionService != null)
                {
                    selectionService.SelectionChanged -= OnSelectionChanged;
                }
                IComponentChangeService componentChangeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (componentChangeService != null)
                {
                    componentChangeService.ComponentChanged -= OnComponentChanged;
                }
                if (Control is TabFormControl tabControl)
                {
                    //tabControl.SelectedIndexChanged -= OnTabSelectedIndexChanged;
                    //tabControl.GotFocus -= OnGotFocus;
                    //tabControl.RightToLeftLayoutChanged -= OnRightToLeftLayoutChanged;
                    //tabControl.ControlAdded -= OnControlAdded;
                }
            }
            base.Dispose(disposing);
        }

        //protected override bool GetHitTest(Point point)
        //{
        //    TabFormControl tabControl = (TabFormControl)Control;
        //    if (tabControlSelected)
        //    {
        //        Point pt = Control.PointToClient(point);
        //        return !tabControl.DisplayRectangle.Contains(pt);
        //    }
        //    return false;
        //}

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    removeVerb = new DesignerVerb(System.Design.SR.GetString("TabControlRemove", CultureInfo.CurrentCulture), OnRemove);
                    addHeaderVerb = new DesignerVerb(SR.GetString("TabControlAddHeader"), OnAddHeader);
                    removeHeaderVerb = new DesignerVerb(SR.GetString("TabControlRemoveHeader"), OnRemoveHeader);
                    //removeVerb = CreateVerb(SR.GetString("TabControlRemove"), OnRemove);
                    verbs = new DesignerVerbCollection();
                    verbs.Add(addHeaderVerb);
                    verbs.Add(removeHeaderVerb);
                    verbs.Add(new DesignerVerb(SR.GetString("TabControlAdd"), OnAdd));
                    //verbs.Add(CreateVerb(SR.GetString("TabControlAdd"), OnAdd));
                    verbs.Add(removeVerb);
                }

                if (Control != null)
                {
                    addHeaderVerb.Enabled = Control.TabHeader == null;
                    removeHeaderVerb.Enabled = Control.TabHeader != null;
                    removeVerb.Enabled = Control.TabCount > 0;
                }



                return verbs;
            }
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new TabFormControlActionList(this));
                }
                return actionLists;
            }
        }

        //public override bool CanParent(Control control)
        //{
        //    if (control is TabFormPage)
        //    {
        //        return !Control.Contains(control);
        //    }
        //    if (control is TabFormHeader)
        //    {
        //        return !Control.Contains(control);
        //    }
        //    return false;
        //}

        public override void InitializeNewComponent(IDictionary? defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            try
            {
                addingOnInitialize = true;
                //OnAddHeader(this, EventArgs.Empty);
                //OnAdd(this, EventArgs.Empty);
                //OnAdd(this, EventArgs.Empty);
            }
            finally
            {
                addingOnInitialize = false;
            }
            MemberDescriptor member = TypeDescriptor.GetProperties(base.Component)["Controls"];
            RaiseComponentChanging(member);
            RaiseComponentChanged(member, null, null);
            TabFormControl tabControl = (TabFormControl)base.Component;
            tabControl.Padding = new Padding(3);
            if (tabControl != null && tabControl.SelectedIndex > 0)
            {
                tabControl.SelectedIndex = 0;
            }

        }

        internal void OnAddHeader(object? sender, EventArgs e)
        {
            var tabControl = base.Component as TabFormControl;
            if (tabControl == null)
            {
                return;
            }

            if(tabControl.TabHeader != null)
            {
                return;
            }

            var designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost == null)
            {
                return;
            }

            DesignerTransaction designerTransaction = null;
            try
            {
                try
                {
                    designerTransaction = designerHost.CreateTransaction(SR.GetString("TabControlAddHeader"));
                }
                catch (CheckoutException ex)
                {
                    if (ex == CheckoutException.Canceled)
                    {
                        return;
                    }
                    throw ex;
                }
                var member = TypeDescriptor.GetProperties(tabControl)["Controls"];
                TabFormHeader tabHeader = (TabFormHeader)designerHost.CreateComponent(typeof(TabFormHeader));
                if (!addingOnInitialize)
                {
                    RaiseComponentChanging(member);
                }
                tabHeader.Padding = new Padding(3);
                tabHeader.Dock = DockStyle.Top;
                tabHeader.Height = tabControl.MinHeaderHeight;
                string text = null;
                var propertyDescriptor = TypeDescriptor.GetProperties(tabHeader)["Name"];
                if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
                {
                    text = (string)propertyDescriptor.GetValue(tabHeader);
                }
                if (text != null)
                {
                    TypeDescriptor.GetProperties(tabHeader)["Text"]?.SetValue(tabHeader, text);
                }
                //var propertyDescriptor2 = TypeDescriptor.GetProperties(tabPage)["UseVisualStyleBackColor"];
                //if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(bool) && !propertyDescriptor2.IsReadOnly && propertyDescriptor2.IsBrowsable)
                //{
                //    propertyDescriptor2.SetValue(tabPage, true);
                //}
                tabControl.Controls.Add(tabHeader);
                //tabControl.SelectedIndex = tabControl.TabCount - 1;

                if (!addingOnInitialize)
                {
                    RaiseComponentChanged(member, null, null);
                }
            }
            finally
            {
                designerTransaction?.Commit();
            }
        }

        internal void OnAdd(object? sender, EventArgs e)
        {
            //TabFormControl tabControl = (TabFormControl)base.Component;
            var tabControl = base.Component as TabFormControl;
            if (tabControl == null)
            {
                return;
            }

            var designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost == null)
            {
                return;
            }

            DesignerTransaction designerTransaction = null;
            try
            {
                try
                {
                    designerTransaction = designerHost.CreateTransaction(SR.GetString("TabControlAddTab", base.Component?.Site?.Name ?? ""));
                }
                catch (CheckoutException ex)
                {
                    if (ex == CheckoutException.Canceled)
                    {
                        return;
                    }
                    throw ex;
                }
                var member = TypeDescriptor.GetProperties(tabControl)["Controls"];
                var tabPage = (ITabFormPage)designerHost.CreateComponent(tabControl.GetTypeOfTabFormPage());
                if (!addingOnInitialize)
                {
                    RaiseComponentChanging(member);
                }
                tabPage.Padding = new Padding(3);
                string text = null;
                var propertyDescriptor = TypeDescriptor.GetProperties(tabPage)["Name"];
                if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
                {
                    text = (string)propertyDescriptor.GetValue(tabPage);
                }
                if (text != null)
                {
                    TypeDescriptor.GetProperties(tabPage)["Text"]?.SetValue(tabPage, text);
                }
                var propertyDescriptor2 = TypeDescriptor.GetProperties(tabPage)["UseVisualStyleBackColor"];
                if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(bool) && !propertyDescriptor2.IsReadOnly && propertyDescriptor2.IsBrowsable)
                {
                    propertyDescriptor2.SetValue(tabPage, true);
                }
                tabControl.Controls.Add((Control)tabPage);
                tabControl.SelectedIndex = tabControl.TabCount - 1;

                if (!addingOnInitialize)
                {
                    RaiseComponentChanged(member, null, null);
                }
            }
            finally
            {
                designerTransaction?.Commit();
            }
            //ShowContextMenu();
        }

        internal void OnRemove(object? sender, EventArgs e)
        {
            //TabFormControl tabControl = (TabFormControl)base.Component;
            var tabControl = base.Component as TabFormControl;
            if(tabControl == null)
            {
                return;
            }

            var selectedTab = tabControl.SelectedTab;

            if(selectedTab == null)
            {
                return;
            }

            if (tabControl.TabPages.Count == 0)
            {
                return;
            }

            var member = TypeDescriptor.GetProperties(base.Component)["Controls"];
            
            var designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost == null)
            {
                return;
            }
            DesignerTransaction designerTransaction = null;
            try
            {
                try
                {
                    var comp_selected = (selectedTab as IComponent)?.Site?.Name;
                    var comp_tabctl = base.Component?.Site?.Name;

                    if(comp_selected == null || comp_tabctl == null)
                    {
                        return;
                    }

                    designerTransaction = designerHost.CreateTransaction(
                        SR.GetString("TabControlRemoveTab", 
                        comp_selected, 
                        comp_tabctl));

                    RaiseComponentChanging(member);
                }
                catch (CheckoutException ex)
                {
                    if (ex == CheckoutException.Canceled)
                    {
                        return;
                    }
                    throw ex;
                }
                designerHost.DestroyComponent((Control)selectedTab);
                RaiseComponentChanged(member, null, null);
            }
            finally
            {
                designerTransaction?.Commit();
            }
            //ShowContextMenu();
        }

        internal void OnRemoveHeader(object? sender, EventArgs e)
        {
            //TabFormControl tabControl = (TabFormControl)base.Component;
            var tabControl = base.Component as TabFormControl;
            if (tabControl == null)
            {
                return;
            }

            TabFormHeader tabHeader = tabControl.TabHeader;

            Console.WriteLine($"设计器删除选项卡头部：{tabHeader}");

            if (tabHeader == null)
            {
                return;
            }

            var member = TypeDescriptor.GetProperties(base.Component)["Controls"];

            var designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost == null)
            {
                return;
            }
            DesignerTransaction designerTransaction = null;
            try
            {
                try
                {
                    var comp_header = (tabHeader as IComponent)?.Site?.Name;
                    var comp_tabctl = base.Component?.Site?.Name;

                    if (comp_header == null || comp_tabctl == null)
                    {
                        return;
                    }

                    designerTransaction = designerHost.CreateTransaction(
                        SR.GetString("TabControlRemoveTabHeader",
                        comp_header,
                        comp_tabctl));

                    RaiseComponentChanging(member);
                }
                catch (CheckoutException ex)
                {
                    if (ex == CheckoutException.Canceled)
                    {
                        return;
                    }
                    throw ex;
                }
                designerHost.DestroyComponent(tabHeader);
                RaiseComponentChanged(member, null, null);
            }
            finally
            {
                designerTransaction?.Commit();
            }
            //ShowContextMenu();
        }

        private void DrawBorder(Graphics graphics)
        {
            Control control = Control;
            Rectangle clientRectangle = control.ClientRectangle;
            Color color = ((!((double)control.BackColor.GetBrightness() < 0.5)) ? ControlPaint.Dark(control.BackColor) : ControlPaint.Light(control.BackColor));
            Pen pen = new Pen(color);
            pen.DashStyle = DashStyle.Dash;
            clientRectangle.Width--;
            clientRectangle.Height--;
            graphics.DrawRectangle(pen, clientRectangle);
            pen.Dispose();
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            try
            {
                disableDrawGrid = true;
                TabFormControl pictureBox = (TabFormControl)base.Component;
                //if (pictureBox.BorderStyle == BorderStyle.None)
                //{
                    DrawBorder(pe.Graphics);
                //}
                base.OnPaintAdornments(pe);
            }
            finally
            {
                disableDrawGrid = false;
            }
        }

        private TabFormPageDesigner GetSelectedTabPageDesigner()
        {
            TabFormPageDesigner result = null;
            var selectedTab = ((TabFormControl)base.Component).SelectedTab;
            if (selectedTab != null)
            {
                IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (designerHost != null)
                {
                    result = designerHost.GetDesigner((Control)selectedTab) as TabFormPageDesigner;
                }
            }
            return result;
        }

        private void OnComponentChanged(object? sender, ComponentChangedEventArgs e)
        {
            CheckVerbStatus();
            ShowContextMenu();
            RefreshSmartTag();
        }

        protected virtual void ShowContextMenu()
        {
            //if (removeVerb?.CommandID != null)
            //    (GetService(typeof(IMenuCommandService)) as IMenuCommandService)?.ShowContextMenu(removeVerb.CommandID, 0, 0);

            //Console.WriteLine((GetService(typeof(IMenuCommandService)) as IMenuCommandService) ?.Verbs);
            //Console.WriteLine((GetService(typeof(IMenuCommandService))));

            //(GetService(typeof(IPropertyValueUIService)) as IPropertyValueUIService)
            

            //(GetService(typeof(IUIService)) as IUIService)?.SetUIDirty();

            //(GetService(typeof(DesignerActionUIService)) as DesignerActionUIService)?.Refresh(base.Component);
        }

        private void RefreshSmartTag()
        {
            ((DesignerActionUIService)GetService(typeof(DesignerActionUIService)))?.Refresh(base.Component);
        }

        private void CheckVerbStatus()
        {
            if (addHeaderVerb != null)
            {
                addHeaderVerb.Enabled = Control.TabHeader == null;
            }

            if (removeHeaderVerb != null)
            {
                removeHeaderVerb.Enabled = Control.TabHeader != null;
            }

            if (removeVerb != null)
            {
                removeVerb.Enabled = Control.TabCount > 0;
            }
        }

        private void OnGotFocus(object? sender, EventArgs e)
        {
            //MessageBox.Show($"OnGotFocus{sender}");
            //((IEventHandlerService)GetService(typeof(IEventHandlerService)))?.FocusWindow?.Focus();

            //Console.WriteLine($"OnGotFocus：{sender}");
        }

        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
            tabControlSelected = false;
            if (selectionService == null)
            {
                return;
            }
            ICollection selectedComponents = selectionService.GetSelectedComponents();
            TabFormControl tabControl = (TabFormControl)base.Component;
            foreach (object item in selectedComponents)
            {
                if (item == tabControl)
                {
                    tabControlSelected = true;
                }
                var tabPageOfComponent = GetTabPageOfComponent(tabControl, item);
                if (tabPageOfComponent != null && tabPageOfComponent.Parent == tabControl)
                {
                    tabControlSelected = false;
                    tabControl.SelectedTab = tabPageOfComponent;
                    //SelectionManager selectionManager = (SelectionManager)GetService(typeof(SelectionManager));
                    //selectionManager.Refresh();
                    break;
                }
            }
        }

        //private void OnControlAdded(object? sender, ControlEventArgs e)
        //{
        //    if (e.Control != null && !e.Control.IsHandleCreated)
        //    {
        //        IntPtr handle = e.Control.Handle;
        //    }
        //}

        //private void OnTabSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
        //    if (selectionService == null)
        //    {
        //        return;
        //    }
        //    ICollection selectedComponents = selectionService.GetSelectedComponents();
        //    TabFormControl tabControl = (TabFormControl)base.Component;
        //    bool flag = false;
        //    foreach (object item in selectedComponents)
        //    {
        //        TabFormPage tabPageOfComponent = GetTabPageOfComponent(tabControl, item);
        //        if (tabPageOfComponent != null && tabPageOfComponent.Parent == tabControl && tabPageOfComponent == tabControl.SelectedTab)
        //        {
        //            flag = true;
        //            break;
        //        }
        //    }
        //    if (!flag)
        //    {
        //        selectionService.SetSelectedComponents(new object[1] { base.Component });
        //    }
        //}

        //private void OnRightToLeftLayoutChanged(object? sender, EventArgs e)
        //{
        //    if (base.BehaviorService != null)
        //    {
        //        base.BehaviorService.SyncSelection();
        //    }
        //}

        //protected override void PreFilterProperties(IDictionary properties)
        //{
        //    base.PreFilterProperties(properties);
        //    string[] array = new string[1] { "SelectedIndex" };
        //    Attribute[] attributes = new Attribute[0];
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
        //        if (propertyDescriptor != null)
        //        {
        //            properties[array[i]] = TypeDescriptor.CreateProperty(typeof(TabFormControlDesigner), propertyDescriptor, attributes);
        //        }
        //    }
        //}


        //protected override void OnDragEnter(DragEventArgs de)
        //{
        //    //forwardOnDrag = false;
        //    //if (de.Data is DataObject behaviorDataObject)
        //    //{
        //    //    int primaryControlIndex = -1;
        //    //    ArrayList sortedDragControls = behaviorDataObject.GetSortedDragControls(ref primaryControlIndex);
        //    //    if (sortedDragControls != null)
        //    //    {
        //    //        for (int i = 0; i < sortedDragControls.Count; i++)
        //    //        {
        //    //            if (!(sortedDragControls[i] is Control) || (sortedDragControls[i] is Control && !(sortedDragControls[i] is TabFormPage)))
        //    //            {
        //    //                forwardOnDrag = true;
        //    //                break;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    forwardOnDrag = true;
        //    //}

        //    if (forwardOnDrag)
        //    {
        //        GetSelectedTabPageDesigner()?.OnDragEnterInternal(de);
        //    }
        //    else
        //    {
        //        base.OnDragEnter(de);
        //    }

        //    //MessageBox.Show($"de.Data{de.Data}");
        //}


        //protected override void OnDragDrop(DragEventArgs de)
        //{
        //    if (forwardOnDrag)
        //    {
        //        GetSelectedTabPageDesigner()?.OnDragDropInternal(de);
        //    }
        //    else
        //    {
        //        base.OnDragDrop(de);
        //    }
        //    forwardOnDrag = false;
        //}

        //protected override void OnDragLeave(EventArgs e)
        //{
        //    if (forwardOnDrag)
        //    {
        //        GetSelectedTabPageDesigner()?.OnDragLeaveInternal(e);
        //    }
        //    else
        //    {
        //        base.OnDragLeave(e);
        //    }
        //    forwardOnDrag = false;
        //}

        //protected override void OnDragOver(DragEventArgs de)
        //{
        //    if (forwardOnDrag)
        //    {
        //        TabControl tabControl = (TabControl)Control;
        //        Point pt = Control.PointToClient(new Point(de.X, de.Y));
        //        if (!tabControl.DisplayRectangle.Contains(pt))
        //        {
        //            de.Effect = DragDropEffects.None;
        //        }
        //        else
        //        {
        //            GetSelectedTabPageDesigner()?.OnDragOverInternal(de);
        //        }
        //    }
        //    else
        //    {
        //        base.OnDragOver(de);
        //    }
        //}

        //protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        //{
        //    if (forwardOnDrag)
        //    {
        //        GetSelectedTabPageDesigner()?.OnGiveFeedbackInternal(e);
        //    }
        //    else
        //    {
        //        base.OnGiveFeedback(e);
        //    }
        //}




    }
}
