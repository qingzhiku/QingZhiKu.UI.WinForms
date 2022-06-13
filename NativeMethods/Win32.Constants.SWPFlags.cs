namespace System
{
    public partial class Win32
    {
        /// <summary>
        /// 不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分
        /// </summary>
        public const int SWP_NOREDRAW = 0x0008;
        /// <summary>
        /// 给窗口发送 WM_NCCALCSIZE消息，即使窗口尺寸没有改变也会发送 该消息。如果未指定这个标志，只有在改变了窗口尺寸时才 发送WM_NCCALCSIZE。
        /// </summary>
        public const int SWP_FRAMECHANGED = 0x0020;  // The frame changed: send WM_NCCALCSIZE
        /// <summary>
        /// 清除客户区的所有内容。如果未设置该标志，客户区的有效内容被保存并且在窗口尺寸更新和重定位后拷贝回客户区。
        /// </summary>
        public const int SWP_NOCOPYBITS = 0x0100;
        /// <summary>
        /// 不改变z序中的所有者窗口的位置
        /// </summary>
        public const int SWP_NOOWNERZORDER = 0x0200;  // Don't do owner Z ordering
        /// <summary>
        /// 防止窗口接收WM_WINDOWPOSCHANGING消息
        /// </summary>
        public const int SWP_NOSENDCHANGING = 0x0400; // Don't send WM_WINDOWPOSCHANGING
        public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        /// <summary>
        /// WP_DEFERERASE：防止产生WM_SYNCPAINT消息。
        /// </summary>
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        /// <summary>
        /// 维持当前尺寸（忽略cx和Cy参数）
        /// </summary>
        public const int SWP_NOSIZE = 0x0001;
        /// <summary>
        /// 维持当前位置（忽略X和Y参数）
        /// </summary>
        public const int SWP_NOMOVE = 0x0002;
        /// <summary>
        /// 维持当前Z序（忽略hWndlnsertAfter参数）
        /// </summary>
        public const int SWP_NOZORDER = 0x0004;
        /// <summary>
        /// 不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数hWndlnsertAfter设置）
        /// </summary>
        public const int SWP_NOACTIVATE = 0x0010;
        /// <summary>
        /// 显示窗口
        /// </summary>
        public const int SWP_SHOWWINDOW = 0x0040;
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public const int SWP_HIDEWINDOW = 0x0080;
        /// <summary>
        /// 在窗口周围画一个边框（定义在窗口类描述中）
        /// </summary>
        public const int SWP_DRAWFRAME = 0x0020;
    }
}