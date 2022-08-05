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
            => UpdateCreateParams(base.CreateParams);

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
