using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    public static class OSFeatureEx
    {
        public static bool OnXp(this OSFeature feature)
        {
            Type type = feature.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            
            var property = type.GetProperty("OnXp", flags);
            var value = property?.GetValue(feature);

            return value == null ? false : (bool)value;
        }

        public static bool OnWin2k(this OSFeature feature)
        {
            Type type = feature.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var property = type.GetProperty("OnWin2k", flags);
            var value = property?.GetValue(feature);

            return value == null ? false : (bool)value;
        }

        public static bool OnWin11(this OSFeature feature)
        {
            bool onWin11 = false;
            if (Environment.OSVersion.Platform == System.PlatformID.Win32NT)
            {
                onWin11 = Environment.OSVersion.Version.CompareTo(new Version(10, 0, 22000, 0)) >= 0;
            }
            return onWin11;
        }



    }
}
