using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Internal.Interop
{
    public class VersionHelper
    {
        public readonly static VersionHelper Current=new VersionHelper();

        public OperatingSystemVersion GetOperatingSystemVersion()
        {
            OperatingSystemVersion version = OperatingSystemVersion.Unknown;

            OperatingSystem os = Environment.OSVersion;

           if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(10, 0,22000))
                version = OperatingSystemVersion.Windows11OrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(10, 0))
                version = OperatingSystemVersion.Windows10OrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 2))
                version = OperatingSystemVersion.Windows8OrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 1))
                version = OperatingSystemVersion.Windows7OrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 0))
                version = OperatingSystemVersion.WindowsVistaOrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 2))
                version = OperatingSystemVersion.WindowsServer2003OrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 1))
                version = OperatingSystemVersion.WindowsXPOrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 0))
                version = OperatingSystemVersion.Win2KOrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 10))
                version = OperatingSystemVersion.Windows98OrGreater;
            /*else*/ if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 0))
                version = OperatingSystemVersion.Windows95OrGreater;

            return version;
        }


        /// <summary>
        /// 操作系统是否为Windows 11及以上,版本号：10.0.22000.739
        /// </summary>
        public bool IsWindows11OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(10, 0,22000))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 10及以上,版本号：10.0.10240
        /// </summary>
        public bool IsWindows10OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(10, 0))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 8.1及以上,版本号：6.3.9200
        /// <para>或者操作系统是否为Windows Server 2012 R2及以上,版本号：6.3.9200</para>
        /// </summary>
        public bool IsWindows81OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 3))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 8及以上,版本号：6.2.9200
        /// <para>或者操作系统是否为Windows Server 2012及以上,版本号：6.2.9200</para>
        /// </summary>
        public bool IsWindows8OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 2))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows7及以上,版本号：6.1.7601;
        /// <para>或者操作系统是否为Windows Server 2008及以上,版本号：6.0.6001</para>
        /// </summary>
        public bool IsWindows7OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 1))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows Vista Service Pack 2及以上,版本号：6.0.6002
        /// </summary>
        public bool IsWindowsVistaSP
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 0,6002))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows Vista及以上,版本号：6.0.6000
        /// </summary>
        public bool IsWindowsVista
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 0))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        ///// <summary>
        ///// 操作系统是否为Windows Home Server及以上,版本号：5.2.3790
        ///// </summary>
        //public bool IsWindowsHS
        //{
        //    get
        //    {
        //        bool isGreater = false;

        //        OperatingSystem os = Environment.OSVersion;

        //        if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 2))
        //        {
        //            isGreater = true;
        //        }

        //        return isGreater;
        //    }
        //}

        /// <summary>
        /// 操作系统是否为Windows 2003及以上,版本号：5.2.3790
        /// </summary>
        public bool IsWindowsServer2003
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 2))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows XP Service Pack 3及以上,版本号：5.1.2600
        /// </summary>
        public bool IsWindowsXPSP3OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 1,2600))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows XP Service Pack 2及以上,版本号：5.1.2600.2180
        /// </summary>
        public bool IsWindowsXPSP2OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 1,2600,2180))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows XP Service Pack 1及以上,版本号：5.1.2600.1105-1106
        /// </summary>
        public bool IsWindowsXPSP1OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 1,2600,1105))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows XP及以上,版本号：5.1.2600
        /// </summary>
        public bool IsWindowsXPOrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 1))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 2000及以上,版本号：5.00.2195
        /// </summary>
        public bool IsWindows2KOrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(5, 0))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows NT 4.00及以上,版本号：4.00.1381
        /// </summary>
        public bool IsWindowsNT4OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(4, 0))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows NT 3.51及以上,版本号：3.51.1057
        /// </summary>
        public bool IsWindowsNT351OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(3, 51))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows NT 3.5及以上,版本号：3.50.807
        /// </summary>
        public bool IsWindowsNT35OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(3, 50))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows NT 3.1及以上,版本号：3.10.528
        /// </summary>
        public bool IsWindowsNT31OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32NT && os.Version >= new Version(3, 10))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows Millenium及以上,版本号：4.90.3000
        /// </summary>
        public bool IsWindowsMilleniumOrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 90))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 98 Second Edition (SE)版本号：4.10.2222 A
        /// </summary>
        public bool IsWindows98SE
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32Windows 
                    && os.Version.Major == 4 
                    && os.Version.Minor == 10
                    && os.Version.Revision.ToString() == "2222A")
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 98及以上,版本号：4.10.1998
        /// </summary>
        public bool IsWindows98OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                // os.Version.Revision.ToString() == "2222A"

                if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 10))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 95 OEM Service Release 2.5 C及以上,版本号：4.03.1214
        /// </summary>
        public bool IsWindows95SR25OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 03,1214))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 95 OEM Service Release 2.1及以上,版本号：4.03.1212-1214
        /// </summary>
        public bool IsWindows95SR21OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 03,1212))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 95 OEM Service Release 2 (95B)及以上,版本号：4.00.1111
        /// </summary>
        public bool IsWindows95SE2OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 0,1111))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是否为Windows 95 OEM Service Release 1 (95A)及以上,版本号：4.00.950
        /// </summary>
        public bool IsWindows95OrGreater
        {
            get
            {
                bool isGreater = false;

                OperatingSystem os = Environment.OSVersion;

                if (os.Platform == PlatformID.Win32Windows && os.Version >= new Version(4, 0))
                {
                    isGreater = true;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是Unix
        /// </summary>
        public bool IsUnix
        {
            get
            {
                bool isGreater = false;
                OperatingSystem os = Environment.OSVersion;

                switch (os.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                        break;
                    case PlatformID.Unix:
                        isGreater = true;
                        break;
                    case PlatformID.Xbox:
                        break;
                    case PlatformID.MacOSX:
                        break;
                    case PlatformID.Other:
                        break;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是Windows
        /// </summary>
        public bool IsWindows
        {
            get
            {
                bool isGreater = false;
                OperatingSystem os = Environment.OSVersion;

                switch (os.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                        isGreater = true;
                        break;
                    case PlatformID.Unix:
                        break;
                    case PlatformID.Xbox:
                        break;
                    case PlatformID.MacOSX:
                        break;
                    case PlatformID.Other:
                        break;
                }

                return isGreater;
            }
        }

        /// <summary>
        /// 操作系统是MacOSX
        /// </summary>
        public bool IsMacOSX
        {
            get
            {
                bool isGreater = false;
                OperatingSystem os = Environment.OSVersion;

                switch (os.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                        break;
                    case PlatformID.Unix:
                        break;
                    case PlatformID.Xbox:
                        break;
                    case PlatformID.MacOSX:
                        isGreater = true;
                        break;
                    case PlatformID.Other:
                        break;
                }

                return isGreater;
            }
        }


        //internal class DLLVersionHelper
        // {
        //     // The C(++) macro VER_SET_CONDITION mentioned in the documentation for RtlVerifyVersionInfo seems to be equivalent to the VerSetConditionMask function in kernel32.dll

        //     // https://docs.microsoft.com/en-us/windows/win32/api/winnt/nf-winnt-versetconditionmask
        //     [DllImport("kernel32.dll")]
        //     private static extern ulong VerSetConditionMask(ulong dwlConditionMask, uint dwTypeBitMask, byte dwConditionMask);

        //     // https://docs.microsoft.com/en-us/windows/win32/devnotes/rtlgetversion
        //     [DllImport("ntdll.dll")]
        //     private static extern int RtlGetVersion(ref OSVERSIONINFOW lpVersionInformation);

        //     [DllImport("ntdll.dll")]
        //     private static extern int RtlGetVersion(ref OSVERSIONINFOEXW lpVersionInformation);

        //     // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/wdm/nf-wdm-rtlverifyversioninfo
        //     [DllImport("ntdll.dll")]
        //     private static extern bool RtlVerifyVersionInfo([In] ref OSVERSIONINFOEXW lpVersionInformation, uint dwTypeMask, ulong dwlConditionMask);

        //     // https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexw
        //     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //     private struct OSVERSIONINFOEXW
        //     {
        //         internal uint dwOSVersionInfoSize;
        //         internal uint dwMajorVersion;
        //         internal uint dwMinorVersion;
        //         internal uint dwBuildNumber;
        //         internal uint dwPlatformId;
        //         [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        //         internal string szCSDVersion;
        //         internal ushort wServicePackMajor;
        //         internal ushort wServicePackMinor;
        //         internal ushort wSuiteMask;
        //         internal byte wProductType;
        //         internal byte wReserved;
        //     }

        //     // https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfow
        //     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //     private struct OSVERSIONINFOW
        //     {
        //         internal uint dwOSVersionInfoSize;
        //         internal uint dwMajorVersion;
        //         internal uint dwMinorVersion;
        //         internal uint dwBuildNumber;
        //         internal uint dwPlatformId;
        //         [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        //         internal string szCSDVersion;
        //     }

        //     /*
        //      * RTL_OSVERSIONINFOEX(A/W) and OSVERSIONINFOEX(A/W) are aliases for the same structures
        //      * RTL_OSVERSIONINFO(A/W) and OSVERSIONINFO(A/W) are aliases for the same structures
        //      * */

        //     // These constants initialized with corresponding definitions in
        //     // winnt.h (part of Windows SDK)
        //     // https://docs.microsoft.com/en-us/windows/win32/api/winnt/nf-winnt-ver_set_condition
        //     private const byte VER_EQUAL = 1;
        //     private const byte VER_GREATER = 2;
        //     private const byte VER_GREATER_EQUAL = 3;
        //     private const byte VER_LESS = 4;
        //     private const byte VER_LESS_EQUAL = 5;
        //     private const byte VER_AND = 6;
        //     private const byte VER_OR = 7;

        //     private const byte VER_CONDITION_MASK = 7;
        //     private const byte VER_NUM_BITS_PER_CONDITION_MASK = 3;

        //     private const uint STATUS_SUCCESS = 0x00000000;

        //     //
        //     // RtlVerifyVersionInfo() type mask bits
        //     //
        //     private const uint VER_MINORVERSION = 0x0000001;
        //     private const uint VER_MAJORVERSION = 0x0000002;
        //     private const uint VER_BUILDNUMBER = 0x0000004;
        //     private const uint VER_PLATFORMID = 0x0000008;
        //     private const uint VER_SERVICEPACKMINOR = 0x0000010;
        //     private const uint VER_SERVICEPACKMAJOR = 0x0000020;
        //     private const uint VER_SUITENAME = 0x0000040;
        //     private const uint VER_PRODUCT_TYPE = 0x0000080;

        //     // wProductType    
        //     // Any additional information about the system.This member can be one of the following values.
        //     private const byte VER_NT_DOMAIN_CONTROLLER = 0x0000002;
        //     private const byte VER_NT_SERVER = 0x0000003;
        //     private const byte VER_NT_WORKSTATION = 0x0000001;


        //     // You can customize this to check for the condition(s) you need using any field from the OSVERSIONINFOW struct with the corresponding VER_ and VER_<operator> constants
        //     public static bool IsWindowsVersionOrGreater(uint majorVersion, uint minorVersion, ushort servicePackMajor = 0, uint buildNumber = 0)
        //     {
        //         var osVerInfo = new OSVERSIONINFOEXW
        //         {
        //             dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(OSVERSIONINFOEXW)),
        //             dwMajorVersion = majorVersion,
        //             dwMinorVersion = minorVersion,
        //             wServicePackMajor = servicePackMajor,
        //             dwBuildNumber = buildNumber
        //         };
        //         ulong versionOrGreaterMask = VerSetConditionMask(
        //             VerSetConditionMask(
        //                 VerSetConditionMask(
        //                     0, VER_MAJORVERSION, VER_GREATER_EQUAL),
        //                 VER_MINORVERSION, VER_GREATER_EQUAL),
        //             VER_SERVICEPACKMAJOR, VER_GREATER_EQUAL);
        //         uint versionOrGreaterTypeMask = VER_MAJORVERSION | VER_MINORVERSION | VER_SERVICEPACKMAJOR;
        //         if (buildNumber > 0)
        //         {
        //             versionOrGreaterMask = VerSetConditionMask(versionOrGreaterMask, VER_BUILDNUMBER, VER_GREATER_EQUAL);
        //             versionOrGreaterTypeMask |= VER_BUILDNUMBER;
        //         }
        //         return RtlVerifyVersionInfo(ref osVerInfo, versionOrGreaterTypeMask, versionOrGreaterMask);
        //     }

        //     public static bool IsWindowsServer()
        //     {
        //         var osVerInfo = new OSVERSIONINFOEXW
        //         {
        //             dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(OSVERSIONINFOEXW)),
        //             wProductType = VER_NT_WORKSTATION
        //         };
        //         ulong dwlConditionMask = VerSetConditionMask(0, VER_PRODUCT_TYPE, VER_EQUAL);
        //         return !RtlVerifyVersionInfo(ref osVerInfo, VER_PRODUCT_TYPE, dwlConditionMask);
        //     }

        //     public static int GetWindowsBuildNumber()
        //     {
        //         var osVerInfo = new OSVERSIONINFOW
        //         {
        //             dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(OSVERSIONINFOW))
        //         };
        //         if (STATUS_SUCCESS == RtlGetVersion(ref osVerInfo)) // documented to always return STATUS_SUCCESS
        //             return (int)osVerInfo.dwBuildNumber;
        //         throw new Win32Exception("Failed to determine Windows build number.");
        //     }

        //     // Other functions replicating SDK Version Helper functions
        //     // https://docs.microsoft.com/en-us/windows/win32/sysinfo/version-helper-apis

        //     //
        //     // _WIN32_WINNT version constants
        //     //
        //     const ushort _WIN32_WINNT_NT4 = 0x0400;
        //     const ushort _WIN32_WINNT_WIN2K = 0x0500;
        //     const ushort _WIN32_WINNT_WINXP = 0x0501;
        //     const ushort _WIN32_WINNT_WS03 = 0x0502;
        //     const ushort _WIN32_WINNT_WIN6 = 0x0600;
        //     const ushort _WIN32_WINNT_VISTA = 0x0600;
        //     const ushort _WIN32_WINNT_WS08 = 0x0600;
        //     const ushort _WIN32_WINNT_LONGHORN = 0x0600;
        //     const ushort _WIN32_WINNT_WIN7 = 0x0601;
        //     const ushort _WIN32_WINNT_WIN8 = 0x0602;
        //     const ushort _WIN32_WINNT_WINBLUE = 0x0603;
        //     const ushort _WIN32_WINNT_WINTHRESHOLD = 0x0A00;
        //     const ushort _WIN32_WINNT_WIN10 = 0x0A00;

        //     const bool FALSE = false;

        //     static byte LOBYTE(ushort w)
        //     {
        //         return ((byte)(w & 0xff));
        //     }

        //     static byte HIBYTE(ushort w)
        //     {
        //         return ((byte)(w >> 8 & 0xff));
        //     }

        //     public static bool
        //     IsWindowsXPSP1OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WINXP), LOBYTE(_WIN32_WINNT_WINXP), 1);
        //     }

        //     public static bool
        //     IsWindowsXPSP2OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WINXP), LOBYTE(_WIN32_WINNT_WINXP), 2);
        //     }

        //     public static bool
        //     IsWindowsXPSP3OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WINXP), LOBYTE(_WIN32_WINNT_WINXP), 3);
        //     }

        //     public static bool
        //     IsWindowsVistaOrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_VISTA), LOBYTE(_WIN32_WINNT_VISTA), 0);
        //     }

        //     public static bool
        //     IsWindowsVistaSP1OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_VISTA), LOBYTE(_WIN32_WINNT_VISTA), 1);
        //     }

        //     public static bool
        //     IsWindowsVistaSP2OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_VISTA), LOBYTE(_WIN32_WINNT_VISTA), 2);
        //     }

        //     public static bool
        //     IsWindows7OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WIN7), LOBYTE(_WIN32_WINNT_WIN7), 0);
        //     }

        //     public static bool
        //     IsWindows7SP1OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WIN7), LOBYTE(_WIN32_WINNT_WIN7), 1);
        //     }

        //     public static bool
        //     IsWindows8OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WIN8), LOBYTE(_WIN32_WINNT_WIN8), 0);
        //     }

        //     public static bool
        //     IsWindows8Point1OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WINBLUE), LOBYTE(_WIN32_WINNT_WINBLUE), 0);
        //     }

        //     public static bool
        //     IsWindows10OrGreater()
        //     {
        //         return IsWindowsVersionOrGreater(HIBYTE(_WIN32_WINNT_WIN10), LOBYTE(_WIN32_WINNT_WIN10), 0);
        //     }


        // }


    }
}
