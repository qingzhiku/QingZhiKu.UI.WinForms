using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Win32
    {
        /// <summary>
        /// lParam返回值，
        /// 计算窗体客户区域大小和位置的消息的返回值 NCHITTEST_Return
        /// </summary>
        public class NCHITTEST_Result
        {
            ///<summary>
            ///在屏幕背景或窗口之间的分界线上（与 HTNOWHERE 相同，只是 DefWindowProc 函数发出系统蜂鸣音以指示错误）
            ///</summary>、
            public const int HTERROR = -2;

            ///<summary>
            ///在当前由同一线程中的另一个窗口覆盖的窗口中（消息将发送到同一线程中的基础窗口，直到其中一个窗口返回非 HTTRANSPARENT 代码）
            ///</summary>
            public const int HTTRANSPARENT = -1;

            ///<summary>
            ///在屏幕背景或窗口之间的分界线上
            ///</summary>
            public const int HTNOWHERE = 0;

            ///<summary>在客户区</summary>
            public const int HTCLIENT = 1;

            ///<summary>在标题栏中</summary>
            public const int HTCAPTION = 2;

            ///<summary>在窗口菜单或子窗口中的“关闭”按钮中，例如左上角菜单</summary>
            public const int HTSYSMENU = 3;

            ///<summary>在一个尺寸框中（与 HTSIZE 相同）</summary>
            public const int HTGROWBOX = 4;

            ///<summary>在菜单中</summary>
            public const int HTMENU = 5;

            ///<summary>在水平滚动条中</summary>
            public const int HTHSCROLL = 6;

            ///<summary>在垂直滚动条中</summary>
            public const int HTVSCROLL = 7;

            ///<summary>在“最小化”按钮中</summary>
            public const int HTMINBUTTON = 8;

            ///<summary>在“最大化”按钮中</summary>
            public const int HTMAXBUTTON = 9;

            ///<summary>
            /// 在可调整大小的窗口的左边框中（用户可以单击鼠标以水平调整窗口大小）
            /// </summary>
            public const int HTLEFT = 10;

            ///<summary>
            /// 在可调整大小的窗口的右边框中（用户可以单击鼠标以水平调整窗口大小）
            ///</summary>
            public const int HTRIGHT = 11;

            ///<summary>在窗口的水平上边框中</summary>
            public const int HTTOP = 12;

            ///<summary>在窗口边框的左上角</summary>
            public const int HTTOPLEFT = 13;

            ///<summary>在窗口边框的右上角</summary>
            public const int HTTOPRIGHT = 14;

            ///<summary>
            /// 在可调整大小的窗口的水平下边框中（用户可以单击鼠标垂直调整窗口大小）
            /// </summary>
            public const int HTBOTTOM = 15;

            ///<summary>
            /// 在可调整大小的窗口边框的左下角（用户可以单击鼠标以对角线调整窗口大小
            /// </summary>
            public const int HTBOTTOMLEFT = 16;

            ///<summary>
            /// 在可调整大小的窗口边框的右下角（用户可以单击鼠标以对角线调整窗口大小）
            ///</summary>
            public const int HTBOTTOMRIGHT = 17;

            ///<summary>在没有大小边框的窗口边框中</summary>
            public const int HTBORDER = 18;

            public const int HTOBJECT = 19;

            ///<summary>在“关闭”按钮中</summary>
            public const int HTCLOSE = 20;

            ///<summary>在“帮助”按钮中</summary>
            public const int HTHELP = 21;

            public const int TCLIENT = 1;
            public const int TCLOSE = 20;
            public const int HTREDUCE = 8;
            public const int HTSIZE = 4;
            public const int HTZOOM = 9;
        }

        /// <summary>
        /// 定义了一些常用的鼠标消息
        /// </summary>
        public class WM_NCCALCSIZE_Result
        {
            /// <summary>
            /// 指定要保留窗口的工作区，并与窗口的新位置的顶部对齐。 例如，若要将工作区与左上角对齐，请返回WVR_ALIGNTOP和 WVR_ALIGNLEFT 值
            /// </summary>
            public const int WVR_ALIGNTOP = 0x0010;
            /// <summary>
            /// 指定要保留窗口的工作区，并与窗口的新位置的左边对齐。 例如，若要将工作区与左上角对齐，请返回WVR_ALIGNTOP和 WVR_ALIGNLEFT 值
            /// </summary>
            public const int WVR_ALIGNLEFT = 0x0020;
            /// <summary>
            /// 指定要保留窗口的工作区，并与窗口的新位置的底部对齐。 例如，若要将工作区与左上角对齐，请返回WVR_ALIGNTOP和 WVR_ALIGNLEFT 值
            /// </summary>
            public const int WVR_ALIGNBOTTOM = 0x0040;
            /// <summary>
            /// 指定要保留窗口的工作区，并与窗口新位置的右侧对齐。 例如，若要将工作区与右下角对齐，请返回 WVR_ALIGNRIGHT 和WVR_ALIGNBOTTOM值
            /// </summary>
            public const int WVR_ALIGNRIGHT = 0x0080;
            /// <summary>
            /// 与其他任何值（ WVR_VALIDRECTS除外）结合使用时，如果客户端矩形水平更改大小，则窗口将完全重新绘制。 此值类似于 CS_HREDRAW 类样式
            /// </summary>
            public const int WVR_HREDRAW = 0x0100;
            /// <summary>
            /// 与其他任何值结合使用（ WVR_VALIDRECTS除外）会导致在客户端矩形垂直更改大小时完全重新绘制窗口。 此值类似于 CS_VREDRAW 类样式
            /// </summary>
            public const int WVR_VREDRAW = 0x0200;
            /// <summary>
            /// 此值会导致重新绘制整个窗口。 它是 WVR_HREDRAW 和 WVR_VREDRAW 值的组合
            /// </summary>
            public const int WVR_REDRAW = 0x0300;
            /// <summary>
            /// 此值指示，从WM_NCCALCSIZE返回时，NCCALCSIZE_PARAMS结构NCCALCSIZE_PARAMS结构中的 rgrc[1] 和 rgrc[2] 成员指定的矩形分别包含有效的目标和源区域矩形。 系统将这些矩形组合在一起，以计算要保留的窗口区域。 系统将源矩形内窗口图像的任何部分复制到目标矩形。 这两个矩形都位于父相对坐标或屏幕相对坐标中。 此标志不能与任何其他标志组合在一起。
            /// 此返回值允许应用程序实现更详细的客户端应用程序保留策略，例如居中或保留一部分工作区
            /// </summary>
            public const int WVR_VALIDRECTS = 0x400;
        }
        



    }
}
