/******************************************************************************
 * QingZhiKu 开源控件库
******************************************************************************
 * 文件名称: Win32.Constants.WindowStyles.cs
 * 文件说明: 窗口常量的扩展窗口样式
 * 起始日期：2022-5-5
 * 
 * 参考1：https://docs.microsoft.com/zh-cn/windows/win32/winmsg/extended-window-styles
 * 参考2：https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
 *
 ******************************************************************************/

namespace System
{
    public partial class Win32
    {
        /// <summary>
        /// Window扩展样式
        /// </summary>
        public const int GWL_EXSTYLE = -20;

        /// <summary>
        /// 窗口具有双边框;窗口可以通过指定 dwStyle 参数中的WSCAPTION_ 样式，使用标题栏创建窗口
        /// </summary>
        public const int WS_EX_DLGMODALFRAME = 0x00000001;

        /// <summary>
        /// 使用此样式创建的子窗口不会在创建或销毁时将 WMPARENTNOTIFY_ 消息发送到其父窗口
        /// </summary>
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;

        /// <summary>
        /// 窗口应放置在所有非最顶层窗口之上，并且应保持其上方，即使窗口已停用也是如此
        /// </summary>
        public const int WS_EX_TOPMOST = 0x00000008;

        /// <summary>
        /// 窗口接受拖放文件
        /// </summary>
        public const int WS_EX_ACCEPTFILES = 0x00000010;

        /// <summary>
        /// 在绘制同一线程) 创建的窗口下方的同级 (之前，不应绘制该窗口。 窗口显示为透明，因为已绘制基础同级窗口的位
        /// </summary>
        public const int WS_EX_TRANSPARENT = 0x00000020;

        /// <summary>
        /// 窗口是 MDI 子窗口
        /// </summary>
        public const int WS_EX_MDICHILD = 0x00000040;

        /// <summary>
        /// 该窗口旨在用作浮动工具栏
        /// </summary>
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        /// <summary>
        /// 该窗口具有带凸起边缘的边框
        /// </summary>
        public const int WS_EX_WINDOWEDGE = 0x00000100;

        /// <summary>
        /// 窗口有一个边框，带有沉没边缘
        /// </summary>
        public const int WS_EX_CLIENTEDGE = 0x00000200;

        /// <summary>
        /// 窗口的标题栏包含问号
        /// </summary>
        public const int WS_EX_CONTEXTHELP = 0x00000400;

        /// <summary>
        /// 该窗口具有泛型"右对齐"属性
        /// </summary>
        public const int WS_EX_RIGHT = 0x00001000;

        /// <summary>
        /// 该窗口具有泛型左对齐属性。 这是默认值
        /// </summary>
        public const int WS_EX_LEFT = 0x00000000;

        /// <summary>
        /// 如果 shell 语言是希伯来语、阿拉伯语或支持阅读顺序对齐的另一种语言，则窗口的水平原点位于右边缘。 将水平值增大到左侧
        /// </summary>
        public const int WS_EX_RTLREADING = 0x00002000;

        /// <summary>
        /// 窗口文本使用从左到右的阅读顺序属性显示。 这是默认值
        /// </summary>
        public const int WS_EX_LTRREADING = 0x00000000;

        /// <summary>
        /// 如果 shell 语言是希伯来语、阿拉伯语或支持阅读顺序对齐的另一种语言，则垂直滚动条 (如果存在) 位于工作区左侧。 对于其他语言，将忽略该样式
        /// </summary>
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;

        /// <summary>
        /// 如果存在) 位于工作区右侧，则垂直滚动条 (。 这是默认值
        /// </summary>
        public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;

        /// <summary>
        /// 窗口本身包含应参与对话框导航的子窗口
        /// </summary>
        public const int WS_EX_CONTROLPARENT = 0x00010000;

        /// <summary>
        /// 该窗口具有一个三维边框样式，用于不接受用户输入的项目
        /// </summary>
        public const int WS_EX_STATICEDGE = 0x00020000;
        /// <summary>
        /// 当窗口可见时，将顶级窗口强制到任务栏上
        /// </summary>
        public const int WS_EX_APPWINDOW = 0x00040000;

        /// <summary>
        /// 窗口是重叠的窗口
        /// </summary>
        public const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);

        /// <summary>
        /// 窗口是调色板窗口，它是一个无模式对话框，用于显示命令数组
        /// </summary>
        public const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);

        /// <summary>
        /// 窗口是分层 窗口
        /// </summary>
        public const int WS_EX_LAYERED = 0x00080000;

        /// <summary>
        /// 该窗口不将其窗口布局传递给其子窗口
        /// </summary>
        public const int WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children

        /// <summary>
        /// 如果 shell 语言是希伯来语、阿拉伯语或支持阅读顺序对齐的另一种语言，则窗口的水平原点位于右边缘
        /// </summary>
        public const int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring

        /// <summary>
        /// 使用双缓冲按从下到上绘制顺序绘制窗口的所有后代
        /// </summary>
        public const int WS_EX_COMPOSITED = 0x02000000;

        /// <summary>
        /// 当用户单击该样式时，使用此样式创建的顶级窗口不会成为前台窗口
        /// </summary>
        public const int WS_EX_NOACTIVATE = 0x08000000;

    }
}
