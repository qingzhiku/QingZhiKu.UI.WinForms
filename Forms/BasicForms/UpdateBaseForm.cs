using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 第一层，更新基础<see cref="Form"/>
    /// </summary>
    public class UpdateBaseForm : InformationForm
    {
        public UpdateBaseForm()
            : base()
        {
            this.DoubleBuffered = true;
            AdjustStyles();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                return UpdateCreateParams(base.CreateParams);
            }
        }

        /// <summary>  
        /// 标题：获取一个值，用以指示 System.ComponentModel.Component 当前是否处于设计模式。  
        /// 描述：DesignMode 在 Visual Studio 2005 产品中存在 Bug ，使用下面的方式可以解决这个问题。  
        ///        详细信息地址：http://support.microsoft.com/?scid=kb;zh-cn;839202&x=10&y=15  
        /// </summary>  
        protected new bool DesignMode
        {
            get
            {
                bool returnFlag = false;
#if DEBUG
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    returnFlag = true;
                }
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
                {
                    returnFlag = true;
                }

                returnFlag |= base.DesignMode;
#endif
                return returnFlag;
            }
        }

        /// <summary>
        /// 调整样式，移植好以后，名称更改位UpdateStyles
        /// </summary>
        protected virtual void AdjustStyles()
        {
            if (!DesignMode)
            {
                // 使用双缓存
                this.SetStyle(
                ControlStyles.DoubleBuffer |                                     // 双缓冲
               ControlStyles.UserPaint |                                          // 自绘
               ControlStyles.AllPaintingInWmPaint, true);                 // 禁止擦除背景

                this.SetStyle(
                    ControlStyles.OptimizedDoubleBuffer |                      // 双缓冲
                    ControlStyles.ResizeRedraw /*|                                   // 自绘
                    ControlStyles.SupportsTransparentBackColor*/, true);   // 透明边界

                //this.SetStyle(
                //    ControlStyles.EnableNotifyMessage |
                //    ControlStyles.StandardDoubleClick |
                //    ControlStyles.Selectable |
                //    ControlStyles.StandardClick |
                //    ControlStyles.ContainerControl, true);

                UpdateStyles();
            }
        }

        protected virtual CreateParams UpdateCreateParams(CreateParams param)
        {
            return param;
        }


    }
}
