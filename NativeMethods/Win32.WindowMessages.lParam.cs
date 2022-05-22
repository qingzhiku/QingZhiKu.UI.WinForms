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
        /// 正在显示的窗口的状态。 如果 lParam 为零，则消息由于对 ShowWindow 函数的调用而发送;否则， lParam 是以下值之一
        /// </summary>
        public class WMSHOWWINDOW_LParam
        {
            ///<summary>
            /// 正在发现窗口，因为已还原或最小化最大化窗口
            ///</summary>、
            public const int SW_OTHERUNZOOM = 4;

            ///<summary>
            /// 窗口正被已最大化的另一个窗口覆盖
            ///</summary>
            public const int SW_OTHERZOOM = 2;

            ///<summary>
            ///窗口的所有者窗口正在最小化
            ///</summary>
            public const int SW_PARENTCLOSING = 1;

            ///<summary>
            ///正在还原窗口的所有者窗口
            ///</summary>
            public const int SW_PARENTOPENING = 3;
        }

        /// <summary>
        /// lParam返回值，指向消息<see cref="Win32.WM_HOTKEY"/>
        /// 低序单词指定要与高序单词指定的键组合以生成 WM_HOTKEY 消息的键。 此词可以是以下一个或多个值。 高阶单词指定热键的虚拟密钥代码
        /// </summary>
        public class WMHOTKEY_LParam
        {
            ///<summary>
            /// 任一 ALT 键被按住
            ///</summary>、
            public const int MOD_ALT = 0x0001;

            ///<summary>
            /// 任一 CTRL 键被按住
            ///</summary>
            public const int MOD_CONTROL = 0x0002;

            ///<summary>
            ///任何一个 SHIFT 键都被按住
            ///</summary>
            public const int MOD_SHIFT = 0x0004;

            ///<summary>
            ///两个 WINDOWS 密钥都已按住。 这些键标有Windows徽标。 涉及Windows密钥的热键保留供操作系统使用
            ///</summary>
            public const int MOD_WIN = 0x0008;
        }



        
    }
}
