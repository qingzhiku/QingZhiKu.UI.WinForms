using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 第二层，接收Win32消息窗体，仅接收，不处理
    /// </summary>
    public class WindowProcForm : UpdateBaseForm
    {
        protected bool _disposing;

        public new bool IsDisposed
        {
            get
            {
                return _disposing || base.IsDisposed;
            }
        }

        public WindowProcForm()
            :base()
        {
        }

        protected override void Dispose(bool disposing)
        {
            _disposing = disposing;

            if (_disposing)
            {

            }

            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_NCACTIVATE:
                    WM_NCACTIVATE(ref m);
                    break;
                case Win32.WM_NCUAHDRAWCAPTION:
                    WM_NCUAHDRAWCAPTION(ref m);
                    break;
                case Win32.WM_NCUAHDRAWFRAME:
                    WM_NCUAHDRAWFRAME(ref m);
                    break;
                case Win32.WM_ERASEBKGND:
                    WM_ERASEBKGND(ref m);
                    break;
                case Win32.WM_NCPAINT:
                    WM_NCPAINT(ref m);
                    break;
                case Win32.WM_NCCALCSIZE:
                    WM_NCCALCSIZE(ref m);
                    break;
                case Win32.WM_NCHITTEST:
                    WM_NCHITTEST(ref m);
                    break;
                case Win32.WM_NCMOUSEMOVE:
                    WM_NCMOUSEMOVE(ref m);
                    break;
                case Win32.WM_NCLBUTTONDOWN:
                    WM_NCLBUTTONDOWN(ref m);
                    break;
                case Win32.WM_NCRBUTTONDOWN:
                    WM_NCRBUTTONDOWN(ref m);
                    break;
                case Win32.WM_NCMBUTTONDOWN:
                    WM_NCMBUTTONDOWN(ref m);
                    break;
                case Win32.WM_NCXBUTTONDOWN:
                    WM_NCXBUTTONDOWN(ref m);
                    break;
                case Win32.WM_GETMINMAXINFO:
                    WM_GETMINMAXINFO(ref m);
                    break;
                case Win32.WM_NCLBUTTONUP:
                    WM_NCLBUTTONUP(ref m);
                    break;
                case Win32.WM_NCDESTROY:
                    WM_NCDESTROY(ref m);
                    break;
                case Win32.WM_CANCELMODE:
                    WM_CANCELMODE(ref m);
                    break;
                case Win32.WM_ACTIVATE:
                    WM_ACTIVATE(ref m);
                    break;
                case Win32.WM_COMPACTING:
                    WM_COMPACTING(ref m);
                    break;
                case Win32.WM_PRINTCLIENT:
                    WM_PRINTCLIENT(ref m);
                    break;
                case Win32.WM_PAINT:
                    WM_PAINT(ref m);
                    break;
                case Win32.WM_LBUTTONDBLCLK:
                    WM_LBUTTONDBLCLK(ref m);
                    break;
                case Win32.WM_LBUTTONDOWN:
                    WM_LBUTTONDOWN(ref m);
                    break;
                case Win32.WM_LBUTTONUP:
                    WM_LBUTTONUP(ref m);
                    break;
                case Win32.WM_MBUTTONDBLCLK:
                    WM_MBUTTONDBLCLK(ref m);
                    break;
                case Win32.WM_MBUTTONDOWN:
                    WM_MBUTTONDOWN(ref m);
                    break;
                case Win32.WM_MBUTTONUP:
                    WM_MBUTTONUP(ref m);
                    break;
                case Win32.WM_RBUTTONDBLCLK:
                    WM_RBUTTONDBLCLK(ref m);
                    break;
                case Win32.WM_RBUTTONDOWN:
                    WM_RBUTTONDOWN(ref m);
                    break;
                case Win32.WM_RBUTTONUP:
                    WM_RBUTTONUP(ref m);
                    break;
                case Win32.WM_XBUTTONDOWN:
                    WM_XBUTTONDOWN(ref m);
                    break;
                case Win32.WM_XBUTTONUP:
                    WM_XBUTTONUP(ref m);
                    break;
                case Win32.WM_XBUTTONDBLCLK:
                    WM_XBUTTONDBLCLK(ref m);
                    break;
                case Win32.WM_MOUSEWHEEL:
                    WM_MOUSEWHEEL(ref m);
                    break;
                case Win32.WM_MOUSEMOVE:
                    WM_MOUSEMOVE(ref m);
                    break;
                case Win32.WM_MOUSEHOVER:
                    WM_MOUSEHOVER(ref m);
                    break;
                case Win32.WM_MOUSELEAVE:
                    WM_MOUSELEAVE(ref m);
                    break;
                case Win32.WM_EXITMENULOOP:
                    WM_EXITMENULOOP(ref m);
                    break;
                case Win32.WM_ENTERMENULOOP:
                    WM_ENTERMENULOOP(ref m);
                    break;
                case Win32.WM_MENUCHAR:
                    WM_MENUCHAR(ref m);
                    break;
                case Win32.WM_CAPTURECHANGED:
                    WM_CAPTURECHANGED(ref m);
                    break;
                case Win32.WM_IME_STARTCOMPOSITION:
                    WM_IME_STARTCOMPOSITION(ref m);
                    break;
                case Win32.WM_IME_ENDCOMPOSITION:
                    WM_IME_ENDCOMPOSITION(ref m);
                    break;
                case Win32.WM_DWMCOMPOSITIONCHANGED:
                    WM_DWMCOMPOSITIONCHANGED(ref m);
                    break;
                case Win32.WM_DWMNCRENDERINGCHANGED:
                    WM_DWMNCRENDERINGCHANGED(ref m);
                    break;
                case Win32.WM_DWMCOLORIZATIONCOLORCHANGED:
                    WM_DWMCOLORIZATIONCOLORCHANGED(ref m);
                    break;
                case Win32.WM_DWMWINDOWMAXIMIZEDCHANGE:
                    WM_DWMWINDOWMAXIMIZEDCHANGE(ref m);
                    break;
                case Win32.WM_DWMSENDICONICTHUMBNAIL:
                    WM_DWMSENDICONICTHUMBNAIL(ref m);
                    break;
                case Win32.WM_DWMSENDICONICLIVEPREVIEWBITMAP:
                    WM_DWMSENDICONICLIVEPREVIEWBITMAP(ref m);
                    break;
                case Win32.WM_THEMECHANGED:
                    WM_THEMECHANGED(ref m);
                    break;
                case Win32.WM_HOTKEY:
                    WM_HOTKEY(ref m);
                    break;
                case Win32.WM_SYSCHAR:
                    WM_SYSCHAR(ref m);
                    break;
                // 窗口显示
                case Win32.WM_SHOWWINDOW:
                    WM_SHOWWINDOW(ref m);
                    break;
                // 窗口创建                        
                case Win32.WM_CREATE:
                    WM_CREATE(ref m);
                    break;
                // 窗口位置将改变
                case Win32.WM_WINDOWPOSCHANGING:
                    WM_WINDOWPOSCHANGING(ref m);
                    break;
                case Win32.WM_WINDOWPOSCHANGED:
                    WM_WINDOWPOSCHANGED(ref m);
                    break;
                case Win32.WM_ENTERSIZEMOVE:
                    WM_ENTERSIZEMOVE(ref m);
                    break;
                case Win32.WM_EXITSIZEMOVE:
                    WM_EXITSIZEMOVE(ref m);
                    break;
                case Win32.WM_SIZING:
                    WM_SIZING(ref m);
                    break;
                case Win32.WM_SIZE:
                    WM_SIZE(ref m);
                    break;
                case Win32.WM_SYSCOMMAND:
                    WM_SYSCOMMAND(ref m);
                    break;
                case Win32.WM_DPICHANGED:
                case Win32.WM_GETDPISCALEDSIZE:
                    break;
                default:
                    if(m.Msg == Win32.Util.WM_MOUSEENTER)
                    {

                    }
                    else
                    {
                        DefaultWndProc(ref m);
                    }
                    break;
            }
        }

        protected virtual void WM_NCUAHDRAWCAPTION(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_GETMINMAXINFO(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCXBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCMBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCRBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCLBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCMOUSEMOVE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCHITTEST(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCCALCSIZE(ref Message m)
        {

            base.WndProc(ref m);
        }

        protected virtual void WM_NCPAINT(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_ERASEBKGND(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCUAHDRAWFRAME(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCLBUTTONUP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCDESTROY(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_CANCELMODE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_ACTIVATE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_COMPACTING(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_PRINTCLIENT(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_PAINT(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_LBUTTONDBLCLK(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_LBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_LBUTTONUP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MBUTTONDBLCLK(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MBUTTONUP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_RBUTTONDBLCLK(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_RBUTTONUP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_XBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_RBUTTONDOWN(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_XBUTTONUP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_XBUTTONDBLCLK(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MOUSEWHEEL(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MOUSEMOVE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MOUSEHOVER(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MOUSELEAVE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_EXITMENULOOP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_ENTERMENULOOP(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_MENUCHAR(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_CAPTURECHANGED(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_SIZE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_SIZING(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_EXITSIZEMOVE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_ENTERSIZEMOVE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_WINDOWPOSCHANGED(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_WINDOWPOSCHANGING(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_CREATE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_SHOWWINDOW(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_SYSCHAR(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_HOTKEY(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_THEMECHANGED(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 指示窗口提供静态位图以用作 实时预览 (也称为该窗口的 速览预览)
        /// </summary>
        protected virtual void WM_DWMSENDICONICLIVEPREVIEWBITMAP(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 当桌面窗口管理器 (DWM) 组合窗口最大化时发送
        /// </summary>
        /// <param name="m"></param>
        protected virtual void WM_DWMWINDOWMAXIMIZEDCHANGE(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 通知所有顶级窗口着色颜色已更改
        /// </summary>
        protected virtual void WM_DWMCOLORIZATIONCOLORCHANGED(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 当非工作区呈现策略发生更改时发送
        /// </summary>
        protected virtual void WM_DWMNCRENDERINGCHANGED(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 通知所有顶级窗口，桌面窗口管理器 (DWM) 组合已启用或禁用
        /// </summary>
        /// <param name="m"></param>
        protected virtual void WM_DWMCOMPOSITIONCHANGED(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 指示窗口提供静态位图以用作该窗口的缩略图表示形式
        /// </summary>
        protected virtual void WM_DWMSENDICONICTHUMBNAIL(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_IME_ENDCOMPOSITION(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_IME_STARTCOMPOSITION(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_SYSCOMMAND(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void WM_NCACTIVATE(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void DefaultWndProc(ref Message m)
        {
            base.WndProc(ref m);
        }


    }
}
