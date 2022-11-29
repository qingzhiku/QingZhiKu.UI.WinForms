using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class IFilterControlEx
    {
        public static bool IsFilterControl(this IFilterControl filter)
        {
            return filter != null && filter.FilterTypes.Length > 0;
        } 


    }
}
