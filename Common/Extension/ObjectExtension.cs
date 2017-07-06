using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class ObjectExtension
    {
        public static T ConvertTo<T>(this object value)
        {
            var valueType = Nullable.GetUnderlyingType(typeof (T));
            if (valueType != null)
            {
                if (value == null)
                {
                    return default(T);
                }
                var result = Convert.ChangeType(value, valueType);
                return (T) result;
            }
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }

        public static bool IsBoolean(this object value)
        {
            return value is bool;
        }

        public static bool IsDate(this object value)
        {
            return value is DateTime
                   || value is DateTimeOffset;
        }

        public static bool IsGenericList(this object o)
        {
            bool isGenericList = false;

            var oType = o.GetType();

            if (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof (List<>)))
                isGenericList = true;

            return isGenericList;

        }
        public static bool IsClass(this object o)
        {
            return o != null && o.GetType().IsClass && !(o is string);
        }

        public static Dictionary<string, object> ConvertToDictionary(this object o)
        {
            return o.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(o, null));
        }

        #region Copy object
        /* 
         * C# extension method for fast object cloning.
         * Deep object copy extension method, based on recursive "MemberwiseClone".
         * It is fast (3 times faster then BinaryFormatter), it works with any object, you don't need default constructor or serializable attributes.
         * Source: https://github.com/Burtsev-Alexey/net-object-deep-copy
         */

        private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(string)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        public static object Copy(this object originalObject)
        {
            return InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
        }
        private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
        {
            if (originalObject == null) return null;
            var typeToReflect = originalObject.GetType();
            if (IsPrimitive(typeToReflect)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = CloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }
        public static T Copy<T>(this T original)
        {
            return (T)Copy((object)original);
        }
        #endregion

    }
}