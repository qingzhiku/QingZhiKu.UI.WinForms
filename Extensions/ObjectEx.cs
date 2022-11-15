using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ObjectEx
    {
        public static MethodInfo[]? GetMethod(this object currentObject, string name, bool isPublic = true)
        {
            if (isPublic)
            {
                return currentObject?.GetType().GetMethods(TypeHelper.DefaultBingdingFlags).Where(x => x.Name == name).ToArray();
            }
            else
            {
                return currentObject?.GetType().GetMethods(TypeHelper.NonPublicBingdingFlags).Where(x => x.Name == name).ToArray();
            }
        }



    }
}
