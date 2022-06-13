

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

                    if (colorPrevalence is not int prevalence)
                    {
                        throw new RegistryNoneFindException("None ColorPrevalence");
                    }
                    else
                    {
                        return prevalence == 1;
                    }
                }
                catch (Exception ex)
                {
                    throw new RegistryNoneFindException("IsDWMColorPrevalence", ex);
                }
            }
        }



        

    }
}
