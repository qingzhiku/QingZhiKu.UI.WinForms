/******************************************************************************
 * QingZhiKu 开源控件库
******************************************************************************
 * 文件名称: Win32.WindowMessages.cs
 * 文件说明: 窗口/键盘消息与通知
 * 起始日期：2022-5-5
 * 
 * 参考1：https://docs.microsoft.com/zh-cn/windows/win32/winmsg/window-notifications
 * 参考2：https://docs.microsoft.com/zh-cn/windows/win32/winmsg/window-messages
 *
 ******************************************************************************/

namespace System
{
    public partial class Win32
    {
        /// <summary>
        /// 空消息，不执行任何操作
        /// </summary>
        public const int WM_NULL = 0x0000;

        /// <summary>
        /// 创建消息，新窗口的窗口过程在窗口创建之后但在窗口变为可见之前接收此消息
        /// 当应用程序请求通过调用 CreateWindowEx 或 CreateWindow 函数创建窗口时发送。 (函数返回之前发送消息。) 
        /// 新窗口的窗口过程在创建窗口后接收此消息，但在窗口变为可见之前，窗口通过其 WindowProc 函数接收此消息
        /// <para>lParam：指向 <see cref="Win32.CREATESTRUCTA"/></para> 结构的指针，其中包含有关正在创建的窗口的信息，查看：
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-create"/>
        /// </summary>
        public const int WM_CREATE = 0x0001;

        /// <summary>
        /// 销毁窗口时发送。 从屏幕中删除窗口后，该窗口将发送到要销毁的窗口的窗口过程
        /// </summary>
        public const int WM_DESTROY = 0x0002;

        /// <summary>
        /// 移动消息，在窗口移动后发送
        /// </summary>
        public const int WM_MOVE = 0x0003;

        /// <summary>
        /// 更改窗口大小后发送到窗口,窗口通过其 WindowProc 函数接收此消息
        /// <para>lParam：窗口新的大小，以字符串表示，查看：<see cref="Win32.WMSIZE_WParam"/></para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-size"/>
        /// </summary>
        public const int WM_SIZE = 0x0005;

        /// <summary>
        /// 发送到正在激活的窗口和正在停用的窗口。 如果窗口使用相同的输入队列，则会同步发送消息，首先发送到正在停用的顶级窗口的窗口过程，
        /// 然后发送到正在激活的顶级窗口的窗口过程。 如果窗口使用不同的输入队列，则会异步发送消息，以便立即激活该窗口
        /// <para>wParam：低序单词指定窗口是激活还是停用。 此参数的取值可为下列值之一： 高阶单词指定正在激活或停用的窗口的最小化状态。 非零值指示窗口已最小化</para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/inputdev/wm-activate"/>
        /// </summary>
        public const int WM_ACTIVATE = 0x0006;

        /// <summary>
        /// 在窗口获得键盘焦点后发送到窗口
        /// </summary>
        public const int WM_SETFOCUS = 0x0007;

        /// <summary>
        /// 在窗口失去键盘焦点之前立即发送到窗口
        /// </summary>
        public const int WM_KILLFOCUS = 0x0008;

        /// <summary>
        /// 当应用程序更改窗口的启用状态时发送
        /// </summary>
        public const int WM_ENABLE = 0x000A;

        public const int WM_SETREDRAW = 0x000B;

        public const int WM_SETTEXT = 0x000C;

        public const int WM_GETTEXT = 0x000D;

        public const int WM_GETTEXTLENGTH = 0x000E;

        public const int WM_PAINT = 0x000F;

        public const int WM_CLOSE = 0x0010;

        /// <summary>
        /// 检索当前窗口的菜单句柄
        /// <br/>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/mn-gethmenu"/>
        /// </summary>
        public const int MN_GETHMENU = 0x01E1;

        /// <summary>
        /// 指示终止应用程序的请求，并在应用程序调用 PostQuitMessage 函数时生成。 此消息导致 GetMessage 函数返回零
        /// <br/>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-quit"/>
        /// </summary>
        public const int WM_QUIT = 0x0012;

        /// <summary>
        /// 当窗口背景必须在 (擦除时发送，例如，当窗口大小调整为) 时。 将发送消息以准备窗口的无效部分进行绘制
        /// <para>wParam：设备上下文的句柄</para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-erasebkgnd"/>
        /// </summary>
        public const int WM_ERASEBKGND = 0x0014;

        public const int WM_SYSCOLORCHANGE = 0x0015;

