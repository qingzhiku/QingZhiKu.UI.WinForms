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
        /// wParam值
        /// 含义：更改窗口大小后发送到窗口,请求的大小调整的类型。 此参数的取值可为下列值之一
        /// 用法：lParam 的低序字指定工作区的新宽度；lParam 的高序字指定工作区的新高度
        /// </summary>
        public class WMSIZE_WParam
        {
            /// <summary>
            /// 当其他一些窗口最大化时，消息将发送到所有弹出窗口
            /// </summary>
            public const int SIZE_MAXHIDE = 4;
            
            /// <summary>
            /// 窗口已最大化
            /// </summary>
            public const int SIZE_MAXIMIZED = 2;

            /// <summary>
            /// 当其他一些窗口还原到其以前的大小时，消息将发送到所有弹出窗口
            /// </summary>
            public const int SIZE_MAXSHOW = 3;

            /// <summary>
            /// 窗口已最小化
            /// </summary>
            public const int SIZE_MINIMIZED = 1;

            /// <summary>
            /// 窗口已调整大小，但 SIZE_MINIMIZED 和 SIZE_MAXIMIZED 值均未应用
            /// </summary>
            public const int SIZE_RESTORED = 0;
        }

        /// <summary>
        /// wParam值
        /// 含义：当用户从 “窗口 ”菜单中选择命令时，窗口会收到此消息， (以前称为系统或控件菜单) ，或者当用户选择最大化按钮、最小化按钮、还原按钮或关闭按钮时
        /// 用法：低序单词指定光标的水平位置（以屏幕坐标为单位）；高序单词指定光标的垂直位置（以屏幕坐标为单位）
        /// </summary>
        public class WMSYSCOMMAND_WParam
        {
            /*
         * 参考：https://docs.microsoft.com/zh-cn/windows/win32/menurc/wm-syscommand?redirectedfrom=MSDN
         * 
         *   获取屏幕坐标中的位置坐标：
         *   // 低序单词指定光标的水平位置（以屏幕坐标为单位）（如果使用鼠标选择窗口菜单命令）。 否则，不使用此参数
         *   xPos = GET_X_LPARAM(lParam);    // horizontal position
         *   // 高序单词指定光标的垂直位置（以屏幕坐标为单位）（如果使用鼠标选择窗口菜单命令）。 如果使用系统加速器选择命令，则此参数为 1;如果使用助记键，则为 1
         *   yPos = GET_Y_LPARAM(lParam);    // vertical position
         * 
         */


            /// <summary>
            /// 关闭窗口
            /// </summary>
            public const int SC_CLOSE = 0xF060;

            /// <summary>
            /// 使用指针将光标更改为问号。 如果用户随后单击对话框中的控件，该控件将收到 WM_HELP 消息
            /// </summary>
            public const int SC_CONTEXTHELP = 0xF180;

            /// <summary>
            /// 选择默认项;用户双击窗口菜单
            /// </summary>
            public const int SC_DEFAULT = 0xF160;

            /// <summary>
            /// 激活与应用程序指定的热键关联的窗口。 lParam 参数标识要激活的窗口
            /// </summary>
            public const int SC_HOTKEY = 0xF150;

            /// <summary>
            /// 水平滚动
            /// </summary>
            public const int SC_HSCROLL = 0xF080;

            /// <summary>
            /// 指示屏幕保存程序是否安全
            /// </summary>
            public const int SCF_ISSECURE = 0x00000001;

            /// <summary>
            /// 检索窗口菜单作为击键的结果。 有关详细信息，请参见“备注”部分
            /// </summary>
            public const int SC_KEYMENU = 0xF100;

            /// <summary>
            /// 最大化窗口
            /// </summary>
            public const int SC_MAXIMIZE = 0xF030;

            /// <summary>
            /// 最小化窗口
            /// </summary>
            public const int SC_MINIMIZE = 0xF020;

            /// <summary>
            /// 设置显示的状态。 此命令支持具有节能功能的设备，例如电池供电的个人电脑。
            /// lParam 参数可以具有以下值：
            /// -1 (显示器开机)
            /// 1 (显示器将低功率)
            /// 2 (显示器正在关闭)
            /// </summary>
            public const int SC_MONITORPOWER = 0xF170;

            /// <summary>
            /// 在单击鼠标后检索窗口菜单
            /// </summary>
            public const int SC_MOUSEMENU = 0xF090;

            /// <summary>
            /// 移动窗口
            /// </summary>
            public const int SC_MOVE = 0xF010;

            public const int SC_MOUSEMOVE = SC_MOVE + 0x02;

            /// <summary>
            /// 移动到下一个窗口
            /// </summary>
            public const int SC_NEXTWINDOW = 0xF040;

            /// <summary>
            /// 移动到上一个窗口
            /// </summary>
            public const int SC_PREVWINDOW = 0xF050;

            /// <summary>
            /// 将窗口还原到其正常位置和大小
            /// </summary>
            public const int SC_RESTORE = 0xF120;

            /// <summary>
            /// 执行在System.ini文件的 [boot] 节中指定的屏幕保存程序应用程序
            /// </summary>
            public const int SC_SCREENSAVE = 0xF140;

            /// <summary>
            /// 调整窗口的大小
            /// </summary>
            public const int SC_SIZE = 0xF000;

            /// <summary>
            /// 激活"开始"菜单菜单
            /// </summary>
            public const int SC_TASKLIST = 0xF130;

            /// <summary>
            /// 垂直滚动
            /// </summary>
            public const int SC_VSCROLL = 0xF070;
        }

        /// <summary>
        /// wParam值
        /// 含义：发送到正在激活的窗口和正在停用的窗口。 如果窗口使用相同的输入队列，则会同步发送消息，首先发送到正在停用的顶级窗口的窗口过程，然后发送到正在激活的顶级窗口的窗口过程。 如果窗口使用不同的输入队列，则会异步发送消息，以便立即激活该窗口
        /// 用法：低序单词指定窗口是激活还是停用。 此参数的取值可为下列值之一： 高阶单词指定正在激活或停用的窗口的最小化状态。 非零值指示窗口已最小化
        /// </summary>
        public class WMACTIVATE_WParam
        {
            /// <summary>
            /// 例如，通过调用 SetActiveWindow 函数或使用键盘界面选择窗口) 等方法激活鼠标单击
            /// </summary>
            public const int WA_ACTIVE = 1;

            /// <summary>
            /// 通过鼠标单击激活
            /// </summary>
            public const int WA_CLICKACTIVE = 2;

            /// <summary>
            /// 关闭
            /// </summary>
            public const int WA_INACTIVE = 0;
        }

        /// <summary>
        /// wParam值，指向消息<see cref="Win32.WM_HOTKEY"/>
        /// 生成消息的热键的标识符。 如果消息是由系统定义的热键生成的，此参数将是以下值之一
        /// </summary>
        public class WMHOTKEY_WParam
        {
            /// <summary>
            /// 按下了“对齐桌面”("snap desktop")热键
            /// </summary>
            public const int IDHOT_SNAPDESKTOP = -2;
            /// <summary>
            /// 按下了“对齐窗口”("snap window")热键
            /// </summary>
            public const int IDHOT_SNAPWINDOW = -1;
        }
        
    }
}
