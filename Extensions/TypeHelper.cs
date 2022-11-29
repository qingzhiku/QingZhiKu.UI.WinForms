

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace System
{
    internal class TypeHelper
    {
        public const BindingFlags DefaultBingdingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreReturn;
        public const BindingFlags NonPublicBingdingFlags = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreReturn;

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

        internal static bool IsEnumerableType(Type enumerableType)
        {
            return FindGenericType(typeof(IEnumerable<>), enumerableType) != null;
        }

        internal static bool IsKindOfGeneric(Type type, Type definition)
        {
            return FindGenericType(definition, type) != null;
        }

        internal static Type GetElementType(Type enumerableType)
        {
            var ienumType = FindGenericType(typeof(IEnumerable<>), enumerableType);
            if (ienumType != null)
                return ienumType.GetGenericArguments()[0];
            return enumerableType;
        }

        internal static bool IsNullableType(Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        internal static Type GetNonNullableType(Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        internal static Type? FindGenericType(Type definition, Type? type)
        {
            while (type != null && type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == definition)
                    return type;
                if (definition.IsInterface)
                {
                    foreach (Type itype in type.GetInterfaces())
                    {
                         var found = FindGenericType(definition, itype);
                        if (found != null)
                            return found;
                    }
                }
                type = type.BaseType;
            }
            return null;
        }

        public static MethodInfo? GetMethod(object compent, string name, bool isPublic = true)
        {
            if (isPublic)
            {
                return compent?.GetMethod(name)?.FirstOrDefault();
            }
            else
            {
                return compent?.GetType()?.GetMethod(name, isPublic)?.FirstOrDefault();
            }
        }

        public static MethodInfo[] GetMethods(object compent, bool isPublic = true)
        {
            if (isPublic)
            {
                return compent.GetType().GetMethods(DefaultBingdingFlags);
            }
            else
            {
                return compent.GetType().GetMethods(NonPublicBingdingFlags);
            }
        }


    }
}
