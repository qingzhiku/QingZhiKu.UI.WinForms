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
        public class NCHITTEST_Return
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







    }
}
