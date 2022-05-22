using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Win32
    {
        /// <summary>
        /// 在 x 方向) 的字节边界上对齐窗口的工作区 (。 此样式会影响窗口的宽度及其在显示器上的水平位置
        /// </summary>
        public const int CS_BYTEALIGNCLIENT = 0x1000;

        /// <summary>
        /// 在 x 方向) 的字节边界上对齐窗口 (。 此样式会影响窗口的宽度及其在显示器上的水平位置
        /// </summary>
        public const int CS_BYTEALIGNWINDOW = 0x2000;

        /// <summary>
        /// 分配一个设备上下文，供类中的所有窗口共享。 由于窗口类是特定于进程的，因此应用程序多个线程可以创建同一类的窗口。 线程还可以尝试同时使用设备上下文。 发生这种情况时，系统只允许一个线程成功完成其绘图操作
        /// </summary>
        public const int CS_CLASSDC = 0x0040;

        /// <summary>
        /// 当用户双击鼠标位于属于该类的窗口中时，将双击消息发送到窗口过程
        /// </summary>        
        public const int CS_DBLCLKS = 0x0008;

        /// <summary>
        /// 对窗口启用阴影效果。 通过 SPI_SETDROPSHADOW打开和关闭效果。 通常，这为小型生存期窗口（例如菜单）启用，以强调它们与其他窗口的 Z 顺序关系。 从具有此样式的类创建的Windows必须是顶级窗口;它们可能不是子窗口
        /// </summary>        
        public const int CS_DROPSHADOW = 0x00020000;

        /// <summary>
        /// 指示窗口类是应用程序全局类。 有关详细信息，请参阅 “关于窗口类”的“应用程序全局类”部分
        /// </summary>
        public const int CS_GLOBALCLASS = 0x4000;

        /// <summary>
        /// 如果移动或大小调整更改了工作区的宽度，则重新绘制整个窗口
        /// </summary>
        public const int CS_HREDRAW = 0x0002;

        /// <summary>
        /// 禁用窗口菜单上的 “关闭 ”
        /// </summary>
        public const int CS_NOCLOSE = 0x0200;

        /// <summary>
        /// 为类中的每个窗口分配唯一的设备上下文
        /// </summary>
        public const int CS_OWNDC = 0x0020;

        /// <summary>
        /// 将子窗口的剪裁矩形设置为父窗口的剪裁矩形，以便子窗口可以绘制父窗口。 具有 CS_PARENTDC 样式位的窗口从系统的设备上下文缓存接收常规设备上下文。 它不会为子级提供父级的设备上下文或设备上下文设置。 指定 CS_PARENTDC 可增强应用程序的性能
        /// </summary>
        public const int CS_PARENTDC = 0x0080;

        /// <summary>
        /// 以位图形式保存此类窗口遮盖的屏幕图像部分。 删除窗口后，系统使用保存的位图还原屏幕图像，包括遮盖的其他窗口。 因此，如果位图使用的内存尚未丢弃，并且其他屏幕操作未使存储的图像失效，系统不会将 WM_PAINT 消息发送到隐藏的窗口。此样式适用于小型窗口(，例如，菜单或对话框) 短暂显示，然后在其他屏幕活动发生前删除。 此样式增加了显示窗口所需的时间，因为系统必须首先分配内存来存储位图
        /// </summary>
        public const int CS_SAVEBITS = 0x0800;

        /// <summary>
        /// 如果移动或大小调整更改工作区的高度，则重新绘制整个窗口
        /// </summary>
        public const int CS_VREDRAW = 0x0001;
    }
}
