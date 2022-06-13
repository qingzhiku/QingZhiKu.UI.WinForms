using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 系统菜单帮助类
    /// </summary>
    public class SystemMenuHelper
    {

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        public static SystemMenu FromHandle(IntPtr hWnd)
        {
            SystemMenu sysMenu = new SystemMenu(hWnd);

            if (sysMenu.MenuHandle == IntPtr.Zero)
            {
                throw new Exception("获取系统菜单句柄失败");
            }

            return sysMenu;
        }


    }


    /// <summary>
    /// 系统菜单
    /// </summary>
    public sealed class SystemMenu : IDisposable
    {
        #region 菜单常用值

        /// <summary>
        /// 不放置选取标记在菜单项旁边（缺省）。如果应用程序提供一个选取标记位图（参见SetMenultemBitmaps），则将选取标记位图放置在菜单项旁边
        /// </summary>
        public const Int32 MF_UNCHECKED = 0x0;

        /// <summary>
        ///  指定菜单项是一个正文字符串；参数lpNewltem指向该字符串
        /// </summary>
        public const Int32 MF_STRING = 0x0;

        /// <summary>
        /// 指定菜单项是一个禁用的菜单项；不能选取或点击该菜单项 使菜单项无效，但不使菜单项变灰
        /// </summary>
        public const Int32 MF_DISABLED = 0x00000002;

        /// <summary>
        /// 使菜单项变灰，使该项不能被选择
        /// </summary>
        public const Int32 MF_GRAYED = 0x00000001;

        /// <summary>
        /// 在菜单项旁边放置一个选取标记。如果应用程序提供一个选取标记，位图（参见SetMenultemBitmaps），则将选取标记位图放置在菜单项旁边
        /// </summary>
        public const Int32 MF_CHECKED = 0x00000008;

        /// <summary>
        /// 指定菜单项是一个弹出式菜单。参数uIDNewltem指定弹出式菜单的句柄
        /// </summary>
        public const Int32 MF_POPUP = 0x00000010;

        /// <summary>
        /// 对菜单条的功能同MF_MENUBREAK标志。对下拉式菜单、子菜单或快捷菜单，新列和旧列被垂直线分开
        /// </summary>
        public const Int32 MF_MENUBARBREAK = 0x00000020;

        /// <summary>
        /// 将菜单项放置于新行（对菜单条），或新列（对下拉式菜单、子菜单或快捷菜单）且无分割列
        /// </summary>
        public const Int32 MF_MENUBREAK = 0x00000040;

        /// <summary>
        /// 基于下标索引
        /// </summary>
        public const Int32 MF_BYPOSITION = 0x400;

        /// <summary>
        /// 基于id
        /// </summary>
        public const Int32 MF_BYCOMMAND = 0x00000000;

        /// <summary>
        /// 创建一个水平分隔线（只用于下拉式菜单、子菜单或快捷菜单
        /// </summary>
        public const Int32 MF_SEPARATOR = 0x800;

        public const Int32 MF_REMOVE = 0x00001000;
        public const Int32 MF_DELETE = 0x00000200;

        public const Int32 MF_APPEND = 0x00000100;
        public const Int32 MF_CHANGE = 0x00000080;

        /// <summary>
        /// 将一个位图用作菜单项。参数lpNewltem里含有该位图的句柄
        /// </summary>
        public const Int32 MF_BITMAP = 0x00000004;

        public const Int32 IDM_EDITFUNDS = 1000;
        public const Int32 IDM_ANALYZE = 1001;

        public const int WM_INITMENUPOPUP = 0x0117;


        #endregion

        /// <summary>
        /// 系统菜单句柄
        /// </summary>
        private IntPtr _sysMenuHandle = IntPtr.Zero;
        private IntPtr _controlHandle = IntPtr.Zero;

        /// <summary>
        /// 菜单句柄
        /// </summary>
        public IntPtr MenuHandle => _sysMenuHandle;

        /// <summary>
        /// 控件句柄
        /// </summary>
        public IntPtr ControlHandle => _controlHandle;

        /// <summary>
        /// 菜单子项数量
        /// </summary>
        public int Count => _sysMenuHandle == IntPtr.Zero ? 0 : Win32.GetMenuItemCount(_sysMenuHandle);

        internal SystemMenu(IntPtr hWnd)
        {
            _sysMenuHandle = Win32.GetSystemMenu(hWnd, false);
            _controlHandle = hWnd;
        }

        /// <summary>
        /// 插入菜单项
        /// </summary>
        /// <param name="index">位置序号</param>
        /// <param name="text">菜单项描述</param>
        /// <param name="flags">标记</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public bool InsertMenu(int index, int flags, int command, string text)
        {
            return Win32.InsertMenu(_sysMenuHandle, index, flags, command & 0xFFF0, text);
        }

        /// <summary>
        /// 给定的位置（以0为索引开始值）插入一个分隔条
        /// </summary>
        public bool InsertSeparator(int index)
        {
            return Win32.InsertMenu(_sysMenuHandle, index, MF_SEPARATOR | MF_BYPOSITION, 0, "");
        }

        /// <summary>
        /// 简化的InsertMenu()，前提――Pos参数是一个0开头的相对索引位置
        /// </summary>
        public bool InsertMenu(int index, int ID, String text)
        {
            return Win32.InsertMenu(_sysMenuHandle, index, MF_BYPOSITION | MF_STRING, ID & 0xFFF0, text);
        }

        /// <summary>
        /// 添加一个分隔条
        /// </summary>
        public bool AppendSeparator()
        {
            return Win32.AppendMenu(_sysMenuHandle, MF_SEPARATOR, 0, "");
        }


        /// <summary>
        ///  追加菜单
        /// </summary>
        public bool AppendMenu(int Flags, int ID, String text)
        {
            return Win32.AppendMenu(_sysMenuHandle, Flags, ID & 0xFFF0, text);
        }

        /// <summary>
        /// 使用String作为缺省值填充菜单
        /// </summary>
        public bool AppendMenu(int ID, String text)
        {
            return Win32.AppendMenu(_sysMenuHandle, MF_STRING, ID & 0xFFF0, text);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        public bool RemoveMenu(int index, int flag)
        {
            return Win32.RemoveMenu(_sysMenuHandle, index, flag);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        public bool RemoveMenu(int index)
        {
            return Win32.RemoveMenu(_sysMenuHandle, index, MF_REMOVE);
        }

        /// <summary>
        /// 设置菜单左侧图标
        /// </summary>
        public bool SetMenuItemBitmaps(int index, Bitmap uncheckedBitmap, Bitmap checkedBitmap)
        {
            return Win32.SetMenuItemBitmaps(_sysMenuHandle, index, MF_BYPOSITION, uncheckedBitmap, checkedBitmap);
        }

        /// <summary>
        /// 设置菜单左侧图标
        /// </summary>
        public bool SetMenuItemBitmaps(int index, IntPtr uncheckedBitmap, IntPtr checkedBitmap)
        {
            return Win32.SetMenuItemBitmaps(_sysMenuHandle, index, MF_BYPOSITION, Bitmap.FromHbitmap(uncheckedBitmap), Bitmap.FromHbitmap(checkedBitmap));
        }

        /// <summary>
        /// 显示系统菜单
        /// </summary>
        public void ShowSystemMenu(Point screenLocation)
        {
            const uint TPM_RETURNCMD = 0x0100;
            const uint TPM_LEFTBUTTON = 0x0;

            if (_sysMenuHandle == IntPtr.Zero)
            {
                return;
            }

            uint cmd = Win32.TrackPopupMenuEx(_sysMenuHandle, TPM_LEFTBUTTON | TPM_RETURNCMD, screenLocation.X, screenLocation.Y, _controlHandle, IntPtr.Zero);
            if (0 != cmd)
            {
                Win32.PostMessage(_controlHandle, Win32.WM_SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
            }
        }

        public void Dispose()
        {
            _sysMenuHandle = IntPtr.Zero;
            _controlHandle = IntPtr.Zero;
        }
        
    }





}
