using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;

#pragma warning disable CS8603

namespace System.Windows.Forms
{
    //public partial class TabFormControl
    //{
    //    public class TabFormPageCollection : IList<ITabFormPage>
    //    {
    //        private TabFormControl owner;
    //        private int lastAccessedIndex = -1;

    //        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    //        public TabFormPageCollection(TabFormControl owner)
    //        {
    //            if (owner == null)
    //            {
    //                throw new ArgumentNullException("owner");
    //            }
    //            this.owner = owner;
    //        }

    //        public virtual ITabFormPage this[int index]
    //        {
    //            get
    //            {
    //                return owner.GetTabPage(index);
    //            }
    //            set
    //            {
    //                // important
    //                owner.SetTabPage(index, value/*, value.GetTCITEM()*/);
    //            }
    //        }

    //        public virtual ITabFormPage this[string key]
    //        {
    //            get
    //            {
    //                if (string.IsNullOrEmpty(key))
    //                {
    //                    return null;
    //                }

    //                // Search for the key in our collection
    //                int index = IndexOfKey(key);
    //                if (IsValidIndex(index))
    //                {
    //                    return this[index];
    //                }
    //                else
    //                {
    //                    return null;
    //                }
    //            }
    //        }

    //        [Browsable(false)]
    //        public virtual int Count
    //        {
    //            get
    //            {
    //                return owner.TabCount;
    //            }
    //        }

    //        public bool IsReadOnly
    //        {
    //            get
    //            {
    //                return false;
    //            }
    //        }

    //        // <-- NEW ADD OVERLOADS FOR WHIDBEY

    //        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    //        public virtual void Add(ITabFormPage value)
    //        {
    //            if (value == null)
    //            {
    //                throw new ArgumentNullException("value");
    //            }

    //            owner.Controls.Add((Control)value);
    //        }

    //        public void Add(string text)
    //        {
    //            var page = owner.CreateTabFormPage();
    //            page.Text = text;
    //            Add(page);
    //        }

    //        public void Add(string key, string text)
    //        {
    //            var page = owner.CreateTabFormPage();
    //            page.Name = key;
    //           page.Text = text;
    //            Add(page);
    //        }

    //        public void Add(string key, string text, Image image)
    //        {
    //            var page = owner.CreateTabFormPage();
    //           page.Name = key;
    //            ((Control)page).Text = text;
    //            page.Image = image;
    //            Add(page);
    //        }

    //        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    //        public void AddRange(ITabFormPage[] pages)
    //        {
    //            if (pages == null)
    //            {
    //                throw new ArgumentNullException("pages");
    //            }
    //            foreach (ITabFormPage page in pages)
    //            {
    //                Add(page);
    //            }
    //        }

    //        // END - NEW ADD OVERLOADS FOR WHIDBEY -->

    //        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    //        public bool Contains(ITabFormPage page)
    //        {
    //            //check for the page not to be null
    //            if (page == null)
    //                throw new ArgumentNullException("value");
    //            //end check

    //            return IndexOf(page) != -1;
    //        }

    //        public virtual bool ContainsKey(string key)
    //        {
    //            return IsValidIndex(IndexOfKey(key));
    //        }

    //        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    //        public int IndexOf(ITabFormPage page)
    //        {
    //            //check for the page not to be null
    //            if (page == null)
    //                throw new ArgumentNullException("value");
    //            //end check

    //            for (int index = 0; index < Count; ++index)
    //            {
    //                if (this[index] == page)
    //                {
    //                    return index;
    //                }
    //            }
    //            return -1;
    //        }

    //        public virtual int IndexOfKey(String key)
    //        {
    //            // Step 0 - Arg validation
    //            if (string.IsNullOrEmpty(key))
    //            {
    //                return -1; 
    //            }

    //            // step 1 - check the last cached item
    //            if (IsValidIndex(lastAccessedIndex))
    //            {
    //                if (WindowsFormsUtils.SafeCompareStrings(((Control)this[lastAccessedIndex]).Name, key, /* ignoreCase = */ true))
    //                {
    //                    return lastAccessedIndex;
    //                }
    //            }

    //            // step 2 - search for the item
    //            for (int i = 0; i < this.Count; i++)
    //            {
    //                if (WindowsFormsUtils.SafeCompareStrings(((Control)this[i]).Name, key, /* ignoreCase = */ true))
    //                {
    //                    lastAccessedIndex = i;
    //                    return i;
    //                }
    //            }

    //            // step 3 - we didn't find it.  Invalidate the last accessed index and return -1.
    //            lastAccessedIndex = -1;
    //            return -1;
    //        }

    //        // <-- NEW INSERT OVERLOADS FOR WHIDBEY

    //        public virtual void Insert(int index, ITabFormPage tabPage)
    //        {
    //            owner.InsertItem(index, tabPage);
    //            try
    //            {
    //                // 247078 : See InsertingItem property
    //                owner.InsertingItem = true;
    //                owner.Controls.Add((Control)tabPage);
    //            }
    //            finally
    //            {
    //                owner.InsertingItem = false;
    //            }

    //            owner.Controls.SetChildIndex((Control)tabPage, index);
    //        }

    //        public void Insert(int index, string text)
    //        {
    //            var page = owner.CreateTabFormPage();
    //            page.Text = text;
    //            Insert(index, page);
    //        }

    //        public void Insert(int index, string key, string text)
    //        {
    //           var page = owner.CreateTabFormPage();
    //            page.Name = key;
    //            page.Text = text;
    //            Insert(index, page);
    //        }

    //        public void Insert(int index, string key, string text, Image image)
    //        {
    //            var page = owner.CreateTabFormPage();
    //            page.Name = key;
    //            page.Text = text;
    //            Insert(index, page);
    //            // ImageKey and ImageIndex require parenting...
    //            page.Image = image;
    //        }

    //        // END - NEW INSERT OVERLOADS FOR WHIDBEY -->

    //        private bool IsValidIndex(int index)
    //        {
    //            return ((index >= 0) && (index < this.Count));
    //        }

    //        public virtual void Clear()
    //        {
    //            owner.RemoveAll();
    //        }

    //        void ICollection<ITabFormPage>.CopyTo(ITabFormPage[] dest, int index)
    //        {
    //            if (Count > 0)
    //            {
    //                System.Array.Copy(owner.GetTabPages(), 0, dest, index, Count);
    //            }
    //        }

    //        public IEnumerator GetEnumerator()
    //        {
    //            var tabPages = owner.GetTabPages();
    //            if (tabPages != null)
    //            {
    //                return tabPages.GetEnumerator();
    //            }
    //            else
    //            {
    //                return Array.Empty<ITabFormPage>().GetEnumerator();
    //            }
    //        }

    //        IEnumerator<ITabFormPage> IEnumerable<ITabFormPage>.GetEnumerator()
    //        {
    //            return (IEnumerator<ITabFormPage>)GetEnumerator();
    //        }


    //        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    //        public virtual bool Remove(ITabFormPage value)
    //        {
    //            //check for the value not to be null
    //            if (value == null)
    //                throw new ArgumentNullException("value");
    //            //end check

    //            owner.Controls.Remove((Control)value);

    //            return true;
    //        }

    //        public virtual void RemoveAt(int index)
    //        {
    //            owner.Controls.RemoveAt(index);
    //        }

    //        public virtual void RemoveByKey(string key)
    //        {
    //            int index = IndexOfKey(key);
    //            if (IsValidIndex(index))
    //            {
    //                RemoveAt(index);
    //            }
    //        }


    //    }
    //}

}
