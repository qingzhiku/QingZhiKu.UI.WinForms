

using System;

namespace Microsoft.Win32
{
    public class RegistryHelper
    {


        /// <summary>
        /// 获取是否着色到标题栏或窗口边框
        /// </summary>
        public static bool DWMColorPrevalence
        {
            get
            {
                try
                {
                    var colorPrevalence = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorPrevalence", null);

                    var prevalence = (int)colorPrevalence;

                    //if (colorPrevalence is not int prevalence)
                    //{
                    //    throw new RegistryNoneFindException("None ColorPrevalence");
                    //}
                    //else
                    //{
                    return prevalence == 1;
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    //throw new RegistryNoneFindException("IsDWMColorPrevalence", ex);

                    return false;
                }
            }
        }



        

    }
}
