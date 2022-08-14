using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 第三层，检测内存不足窗体
    /// </summary>
    public class CompactForm : WindowProcForm
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("在系统内存过低时自动释放一次内存"), Category("NoneTitle")]
        public bool AutoCompacting { get; set; }

        public CompactForm()
            : base()
        {
        }

        protected override void WM_COMPACTING(ref Message m)
        {
            if (AutoCompacting)
            {
                OnGCCompacting(this, EventArgs.Empty);
            }
            base.WM_COMPACTING(ref m);
        }

        /// <summary>
        /// 内存过低时，自动释放
        /// </summary>
        protected virtual void OnGCCompacting(object sender, EventArgs e)
        {
            if (AutoCompacting)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }



    }
}
