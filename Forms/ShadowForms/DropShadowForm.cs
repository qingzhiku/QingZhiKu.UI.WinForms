using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 给窗体添加阴影
    /// <br />使用方法，在窗体创建句柄时添加：
    /// <br />protected override void CreateHandle()
    /// <br />{
    /// <br />      base.CreateHandle();
    /// <br />      DropShadowForm.ExtendForm(this, configration => {
    /// <br />          	configration.MasterMoveable = true;
    /// <br />          	configration.ShadowBlur = 35;
    /// <br />          	configration.ShadowSpread = 8;
    /// <br />          	configration.CornerRound = 8;
    /// <br />          	configration.StandardMaximizedBounds = true;
    /// <br />      });
    /// <br />}
    /// </summary>
    public partial class DropShadowForm : Form
    {
        private Form? _masterForm;
        private DropShadowNativeWindow? _nativeWindow;
        private bool _ncACTIVATE = false;

        private int _cornerRound;
        private int _shadowSpread = 10;

        public int ShadowSpread
        {
            get => _shadowSpread;
            set => _shadowSpread = Math.Max(1, value);
        }

        public int ShadowBlur { get; set; }
        public Color ShadowColor
        {
            get
            {
                var color = Color.FromArgb(_ncACTIVATE ? 35 : 25, Color.Black);

                return color;
            }
        }

        public bool StandardMaximizedBounds { get; set; }
        
        public Color BorderColor
        {
            get
            {
                var color = _ncACTIVATE ? SystemColors.ControlDark : ControlPaint.LightLight(SystemColors.ControlDark);

                return color;
            }
        }
        public int BorderWidth { get; set; }
        public int CornerRound {
            get
            {
                if (_masterForm != null)
                {
                    if(_masterForm.WindowState == FormWindowState.Maximized)
                    {
                        return 0;
                    }

                    if (_masterForm.WindowState == FormWindowState.Normal)
                    {
                        if (Math.Min(_masterForm.Width, _masterForm.Height) < _cornerRound*2)
                        {
                            return Math.Min(_masterForm.Width, _masterForm.Height) / 2;
                        }
                    }
                }

                return _cornerRound;
            }
            set=> _cornerRound = value; 
        }

        public bool MasterMoveable { get; set; }
        public FormBorderStyle MasterFormBorderStyle { get; set; } 

        public MarginRectangle MarginRectangle { get; private set; }

        public AngleRectangle AngleRectangle { get; private set; }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (!DesignMode)
                {
                    if (ControlBox)
                    {
                        cp.Style |= Win32.WS_SYSMENU;
                    }

                    if (MaximizeBox)
                    {
                        cp.Style |= (int)Win32.WS_MAXIMIZEBOX; // 允许最大化操作
                        cp.ExStyle |= (int)Win32.WS_MAXIMIZEBOX; // 允许最大化操作
                    }

                    if (MinimizeBox)
                    {
                        cp.Style |= (int)Win32.WS_MINIMIZEBOX; // 允许最小化操作
                        cp.ExStyle |= (int)Win32.WS_MINIMIZEBOX; // 允许最小化操作
                    }

                    

                    //cp.ExStyle |= (int)Win32.WS_CLIPCHILDREN;  //防止因窗体控件太多出现闪烁
                    //cp.ExStyle |= Win32.WS_EX_TRANSPARENT; // 设置窗体为透明，启动后无法实现缩放
                    cp.ExStyle |= Win32.WS_EX_LAYERED; // 窗体透明，自定义窗体时需要启用
                }
                return cp;
            }
        }

        private DropShadowForm()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                //FormBorderStyle = FormBorderStyle.None;
                DoubleBuffered = true;

                base.ShowInTaskbar = false;
                //base.BringToFront();
                base.FormBorderStyle = FormBorderStyle.None;

                base.AutoScaleMode = AutoScaleMode.None;

                this.UpdateStylesToReduceFlicker();

                this.BringToFront();
            }
        }

        ///// <summary>
        ///// 创建窗体阴影
        ///// </summary>
        //public static DropShadowForm ExtendForm(Form form)
        //{
        //    //if (form == null || form.Handle == IntPtr.Zero)
        //    //{
        //    //    throw new ArgumentNullException("窗体为空或者窗体句柄为空");
        //    //}

        //    //DropShadowForm ds = new DropShadowForm()
        //    //    .OnExtendForm(form, configuration =>
        //    //    {
        //    //        configuration.ShadowSpread = 10;
        //    //        configuration.ShadowBlur = 35;
        //    //        configuration.CornerRound = 8;
        //    //        configuration.BorderWidth = 1;
        //    //        configuration.MasterMoveable = true;

        //    //    });

        //    ////ds.BindingMasterForm(form, EventArgs.Empty);
        //    ////ds.UpdateStylesMaster(form,EventArgs.Empty);
        //    //ds.Show(form);

        //    //return ds;
        //    return DropShadowForm.ExtendForm(form, null);
        //}

        /// <summary>
        /// 创建窗体阴影
        /// </summary>
        public static DropShadowForm ExtendForm(Form form, Action<DropShadowConfigration>? configureServices)
        {
            if (form == null || form.Handle == IntPtr.Zero)
            {
                throw new ArgumentNullException("窗体为空或者窗体句柄为空");
            }

            DropShadowForm ds = new DropShadowForm()
                .OnExtendForm(form, configureServices);
            
            //ds.BindingMasterForm(form, EventArgs.Empty);
            //ds.UpdateStylesMaster(form,EventArgs.Empty);
            ds.Show(form);

            return ds;
        }

        //protected virtual DropShadowForm OnExtendForm(Form masterForm)
        //{
        //    return OnExtendForm(masterForm, null);
        //}

        protected virtual DropShadowForm OnExtendForm(Form masterForm, Action<DropShadowConfigration>? configureServices)
        {
            if (configureServices != null)
            {
                configureServices(new DropShadowConfigration(this));
            }
            else
            {
                ShadowBlur = 35;
                ShadowSpread = 8;
                CornerRound = 8;
                MasterMoveable = true;
                StandardMaximizedBounds = true;
                //MasterFormBorderStyle = FormBorderStyle.None;
            }
            
            BindingMasterForm(masterForm, EventArgs.Empty);

            return this;
        }

        protected virtual void BindingMasterForm(object sender, EventArgs args)
        {
            if (sender is not Form master) return;

            this.Owner = master;
            this._masterForm = master;

            this.TopMost = master.TopMost;
            this.MaximizeBox = master.MaximizeBox;
            this.MinimizeBox = master.MinimizeBox;
            this.ControlBox = master.ControlBox;

            this._nativeWindow = new DropShadowNativeWindow(master, this);
            this._nativeWindow.AssignHandle(master.Handle);

            this.Location = new Point(master.Left - this.ShadowSpread, master.Top - this.ShadowSpread);
            this.Size = new Size(master.Width + this.ShadowSpread * 2, master.Height + this.ShadowSpread * 2);

            Icon = master.Icon;
            ShowIcon = master.ShowIcon;
            Text = master.Text;

            //if (!this.ControlBox)
            //this.CanPenetrate();

            this.UpdateMasterStyles(sender, args);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //if (_masterForm != null)
            //{
            //    if (_masterForm.WindowState == FormWindowState.Normal)
            //    {
            //        UpdateShadowBitmap();
            //    }

            //    if (_masterForm.WindowState == FormWindowState.Maximized)
            //    {
            //        UpdateShadowBitmap();
            //    }
            //}

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            
            
        }



    }
}