        /// <summary>
        /// 当窗口即将隐藏或显示时发送到窗口，窗口通过其 WindowProc 函数接收此消息
        /// <para>wParam：指示是否显示窗口。 如果 wParam 为 TRUE，则会显示窗口。 如果 wParam 为 FALSE，则窗口处于隐藏状态</para>
        /// <para>lParam：正在显示的窗口的状态。 如果 lParam 为零，则消息由于对 ShowWindow 函数的调用而发送;否则， lParam 是以下值之一</para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-showwindow?redirectedfrom=MSDN"/>
        /// </summary>
        public const int WM_SHOWWINDOW = 0x0018;

        /// <summary>
        /// WM_ACTIVATEAPP消息在应用程序启动时出现，变为活动应用程序或变为非活动应用程序
        /// </summary>
        public const int WM_ACTIVATEAPP = 0x001C;

        public const int WM_SETCURSOR = 0x0020;

        public const int WM_MOUSEACTIVATE = 0x0021;

        /// <summary>
        /// 当窗口的大小或位置即将更改时，发送到窗口。 应用程序可以使用此消息替代窗口的默认最大化大小和位置，或者默认的最小或最大跟踪大小
        /// 可操作结构体是<see cref="Win32.MINMAXINFO"/>
        /// 参考：<seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-getminmaxinfo"/>
        /// </summary>
        public const int WM_GETMINMAXINFO = 0x0024;

        /// <summary>
        /// 在主题更改事件之后广播到每个窗口。 主题更改事件的示例包括主题的激活、主题停用或从一个主题过渡到另一个主题
        /// </summary>
        public const int WM_THEMECHANGED = 0x031A;

        /// <summary>
        /// 用户登录或关闭后发送到所有窗口。 当用户登录或关闭时，系统会更新用户特定的设置。 系统更新设置后立即发送此消息
        /// </summary>
        public const int WM_USERCHANGED = 0x0054;

        /// <summary>
        /// 发送到一个窗口，其大小、位置或位置在 Z 顺序中将随着 对 SetWindowPos 函数或其他窗口管理函数的调用而更改,窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_WINDOWPOSCHANGING = 0x0046;

        /// <summary>
        /// 由于对 SetWindowPos 函数或其他窗口管理函数的调用，其大小、位置或位置在 Z 顺序中的窗口已更改
        /// wParam 未使用此参数。
        /// lParam 指向 WINDOWPOS 结构的指针，其中包含有关窗口的新大小和位置的信息
        /// </summary>
        public const int WM_WINDOWPOSCHANGED = 0x0047;

        public const int WM_CONTEXTMENU = 0x007B;

        /// <summary>
        /// 当 SetWindowLong 函数即将更改窗口的一个或多个样式时，发送到窗口。窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_STYLECHANGING = 0x007C;

        /// <summary>
        /// 当 SetWindowLong 函数更改窗口的一个或多个样式后，发送到窗口。窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_STYLECHANGED = 0x007D;

        /// <summary>
        /// 当显示器的分辨率更改时发送到所有的窗口
        /// </summary>
        public const int WM_DISPLAYCHANGE = 0x007E;

        public const int WM_GETICON = 0x007F;

        public const int WM_SETICON = 0x0080;

        // non client area
        /// <summary>
        /// 在WM_CREATE前的一个消息
        /// </summary>
        public const int WM_NCCREATE = 0x0081;

        public const int WM_NCDESTROY = 0x0082;

        /// <summary>
        /// 当某个窗口的客户区域必须被核算时发送此消息,计算窗体客户区域大小和位置的消息
        /// 
        /// </summary>
        public const int WM_NCCALCSIZE = 0x0083;

        /// <summary>
        /// 发送到窗口以确定窗口的哪个部分对应于特定的屏幕坐标
        /// <para>lParam，计算窗体客户区域大小和位置的消息的返回值，参考：<see cref="Win32.NCHITTEST_Return"/></para>
        /// </summary>
        public const int WM_NCHITTEST = 0x0084;

        /// <summary>
        /// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时
        /// </summary>
        public const int WM_NCPAINT = 0x0085;

        /// <summary>
        /// 此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态 
        /// </summary>
        public const int WM_NCACTIVATE = 0x0086;

        public const int WM_GETDLGCODE = 0x0087;

        public const int WM_SYNCPAINT = 0x0088;

        // non client mouse
        public const int WM_NCMOUSEMOVE = 0x00A0;

        /// <summary>
        /// 当用户按下鼠标左键时，光标位于窗口的非封闭区域时发布。 此消息将发布到包含光标的窗口。 如果窗口已捕获鼠标，则不会发布此消息
        /// 窗口通过其 WindowProc 函数接收此消息
        /// <para>wParam：DefWindowProc 函数返回的命中测试值，因为处理WM_NCHITTEST消息。 有关命中测试值的列表，请参阅 <see cref="Win32.WM_NCHITTEST"/></para>
        /// <para>lParam：包含光标的 x 坐标和 y 坐标的 POINTS 结构。 坐标相对于屏幕左上角</para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/inputdev/wm-nclbuttondown"/>
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0x00A1;

