using System;
using System.Collections.Generic;
using System.Reflection;
//using System.Management;
using System.Text;
using Microsoft.Internal.Interop;
using System.Windows.Forms;

namespace System
{
    public static class OSFeatureEx
    {
        /// <summary>
        /// 通过发行版本判断是否是XP
        /// </summary>
        public static bool OnXp(this OSFeature feature)
        {
            Type type = feature.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            
            var property = type.GetProperty("OnXp", flags);
            var value = property?.GetValue(feature);

            return value == null ? false : (bool)value;
        }

        /// <summary>
        /// 通过发行版本判断是否是Win2k
        /// </summary>
        public static bool OnWin2k(this OSFeature feature)
        {
            Type type = feature.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var property = type.GetProperty("OnWin2k", flags);
            var value = property?.GetValue(feature);

            return value == null ? false : (bool)value;
        }

        /// <summary>
        /// 通过发行版本判断是否在Win11以上
        /// </summary>
        public static bool IsWindows11OrGreater(this OSFeature feature)
        {
            //bool onWin11 = false;
            //if (Environment.OSVersion.Platform == System.PlatformID.Win32NT)
            //{
            //    onWin11 = Environment.OSVersion.Version.CompareTo(new Version(10, 0, 22000, 0)) >= 0;
            //}
            //return onWin11;
            return VersionHelper.Current.IsWindows11OrGreater;
        }

        /// <summary>
        /// 通过发行版本判断是否在Win10以上
        /// </summary>
        public static bool IsWindows10OrGreater(this OSFeature feature)
        {
            //bool onWin11 = false;
            //if (Environment.OSVersion.Platform == System.PlatformID.Win32NT)
            //{
            //    onWin11 = Environment.OSVersion.Version.CompareTo(new Version(10, 0, 22000, 0)) >= 0;
            //}
            //return onWin11;
            return VersionHelper.Current.IsWindows10OrGreater;
        }

        //        /// <summary>
        //        /// 通过<see cref="System.Management.ManagementObjectSearcher"/> 获取系统版本字符串
        //        /// </summary>
        //        public static string GetOSVersion(this OSFeature feature)
        //        {
        //            try
        //            {
        //                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        //                string sCPUSerialNumber = "";
        //                foreach (ManagementObject mo in searcher.Get())
        //                {
        //#pragma warning disable CS8602 // 解引用可能出现空引用。
        //                    sCPUSerialNumber = mo["Name"].ToString().Trim();    //操作系统名字
        //#pragma warning restore CS8602 // 解引用可能出现空引用。
        ////#pragma warning disable CS8602 // 解引用可能出现空引用。
        ////                    sCPUSerialNumber = mo["BootDevice"].ToString().Trim();  //系统启动分区
        ////#pragma warning restore CS8602 // 解引用可能出现空引用。
        //                              //  sCPUSerialNumber = mo["NumberOfProcesses"].ToString().Trim();   //当前运行的进程数
        //                              //  sCPUSerialNumber = mo["SerialNumber"].ToString().Trim();    //操作系统序列号
        //                              //  sCPUSerialNumber = mo["OSLanguage"].ToString().Trim();  //操作系统的语言
        //                              //  sCPUSerialNumber = mo["Manufacturer"].ToString().Trim();     //
        //                }
        //                //  return sCPUSerialNumber.Substring(10, 10);//分割字符串
        //                return sCPUSerialNumber;//分割字符串
        //            }
        //            catch (Exception)
        //            {
        //                return string.Empty;
        //            }
        //        }

        /// <summary>
        /// Window版本名称
        /// </summary>
        public static OperatingSystemVersion GetOperatingSystemVersion(this OSFeature feature)
        {
            //OperatingSystemVersion productName = OperatingSystemVersion.Unknown;

            //var versionString = feature.GetOSVersion();

            //if(versionString != String.Empty)
            //{
            //    if(versionString.ToLower().Contains("windows 95"))  // 1995年
            //    {
            //        productName = OperatingSystemVersion.Win95;
            //    }
            //    else  if (versionString.ToLower().Contains("windows 98"))   // 1998年
            //    {
            //        productName = OperatingSystemVersion.Win98;
            //    }
            //    else if (versionString.ToLower().Contains("windows millennium edition"))    // 1998年
            //    {
            //        productName = OperatingSystemVersion.WinME;
            //    }
            //    else if (versionString.ToLower().Contains("windows xp"))    // 2001-2005年
            //    {
            //        productName = OperatingSystemVersion.WinXP;
            //    }
            //    else if (versionString.ToLower().Contains("windows vista")) // 2006-2008年
            //    {
            //        productName = OperatingSystemVersion.WinVista;
            //    }
            //    else if (versionString.ToLower().Contains("windows 7")) // 2009年
            //    {
            //        productName = OperatingSystemVersion.Win7;
            //    }
            //    else if (versionString.ToLower().Contains("windows 8")) // 2012年
            //    {
            //        productName = OperatingSystemVersion.Win8;
            //    }
            //    else if (versionString.ToLower().Contains("windows 10"))    // 2015年
            //    {
            //        productName = OperatingSystemVersion.Win10;
            //    }
            //    else if (versionString.ToLower().Contains("windows 11"))    // 2021年
            //    {
            //        productName = OperatingSystemVersion.Win11;
            //    }
            //}
            //else
            //{
            //    if (feature.OnXp())
            //    {
            //        productName = OperatingSystemVersion.WinXP;
            //    }
            //    else if (feature.OnWin2k())
            //    {
            //        productName = OperatingSystemVersion.Win2K;
            //    }
            //}

            //if(productName == OperatingSystemVersion.Unknown)
            //{
            //    if (feature.OnWin11OrLarge())
            //    {
            //        productName = OperatingSystemVersion.WinLargerWin11;
            //    }
            //}

            //return productName;

            return VersionHelper.Current.GetOperatingSystemVersion();
        }




    }
}
