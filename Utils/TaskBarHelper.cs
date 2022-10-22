using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public static class TaskBarHelper
    {
        /// <summary>
        /// 动态设置任务栏图标
        /// </summary>
        public static void SetTaskIconDynamic(Form form,string number)
        {
            //动态绘制图标样式
            Size size = form.Icon.Size;
            Bitmap cursorBitmap = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(cursorBitmap);
            graphics.Clear(Color.FromArgb(0, 0, 0, 0));
            graphics.ResetClip();
            Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);

            //Gdi+自定义绘制图标
            graphics.DrawImage(form.Icon.ToBitmap(), rect);
            graphics.FillEllipse(new SolidBrush(Color.FromArgb(244, 107, 10)), new Rectangle(rect.Width / 2 - 2, rect.Height / 2 - 2, rect.Width / 2, rect.Height / 2));
            graphics.DrawString(number, form.Font, Brushes.White, new Rectangle(rect.Width / 2 - 2, rect.Height / 2 - 2, rect.Width / 2, rect.Height / 2), new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            });

            //生成Icon
            Icon cursor = Icon.FromHandle(cursorBitmap.GetHicon());
            graphics.Dispose();
            cursorBitmap.Dispose();

            //更新任务栏图标样式
            form.Icon = cursor;
        }



        
    }
}
