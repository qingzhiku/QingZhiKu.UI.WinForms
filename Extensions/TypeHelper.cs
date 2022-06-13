

namespace System
{
    internal static class TypeHelper
    {
        public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
        {
            var type = Type.GetType("System.Windows.Forms.Form");
            // System.Windows.Forms.DpiHelper
            return Type.GetTypeFromHandle(handle);
        }

        public static Type? GetDpiHelper()
        {
            //System.Windows.Forms.DpiHelper
            var t = Type.GetType("System.Windows.Forms.DpiHelper,System.Windows.Forms");
            return t;
        }

        

    }
}