        public const int WM_NCLBUTTONUP = 0x00A2;

        /// <summary>
        /// 当用户双击鼠标左键时，光标位于窗口的非封闭区域时发布。 此消息将发布到包含光标的窗口。 如果窗口已捕获鼠标，则不会发布此消息。窗口通过其 WindowProc 函数接收此消息
        /// <para>wParam：DefWindowProc 函数返回的命中测试值，因为处理WM_NCHITTEST消息。 有关命中测试值的列表，请参阅 WM_NCHITTEST</para>
        /// <para>lParam：包含光标的 x 坐标和 y 坐标的 POINTS 结构。 坐标相对于屏幕左上角</para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/inputdev/wm-nclbuttondblclk"/>
        /// </summary>
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCRBUTTONDOWN = 0x00A4;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK = 0x00A6;
        public const int WM_NCMBUTTONDOWN = 0x00A7;
        public const int WM_NCMBUTTONUP = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK = 0x00A9;

        // keyboard
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x0102;

        /// <summary>
        /// 当 TranslateMessage 函数翻译WM_SYSKEYDOWN消息时，使用键盘焦点发布到窗口。WM_EXITSIZEMOVE 它指定系统字符键的字符代码，即在 Alt 键关闭时按下的字符键
        /// 参考：<seealso href="https://docs.microsoft.com/zh-cn/windows/win32/menurc/wm-syschar"/>
        /// </summary>
        public const int WM_SYSCHAR = 0x0106;

        /// <summary>
        /// 当用户从 “窗口 ”菜单中选择命令时，窗口会收到此消息， (以前称为系统或控件菜单) ，或者当用户选择最大化按钮、最小化按钮、还原按钮或关闭按钮时
        /// lParam参数：<see cref="Win32.WMSYSCOMMAND_WParam"/>
        /// 参考：<seealso href="https://docs.microsoft.com/zh-cn/windows/win32/menurc/wm-syscommand?redirectedfrom=MSDN"/>
        /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;

        public const int WM_SYSKEYDOWN = 0x0104;

        public const int WM_SYSKEYUP = 0x0105;

        /// <summary>
        /// 当系统检测到超过 30 秒到 60 秒间隔的系统时间超过 12.5% 时，发送到所有顶级窗口。 这表示系统内存较低,窗口通过其 WindowProc 函数接收此消息。
        /// 参考：<seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-compacting"/>
        /// </summary>
        public const int WM_COMPACTING = 0x0041;

        #region menu

        /// <summary>
        /// 当用户从菜单中选择命令项、控件将通知消息发送到其父窗口或翻译快捷键击时发送
        /// <return>如果应用程序处理此消息，它应返回零</return>
        /// 参考：<seealso href="https://docs.microsoft.com/zh-cn/windows/win32/menurc/wm-command"/>
        /// |——消息来源——|——WPARAM 高位——|——WPARAM 低位——|——LPARAM——|
        /// |—菜单——|—0——|——菜单标识符 (IDM_*)——|——0——|
        /// |—快捷键（加速器）——|——1—|——加速器标识符 (IDM_*)——|——0——|
        /// |——控件—|—控制定义的通知代码——|——控制标识符——|——控制窗口的句柄——|
        /// </summary>
        public const int WM_COMMAND = 0x0111;
        
        public const int WM_INITMENU = 0x0116;

        /// <summary>
        /// 下拉菜单或子菜单即将处于活动状态时发送。 这样，应用程序就可以在显示菜单之前修改菜单，而无需更改整个菜单
        /// </summary>
        public const int WM_INITMENUPOPUP = 0x0117;

        /// <summary>
        /// 当用户选择菜单项时发送到菜单的所有者窗口
        /// </summary>
        public const int WM_MENUSELECT = 0x011F;

        /// <summary>
        /// 当菜单处于活动状态并且用户按下与任何助记键或快捷键不对应的键时发送。 此消息将发送到拥有菜单的窗口
        /// </summary>
        public const int WM_MENUCHAR = 0x0120;
        public const int WM_ENTERIDLE = 0x0121;
        public const int WM_MENURBUTTONUP = 0x0122;
        public const int WM_MENUDRAG = 0x0123;
        public const int WM_MENUGETOBJECT = 0x0124;
        public const int WM_UNINITMENUPOPUP = 0x0125;
        public const int WM_MENUCOMMAND = 0x0126;

        public const int WM_CHANGEUISTATE = 0x0127;
        public const int WM_UPDATEUISTATE = 0x0128;
        public const int WM_QUERYUISTATE = 0x0129;

        
        #endregion

        // mouse
        public const int WM_MOUSEFIRST = 0x0200;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_MOUSELAST = 0x020D;
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_XBUTTONDBLCLK = 0x020D;
        

