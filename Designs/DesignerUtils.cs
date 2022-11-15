using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Behaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Design
{
    internal static class DesignerUtils
    {
        private static Size minDragSize;
        private static SolidBrush hoverBrush;
        private static HatchBrush selectionBorderBrush;
        private static IntPtr grabHandleFillBrushPrimary;
        private static IntPtr grabHandleFillBrush;
        private static IntPtr grabHandlePenPrimary;
        private static IntPtr grabHandlePen;
        private static Bitmap boxImage;
        public static int BOXIMAGESIZE;
        public static int SELECTIONBORDERSIZE;
        public static int SELECTIONBORDERHITAREA;
        public static int HANDLESIZE;
        public static int HANDLEOVERLAP;
        public static int SELECTIONBORDEROFFSET;
        public static int NORESIZEHANDLESIZE;
        public static int NORESIZEBORDEROFFSET;
        public static int LOCKHANDLEHEIGHT;
        public static int LOCKHANDLEWIDTH;
        public static int LOCKHANDLEOVERLAP;
        public static int LOCKEDSELECTIONBORDEROFFSET_Y;
        public static int LOCKEDSELECTIONBORDEROFFSET_X;
        public static int LOCKHANDLESIZE_UPPER;
        public static int LOCKHANDLEHEIGHT_LOWER;
        public static int LOCKHANDLEWIDTH_LOWER;
        public static int LOCKHANDLEUPPER_OFFSET;
        public static int LOCKHANDLELOWER_OFFSET;
        public static int CONTAINERGRABHANDLESIZE;
        public static int SNAPELINEDELAY;
        public static int MINIMUMSTYLESIZE;
        public static int MINIMUMSTYLEPERCENT;
        public static int MINCONTROLBITMAPSIZE;
        public static int MINUMUMSTYLESIZEDRAG;
        public static int DEFAULTROWCOUNT;
        public static int DEFAULTCOLUMNCOUNT;
        public static int RESIZEGLYPHSIZE;
        public static int DEFAULTFORMPADDING;
        public static readonly ContentAlignment anyTopAlignment;
        public static readonly ContentAlignment anyMiddleAlignment;

        public static Image BoxImage
        {
            get
            {
                if (boxImage == null)
                {
                    boxImage = new Bitmap(BOXIMAGESIZE, BOXIMAGESIZE, PixelFormat.Format32bppPArgb);
                    using Graphics graphics = Graphics.FromImage(boxImage);
                    graphics.FillRectangle(new SolidBrush(SystemColors.InactiveBorder), 0, 0, BOXIMAGESIZE, BOXIMAGESIZE);
                    graphics.DrawRectangle(new Pen(SystemColors.ControlDarkDark), 0, 0, BOXIMAGESIZE - 1, BOXIMAGESIZE - 1);
                }
                return boxImage;
            }
        }

        public static Brush HoverBrush => hoverBrush;

        public static Size MinDragSize
        {
            get
            {
                if (minDragSize == Size.Empty)
                {
                    Size dragSize = SystemInformation.DragSize;
                    Size doubleClickSize = SystemInformation.DoubleClickSize;
                    minDragSize.Width = Math.Max(dragSize.Width, doubleClickSize.Width);
                    minDragSize.Height = Math.Max(dragSize.Height, doubleClickSize.Height);
                }
                return minDragSize;
            }
        }

        public static Point LastCursorPoint
        {
            get
            {
                int messagePos = Win32.GetMessagePos();
                return new Point(Win32.Util.SignedLOWORD(messagePos), Win32.Util.SignedHIWORD(messagePos));
            }
        }

        static DesignerUtils()
        {
            minDragSize = Size.Empty;
            hoverBrush = new SolidBrush(Color.FromArgb(50, SystemColors.Highlight));
            selectionBorderBrush = new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDarkDark, Color.Transparent);
            grabHandleFillBrushPrimary = Win32.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.Window));
            grabHandleFillBrush = Win32.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.ControlText));
            grabHandlePenPrimary = Win32.CreatePen(Win32.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.ControlText));
            grabHandlePen = Win32.CreatePen(Win32.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.Window));
            boxImage = null;
            BOXIMAGESIZE = 16;
            SELECTIONBORDERSIZE = 1;
            SELECTIONBORDERHITAREA = 3;
            HANDLESIZE = 7;
            HANDLEOVERLAP = 2;
            SELECTIONBORDEROFFSET = (HANDLESIZE - SELECTIONBORDERSIZE) / 2 - HANDLEOVERLAP;
            NORESIZEHANDLESIZE = 5;
            NORESIZEBORDEROFFSET = (NORESIZEHANDLESIZE - SELECTIONBORDERSIZE) / 2;
            LOCKHANDLEHEIGHT = 9;
            LOCKHANDLEWIDTH = 7;
            LOCKHANDLEOVERLAP = 2;
            LOCKEDSELECTIONBORDEROFFSET_Y = (LOCKHANDLEHEIGHT - SELECTIONBORDERSIZE) / 2 - LOCKHANDLEOVERLAP;
            LOCKEDSELECTIONBORDEROFFSET_X = (LOCKHANDLEWIDTH - SELECTIONBORDERSIZE) / 2 - LOCKHANDLEOVERLAP;
            LOCKHANDLESIZE_UPPER = 5;
            LOCKHANDLEHEIGHT_LOWER = 6;
            LOCKHANDLEWIDTH_LOWER = 7;
            LOCKHANDLEUPPER_OFFSET = (LOCKHANDLEWIDTH_LOWER - LOCKHANDLESIZE_UPPER) / 2;
            LOCKHANDLELOWER_OFFSET = LOCKHANDLEHEIGHT - LOCKHANDLEHEIGHT_LOWER;
            CONTAINERGRABHANDLESIZE = 15;
            SNAPELINEDELAY = 1000;
            MINIMUMSTYLESIZE = 20;
            MINIMUMSTYLEPERCENT = 50;
            MINCONTROLBITMAPSIZE = 1;
            MINUMUMSTYLESIZEDRAG = 8;
            DEFAULTROWCOUNT = 2;
            DEFAULTCOLUMNCOUNT = 2;
            RESIZEGLYPHSIZE = 4;
            DEFAULTFORMPADDING = 9;
            anyTopAlignment = (ContentAlignment)7;
            anyMiddleAlignment = (ContentAlignment)112;
        }

        public static void SyncBrushes()
        {
            hoverBrush.Dispose();
            hoverBrush = new SolidBrush(Color.FromArgb(50, SystemColors.Highlight));
            selectionBorderBrush.Dispose();
            selectionBorderBrush = new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDarkDark, Color.Transparent);
            Win32.DeleteObject(new HandleRef(null, grabHandleFillBrushPrimary));
            grabHandleFillBrushPrimary = Win32.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.Window));
            Win32.DeleteObject(new HandleRef(null, grabHandleFillBrush));
            grabHandleFillBrush = Win32.CreateSolidBrush(ColorTranslator.ToWin32(SystemColors.ControlText));
            Win32.DeleteObject(new HandleRef(null, grabHandlePenPrimary));
            grabHandlePenPrimary = Win32.CreatePen(Win32.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.ControlText));
            Win32.DeleteObject(new HandleRef(null, grabHandlePen));
            grabHandlePen = Win32.CreatePen(Win32.PS_SOLID, 1, ColorTranslator.ToWin32(SystemColors.Window));
        }

        private static void DrawDragBorder(Graphics g, Size imageSize, int borderSize, Color backColor)
        {
            Pen pen = SystemPens.ControlDarkDark;
            if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
            {
                pen = SystemPens.ControlLight;
            }
            g.DrawLine(pen, 1, 0, imageSize.Width - 2, 0);
            g.DrawLine(pen, 1, imageSize.Height - 1, imageSize.Width - 2, imageSize.Height - 1);
            g.DrawLine(pen, 0, 1, 0, imageSize.Height - 2);
            g.DrawLine(pen, imageSize.Width - 1, 1, imageSize.Width - 1, imageSize.Height - 2);
            for (int i = 1; i < borderSize; i++)
            {
                g.DrawRectangle(pen, i, i, imageSize.Width - (2 + i), imageSize.Height - (2 + i));
            }
        }

        public static void DrawResizeBorder(Graphics g, Region resizeBorder, Color backColor)
        {
            Brush brush = SystemBrushes.ControlDarkDark;
            if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
            {
                brush = SystemBrushes.ControlLight;
            }
            g.FillRegion(brush, resizeBorder);
        }

        public static void DrawFrame(Graphics g, Region resizeBorder, FrameStyle style, Color backColor)
        {
            Color color = SystemColors.ControlDarkDark;
            if (backColor != Color.Empty && (double)backColor.GetBrightness() < 0.5)
            {
                color = SystemColors.ControlLight;
            }
            Brush brush = style switch
            {
                FrameStyle.Dashed => new HatchBrush(HatchStyle.Percent50, color, Color.Transparent),
                _ => new SolidBrush(color),
            };
            g.FillRegion(brush, resizeBorder);
            brush.Dispose();
        }

        public static void DrawGrabHandle(Graphics graphics, Rectangle bounds, bool isPrimary, Glyph glyph)
        {
            IntPtr hdc = graphics.GetHdc();
            try
            {
                IntPtr handle = Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? grabHandleFillBrushPrimary : grabHandleFillBrush));
                IntPtr handle2 = Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? grabHandlePenPrimary : grabHandlePen));
                Win32.RoundRect(new HandleRef(glyph, hdc), bounds.Left, bounds.Top, bounds.Right, bounds.Bottom, 2, 2);
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle));
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle2));
            }
            finally
            {
                graphics.ReleaseHdcInternal(hdc);
            }
        }

        public static void DrawNoResizeHandle(Graphics graphics, Rectangle bounds, bool isPrimary, Glyph glyph)
        {
            IntPtr hdc = graphics.GetHdc();
            try
            {
                IntPtr handle = Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? grabHandleFillBrushPrimary : grabHandleFillBrush));
                IntPtr handle2 = Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, grabHandlePenPrimary));
                Win32.Rectangle(new HandleRef(glyph, hdc), bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle));
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle2));
            }
            finally
            {
                graphics.ReleaseHdcInternal(hdc);
            }
        }

        public static void DrawLockedHandle(Graphics graphics, Rectangle bounds, bool isPrimary, Glyph glyph)
        {
            IntPtr hdc = graphics.GetHdc();
            try
            {
                IntPtr handle = Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, grabHandlePenPrimary));
                IntPtr handle2 = Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, grabHandleFillBrushPrimary));
                Win32.RoundRect(new HandleRef(glyph, hdc), bounds.Left + LOCKHANDLEUPPER_OFFSET, bounds.Top, bounds.Left + LOCKHANDLEUPPER_OFFSET + LOCKHANDLESIZE_UPPER, bounds.Top + LOCKHANDLESIZE_UPPER, 2, 2);
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, isPrimary ? grabHandleFillBrushPrimary : grabHandleFillBrush));
                Win32.Rectangle(new HandleRef(glyph, hdc), bounds.Left, bounds.Top + LOCKHANDLELOWER_OFFSET, bounds.Right, bounds.Bottom);
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle2));
                Win32.SelectObject(new HandleRef(glyph, hdc), new HandleRef(glyph, handle));
            }
            finally
            {
                graphics.ReleaseHdcInternal(hdc);
            }
        }

        public static void DrawSelectionBorder(Graphics graphics, Rectangle bounds)
        {
            graphics.FillRectangle(selectionBorderBrush, bounds);
        }

        public static void GenerateSnapShot(Control control, ref Image image, int borderSize, double opacity, Color backColor)
        {
            if (!GenerateSnapShotWithWM_PRINT(control, ref image))
            {
                GenerateSnapShotWithBitBlt(control, ref image);
            }
            if (opacity < 1.0 && opacity > 0.0)
            {
                SetImageAlpha((Bitmap)image, opacity);
            }
            if (borderSize > 0)
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    DrawDragBorder(g, image.Size, borderSize, backColor);
                }
            }
        }

        public static bool UseSnapLines(IServiceProvider provider)
        {
            bool result = true;
            object obj = null;
            if (provider.GetService(typeof(DesignerOptionService)) is DesignerOptionService designerOptionService)
            {
                PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties["UseSnapLines"];
                if (propertyDescriptor != null)
                {
                    obj = propertyDescriptor.GetValue(null);
                }
            }
            if (obj != null && obj is bool)
            {
                result = (bool)obj;
            }
            return result;
        }

        public static object GetOptionValue(IServiceProvider provider, string name)
        {
            object result = null;
            if (provider != null)
            {
                if (provider.GetService(typeof(DesignerOptionService)) is DesignerOptionService designerOptionService)
                {
                    PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties[name];
                    if (propertyDescriptor != null)
                    {
                        result = propertyDescriptor.GetValue(null);
                    }
                }
                else if (provider.GetService(typeof(IDesignerOptionService)) is IDesignerOptionService designerOptionService2)
                {
                    result = designerOptionService2.GetOptionValue("WindowsFormsDesigner\\General", name);
                }
            }
            return result;
        }

        public static void GenerateSnapShotWithBitBlt(Control control, ref Image image)
        {
            HandleRef hWnd = new HandleRef(control, control.Handle);
            IntPtr dC = Win32.GetDC(hWnd);
            image = new Bitmap(Math.Max(control.Width, MINCONTROLBITMAPSIZE), Math.Max(control.Height, MINCONTROLBITMAPSIZE), PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(image);
            if (control.BackColor == Color.Transparent)
            {
                graphics.Clear(SystemColors.Control);
            }
            IntPtr hdc = graphics.GetHdc();
            Win32.BitBlt(hdc, 0, 0, image.Width, image.Height, dC, 0, 0, 13369376);
            graphics.ReleaseHdc(hdc);
        }

        public static bool GenerateSnapShotWithWM_PRINT(Control control, ref Image image)
        {
            IntPtr handle = control.Handle;
            image = new Bitmap(Math.Max(control.Width, MINCONTROLBITMAPSIZE), Math.Max(control.Height, MINCONTROLBITMAPSIZE), PixelFormat.Format32bppPArgb);
            if (control.BackColor == Color.Transparent)
            {
                using Graphics graphics = Graphics.FromImage(image);
                graphics.Clear(SystemColors.Control);
            }
            Color color = Color.FromArgb(255, 252, 186, 238);
            ((Bitmap)image).SetPixel(image.Width / 2, image.Height / 2, color);
            using (Graphics graphics2 = Graphics.FromImage(image))
            {
                IntPtr hdc = graphics2.GetHdc();
                Win32.SendMessage(handle, 791, hdc, (IntPtr)30);
                graphics2.ReleaseHdc(hdc);
            }
            if (((Bitmap)image).GetPixel(image.Width / 2, image.Height / 2).Equals(color))
            {
                return false;
            }
            return true;
        }

        internal enum SelectionBorderGlyphType
        {
            Top = 0,
            Bottom = 1,
            Left = 2,
            Right = 3,
            Body = 4
        }

        public static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type, int borderSize)
        {
            Rectangle result = Rectangle.Empty;
            switch (type)
            {
                case SelectionBorderGlyphType.Top:
                    result = new Rectangle(originalBounds.Left - borderSize, originalBounds.Top - borderSize, originalBounds.Width + 2 * borderSize, borderSize);
                    break;
                case SelectionBorderGlyphType.Bottom:
                    result = new Rectangle(originalBounds.Left - borderSize, originalBounds.Bottom, originalBounds.Width + 2 * borderSize, borderSize);
                    break;
                case SelectionBorderGlyphType.Left:
                    result = new Rectangle(originalBounds.Left - borderSize, originalBounds.Top - borderSize, borderSize, originalBounds.Height + 2 * borderSize);
                    break;
                case SelectionBorderGlyphType.Right:
                    result = new Rectangle(originalBounds.Right, originalBounds.Top - borderSize, borderSize, originalBounds.Height + 2 * borderSize);
                    break;
                case SelectionBorderGlyphType.Body:
                    result = originalBounds;
                    break;
            }
            return result;
        }

        private static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type, int bordersize, int offset)
        {
            Rectangle result = GetBoundsForSelectionType(originalBounds, type, bordersize);
            if (offset != 0)
            {
                switch (type)
                {
                    case SelectionBorderGlyphType.Top:
                        result.Offset(-offset, -offset);
                        result.Width += 2 * offset;
                        break;
                    case SelectionBorderGlyphType.Bottom:
                        result.Offset(-offset, offset);
                        result.Width += 2 * offset;
                        break;
                    case SelectionBorderGlyphType.Left:
                        result.Offset(-offset, -offset);
                        result.Height += 2 * offset;
                        break;
                    case SelectionBorderGlyphType.Right:
                        result.Offset(offset, -offset);
                        result.Height += 2 * offset;
                        break;
                    case SelectionBorderGlyphType.Body:
                        result = originalBounds;
                        break;
                }
            }
            return result;
        }

        public static Rectangle GetBoundsForSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type)
        {
            return GetBoundsForSelectionType(originalBounds, type, SELECTIONBORDERSIZE, SELECTIONBORDEROFFSET);
        }

        public static Rectangle GetBoundsForNoResizeSelectionType(Rectangle originalBounds, SelectionBorderGlyphType type)
        {
            return GetBoundsForSelectionType(originalBounds, type, SELECTIONBORDERSIZE, NORESIZEBORDEROFFSET);
        }

        public static int GetTextBaseline(Control ctrl, ContentAlignment alignment)
        {
            Rectangle clientRectangle = ctrl.ClientRectangle;
            int num = 0;
            int num2 = 0;
            using (Graphics graphics = ctrl.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                IntPtr handle = ctrl.Font.ToHfont();
                try
                {
                    IntPtr handle2 = Win32.SelectObject(new HandleRef(ctrl, hdc), new HandleRef(ctrl, handle));
                    Win32.TEXTMETRIC tEXTMETRIC = new Win32.TEXTMETRIC();
                    Win32.GetTextMetrics(new HandleRef(ctrl, hdc), tEXTMETRIC);
                    num = tEXTMETRIC.tmAscent + 1;
                    num2 = tEXTMETRIC.tmHeight;
                    Win32.SelectObject(new HandleRef(ctrl, hdc), new HandleRef(ctrl, handle2));
                }
                finally
                {
                    Win32.DeleteObject(new HandleRef(ctrl.Font, handle));
                    graphics.ReleaseHdc(hdc);
                }
            }
            if ((alignment & anyTopAlignment) != 0)
            {
                return clientRectangle.Top + num;
            }
            if ((alignment & anyMiddleAlignment) != 0)
            {
                return clientRectangle.Top + clientRectangle.Height / 2 - num2 / 2 + num;
            }
            return clientRectangle.Bottom - num2 + num;
        }


        public static string GetUniqueSiteName(IDesignerHost host, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            INameCreationService nameCreationService = (INameCreationService)host.GetService(typeof(INameCreationService));
            if (nameCreationService == null)
            {
                return null;
            }
            object obj = host.Container.Components[name];
            if (obj == null)
            {
                if (!nameCreationService.IsValidName(name))
                {
                    return null;
                }
                return name;
            }
            string text = name;
            int num = 1;
            while (!nameCreationService.IsValidName(text))
            {
                text = name + num.ToString(CultureInfo.InvariantCulture);
                num++;
            }
            return text;
        }

        private unsafe static void SetImageAlpha(Bitmap b, double opacity)
        {
            if (opacity == 1.0)
            {
                return;
            }
            byte[] array = new byte[256];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)((double)i * opacity);
            }
            BitmapData bitmapData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            try
            {
                int num = bitmapData.Height * bitmapData.Width;
                int* ptr = (int*)(void*)bitmapData.Scan0;
                byte* ptr2 = (byte*)(ptr + num);
                for (byte* ptr3 = (byte*)ptr + 3; ptr3 < ptr2; ptr3 += 4)
                {
                    *ptr3 = array[*ptr3];
                }
            }
            finally
            {
                b.UnlockBits(bitmapData);
            }
        }

        public static ICollection FilterGenericTypes(ICollection types)
        {
            if (types == null || types.Count == 0)
            {
                return types;
            }
            ArrayList arrayList = new ArrayList(types.Count);
            foreach (Type type in types)
            {
                if (!type.ContainsGenericParameters)
                {
                    arrayList.Add(type);
                }
            }
            return arrayList;
        }

        public static IContainer CheckForNestedContainer(IContainer container)
        {
            if (container is NestedContainer nestedContainer)
            {
                return nestedContainer.Owner.Site.Container;
            }
            return container;
        }

        public static ICollection CopyDragObjects(ICollection objects, IServiceProvider svcProvider)
        {
            if (objects == null || svcProvider == null)
            {
                return null;
            }
            Cursor current = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ComponentSerializationService componentSerializationService = svcProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
                IDesignerHost designerHost = svcProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (componentSerializationService != null && designerHost != null)
                {
                    SerializationStore serializationStore = null;
                    serializationStore = componentSerializationService.CreateStore();
                    ICollection copySelection = GetCopySelection(objects, designerHost);
                    foreach (IComponent item in copySelection)
                    {
                        componentSerializationService.Serialize(serializationStore, item);
                    }
                    serializationStore.Close();
                    copySelection = componentSerializationService.Deserialize(serializationStore);
                    ArrayList arrayList = new ArrayList(objects.Count);
                    foreach (IComponent item2 in copySelection)
                    {
                        Control control = item2 as Control;
                        if (control != null && control.Parent == null)
                        {
                            arrayList.Add(item2);
                        }
                        else if (control == null && item2 is ToolStripItem toolStripItem && toolStripItem.GetCurrentParent() == null)
                        {
                            arrayList.Add(item2);
                        }
                    }
                    return arrayList;
                }
            }
            finally
            {
                Cursor.Current = current;
            }
            return null;
        }

        private static ICollection GetCopySelection(ICollection objects, IDesignerHost host)
        {
            if (objects == null || host == null)
            {
                return null;
            }
            ArrayList arrayList = new ArrayList();
            foreach (IComponent @object in objects)
            {
                arrayList.Add(@object);
                GetAssociatedComponents(@object, host, arrayList);
            }
            return arrayList;
        }

        internal static void GetAssociatedComponents(IComponent component, IDesignerHost host, ArrayList list)
        {
            if (host == null || !(host.GetDesigner(component) is ComponentDesigner componentDesigner))
            {
                return;
            }
            foreach (IComponent associatedComponent in componentDesigner.AssociatedComponents)
            {
                if (associatedComponent.Site != null)
                {
                    list.Add(associatedComponent);
                    GetAssociatedComponents(associatedComponent, host, list);
                }
            }
        }

        private static int TreeView_GetExtendedStyle(IntPtr handle)
        {
            return Convert.ToInt32(Win32.SendMessage(handle, 4397, IntPtr.Zero, IntPtr.Zero));
        }

        private static void TreeView_SetExtendedStyle(IntPtr handle, int extendedStyle, int mask)
        {
            Win32.SendMessage(handle, 4396, new IntPtr(mask), new IntPtr(extendedStyle));
        }

        public static void ApplyTreeViewThemeStyles(TreeView treeView)
        {
            if (treeView == null)
            {
                throw new ArgumentNullException("treeView");
            }
            treeView.HotTracking = true;
            treeView.ShowLines = false;
            IntPtr handle = treeView.Handle;
            Win32.SetWindowTheme(handle, "Explorer", null);
            int num = TreeView_GetExtendedStyle(handle);
            num |= 0x44;
            TreeView_SetExtendedStyle(handle, num, 0);
        }

        private static void ListView_SetExtendedListViewStyleEx(IntPtr handle, int mask, int extendedStyle)
        {
            Win32.SendMessage(handle, 4150, new IntPtr(mask), new IntPtr(extendedStyle));
        }

        public static void ApplyListViewThemeStyles(ListView listView)
        {
            if (listView == null)
            {
                throw new ArgumentNullException("listView");
            }
            IntPtr handle = listView.Handle;
            Win32.SetWindowTheme(handle, "Explorer", null);
            ListView_SetExtendedListViewStyleEx(handle, 65536, 65536);
        }




    }
}
