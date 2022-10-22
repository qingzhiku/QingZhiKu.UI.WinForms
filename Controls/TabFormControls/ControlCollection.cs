using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public partial class TabFormControl
    {
        [ComVisible(false)]
        public new class ControlCollection : Control.ControlCollection
        {
            private TabFormControl owner;
            //private TabFormHeader cacheTabFormHeader;

            public ControlCollection(TabFormControl owner)
                : base(owner)
            {
                this.owner = owner;
            }

            public override void Add(Control value)
            {
                if(value == null)
                {
                    Console.WriteLine($"{DateTime.Now} :When adding paramters in {this.GetType()} is null  !");
                    return;
                }

                if(value is TabFormHeader tabHeader)
                {
                    if (null == GetTabHeader())
                    {
                        //cacheTabFormHeader = tabHeader;
                        base.Add(tabHeader);
                        SetChildIndex(tabHeader, 0);
                    }

                    return;
                }

                // TabPage
                var tabPage = value as ITabFormPage;
                if (tabPage == null)
                {
                    Console.WriteLine($"{DateTime.Now} : {SR.GetString(SR.TabControlInvalidTabPageType, value.GetType().Name)}");
                    return;
                }

                //if (value is not ITabFormPage<Control> tabPage)
                //{
                //    Console.WriteLine($"{DateTime.Now} : {SR.GetString(SR.TabControlInvalidTabPageType, value.GetType().Name)}");
                //    return;
                //}

                //if (value is not Control tabPageControl)
                //{
                //    Console.WriteLine(SR.GetString(SR.TabControlInvalidTabPageType, value.GetType().Name));
                //    return;
                //}

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

                // TabHeader
                if (value is TabFormHeader tabHeader)
                {
                    if (null == GetTabHeader())
                    {
                        //Console.WriteLine($"cacheTabFormHeader：{cacheTabFormHeader}");

                        //cacheTabFormHeader = null;
                        base.Remove(tabHeader);
                    }

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

            protected internal virtual TabFormHeader GetTabHeader() 
            {
                //if (cacheTabFormHeader != null)
                //{
                //    return cacheTabFormHeader;
                //}

                TabFormHeader result = null;

                for (int i = 0; i < base.Count; i++)
                {
                    if(base[i] is TabFormHeader header)
                    {
                        result = header;
                    }
                }

                return result;
            }

            //public override void Clear()
            //{
            //    base.Clear();

            //    owner.RemoveAll();
            //}




        }

    }

}
