/******************************************************************************
 * QingZhiKu 开源控件库
******************************************************************************
 * 文件名称: Win32.Constants.WindowStyles.cs
 * 文件说明: 窗口常量的窗口样式
 * 起始日期：2022-5-5
 * 
 * 参考1：https://docs.microsoft.com/zh-cn/windows/win32/winmsg/window-styles
 * 参考2：https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
 *
 ******************************************************************************/

namespace System
{
	public partial class Win32
	{
		/// <summary>
		/// 窗口是重叠窗口
		/// </summary>
		public const int WS_OVERLAPPED = 0x00000000;

		/// <summary>
		/// 窗口是一个弹出窗口
		/// </summary>
		public const long WS_POPUP = 0x80000000;

		/// <summary>
		/// 窗口是一个子窗口
		/// </summary>
		public const int WS_CHILD = 0x40000000;

		/// <summary>
		/// 窗口最初最小化
		/// </summary>
		public const int WS_MINIMIZE = 0x20000000;

		/// <summary>
		/// 窗口最初可见
		/// </summary>
		public const int WS_VISIBLE = 0x10000000;

		/// <summary>
		/// 窗口最初处于禁用状态
		/// </summary>
		public const int WS_DISABLED = 0x08000000;

		/// <summary>
		/// 相对于彼此的位置剪辑子窗口,作为子窗口当重绘时不重绘被其它子窗口遮挡的区域
		/// </summary>
		public const int WS_CLIPSIBLINGS = 0x04000000;

		/// <summary>
		///  当在父窗口中进行绘制时，排除子窗口占用的区域,作为父窗口当重绘时不重绘制被子窗口遮挡的区域
		/// </summary>
		public const int WS_CLIPCHILDREN = 0x02000000;

		/// <summary>
		/// 窗口最初处于最大化状态
		/// </summary>
		public const int WS_MAXIMIZE = 0x01000000;

		/// <summary>
		/// 窗口的标题栏
		/// </summary>
		public const int WS_CAPTION = 0x00C00000;

		/// <summary>
		/// 窗口具有细线边框
		/// </summary>
		public const int WS_BORDER = 0x00800000;

		/// <summary>
		/// 不能改变大小的边框，窗口的边框通常与对话框一起使用
		/// </summary>
		public const int WS_DLGFRAME = 0x00400000;

		/// <summary>
		/// 窗口具有垂直滚动条
		/// </summary>
		public const int WS_VSCROLL = 0x00200000;

		/// <summary>
		/// 窗口具有水平滚动条
		/// </summary>
		public const int WS_HSCROLL = 0x00100000;

		/// <summary>
		/// 窗口的标题栏上有一个窗口菜单
		/// </summary>
		public const int WS_SYSMENU = 0x00080000;

		/// <summary>
		/// 窗口的大小调整边框，可改变大小的边框
		/// </summary>
		public const int WS_THICKFRAME = 0x00040000;

		/// <summary>
		/// 该窗口包含 "最小化" 按钮
		/// </summary>
		public const int WS_GROUP = 0x00020000;

		/// <summary>
		/// 窗口是一个控件，当用户按 TAB 键时，该控件可以接收键盘焦点
		/// </summary>
		public const int WS_TABSTOP = 0x00010000;

		/// <summary>
		/// 该窗口包含 "最小化" 按钮
		/// </summary>
		public const int WS_MINIMIZEBOX = 0x00020000;

		/// <summary>
		/// 窗口有最大化按钮
		/// </summary>
		public const int WS_MAXIMIZEBOX = 0x00010000;

		/// <summary>
		/// 窗口是重叠窗口
		/// </summary>
		public const int WS_TILED = WS_OVERLAPPED;

		/// <summary>
		/// 窗口最初最小化
		/// </summary>
		public const int WS_ICONIC = WS_MINIMIZE;

		/// <summary>
		/// 窗口的大小调整边框
		/// </summary>
		public const int WS_SIZEBOX = WS_THICKFRAME;

		/// <summary>
		/// 窗口是重叠窗口
		/// </summary>
		public const int WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

		/// <summary>
		/// 窗口是重叠窗口
		/// </summary>
		public const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
			WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);

		/// <summary>
		/// 窗口是一个弹出窗口
		/// </summary>
		public const long WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU);

		/// <summary>
		/// 窗口是一个子窗口
		/// </summary>
		public const int WS_CHILDWINDOW = (WS_CHILD);


	}
}


