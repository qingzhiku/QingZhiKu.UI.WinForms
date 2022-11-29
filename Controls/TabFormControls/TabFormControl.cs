﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Text;
using System.Collections;
using System.Design;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.Layout;
using System.Diagnostics.CodeAnalysis;
using Microsoft.DotNet.DesignTools.Protocol.Values;

#pragma warning disable CS8603

namespace System.Windows.Forms
{
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultProperty("TabPages")]
    [Designer(typeof(System.Windows.Forms.Design.TabFormControlDesigner), typeof(IDesigner))]
    [SRDescriptionAttribute("DescriptionTabControl")]
    [ToolboxBitmap(typeof(TabControl))]
    public partial class TabFormControl : ContainerControlBase
    {
        private TabFormPageCollection tabCollection;

        private TabAlignment alignment = TabAlignment.Top;
        //private ImageList imageList = null;
        private TabAppearance appearance = TabAppearance.Normal;
        private Rectangle cachedDisplayRect = Rectangle.Empty;
        private int selectedIndex = -1;
        private bool currentlyScaling = false;
        private int minHeaderHeight = 30;

        private LayoutEngine layoutEngine = null;

        private EventHandler onSelectedIndexChanged = null;
        private DrawItemEventHandler onDrawItem = null;

        // //events
        private static readonly object EVENT_DESELECTING = new object();
        private static readonly object EVENT_DESELECTED = new object();
        private static readonly object EVENT_SELECTING = new object();
        private static readonly object EVENT_SELECTED = new object();
        private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

        private const int TABCONTROLSTATE_hotTrack = 0x00000001;
        private const int TABCONTROLSTATE_multiline = 0x00000002;
        private const int TABCONTROLSTATE_showToolTips = 0x00000004;
        private const int TABCONTROLSTATE_getTabRectfromItemSize = 0x00000008;
        private const int TABCONTROLSTATE_fromCreateHandles = 0x00000010;
        private const int TABCONTROLSTATE_UISelection = 0x00000020;
        private const int TABCONTROLSTATE_selectFirstControl = 0x00000040;
        private const int TABCONTROLSTATE_insertingItem = 0x00000080;
        private const int TABCONTROLSTATE_autoSize = 0x00000100;

        //private const int TABCONTROLTABHEADERVISIABLE = 0x00000200;

        public delegate void TabFormControlCancelEventHandler(object sender, TabFormControlCancelEventArgs e);
        public delegate void TabFormControlEventHandler(object sender, TabFormControlEventArgs e);

        private System.Collections.Specialized.BitVector32 tabControlState;

        // private readonly int tabBaseReLayoutMessage = Win32.RegisterWindowMessage(/*Application.WindowMessagesVersion +*/ "WindowsForms12_TabBaseReLayout");


        private ITabFormPage[] tabPages = null;
        private int tabPageCount;
        private int lastSelection;

        // private bool rightToLeftLayout = false;
        // private bool skipUpdateSize;


        [Browsable(false),
        SRCategory(SR.CatBehavior),
        DefaultValue(-1),
        SRDescription("selectedIndexDescr")]
        public int SelectedIndex
        {
            get
            {
                Console.WriteLine($"{DateTime.Now} : ***** Access SelectedIndex Property of TabFormControl ! *****");

                if (IsHandleCreated)
                {
                    int n = Array.FindIndex(GetTabPages(), tp => tp.Active == true);

                    Console.WriteLine($"{DateTime.Now} : TabFormControl TabCount Value is {tabPageCount}");

                    if (n < 0 && tabPageCount > 0)
                    {
                        n = 0;
                    }

                    Console.WriteLine($"{DateTime.Now} : TabFormControl SelectedIndex Value is {n}");

                    return n;
                }
                else
                {
                    return selectedIndex;
                }
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidLowBoundArgumentEx", "SelectedIndex", value.ToString(CultureInfo.CurrentCulture), (-1).ToString(CultureInfo.CurrentCulture)));
                }
                //MessageBox.Show($"SelectedIndex：{value}");
                // TabFormPage selectedTab = SelectedTab;
                // int selectedTabIndex = SelectedIndex;
                //    if (selectedTab != null && selectedTabIndex != value)

                if (SelectedIndex != value)
                {
                    if (IsHandleCreated)
                    {
                        // Raise the TabDeselecting event
                        //TitleBarTabCancelEventArgs e = new TitleBarTabCancelEventArgs
                        //{
                        //    Action = TabControlAction.Deselecting,
                        //    Tab = selectedTab,
                        //    TabIndex = selectedTabIndex
                        //};

                        //OnTabDeselecting(e);

                        //// If the subscribers to the event canceled it, return before we do anything else
                        //if (e.Cancel)
                        //{
                        //    return;
                        //}

                        if (WmDeselChange())
                        {
                            return;
                        }

                        //selectedTab.Active = false;

                        //// Raise the TabDeselected event
                        //OnTabDeselected(
                        //    new TitleBarTabEventArgs
                        //    {
                        //        Tab = selectedTab,
                        //        TabIndex = selectedTabIndex,
                        //        Action = TabControlAction.Deselected
                        //    });

                        if (value != -1)
                        {
                            //// Raise the TabSelecting event
                            //TitleBarTabCancelEventArgs e = new TitleBarTabCancelEventArgs
                            //{
                            //    Action = TabControlAction.Selecting,
                            //    Tab = Tabs[value],
                            //    TabIndex = value
                            //};

                            //OnTabSelecting(e);

                            //// If the subscribers to the event canceled it, return before we do anything else
                            //if (e.Cancel)
                            //{
                            //    return;
                            //}

                            //Tabs[value].Active = true;

                            // Raise the TabSelected event
                            //OnTabSelected(
                            //    new TitleBarTabEventArgs
                            //    {
                            //        Tab = Tabs[value],
                            //        TabIndex = value,
                            //        Action = TabControlAction.Selected
                            //    });

                            if (WmSelChange(value))
                            {

                                return;
                            }

                            Console.WriteLine($"value{value}\r\nSelectedIndex{SelectedIndex}");
                        }

                    }
                    else
                    {
                        selectedIndex = value;
                    }

                }

                //if (SelectedIndex != value)
                //{
                //    if (IsHandleCreated)
                //    {
                //        //// Guard Against CreateHandle ..
                //        //// And also if we are setting SelectedIndex ourselves from SelectNextTab..
                //        ////if (!tabControlState[TABCONTROLSTATE_fromCreateHandles] && !tabControlState[TABCONTROLSTATE_selectFirstControl])
                //        ////{
                //        ////    tabControlState[TABCONTROLSTATE_UISelection] = true;
                //        //    // Fire Deselecting .. Deselected on currently selected TabPage...
                //        //    if (WmSelChanging())
                //        //    {
                //        //        //tabControlState[TABCONTROLSTATE_UISelection] = false;
                //        //        return;
                //        //    }
                //        ////    if (ValidationCancelled)
                //        ////    {
                //        ////        tabControlState[TABCONTROLSTATE_UISelection] = false;
                //        ////        return;
                //        ////    }
                //        ////}

                //        ////SendMessage(Win32.TCM_SETCURSEL, value, 0);

                //        ////if (!tabControlState[TABCONTROLSTATE_fromCreateHandles] && !tabControlState[TABCONTROLSTATE_selectFirstControl])
                //        ////{
                //        //    // Fire Selecting & Selected .. Also if Selecting is Canceled..
                //        //    // then retuern as we do not change the SelectedIndex...
                //        //    tabControlState[TABCONTROLSTATE_selectFirstControl] = true;
                //        //    if (WmSelChange())
                //        //    {
                //        //        //tabControlState[TABCONTROLSTATE_UISelection] = false;
                //        //        tabControlState[TABCONTROLSTATE_selectFirstControl] = false;
                //        //        return;
                //        //    }
                //        //    else
                //        //    {
                //        //        tabControlState[TABCONTROLSTATE_selectFirstControl] = false;
                //        //    }
                //        ////}


                //    }
                //    else
                //    {
                //        selectedIndex = value;
                //    }
            //}
            }
        }

        [SRCategory(SR.CatAppearance),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription("TabBaseTabCountDescr")]
        public int TabCount
        {
            get { return tabPageCount; }
        }

        [SRCategory(SR.CatBehavior)]
        [SRDescription("TabControlTabsDescr")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[Editor("System.Windows.Forms.Design.TabPageCollectionEditor, " + AssemblyRef.SystemDesign, typeof(UITypeEditor))]
        [MergableProperty(false)]
        public TabFormPageCollection TabPages
        {
            get
            {
                return tabCollection;
            }
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if(layoutEngine == null)
                {
                    layoutEngine = new TabFormControlLayout();
                }
                return layoutEngine;
            }
        }

        private bool InsertingItem
        {
            get
            {
                return (bool)tabControlState[TABCONTROLSTATE_insertingItem];
            }
            set
            {
                tabControlState[TABCONTROLSTATE_insertingItem] = value;
            }
        }

        [SRCategory(SR.CatBehavior)]
        [Localizable(true)]
        [DefaultValue(TabAppearance.Normal)]
        //[SRDescription(SR.TabBaseAppearanceDescr)]
        public TabAppearance Appearance
        {
            get
            {
                if (appearance == TabAppearance.FlatButtons && alignment != TabAlignment.Top)
                {
                    return TabAppearance.Buttons;
                }
                else
                {
                    return appearance;
                }
            }

            set
            {
                if (this.appearance != value)
                {
                    //valid values are 0x0 to 0x2
                    //if (!ClientUtils.IsEnumValid(value, (int)value, (int)TabAppearance.Normal, (int)TabAppearance.FlatButtons))
                    //{
                    //    throw new InvalidEnumArgumentException("value", (int)value, typeof(TabAppearance));
                    //}

                    this.appearance = value;
                    RecreateHandle();

                    //Fire OnStyleChanged(EventArgs.Empty) here since we are no longer calling UpdateStyles( ) but always reCreating the Handle.
                    OnStyleChanged(EventArgs.Empty);
                }
            }
        }


        //[
        //SRCategory(SR.CatAppearance),
        //RefreshProperties(RefreshProperties.Repaint),
        //DefaultValue(null),
        //SRDescription("TabBaseImageListDescr")
        //]
        //public ImageList ImageList
        //{
        //    get
        //    {
        //        return imageList;
        //    }
        //    //set
        //    //{
        //    //    //if (this.imageList != value)
        //    //    //{
        //    //    //    EventHandler recreateHandler = new EventHandler(ImageListRecreateHandle);
        //    //    //    EventHandler disposedHandler = new EventHandler(DetachImageList);

        //    //    //    if (imageList != null)
        //    //    //    {
        //    //    //        imageList.RecreateHandle -= recreateHandler;
        //    //    //        imageList.Disposed -= disposedHandler;
        //    //    //    }

        //    //    //    this.imageList = value;
        //    //    //    IntPtr handle = (value != null) ? value.Handle : IntPtr.Zero;
        //    //    //    if (IsHandleCreated)
        //    //    //        SendMessage(Win32.TCM_SETIMAGELIST, IntPtr.Zero, handle);

        //    //    //    // Update the image list in the tab pages.
        //    //    //    foreach (TabFormPage tabPage in TabPages)
        //    //    //    {
        //    //    //        tabPage.ImageIndexer.ImageList = value;
        //    //    //    }


        //    //    //    if (value != null)
        //    //    //    {
        //    //    //        value.RecreateHandle += recreateHandler;
        //    //    //        value.Disposed += disposedHandler;
        //    //    //    }
        //    //    //}
        //    //}
        //}

        [SRCategory(SR.CatAppearance),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription("TabControlSelectedTabDescr")]
        public ITabFormPage SelectedTab
        {
            get
            {
                int index = SelectedIndex;

                if (index == -1)
                {
                    return null;
                }
                else
                {
                    Debug.Assert(0 <= index && index < tabPages.Length, "SelectedIndex returned an invalid index");
                    return tabPages[index];
                }
            }
            set
            {
                int index = FindTabPage(value);
                SelectedIndex = index;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinHeaderHeight
        {
            get { 
                return minHeaderHeight; 
            }
            //set { 
            //    minHeaderHeight = value;
            //}
        }

        //[SRCategory(SR.CatBehavior)]
        //[DefaultValue(false)]
        //[Localizable(true)]
        ////[SRDescription("TextBoxMultilineDescr")]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual TabFormHeader TabHeader
        //{
        //    get
        //    {
        //        return ((ControlCollection)Controls).GetTabHeader();
        //        //return tabControlState[TABCONTROLTABHEADERVISIABLE];
        //    }
        //    //set
        //    //{
        //    //    if (TabHeaderVisiable == value)
        //    //    {
        //    //        return;
        //    //    }

        //    //    tabControlState[TABCONTROLTABHEADERVISIABLE] = value;

        //    //    if (value)
        //    //    {
        //    //        Controls.Add(new TabFormHeader());
        //    //    }
        //    //    else
        //    //    {

        //    //    }
        //    //    //foreach (var item in Controls)
        //    //    //{
        //    //    //    MessageBox.Show($"Controls:{item}");
        //    //    //}

        //    //}
        //}

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public virtual TabFormHeader TabHeader
        //{
        //    get;
        //    set;
        //}

        // [Browsable(false)]
        // [EditorBrowsable(EditorBrowsableState.Never)]
        // [Bindable(false)]
        // public override string Text
        // {
        //     get
        //     {
        //         return base.Text;
        //     }
        //     set
        //     {
        //         base.Text = value;
        //     }
        // }

        // internal TabFormPage SelectedTabInternal
        // {
        //     get
        //     {
        //         int index = SelectedIndex;
        //         if (index == -1)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             Debug.Assert(0 <= index && index < tabPages.Length, "SelectedIndex returned an invalid index");
        //             return tabPages[index];
        //         }
        //     }
        //     set
        //     {
        //         int index = FindTabPage(value);
        //         SelectedIndex = index;
        //     }
        // }


        // [Browsable(false)]
        // [EditorBrowsable(EditorBrowsableState.Never)]
        // public new event EventHandler TextChanged
        // {
        //     add
        //     {
        //         base.TextChanged += value;
        //     }
        //     remove
        //     {
        //         base.TextChanged -= value;
        //     }
        // }

        // [SRCategory(SR.CatBehavior)]
        // [SRDescription("drawItemEventDescr")]
        // public event DrawItemEventHandler DrawItem
        // {
        //     add
        //     {
        //         onDrawItem += value;
        //     }
        //     remove
        //     {
        //         onDrawItem -= value;
        //     }
        // }

        // [SRCategory(SR.CatPropertyChanged)]
        // [SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
        // public event EventHandler RightToLeftLayoutChanged
        // {
        //     add
        //     {
        //         Events.AddHandler(EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
        //     }
        //     remove
        //     {
        //         Events.RemoveHandler(EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
        //     }
        // }


        // [SRCategory(SR.CatBehavior)]
        // [SRDescription("selectedIndexChangedEventDescr")]
        // public event EventHandler SelectedIndexChanged
        // {
        //     add
        //     {
        //         onSelectedIndexChanged += value;
        //     }
        //     remove
        //     {
        //         onSelectedIndexChanged -= value;
        //     }
        // }

        // [SRCategory(SR.CatAction)]
        // [SRDescription("TabControlSelectingEventDescr")
        // ]
        // public event TabControlCancelEventHandler Selecting
        // {
        //     add
        //     {
        //         Events.AddHandler(EVENT_SELECTING, value);
        //     }
        //     remove
        //     {
        //         Events.RemoveHandler(EVENT_SELECTING, value);
        //     }
        // }

        // [SRCategory(SR.CatAction)]
        // [SRDescription("TabControlSelectedEventDescr")
        // ]
        // public event TabControlEventHandler Selected
        // {
        //     add
        //     {
        //         Events.AddHandler(EVENT_SELECTED, value);
        //     }
        //     remove
        //     {
        //         Events.RemoveHandler(EVENT_SELECTED, value);
        //     }
        // }

        // [SRCategory(SR.CatAction)]
        // [SRDescription("TabControlDeselectingEventDescr")]
        // public event TabControlCancelEventHandler Deselecting
        // {
        //     add
        //     {
        //         Events.AddHandler(EVENT_DESELECTING, value);
        //     }
        //     remove
        //     {
        //         Events.RemoveHandler(EVENT_DESELECTING, value);
        //     }
        // }

        // [SRCategory(SR.CatAction)]
        // [SRDescription("TabControlDeselectedEventDescr")]
        // public event TabControlEventHandler Deselected
        // {
        //     add
        //     {
        //         Events.AddHandler(EVENT_DESELECTED, value);
        //     }
        //     remove
        //     {
        //         Events.RemoveHandler(EVENT_DESELECTED, value);
        //     }
        // }

        // [Browsable(false)]
        // [EditorBrowsable(EditorBrowsableState.Never)]
        // public new event PaintEventHandler Paint
        // {
        //     add
        //     {
        //         base.Paint += value;
        //     }
        //     remove
        //     {
        //         base.Paint -= value;
        //     }
        // }

        public TabFormControl()
        {
            tabControlState = new System.Collections.Specialized.BitVector32(0x00000000);
            tabCollection = new TabFormPageCollection(this);
            //imageList = new ImageList(this.Container);


            
        }

        // //public TabFormPage SelectedTab { get; internal set; }
        // //public int SelectedIndex { get; internal set; }
        // //public int TabCount { get; internal set; }
        // //public ICollection TabPages { get; internal set; }

        // //protected override void OnCreateControl()
        // //{
        // //    SetStyle(
        // //        ControlStyles.DoubleBuffer |
        // //        ControlStyles.UserPaint |
        // //        ControlStyles.OptimizedDoubleBuffer |
        // //        ControlStyles.AllPaintingInWmPaint |
        // //        ControlStyles.ResizeRedraw |
        // //        ControlStyles.SupportsTransparentBackColor, true);
        // //    UpdateStyles();

        // //    base.OnCreateControl();
        // //}

        // internal void UpdateSize()
        // {
        //     //if (this.skipUpdateSize)
        //     //{
        //     //    return;
        //     //}
        //     if (Parent != null && Parent.RecreatingHandle)
        //     {
        //         return;
        //     }
        //     // the spin control (left right arrows) won't update without resizing.
        //     // the most correct thing would be to recreate the handle, but this works
        //     // and is cheaper.
        //     //
        //     //BeginUpdate();
        //     Size size = Size;
        //     Size = new Size(size.Width + 1, size.Height);
        //     Size = size;
        //     //EndUpdate();
        // }

        // protected void BeginUpdate()
        // {
        //     BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase;

        //     var methodinfo = base.GetType().GetMethod("BeginUpdateInternal", flags);

        //     methodinfo?.Invoke(this, null /*new object[] { }*/);
        // }

        // protected void EndUpdate()
        // {
        //     BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase;

        //     var methodinfo = base.GetType().GetMethod("EndUpdateInternal", flags);

        //     methodinfo?.Invoke(this, null /*new object[] { }*/);
        // }

        // [
        // EditorBrowsable(EditorBrowsableState.Advanced)
        // ]
        // protected new void RecreateHandle()
        // {
        //     //TabFormPage[] tabPages = GetTabPages();

        //     //int index = ((tabPages.Length > 0) && (selectedIndex == -1)) ? 0 : SelectedIndex;

        //     //this.tabPages = null;
        //     //tabPageCount = 0;

        //     base.RecreateHandle();

        //     //for (int i = 0; i < tabPages.Length; i++)
        //     //{
        //     //    TabPages.Add(tabPages[i]);
        //     //}
        //     //try
        //     //{
        //     //    tabControlState[TABCONTROLSTATE_fromCreateHandles] = true;
        //     //    SelectedIndex = index;
        //     //}
        //     //finally
        //     //{
        //     //    tabControlState[TABCONTROLSTATE_fromCreateHandles] = false;
        //     //}

        //     //UpdateSize();

        // }


        // protected override void CreateHandle()
        // {
        //     base.CreateHandle();

        //     //var designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;

        //     //if (designerHost == null)
        //     //{
        //     //    return;
        //     //}

        //     //var dg = designerHost.GetDesigner(this);

        //     //// MessageBox.Show($"{dg.Verbs[1].CommandID}");

        //     //dg.Verbs.Add(new DesignerVerb("添加", OnText));
        // }

        // //private void OnText(object? sender, EventArgs e)
        // //{
        // //    RemoveAll();
        // //}

        //protected override Control.ControlCollection CreateControlsInstance()
        //{
        //    return new ControlCollection(this);
        //}

        internal virtual ITabFormPage CreateTabFormPage()
        {
            if (Activator.CreateInstance(GetTypeOfTabFormPage()) is ITabFormPage tabFormPage)
            {
                return tabFormPage;
            }
            return null;
        }

        protected internal virtual Type GetTypeOfTabFormPage()
        {
            return typeof(TabFormPage);
        }

        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    base.OnControlAdded(e);

        //    //try
        //    //{
        //    //    //if (e.Control is not TabFormPage)
        //    //    //{

        //    //    var tp = (TabPage)e.Control;
        //    //    //    //e.Control.Dispose();

        //    //    int index = FindTabPage(tp);

        //    //    SelectedIndex = index;

        //    //    //    //TabPages[index] = new TabFormPage();

        //    //    //    //this.Controls.Remove((TabPage)e.Control);

        //    //    //    //RemoveTabPage(index);

        //    //    //    //TabPages.Insert(index, new TabFormPage());

        //    //    //    //TabPages.Add(new TabFormPage());

        //    //    var designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;

        //    //    if (designerHost == null)
        //    //    {
        //    //        return;
        //    //    }

        //    //    var designer = designerHost.GetDesigner(this);

        //    //    if (designer == null)
        //    //    {
        //    //        return;
        //    //    }



        //    //    var remove = designer.GetType().GetMethod("OnRemove", BindingFlags.Instance | BindingFlags.NonPublic);

        //    //    remove?.Invoke(designer, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { SelectedTab, EventArgs.Empty }, null);


        //    //    //    //tp.Parent = null;
        //    //    //    //tp = null;
        //    //    //    //designerHost.DestroyComponent(e.Control);

        //    //    //    //var  form = designerHost.RootComponent as Form;
        //    //    //}

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.Message);
        //    //}

        //}

        internal int FindTabPage(ITabFormPage tabPage)
        {
            if (TabPages != null)
            {
                for (int i = 0; i < TabCount; i++)
                {
                    if (TabPages[i].Equals(tabPage))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        internal ITabFormPage GetTabPage(int index)
        {
            if (index < 0 || index >= tabPageCount)
            {
                Console.WriteLine($"{DateTime.Now} : TabFormControl get tabFormPage wrong {SR.GetString(SR.InvalidArgument, "index", index)}");
                //throw new ArgumentOutOfRangeException("index", SR.GetString(SR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
                return null;
            }

            return tabPages[index];
        }

        internal void SetTabPage(int index, ITabFormPage tabPage/*, Win32.TCITEM_T tcitem*/)
        {
            if (index < 0 || index >= tabPageCount)
                throw new ArgumentOutOfRangeException("index", SR.GetString(SR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));

            if (IsHandleCreated)
            {
                //UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.TCM_SETITEM, index, tcitem);
                // Make the Updated tab page the currently selected tab page
            }

            if (DesignMode && IsHandleCreated)
            {
                //UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.TCM_SETCURSEL, (IntPtr)index, IntPtr.Zero);
            }

            tabPages[index] = tabPage;
        }

        internal void RemoveTabPage(int index)
        {
            if (index < 0 || index >= tabPageCount)
                throw new ArgumentOutOfRangeException("index", SR.GetString(SR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
            tabPageCount--;
            if (index < tabPageCount)
            {
                Array.Copy(tabPages, index + 1, tabPages, index, tabPageCount - index);
            }
            tabPages[tabPageCount] = null;
            //if (IsHandleCreated)
            //{
            //    SendMessage(NativeMethods.TCM_DELETEITEM, index, 0);
            //}
            cachedDisplayRect = Rectangle.Empty;
        }

        protected virtual object[] GetItems()
        {
            if(tabPages == null  || tabPageCount <= 0)
            {
                return Array.Empty<ITabFormPage>();
            }

            ITabFormPage[] result = new ITabFormPage[tabPageCount];
            if (tabPageCount > 0) 
                Array.Copy(tabPages, 0, result, 0, tabPageCount);
            return result;
        }

        // protected virtual object[] GetItems(Type baseType)
        // {
        //     object[] result = (object[])Array.CreateInstance(baseType, tabPageCount);
        //     if (tabPageCount > 0) Array.Copy(tabPages, 0, result, 0, tabPageCount);
        //     return result;
        // }

        internal ITabFormPage[] GetTabPages()
        {
            return (ITabFormPage[])GetItems();
        }

        protected void RemoveAll()
        {
            this.Controls.Clear();

            //SendMessage(NativeMethods.TCM_DELETEALLITEMS, 0, 0);

            // important

            tabPages = null;
            tabPageCount = 0;
        }

        internal int AddTabPage(ITabFormPage tabPage/*, Win32.TCITEM_T tcitem*/)
        {
            int index = tabPageCount < 0 ? 0 : tabPageCount;// AddNativeTabPage(tcitem);
            if (index >= 0)
            {
                Insert(index, tabPage);
            }
            return index;
        }

        // internal int AddNativeTabPage(Win32.TCITEM_T tcitem)
        // {
        //     int index = (int)Win32.SendMessage(new HandleRef(this, Handle), Win32.TCM_INSERTITEM, tabPageCount + 1, tcitem);
        //     Win32.PostMessage(new HandleRef(this, Handle), tabBaseReLayoutMessage, IntPtr.Zero, IntPtr.Zero);
        //     return index;
        // }

        internal void ApplyItemSize()
        {
            //if (IsHandleCreated && ShouldSerializeItemSize())
            //{
            //    SendMessage(NativeMethods.TCM_SETITEMSIZE, 0, (int)NativeMethods.Util.MAKELPARAM(itemSize.Width, itemSize.Height));
            //}
            cachedDisplayRect = Rectangle.Empty;
        }

        // private void ImageListRecreateHandle(object sender, EventArgs e)
        // {
        //     //if (IsHandleCreated)
        //     //    SendMessage(Win32.TCM_SETIMAGELIST, 0, ImageList.Handle);
        // }

        // //internal IntPtr SendMessage(int msg, int wparam, int lparam)
        // //{
        // //    Debug.Assert(IsHandleCreated, "Performance alert!  Calling Control::SendMessage and forcing handle creation.  Re-work control so handle creation is not required to set properties.  If there is no work around, wrap the call in an IsHandleCreated check.");
        // //    return Win32.SendMessage(new HandleRef(this, Handle), msg, wparam, lparam);
        // //}

        // //internal IntPtr SendMessage(int msg, ref int wparam, ref int lparam)
        // //{
        // //    Debug.Assert(IsHandleCreated, "Performance alert!  Calling Control::SendMessage and forcing handle creation.  Re-work control so handle creation is not required to set properties.  If there is no work around, wrap the call in an IsHandleCreated check.");
        // //    return Win32.SendMessage(new HandleRef(this, Handle), msg, ref wparam, ref lparam);
        // //}

        // internal IntPtr SendMessage(int msg, int wparam, IntPtr lparam)
        // {
        //     Debug.Assert(IsHandleCreated, "Performance alert!  Calling Control::SendMessage and forcing handle creation.  Re-work control so handle creation is not required to set properties.  If there is no work around, wrap the call in an IsHandleCreated check.");
        //     return Win32.SendMessage(new HandleRef(this, Handle), msg, (IntPtr)wparam, lparam);
        // }

        // internal IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
        // {
        //     Debug.Assert(IsHandleCreated, "Performance alert!  Calling Control::SendMessage and forcing handle creation.  Re-work control so handle creation is not required to set properties.  If there is no work around, wrap the call in an IsHandleCreated check.");
        //     return Win32.SendMessage(new HandleRef(this, Handle), msg, wparam, lparam);
        // }

        // internal IntPtr SendMessage(int msg, IntPtr wparam, int lparam)
        // {
        //     Debug.Assert(IsHandleCreated, "Performance alert!  Calling Control::SendMessage and forcing handle creation.  Re-work control so handle creation is not required to set properties.  If there is no work around, wrap the call in an IsHandleCreated check.");
        //     return Win32.SendMessage(new HandleRef(this, Handle), msg, wparam, (IntPtr)lparam);
        // }


        // private void DetachImageList(object sender, EventArgs e)
        // {
        //     ImageList = null;
        // }

        internal void Insert(int index, ITabFormPage tabPage)
        {
            if (tabPages == null)
            {
                tabPages = new ITabFormPage[4];
            }
            else if (tabPages.Length == tabPageCount)
            {
                ITabFormPage[] newTabPages = new ITabFormPage[tabPageCount * 2];
                Array.Copy(tabPages, 0, newTabPages, 0, tabPageCount);
                tabPages = newTabPages;
            }
            if (index < tabPageCount)
            {
                Array.Copy(tabPages, index, tabPages, index + 1, tabPageCount - index);
            }
            tabPages[index] = tabPage;
            tabPageCount++;
            cachedDisplayRect = Rectangle.Empty;
            ApplyItemSize();
            if (Appearance == TabAppearance.FlatButtons)
            {
                Invalidate();
            }
        }

        private void InsertItem(int index, ITabFormPage tabPage)
        {
            if (index < 0 || ((tabPages != null) && index > tabPageCount))
                throw new ArgumentOutOfRangeException("index", SR.GetString(SR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
            if (tabPage == null)
                throw new ArgumentNullException("tabPage");

            int retIndex = -1;
            if (IsHandleCreated)
            {
                //Win32.TCITEM_T tcitem = tabPage.GetTCITEM();
                //retIndex = (int)Win32.SendMessage(new HandleRef(this, Handle), Win32.TCM_INSERTITEM, index, tcitem);
                if (retIndex >= 0)
                    Insert(retIndex, tabPage);
            }
        }

        protected override void ScaleCore(float dx, float dy)
        {
            currentlyScaling = true;
            base.ScaleCore(dx, dy);
            currentlyScaling = false;
        }

        protected internal virtual void SetTabPageBoundCore(ITabFormPage page)
        {
            if(page is not Control pageControl)
            {
                return;
            }

            pageControl.Location = new Point(0, MinHeaderHeight);
            pageControl.Bounds = new Rectangle(
                new Point(0, MinHeaderHeight),
                new Size(DisplayRectangle.Width, DisplayRectangle.Height - MinHeaderHeight));
        }

        protected void UpdateTabSelection(bool updateFocus)
        {
            if (IsHandleCreated)
            {
                int index = SelectedIndex;

                var tabPages = GetTabPages();
                if (index != -1)
                {
                    if (currentlyScaling)
                    {
                        ((Control)tabPages[index]).SuspendLayout();
                    }

                    SetTabPageBoundCore(tabPages[index]);
                    //tabPages[index].Location = new Point(0, TabHeaderHeight);
                    //tabPages[index].Bounds = new Rectangle(
                    //    new Point(0, TabHeaderHeight),
                    //    new Size(DisplayRectangle.Width, DisplayRectangle.Height -TabHeaderHeight));

                    ((Control)tabPages[index]).Invalidate();

                    if (currentlyScaling)
                    {
                        ((Control)tabPages[index]).ResumeLayout(false);
                    }

                    ((Control)tabPages[index]).Visible = true;
                    //if (updateFocus)
                    //{
                    //    //if (!Focused || tabControlState[TABCONTROLSTATE_selectFirstControl])
                    //    //{
                    //    //    //tabControlState[TABCONTROLSTATE_UISelection] = false;
                    //    //    //bool selectNext = false;

                    //    //    //IntSecurity.ModifyFocus.Assert();
                    //    //    //try
                    //    //    //{
                    //    //    //    selectNext = tabPages[index].SelectNextControl(null, true, true, false, false);
                    //    //    //}
                    //    //    //finally
                    //    //    //{
                    //    //    //    //CodeAccessPermission.RevertAssert();
                    //    //    //}

                    //    //    //if (selectNext)
                    //    //    //{
                    //    //    //    if (!ContainsFocus)
                    //    //    //    {
                    //    //    //        IContainerControl c = GetContainerControl();
                    //    //    //        if (c != null)
                    //    //    //        {
                    //    //    //            while (c.ActiveControl is ContainerControl)
                    //    //    //            {
                    //    //    //                c = (IContainerControl)c.ActiveControl;
                    //    //    //            }
                    //    //    //            if (c.ActiveControl != null)
                    //    //    //            {
                    //    //    //                c.ActiveControl.Focus();
                    //    //    //            }
                    //    //    //        }
                    //    //    //    }
                    //    //    //}
                    //    //    //else
                    //    //    //{
                    //    //    //    IContainerControl c = GetContainerControl();
                    //    //    //    if (c != null && !DesignMode)
                    //    //    //    {
                    //    //    //        if (c is ContainerControl)
                    //    //    //        {
                    //    //    //            ((ContainerControl)c).ActiveControl = this; //SetActiveControlInternal(this);
                    //    //    //        }
                    //    //    //        else
                    //    //    //        {
                    //    //    //            //IntSecurity.ModifyFocus.Assert();
                    //    //    //            try
                    //    //    //            {
                    //    //    //                c.ActiveControl = this;
                    //    //    //            }
                    //    //    //            finally
                    //    //    //            {
                    //    //    //                //CodeAccessPermission.RevertAssert();
                    //    //    //            }
                    //    //    //        }
                    //    //    //    }
                    //    //    //}
                    //    //}
                    //}
                }

                for (int i = 0; i < tabPages.Length; i++)
                {
                    if (i != SelectedIndex)
                    {
                        ((Control)tabPages[i]).Visible = false;
                    }
                }

            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            //Win32.AddWindowToIDTable(this, this.Handle);
            //handleInTable = true;

            //if (!padding.IsEmpty)
            //{
            //    SendMessage(NativeMethods.TCM_SETPADDING, 0, NativeMethods.Util.MAKELPARAM(padding.X, padding.Y));
            //}

            base.OnHandleCreated(e);

            cachedDisplayRect = Rectangle.Empty;
            ApplyItemSize();
            //if (imageList != null)
            //{
            //    SendMessage(NativeMethods.TCM_SETIMAGELIST, 0, imageList.Handle);
            //}

            //if (ShowToolTips)
            //{
            //    IntPtr tooltipHwnd;
            //    tooltipHwnd = SendMessage(NativeMethods.TCM_GETTOOLTIPS, 0, 0);
            //    if (tooltipHwnd != IntPtr.Zero)
            //    {
            //        SafeNativeMethods.SetWindowPos(new HandleRef(this, tooltipHwnd),
            //                             NativeMethods.HWND_TOPMOST,
            //                             0, 0, 0, 0,
            //                             NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE |
            //                             NativeMethods.SWP_NOACTIVATE);
            //    }
            //}

            // Add the pages
            //if(TabHeader == null)
            //{
            //    TabHeader = new TabFormHeader();
            //    TabHeader.Dock = DockStyle.Top;
            //    TabHeader.Height = TabHeaderHeight;
            //    TabHeader.BackColor = Color.White;
            //    this.Controls.Add(TabHeader);
            //}


            //foreach (TabFormPage page in TabPages)
            //{
            //    AddNativeTabPage(page.GetTCITEM());
            //}

            // Resize the pages
            //ResizePages();

            //if (selectedIndex != -1)
            //{
            //    try
            //    {
            //        tabControlState[TABCONTROLSTATE_fromCreateHandles] = true;
            //        SelectedIndex = selectedIndex;
            //    }
            //    finally
            //    {
            //        tabControlState[TABCONTROLSTATE_fromCreateHandles] = false;
            //    }
            //    selectedIndex = -1;
            //}

            UpdateTabSelection(false);
        }

        protected override void OnControlAdding(ControlCancelEventArgs e)
        {
            if(e.Control is not ITabFormPage tabPage)
            {
                Console.WriteLine($"{DateTime.Now} : {SR.GetString(SR.TabControlInvalidTabPageType, e.Control.GetType().Name)}");
                e.Cancel = true;
                return;
            }

            // 247078 : See InsertingItem property
            if (!InsertingItem)
            {
                if (IsHandleCreated)
                {
                    AddTabPage(tabPage/*, tabPage.GetTCITEM()*/);
                }
                else
                {
                    Insert(TabCount, tabPage);
                }
            }
            
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is not ITabFormPage tabPage)
            {
                return;
            }

            e.Control.Visible = false;
            if (IsHandleCreated)
            {
                SetTabPageBoundCore(tabPage);
                //tabPage.Bounds = owner.DisplayRectangle;
            }

            // site the tabPage if necessary.
            var site = Site;
            if (site != null)
            {
                var siteTab = tabPage.Site;
                if (siteTab == null)
                {
                    var container = site.Container;
                    if (container != null)
                    {
                        container.Add(tabPage);
                    }
                }
            }


            // important
            ApplyItemSize();
            UpdateTabSelection(false);

        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (e.Control == null)
            {
                Console.WriteLine($"{DateTime.Now} :When removing paramters in {this.GetType()} is null  !");
                return;
            }

            // TabPage
            if (e.Control is not ITabFormPage tabPage)
            {
                return;
            }

            int index = FindTabPage(tabPage);
            int curSelectedIndex = SelectedIndex;

            if (index != -1)
            {
                RemoveTabPage(index);
                if (index == curSelectedIndex)
                {
                    SelectedIndex = 0; //Always select the first tabPage is the Selected TabPage is removed.
                }
            }

            UpdateTabSelection(false);

        }

        // internal void UpdateTab(TabFormPage tabPage)
        // {
        //     int index = FindTabPage(tabPage);
        //     SetTabPage(index, tabPage, tabPage.GetTCITEM());

        //     // It's possible that changes to this TabPage will change the DisplayRectangle of the
        //     // TabControl (e.g. ASURT 99087), so invalidate and resize the size of this page.
        //     //
        //     cachedDisplayRect = Rectangle.Empty;
        //     UpdateTabSelection(false);
        // }

        private bool WmSelChange(int index)
        {
            var newSelected = GetTabPage(index);

            if (newSelected == null)
                return true;

            TabFormControlCancelEventArgs tcc = new TabFormControlCancelEventArgs(newSelected, index, false, TabControlAction.Selecting);
            OnSelecting(tcc);
            if (!tcc.Cancel)
            {
                OnSelected(new TabFormControlEventArgs(newSelected, index, TabControlAction.Selected));
                OnSelectedIndexChanged(EventArgs.Empty);
            }
            else
            {
                // user Cancelled the Selection of the new Tab.
                //SendMessage(Win32.TCM_SETCURSEL, lastSelection, 0);
                UpdateTabSelection(true);
            }
            return tcc.Cancel;
        }

        private bool WmDeselChange()
        {
            IContainerControl c = GetContainerControl();
            if (c != null && !DesignMode)
            {
                if (c is ContainerControl)
                {
                    ((ContainerControl)c).ActiveControl = this;
                }
                else
                {
                    // SECREVIEW : Taking focus and activating a control in response
                    //           : to a user gesture (WM_SETFOCUS) is OK.
                    //
                    //IntSecurity.ModifyFocus.Assert();
                    try
                    {
                        c.ActiveControl = this;
                    }
                    finally
                    {
                        //CodeAccessPermission.RevertAssert();
                    }
                }
            }

            // Fire DeSelecting .... on the current Selected Index...
            // Set the return value to a global
            // if 'cancelled' return from here else..
            // fire Deselected.
            lastSelection = SelectedIndex;
            TabFormControlCancelEventArgs tcc = new TabFormControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Deselecting);
            OnDeselecting(tcc);
            if (!tcc.Cancel)
            {
                OnDeselected(new TabFormControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Deselected));
            }
            return tcc.Cancel;

        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            int index = SelectedIndex;
            cachedDisplayRect = Rectangle.Empty;
            UpdateTabSelection(tabControlState[TABCONTROLSTATE_UISelection]);
            tabControlState[TABCONTROLSTATE_UISelection] = false;
            if (onSelectedIndexChanged != null) onSelectedIndexChanged(this, e);

        }

        protected virtual void OnDeselecting(TabFormControlCancelEventArgs e)
        {
            var handler = Events[EVENT_DESELECTING] as TabFormControlCancelEventHandler;
            if (handler != null) handler(this, e);

        }

        protected virtual void OnDeselected(TabFormControlEventArgs e)
        {
            var handler = Events[EVENT_DESELECTED] as TabFormControlEventHandler;
            if (handler != null) handler(this, e);

            if (SelectedTab != null)
            {
                SelectedTab.FireLeave(EventArgs.Empty);
            }
        }

        protected virtual void OnSelecting(TabFormControlCancelEventArgs e)
        {
            var handler = Events[EVENT_SELECTING] as TabFormControlCancelEventHandler;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnSelected(TabFormControlEventArgs e)
        {
            var handler = Events[EVENT_SELECTED] as TabFormControlEventHandler;
            if (handler != null) handler(this, e);

            if (e.TabPage != null)
            {
                e.TabPage.FireEnter(EventArgs.Empty);
            }

        }



        [ComVisible(false)]
        public new class ControlCollection : Control.ControlCollection
        {
            private TabFormControl owner;

            public ControlCollection(TabFormControl owner) : base(owner)
            {
                this.owner = owner;
            }

            public override void Add(Control value)
            {
                if (value == null)
                {
                    Console.WriteLine($"{DateTime.Now} :When adding paramters in {this.GetType()} is null  !");
                    return;
                }

                // TabPage
                var tabPage = value as ITabFormPage;
                if (tabPage == null)
                {
                    Console.WriteLine($"{DateTime.Now} : {SR.GetString(SR.TabControlInvalidTabPageType, value.GetType().Name)}");
                    return;
                }

                // 247078 : See InsertingItem property
                if (!owner.InsertingItem)
                {
                    if (owner.IsHandleCreated)
                    {
                        owner.AddTabPage(tabPage/*, tabPage.GetTCITEM()*/);
                    }
                    else
                    {
                        owner.Insert(owner.TabCount, tabPage);
                    }
                }

                base.Add(value);
                value.Visible = false;

                if (owner.IsHandleCreated)
                {
                    owner.SetTabPageBoundCore(tabPage);
                    //tabPage.Bounds = owner.DisplayRectangle;
                }

                // site the tabPage if necessary.
                var site = owner.Site;
                if (site != null)
                {
                    var siteTab = value.Site;
                    if (siteTab == null)
                    {
                        var container = site.Container;
                        if (container != null)
                        {
                            container.Add(value);
                        }
                    }
                }

                // important
                owner.ApplyItemSize();
                owner.UpdateTabSelection(false);

            }

            public override void Remove(Control value)
            {
                base.Remove(value);

                if (value == null)
                {
                    Console.WriteLine($"{DateTime.Now} :When removing paramters in {this.GetType()} is null  !");
                    return;
                }

                // TabPage
                if (value is not ITabFormPage tabPage)
                {
                    return;
                }

                int index = owner.FindTabPage(tabPage);
                int curSelectedIndex = owner.SelectedIndex;

                if (index != -1)
                {
                    owner.RemoveTabPage(index);
                    if (index == curSelectedIndex)
                    {
                        owner.SelectedIndex = 0; //Always select the first tabPage is the Selected TabPage is removed.
                    }
                }
                owner.UpdateTabSelection(false);
            }

        }


        internal class TabFormControlLayout : LayoutEngine
        {
            public TabFormControlLayout()
                : base()
            {
            }

            public override void InitLayout(object child, BoundsSpecified specified)
            {
                base.InitLayout(child, specified);
            }

            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                var parent = container as TabFormControl;

                if (parent != null)
                {
                    // Use DisplayRectangle
                    Rectangle parentDisplayRectangle = parent.DisplayRectangle;
                    Point nextControlLocation = parentDisplayRectangle.Location;

                    //var tabhd = parent.TabHeader;
                    //bool hadth = parent.HasTabHeader();

                    int width = parentDisplayRectangle.Width - parent.Padding.Horizontal;
                    //int headerHeight = tabhd ==null ? 0 : parent.MinHeaderHeight;

                    //MessageBox.Show($"tabhd:{tabhd}");

                    // TabFormHeader
                    //if (tabhd != null)
                    //{
                    //    nextControlLocation.Offset(parent.Padding.Left, parent.Padding.Top);
                    //    headerHeight = Math.Max(tabhd.Height, headerHeight);

                    //    if (tabhd.AutoSize)
                    //    {
                    //        tabhd.Size = tabhd.GetPreferredSize(parentDisplayRectangle.Size);
                    //    }

                    //    tabhd.Location = nextControlLocation;
                    //    tabhd.Size = new Size(width, headerHeight);

                    //    nextControlLocation.X = parentDisplayRectangle.X;
                    //    nextControlLocation.Y += headerHeight;
                    //}

                    var selTab = parent.SelectedTab;

                    // Selected
                    if (selTab != null)
                    {
                        nextControlLocation.Offset(parent.Padding.Left, /*tabhd == null ?*/ parent.Padding.Top/* : -1*/);

                        selTab.Location = nextControlLocation;
                        selTab.Size = new Size(width,
                            parent.DisplayRectangle.Height /*- headerHeight*/ - parent.Padding.Vertical);

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

        [ComVisible(false)]
        public class TabFormPageCollection : IList<ITabFormPage>
        {
            private TabFormControl owner;
            private int lastAccessedIndex = -1;

            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            public TabFormPageCollection(TabFormControl owner)
            {
                if (owner == null)
                {
                    throw new ArgumentNullException("owner");
                }
                this.owner = owner;
            }

            public virtual ITabFormPage this[int index]
            {
                get
                {
                    return owner.GetTabPage(index);
                }
                set
                {
                    // important
                    owner.SetTabPage(index, value/*, value.GetTCITEM()*/);
                }
            }

            public virtual ITabFormPage this[string key]
            {
                get
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        return null;
                    }

                    // Search for the key in our collection
                    int index = IndexOfKey(key);
                    if (IsValidIndex(index))
                    {
                        return this[index];
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            [Browsable(false)]
            public virtual int Count
            {
                get
                {
                    return owner.TabCount;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            // <-- NEW ADD OVERLOADS FOR WHIDBEY

            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            public virtual void Add(ITabFormPage value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                owner.Controls.Add((Control)value);
            }

            public void Add(string text)
            {
                var page = owner.CreateTabFormPage();
                page.Text = text;
                Add(page);
            }

            public void Add(string key, string text)
            {
                var page = owner.CreateTabFormPage();
                page.Name = key;
                page.Text = text;
                Add(page);
            }

            public void Add(string key, string text, Image image)
            {
                var page = owner.CreateTabFormPage();
                page.Name = key;
                ((Control)page).Text = text;
                page.Image = image;
                Add(page);
            }

            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            public void AddRange(ITabFormPage[] pages)
            {
                if (pages == null)
                {
                    throw new ArgumentNullException("pages");
                }
                foreach (ITabFormPage page in pages)
                {
                    Add(page);
                }
            }

            // END - NEW ADD OVERLOADS FOR WHIDBEY -->

            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            public bool Contains(ITabFormPage page)
            {
                //check for the page not to be null
                if (page == null)
                    throw new ArgumentNullException("value");
                //end check

                return IndexOf(page) != -1;
            }

            public virtual bool ContainsKey(string key)
            {
                return IsValidIndex(IndexOfKey(key));
            }

            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            public int IndexOf(ITabFormPage page)
            {
                //check for the page not to be null
                if (page == null)
                    throw new ArgumentNullException("value");
                //end check

                for (int index = 0; index < Count; ++index)
                {
                    if (this[index] == page)
                    {
                        return index;
                    }
                }
                return -1;
            }

            public virtual int IndexOfKey(String key)
            {
                // Step 0 - Arg validation
                if (string.IsNullOrEmpty(key))
                {
                    return -1;
                }

                // step 1 - check the last cached item
                if (IsValidIndex(lastAccessedIndex))
                {
                    if (WindowsFormsUtils.SafeCompareStrings(((Control)this[lastAccessedIndex]).Name, key, /* ignoreCase = */ true))
                    {
                        return lastAccessedIndex;
                    }
                }

                // step 2 - search for the item
                for (int i = 0; i < this.Count; i++)
                {
                    if (WindowsFormsUtils.SafeCompareStrings(((Control)this[i]).Name, key, /* ignoreCase = */ true))
                    {
                        lastAccessedIndex = i;
                        return i;
                    }
                }

                // step 3 - we didn't find it.  Invalidate the last accessed index and return -1.
                lastAccessedIndex = -1;
                return -1;
            }

            // <-- NEW INSERT OVERLOADS FOR WHIDBEY

            public virtual void Insert(int index, ITabFormPage tabPage)
            {
                owner.InsertItem(index, tabPage);
                try
                {
                    // 247078 : See InsertingItem property
                    owner.InsertingItem = true;
                    owner.Controls.Add((Control)tabPage);
                }
                finally
                {
                    owner.InsertingItem = false;
                }

                owner.Controls.SetChildIndex((Control)tabPage, index);
            }

            public void Insert(int index, string text)
            {
                var page = owner.CreateTabFormPage();
                page.Text = text;
                Insert(index, page);
            }

            public void Insert(int index, string key, string text)
            {
                var page = owner.CreateTabFormPage();
                page.Name = key;
                page.Text = text;
                Insert(index, page);
            }

            public void Insert(int index, string key, string text, Image image)
            {
                var page = owner.CreateTabFormPage();
                page.Name = key;
                page.Text = text;
                Insert(index, page);
                // ImageKey and ImageIndex require parenting...
                page.Image = image;
            }

            // END - NEW INSERT OVERLOADS FOR WHIDBEY -->

            private bool IsValidIndex(int index)
            {
                return ((index >= 0) && (index < this.Count));
            }

            public virtual void Clear()
            {
                owner.RemoveAll();
            }

            void ICollection<ITabFormPage>.CopyTo(ITabFormPage[] dest, int index)
            {
                if (Count > 0)
                {
                    System.Array.Copy(owner.GetTabPages(), 0, dest, index, Count);
                }
            }

            public IEnumerator GetEnumerator()
            {
                var tabPages = owner.GetTabPages();
                if (tabPages != null)
                {
                    return tabPages.GetEnumerator();
                }
                else
                {
                    return Array.Empty<ITabFormPage>().GetEnumerator();
                }
            }

            IEnumerator<ITabFormPage> IEnumerable<ITabFormPage>.GetEnumerator()
            {
                return (IEnumerator<ITabFormPage>)GetEnumerator();
            }


            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            public virtual bool Remove(ITabFormPage value)
            {
                //check for the value not to be null
                if (value == null)
                    throw new ArgumentNullException("value");
                //end check

                owner.Controls.Remove((Control)value);

                return true;
            }

            public virtual void RemoveAt(int index)
            {
                owner.Controls.RemoveAt(index);
            }

            public virtual void RemoveByKey(string key)
            {
                int index = IndexOfKey(key);
                if (IsValidIndex(index))
                {
                    RemoveAt(index);
                }
            }


        }




    }






}
