using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    public static class FormEx
    {
        /// <summary>
        /// 为窗体创建圆角矩形区域
        /// </summary>
        public static void CreateRoundRectRegion(this Form form, int radius, int? width, int? height)
        {
            var bounds = new Rectangle(0,0, (width ?? form.Width), (height ?? form.Height));

            //using (GraphicsPath path = DrawingHelper.CreateRoundRectanglePath(bounds, radius))
            //{
            //    Region region = new Region(path);
            //    //path.Widen(pen);
            //    region.Union(path);
            //    form.Region = region;
            //}
            form.CreateRoundRectRegion(bounds, radius);
        }

        public static void CreateRoundRectRegion(this Form form, Rectangle rectangle, int radius)
        {
            using (GraphicsPath path = DrawingHelper.CreateRoundRectanglePath(rectangle, radius))
            {
                Region region = new Region(path);
                //path.Widen(pen);
                region.Union(path);
                form.Region = region;
            }
        }

        /// <summary>
        /// 为窗体创建圆角矩形区域
        /// </summary>
        public static void CreateRoundRectRegion(this Form form, short radius, int? width, int? height)
        {
            form.Region?.Dispose();

            var rgn = Win32.CreateRoundRectRgn(0, 0, (width ?? form.Width), (height ?? form.Height), radius, radius);

            Win32.SetWindowRgn(form.Handle, (int)rgn, true);
        }
        
        /// <summary>
        /// 更新阴影层背景图片
        /// </summary>
        //public static void UpdateShadowBitmap(this DropShadowForm form)
        //{
        //    // 此时还没有Master窗体赋值，不添加图片
        //    if (form.Owner == null) return;

        //    //form.GetMarginRectangle();
        //}

        /// <summary>
        /// 通过反射调用窗体的UpdateStyles方法
        /// </summary>
        public static void SetStyles(this Form form, ControlStyles flag, bool value)
        {
            Type type = form.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var setStyle = type.GetMethod("SetStyle", flags);
            setStyle?.Invoke(form, new object[] { flag, value });

            var updateStyle = type.GetMethod("UpdateStyles", flags);
            updateStyle?.Invoke(form, null);
            //form.UpdateStyle();
        }

        public static void UpdateStyles(this Form form)
        {
            Type type = form.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var updateStyle = type.GetMethod("UpdateStyles", flags);
            updateStyle?.Invoke(form, null);
        }

        public static Size SizeFromClientSize(this Form form, Size size)
        {
            Size gap = Size.Empty;
            
            //Type type = form.GetType();
            //BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            //var sizeFromClientSize = type.GetMethod("SizeFromClientSize", flags);

            //gap = (Size)sizeFromClientSize?.Invoke(form, new object[] { size });

            return gap;
        }

        /// <summary>
        /// 更改样式
        /// </summary>
        public static void UpdateStyles(this Form form, int style, bool isOR = true)
        {
            int windowLong = (Win32.GetWindowLong(new HandleRef(form, form.Handle), Win32.GWL_STYLE));
            
            if (isOR)
            {
                windowLong |= style;
            }
            else
            {
                windowLong &= ~style;
            }

            Win32.SetWindowLong(new HandleRef(form, form.Handle), Win32.GWL_STYLE, windowLong); 
        }

        /// <summary>
        /// 更改扩展样式
        /// </summary>
        public static void UpdateEXStyles(this Form form, int exstyle, bool isOR = true)
        {
            int windowLong = (Win32.GetWindowLong(new HandleRef(form, form.Handle), Win32.GWL_EXSTYLE));
            
            if (isOR)
            {
                windowLong |= exstyle;
            }
            else
            {
                windowLong &= ~exstyle;
            }
            
            Win32.SetWindowLong(form.Handle, Win32.GWL_EXSTYLE, windowLong);
        }

        /// <summary>
        /// 通过反射调用窗体的DoubleBuffered属性
        /// </summary>
        public static void SetDoubleBuffered(this Control control, bool enable)
        {
            Type type = control.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            // 双缓存
            var doubleBufferPropertyInfo = type.GetProperty("DoubleBuffered", flags);
            doubleBufferPropertyInfo?.SetValue(control, enable, null);
        }

        /// <summary>
        /// 使窗口有鼠标穿透功能
        /// </summary>
        public static void CanPenetrate(this Form form)
        {
            int intExTemp = Win32.GetWindowLong(form.Handle, Win32.GWL_EXSTYLE);
            int oldGWLEx = Win32.SetWindowLong(form.Handle, Win32.GWL_EXSTYLE,
                Win32.WS_EX_TRANSPARENT | Win32.WS_EX_LAYERED);
        }

        /// <summary>
        /// 更新窗体样式
        /// </summary>
        public static void UpdateSysMenu(this Form form)
        {
            int windowLong = (Win32.GetWindowLong(new HandleRef(form, form.Handle), Win32.GWL_STYLE));

            if(form.ControlBox)
                windowLong |= Win32.WS_SYSMENU | Win32.WS_MINIMIZEBOX | Win32.WS_MAXIMIZEBOX;

            if (form.MaximizeBox)
                windowLong |= Win32.WS_MAXIMIZEBOX;

            if (form.MinimizeBox)
                windowLong |= Win32.WS_MINIMIZEBOX;

            Win32.SetWindowLong(new HandleRef(form, form.Handle), Win32.GWL_STYLE, windowLong);

            // 更新系统菜单
            IntPtr menu = Win32.GetSystemMenu(form.Handle, false);

            if (!form.ControlBox)
            {
                Win32.DeleteMenu(menu, Win32.WMSYSCOMMAND_WParam.SC_CLOSE, 0x0);//关闭  
                Win32.DeleteMenu(menu, Win32.WMSYSCOMMAND_WParam.SC_MINIMIZE, 0x0);//最小化  
                Win32.DeleteMenu(menu, Win32.WMSYSCOMMAND_WParam.SC_MAXIMIZE, 0x0);//最大化  
            }
            else
            {
                if (!form.MinimizeBox)
                {
                    Win32.DeleteMenu(menu, Win32.WMSYSCOMMAND_WParam.SC_MINIMIZE, 0x0);//最小化  
                }
                if (!form.MaximizeBox)
                {
                    Win32.DeleteMenu(menu, Win32.WMSYSCOMMAND_WParam.SC_MAXIMIZE, 0x0);//最大化  
                }
            }
        }


        /// <summary>
        /// 拖动窗体，仅在窗体上实现
        /// </summary>
        public static void DragMove(this Form form)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                Win32.Util.MoveWindow(new HandleRef(form, form.Handle));
            }
        }

        /// <summary>
        /// 获取窗体当前状态
        /// </summary>
        public static FormWindowState GetWindowState(this Form form)
        {
            return Win32.Util.GetWindowState(form);
        }

        /// <summary>
        /// 判断win11以上系统
        /// </summary>
        /// <returns></returns>
        public static bool IsGreaterWin10(this Form form)
        {
            return OSFeature.Feature.IsWindows11OrGreater();
        }

        /// <summary>
        /// 是否启用了AERO
        /// </summary>
        public static bool AeroEnabled(this Form form)
        {
            bool _aeroEnabled = false;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                Win32.DwmIsCompositionEnabled(ref enabled);
                _aeroEnabled = (enabled == 1) ? true : false;
            }

            return _aeroEnabled;
        }

        /// <summary>
        /// 在控制台写入信息
        /// </summary>
        public static void WriteLine(this Form form, string? message)
        {
            Debug.WriteLine(message);
        }


    }
}
