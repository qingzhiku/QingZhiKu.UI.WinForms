using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Layout
{
    internal class LayoutTransaction
    {
        public static void DoLayout(Control elementToLayout, Control elementCausingLayout, string propertyName)
        {
            //var type = typeof(LayoutEngine).Assembly.GetTypes().FirstOrDefault(o => o.FullName == "System.Windows.Forms.Layout.LayoutTransaction");
            var type = typeof(LayoutEngine).Assembly.GetType("System.Windows.Forms.Layout.LayoutTransaction",false,true);
            var methodInfo = type?.GetMethod("DoLayout", BindingFlags.Static| BindingFlags.IgnoreCase);
            methodInfo?.Invoke(null, new Object[] { elementToLayout, elementCausingLayout, propertyName } );
        }



    }
}
