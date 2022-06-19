using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    public partial class DropShadowForm
    {
        class DropShadowNativeWindow : NativeWindow
        {
            private readonly DropShadowForm _dropShadowForm;
            private readonly Form _masterForm;
            
            public DropShadowNativeWindow(Form masterform, DropShadowForm dropform)
            {
                if(dropform == null || masterform == null)
                    throw new ArgumentNullException("背影窗体和宿主窗体不能为空");

                _masterForm = masterform;
                _dropShadowForm = dropform;
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == Win32.WM_NCACTIVATE)
                {
                    //_dropShadowForm.OnMasterNCActivate(_masterForm, new NCActivateEventArgs(m.WParam != IntPtr.Zero));
                    //if (m.WParam == IntPtr.Zero /*&& this.GetWindowState() != FormWindowState.Maximized*/) // 窗口失去焦点
                    //{
                    //    _dropShadowForm.OnMasterNCActivate(_masterForm, new NCActivateEventArgs(false));
                    //}
                    //else // 窗口获得焦点
                    //{
                    //    _dropShadowForm.OnMasterNCActivate(_masterForm, new NCActivateEventArgs(true));
                    //}
                }
                
                base.WndProc(ref m);

                if (!_dropShadowForm.DesignMode)
                {
                    switch (m.Msg)
                    {
                        case Win32.WM_HOTKEY: // 热键
                            break;
                        case Win32.WM_NCHITTEST: // 窗口边框拖动
                            break;
                        case Win32.WM_SYSCHAR: // Alt+F4
                            break;
                        case Win32.WM_SHOWWINDOW: // 窗口显示
                            //bool visible = m.WParam != IntPtr.Zero;
                            _dropShadowForm.OnMasterVisibleChanged(_masterForm, EventArgs.Empty);
                            break;
                        case Win32.WM_CREATE: // 窗口创建
                            //OnWM_CREATE(ref m);
                            break;
                        case Win32.WM_WINDOWPOSCHANGING: // 窗口位置将改变
                            //FormWindowState currentWindowState = Win32.Util.GetWindowState(this);
                            break;
                        case Win32.WM_WINDOWPOSCHANGED: // 窗口位置已改变
                            _dropShadowForm.OnMasterLocationChanged(_masterForm, EventArgs.Empty);
                            break;
                        case Win32.WM_ENTERSIZEMOVE: // 开始移动窗体
                            //OnWM_ENTERSIZEMOVE(ref m);
                            //_dropShadowForm.OnMasterLocationChanged(_masterForm, EventArgs.Empty);
                            _dropShadowForm.OnMasterResizeBegin(_masterForm, EventArgs.Empty);
                            break;
                        case Win32.WM_EXITSIZEMOVE: // 结束移动窗体
                            //_dropShadowForm.OnMasterLocationChanged(_masterForm, EventArgs.Empty);
                            //OnWM_EXITSIZEMOVE(ref m);
                            _dropShadowForm.OnMasterResizeEnd(_masterForm, EventArgs.Empty);
                            break;
                        case Win32.WM_MOUSEMOVE:
                            _dropShadowForm.OnMasterMouseMove(_masterForm, new MouseEventArgs(Control.MouseButtons, 0, Win32.Util.LoWord(m.LParam), Win32.Util.HiWord(m.LParam), 0));
                            break;
                        case Win32.WM_MOVE:
                            _dropShadowForm.OnMasterLocationChanged(_masterForm, EventArgs.Empty);
                            //OnWM_EXITSIZEMOVE(ref m);
                            break;
                        case Win32.WM_SIZE:
                            switch ((int)m.WParam)
                            {
                                // 窗口最大化
                                case Win32.WMSIZE_WParam.SIZE_MAXIMIZED:
                                    break;
                                // 窗口最小化
                                case Win32.WMSIZE_WParam.SIZE_MINIMIZED:
                                    break;
                                // 窗口恢复
                                case Win32.WMSIZE_WParam.SIZE_RESTORED:
                                    break;
                            }

                            int nWidth = Win32.Util.LOWORD(m.LParam);
                            int nHeight = Win32.Util.HIWORD(m.LParam);

                            _dropShadowForm.OnMasterSizeChanged(_masterForm, new SizeEventArgs(nWidth, nHeight));
                            break;
                        case Win32.WM_SYSCOMMAND:
                            //OnWM_SYSCOMMAND(ref m);
                            break;
                        case Win32.WM_GETMINMAXINFO:
                            WM_GETMINMAXINFO(ref m);
                            break;
                        case Win32.WM_PAINT:
                            //OnWM_SYSCOMMAND(ref m);
                            _dropShadowForm.OnMasterPaint(_masterForm, new PaintEventArgs(Graphics.FromHwnd(_masterForm.Handle), _masterForm.Bounds));
                            break;
                        case Win32.WM_NCACTIVATE:
                            if (_dropShadowForm != null && _dropShadowForm.IsHandleCreated)
                                _dropShadowForm.OnMasterNCActivate(_masterForm, new NCActivateEventArgs(m.WParam != IntPtr.Zero));
                            break;
                        case Win32.WM_CLOSE:
                            _dropShadowForm.OnMasterClose();
                            break;
                        case Win32.WM_DESTROY:
                            _dropShadowForm.OnMasterDestroyHandle();
                            break;
                            
                    }
                }
            }

            protected virtual void WM_GETMINMAXINFO(ref Message m)
            {
                if (_masterForm.IsRestrictedWindow || !_dropShadowForm.StandardMaximizedBounds) return;

                Win32.MINMAXINFO minmax = (Win32.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(Win32.MINMAXINFO));

                if (_masterForm.MaximumSize != Size.Empty)
                {
                    minmax.ptMaxTrackSize = _masterForm.MaximumSize;
                }
                else
                {
                    if (_masterForm.Parent == null)
                    {
                        minmax.ptMaxPosition.X += 0;
                        minmax.ptMaxPosition.Y += 0;
                        minmax.ptMaxTrackSize.Width = SystemInformation.WorkingArea.Width;
                        minmax.ptMaxTrackSize.Height = SystemInformation.WorkingArea.Height + (_masterForm.Size - _masterForm.ClientSize).Height;

                    }

                    if (_masterForm.Parent != null)
                    {
                        Rectangle rect = _masterForm.Parent.ClientRectangle;
                        minmax.ptMaxTrackSize = new Size(rect.Width, rect.Height);
                        minmax.ptMaxPosition = new Point(rect.X, rect.Y);
                        minmax.ptMaxSize = new Size(rect.Width, rect.Height);
                    }
                }

                if (_masterForm.MinimumSize != Size.Empty)
                {
                    minmax.ptMinTrackSize = _masterForm.MinimumSize;
                }
                else
                {
                    minmax.ptMinTrackSize = SystemInformation.MinWindowTrackSize;
                }

                Marshal.StructureToPtr(minmax, m.LParam, false);
                m.Result = IntPtr.Zero;
            }

        }
    }
}
