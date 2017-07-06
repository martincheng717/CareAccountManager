using System;
using System.Linq;
using System.Reflection;
using Autofac;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Dependency
{
    [ExcludeFromCodeCoverage]
    public static class AutowiringPropertyInjector
    {
        private static readonly Type ReadOnlyCollectionType = Type.GetType("System.Collections.Generic.IReadOnlyCollection`1", false);

        private static readonly Type ReadOnlyListType = Type.GetType("System.Collections.Generic.IReadOnlyList`1", false);

        public static void InjectProperties(IComponentContext context, object instance, bool overrideSetValues)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (instance == null) throw new ArgumentNullException("instance");

            var instanceType = instance.GetType();

            foreach (var property in instanceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi => pi.CanWrite))
            {
                var propertyType = property.PropertyType;

                if (propertyType.IsValueType && !propertyType.IsEnum)
                    continue;

                if (propertyType.IsArray && propertyType.GetElementType().IsValueType)
                    continue;

                if (propertyType.IsGenericEnumerableInterfaceType() && propertyType.GetGenericArguments()[0].IsValueType)
                    continue;

                if (property.GetIndexParameters().Length != 0)
                    continue;

                var accessors = property.GetAccessors(false);
                if (accessors.Length == 1 && accessors[0].ReturnType != typeof(void))
                    continue;

                if (!overrideSetValues &&
                    accessors.Length == 2 &&
                    (property.GetValue(instance, null) != null))
                    continue;

                if (!context.IsRegistered(propertyType))
                {
                    var attr = property.GetCustomAttribute<WithKeyAttribute>(false);

                    if (attr == null) continue;

                    var value = context.ResolveKeyed(attr.Key, propertyType);
                    property.SetValue(instance, value, null);
                    continue;
                }
                
                var propertyValue = context.Resolve(propertyType);
                property.SetValue(instance, propertyValue, null);
            }
        }

        public static bool IsGenericEnumerableInterfaceType(this Type type)
        {
            return type.IsGenericTypeDefinedBy(typeof(IEnumerable<>))
                || type.IsGenericListOrCollectionInterfaceType();
        }

        public static bool IsGenericTypeDefinedBy(this Type @this, Type openGeneric)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (openGeneric == null) throw new ArgumentNullException(nameof(openGeneric));

            return !@this.GetTypeInfo().ContainsGenericParameters && @this.GetTypeInfo().IsGenericType && @this.GetGenericTypeDefinition() == openGeneric;
        }

        public static bool IsGenericListOrCollectionInterfaceType(this Type type)
        {
            return type.IsGenericTypeDefinedBy(typeof(IList<>))
                   || type.IsGenericTypeDefinedBy(typeof(ICollection<>))
                   || (ReadOnlyCollectionType != null && type.IsGenericTypeDefinedBy(ReadOnlyCollectionType))
                   || (ReadOnlyListType != null && type.IsGenericTypeDefinedBy(ReadOnlyListType));
        }
    }
}
