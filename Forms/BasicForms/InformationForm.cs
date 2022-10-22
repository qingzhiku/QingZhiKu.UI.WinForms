using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// 第零层，系统信息窗体
    /// </summary>
    public class InformationForm : Form
    {
        private UserPreferenceChangedEventHandler _userPreferenceChanged;

        /// <summary>
        /// 是否是64位进程
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Is64BitProcess
        {
            get { return Environment.Is64BitProcess; }
        }

        /// <summary>
        /// 是否是64位系统
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Is64BitOperatingSystem
        {
            get { return Environment.Is64BitOperatingSystem; }
        }

        /// <summary>
        /// 是否联网
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Network
        {
            get { return NativeMethodHelper.Network;  }
        }

        /// <summary>
        /// 当前窗体状态，比<see cref="Form.WindowState"/> 更灵活，可以准确实时获取
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormWindowState PrevisionWindowState
        {
            get
            {
                return NativeMethodHelper.GetWindowState(this);
            }
        }

        /// <summary>
        /// Windows系统版本名称
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperatingSystemVersion OperatingSystemVersion
        {
            get
            {
                OperatingSystemVersion osv = OSFeature.Feature.GetOperatingSystemVersion();

                //SetOperatingSystemVersionCore(ref osv);

                return osv;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CaptionHeight
        {
            get
            {
                return SystemInformation.CaptionHeight;
            }
        }


        public InformationForm()
            : base()
        {
            _userPreferenceChanged = new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
            SystemEvents.UserPreferenceChanged += _userPreferenceChanged;
        }

        protected virtual void SetOperatingSystemVersionCore(ref OperatingSystemVersion version)
        {
            //string operatingSystem= string.Empty;

            // 引入System.Management.dll

            //            try
            //            {
            //                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            //                string sCPUSerialNumber = "";
            //                foreach (ManagementObject mo in searcher.Get())
            //                {
            //#pragma warning disable CS8602 // 解引用可能出现空引用。
            //                    sCPUSerialNumber = mo["Name"].ToString().Trim();    //操作系统名字
            //#pragma warning restore CS8602 // 解引用可能出现空引用。
            //                    //#pragma warning disable CS8602 // 解引用可能出现空引用。
            //                    //                    sCPUSerialNumber = mo["BootDevice"].ToString().Trim();  //系统启动分区
            //                    //#pragma warning restore CS8602 // 解引用可能出现空引用。
            //                    //  sCPUSerialNumber = mo["NumberOfProcesses"].ToString().Trim();   //当前运行的进程数
            //                    //  sCPUSerialNumber = mo["SerialNumber"].ToString().Trim();    //操作系统序列号
            //                    //  sCPUSerialNumber = mo["OSLanguage"].ToString().Trim();  //操作系统的语言
            //                    //  sCPUSerialNumber = mo["Manufacturer"].ToString().Trim();     //
            //                }
            //                //  return sCPUSerialNumber.Substring(10, 10);//分割字符串
            //                operatingSystem = sCPUSerialNumber;//分割字符串
            //            }
            //            catch (Exception)
            //            { }

            //OperatingSystemVersion productName = OperatingSystemVersion.Unknown;

            //if (operatingSystem != String.Empty)
            //{
            //    if (operatingSystem.ToLower().Contains("windows 95"))  // 1995年
            //    {
            //        productName = OperatingSystemVersion.Win95;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows 98"))   // 1998年
            //    {
            //        productName = OperatingSystemVersion.Win98;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows millennium edition"))    // 1998年
            //    {
            //        productName = OperatingSystemVersion.WinME;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows xp"))    // 2001-2005年
            //    {
            //        productName = OperatingSystemVersion.WinXP;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows vista")) // 2006-2008年
            //    {
            //        productName = OperatingSystemVersion.WinVista;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows 7")) // 2009年
            //    {
            //        productName = OperatingSystemVersion.Win7;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows 8")) // 2012年
            //    {
            //        productName = OperatingSystemVersion.Win8;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows 10"))    // 2015年
            //    {
            //        productName = OperatingSystemVersion.Win10;
            //    }
            //    else if (operatingSystem.ToLower().Contains("windows 11"))    // 2021年
            //    {
            //        productName = OperatingSystemVersion.Win11;
            //    }
            //}
            //else
            //{
            //    if (OSFeature.Feature.OnXp())
            //    {
            //        productName = OperatingSystemVersion.WinXP;
            //    }
            //    else if (OSFeature.Feature.OnWin2k())
            //    {
            //        productName = OperatingSystemVersion.Win2K;
            //    }
            //}

            //if (productName == OperatingSystemVersion.Unknown)
            //{
            //    if (OSFeature.Feature.OnWin11OrLarge())
            //    {
            //        productName = OperatingSystemVersion.WinLargerWin11;
            //    }
            //}

            //version = productName;

        }

        protected virtual void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                SystemEvents.UserPreferenceChanged -= _userPreferenceChanged;
            }
        }



    }
}