        public const int WM_PARENTNOTIFY = 0x0210;
        public const int WM_ENTERMENULOOP = 0x0211;
        public const int WM_EXITMENULOOP = 0x0212;

        public const int WM_NEXTMENU = 0x0213;

        /// <summary>
        /// 发送到用户正在调整大小的窗口。 通过处理此消息，应用程序可以监视拖动矩形的大小和位置，并根据需要更改其大小或位置
        /// </summary>
        public const int WM_SIZING = 0x0214;
        public const int WM_CAPTURECHANGED = 0x0215;
        public const int WM_MOVING = 0x0216;

        /// <summary>
        /// 在窗口进入移动模式循环或调整大小循环后，将一次发送到窗口。 当用户单击窗口的标题栏或大小调整边框或窗口将 
        /// WM_SYSCOMMAND 消息传递到 DefWindowProc 函数和消息 的 wParam 参数指定 SC_MOVE 或 SC_SIZE 值时，
        /// 窗口将输入移动或调整模式循环。 当 DefWindowProc 返回时，该操作已完成。
        /// 无论是否启用完整窗口拖动，系统都会发送 WM_ENTERSIZEMOVE 消息
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-entersizemove"/>
        /// </summary>
        public const int WM_ENTERSIZEMOVE = 0x0231;

        /// <summary>
        /// 在窗口退出移动或调整模式循环后，将一次发送到窗口。 当用户单击窗口的标题栏或大小调整边框或窗口将 WM_SYSCOMMAND 消息
        /// 传递到 DefWindowProc 函数时，窗口将输入移动或调整模式循环，而消息的 wParam 参数指定 SC_MOV E 或 SC_SIZE 值。 
        /// 当 DefWindowProc 返回时，此操作将完成。
        /// 窗口通过其 WindowProc 函数接收此消息。
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/winmsg/wm-entersizemove"/>
        /// </summary>
        public const int WM_EXITSIZEMOVE = 0x0232;

        public const int WM_MOUSELEAVE = 0x02A3;
        public const int WM_MOUSEHOVER = 0x02A1;
        public const int WM_NCMOUSEHOVER = 0x02A0;
        public const int WM_NCMOUSELEAVE = 0x02A2;

        public const int WM_MDIACTIVATE = 0x0222;
        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;

        /// <summary>
        /// 当用户按下 RegisterHotKey 函数注册的热键时发布。 消息放置在与注册热键的线程关联的消息队列的顶部
        /// <para>wParam：生成消息的热键的标识符。 如果消息是由系统定义的热键生成的，此参数将是以下值之一</para>
        /// <para>lParam：低序单词指定要与高序单词指定的键组合以生成 WM_HOTKEY 消息的键。 此词可以是以下一个或多个值。 高阶单词指定热键的虚拟密钥代码</para>
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/inputdev/wm-hotkey"/>
        /// </summary>
        public const int WM_HOTKEY = 0x312;

        public const int WM_PRINT = 0x0317;
        public const int WM_PRINTCLIENT = 0x0318;
        public const int WM_PASTE = 0X302;


        // IME
        
        /// <summary>
        /// 当 IME 获取转换结果的字符时发送到应用程序。 窗口通过其 WindowProc 函数接收此消息
        /// <seealso href="https://docs.microsoft.com/zh-cn/windows/win32/intl/wm-ime-char"/>
        /// </summary>
        public const int WM_IME_CHAR = 0x286;
        /// <summary>
        /// IME 发送到应用程序以通知应用程序按下键并保留消息顺序。 窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_IME_KEYDOWN = 0x290;
        /// <summary>
        /// IME 发送到应用程序以通知应用程序密钥发布并保留消息顺序。 窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_IME_KEYUP = 0x291;
        /// <summary>
        /// 在 IME 生成合成字符串之前立即发送，因为击键。 窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_IME_STARTCOMPOSITION = 0x10D;
        /// <summary>
        /// 当 IME 结束组合时发送到应用程序。 窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_IME_ENDCOMPOSITION = 0x10E;
        /// <summary>
        /// 当 IME 因击键而更改组合状态时发送到应用程序。 窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_IME_COMPOSITION = 0x10F;
        /// <summary>
        /// 激活窗口时发送到应用程序。 窗口通过其 WindowProc 函数接收此消息
        /// </summary>
        public const int WM_IME_SETCONTEXT = 0x281;
        public const int WM_IME_NOTIFY = 0x282;
        public const int WM_IME_CONTROL = 0x283;
        public const int WM_IME_COMPOSITIONFULL = 0x284;
        public const int WM_IME_SELECT = 0x285;
        public const int WM_IME_KEYLAST = 0x10F;
        public const int WM_IME_REQUEST = 0x288;

        
    }
}
