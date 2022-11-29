using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 删除标题栏的标准可调整尺寸窗体
    /// </summary>
    public class FormBase2 : RecalculateNCForm
    {
        /// <summary>
        /// 覆盖设置窗体边缘样式属性，使其只读
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new WindowBorderStyle FormBorderStyle
        {
            get
            {
                return WindowBorderStyle.Sizeable;
            }
        }

        public FormBase2()
            : base()
        {
            base.FormBorderStyle = WindowBorderStyle.Sizeable;
            DoubleBuffered = true;
        }

        protected override CreateParams UpdateCreateParams(CreateParams param)
        {
            CreateParams cp = base.UpdateCreateParams(param);

            if (!DesignMode)
            {
                //param.Style &= ~Win32.WS_CAPTION;
                //param.Style &= ~Win32.WS_SYSMENU;
                //param.Style &= ~Win32.WS_MINIMIZEBOX;
                //param.Style &= ~Win32.WS_MAXIMIZEBOX;
                //param.ExStyle &= ~Win32.WS_EX_WINDOWEDGE;

                //if (AeroEnable)
                //{
                //    //cp.Style |= Win32.WS_BORDER;
                //    //cp.Style |= Win32.WS_CLIPSIBLINGS;
                //}
                //else
                //{
                //    //cp.Style &= ~Win32.WS_BORDER;
                //    //cp.Style &= ~Win32.CS_DROPSHADOW;
                //}

                if (ControlBox)
                {
                    cp.Style |= Win32.WS_SYSMENU; // 明确要求标题栏支持通过 Win + ← / Win + → 捕捉
                }

                if (MaximizeBox)
                {
                    cp.Style |= Win32.WS_MAXIMIZEBOX;  // 添加最大化按钮，支持鼠标拖动最大化 到屏幕顶部
                }

                if (MinimizeBox)
                {
                    cp.Style |= Win32.WS_MINIMIZEBOX; // 添加最小化按钮，支持点击任务栏图标最小化
                }

                //cp.Style |= Win32.WS_SIZEBOX;  // 标准可调整大小窗口所需
                //param.Style |= BitConverter.ToInt32(BitConverter.GetBytes(Win32.WS_POPUP));
                //cp.Style |= Win32.WS_VISIBLE; // 使窗口在创建后可见（不重要）
                // 防止因窗体控件太多出现闪烁，原理主窗口不绘制子窗口背景，由子窗口自己绘制
                // 一个按钮也是窗口
                cp.Style |= Win32.WS_CLIPCHILDREN;
                //cp.Style |= Win32.WS_CLIPSIBLINGS;

                param.ExStyle &= ~Win32.WS_EX_COMPOSITED;
                cp.ExStyle |= Win32.WS_EX_APPWINDOW;

                //cp.ExStyle |= Win32.WS_EX_LAYERED; // 窗体透明，自定义窗体时需要启用
                // 不激活窗口
                //param.ExStyle |= Win32.WS_EX_NOACTIVATE;

                //param.ClassStyle &= ~Win32.CS_NOCLOSE;
                cp.ClassStyle |= Win32.CS_VREDRAW;
                cp.ClassStyle |= Win32.CS_DBLCLKS;

            }

            //if (createParamsHack)
            //{
            //    // cp.Style &= ~(int)(Win32.WS_BORDER | Win32.WS_CAPTION | Win32.WS_DLGFRAME | Win32.WS_THICKFRAME);
            //    cp.Style &= ~(Win32.WS_CAPTION | Win32.WS_DLGFRAME | Win32.WS_BORDER | Win32.WS_THICKFRAME); //
            //}

            return cp;
        }

        protected override void SetWindowBorderAdjustGapCore(int left, int right, int top, int bottom)
        {
            Padding extendGap = WindowNCBorderThickness;

            if (IsWindows10OrGreater)
            {
                if (AeroEnable)
                {
                    //Padding = new Padding(0, WindowBorderSize.Height, 0, 0);
                    extendGap = new Padding(
                        0,
                        WindowNCBorderThickness.Top,
                        0,
                        0);
                    WindowBorderSpecified = ToolStripStatusLabelBorderSides.Top;
                }
                else
                {
                    //Padding = new Padding(WindowBorderSize.Width, WindowBorderSize.Height, WindowBorderSize.Width, WindowBorderSize.Height);
                    WindowBorderSpecified = ToolStripStatusLabelBorderSides.All;
                }
            }
            else
            {
                //Padding = new Padding(WindowBorderSize.Width, WindowBorderSize.Height, WindowBorderSize.Width, WindowBorderSize.Height);
                WindowBorderSpecified = ToolStripStatusLabelBorderSides.All;
            }

            base.SetWindowBorderAdjustGapCore(
                DesignMode ? 0 : extendGap.Left,
                DesignMode ? 0 : extendGap.Right,
                DesignMode ? 0 : extendGap.Top,
                DesignMode ? 0 : extendGap.Bottom);
        }

        protected override void SetWindowMinPaddingCore(int minleft, int mintop, int minright, int minbottom)
        {
            Padding minpd = Padding.Empty;

            if (IsWindows10OrGreater)
            {
                if (AeroEnable)
                {
                    minpd = new Padding(0, WindowBorderSize.Height, 0, 0);
                }
                else
                {
                    minpd = new Padding(WindowBorderSize.Width, WindowBorderSize.Height, WindowBorderSize.Width, WindowBorderSize.Height);
                }
            }
            else
            {
                minpd = new Padding(WindowBorderSize.Width, WindowBorderSize.Height, WindowBorderSize.Width, WindowBorderSize.Height);
            }

            base.SetWindowMinPaddingCore(minpd.Left, minpd.Top, minpd.Right, minpd.Bottom);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            Padding = new Padding(
                Math.Max(Padding.Left, WindowMinPadding.Left),
                Math.Max(Padding.Top, WindowMinPadding.Top),
                Math.Max(Padding.Right, WindowMinPadding.Right),
                Math.Max(Padding.Bottom, WindowMinPadding.Bottom)
                );
        }

        protected override void CalculateNewWindowGapCore(Padding newWindowGap)
        {
            var gap = newWindowGap;

            if (PrevisionWindowState == FormWindowState.Maximized)
            {
                gap.Top -= (WindowNCBorderThickness.Bottom - WindowBorderSize.Height);

                if (IsWindows10OrGreater)
                {
                    if (AeroEnable)
                    {
                        gap.Left = 0;
                        gap.Right = 0;
                        gap.Bottom = 0;
                    }
                    else
                    {
                        gap.Left = WindowBorderSize.Width;
                        gap.Right = WindowBorderSize.Width;
                        gap.Bottom = WindowBorderSize.Height;
                    }
                }
                else
                {
                    gap.Left = WindowBorderSize.Width;
                    gap.Right = WindowBorderSize.Width;
                    gap.Bottom = WindowBorderSize.Height;
                }
            }

            base.CalculateNewWindowGapCore(gap);
        }

        protected override void SetWindowMinMaxBoundsCore(Size ptMaxSize, Point ptMaxPosition, Size ptMinTrackSize, Size ptMaxTrackSize)
        {
            Point maxPosition = ptMaxPosition;
            //maxPosition.X = 0/*WindowExtendClientAreaIntoFrame.Left - WindowNCBorderThickness.Left*/;

            Size maxTrackSize = ptMaxTrackSize;
            maxTrackSize.Width = SystemInformation.WorkingArea.Width;
            maxTrackSize.Height = SystemInformation.WorkingArea.Height + (Size - ClientSize).Height;

            base.SetWindowMinMaxBoundsCore(ptMaxSize, maxPosition, ptMinTrackSize, maxTrackSize);
        }

       



    }
}
