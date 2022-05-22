using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    /// <summary>
    /// WM_SYSCOMMAND消息状态，可以用来提前判断窗口状态
    /// </summary>
    public enum PrevisionFormWindowState
    {
        /// <summary>
        ///  窗体装备恢复到之前状态，可以在<see cref="Form.WindowState"/>查询到之前的状态
        /// </summary>
        Restoring = 0,
        
        /// <summary>
        /// 窗体即将最小化
        /// </summary>
        Minimizing = 1,
        
        /// <summary>
        /// 窗体即将最大化
        /// </summary>
        Maximizing = 2,

        /// <summary>
        /// 无动作
        /// </summary>
        None = 3,

        /// <summary>
        ///  已经调整窗口大小
        /// </summary>
        Resized = 4
    }


}
