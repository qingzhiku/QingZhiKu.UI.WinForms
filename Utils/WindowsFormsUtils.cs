using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public sealed class WindowsFormsUtils
    {


        public static bool SafeCompareStrings(string string1, string string2, bool ignoreCase)
        {
            if ((string1 == null) || (string2 == null))
            {
                return false;
            }

            if (string1.Length != string2.Length)
            {
                return false;
            }

            return String.Compare(string1, string2, ignoreCase, CultureInfo.InvariantCulture) == 0;
        }



    }
}
