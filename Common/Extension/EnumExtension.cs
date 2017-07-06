using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class EnumExtension
    {
        public static string GetDescription(this System.Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof (DescriptionAttribute),
                    false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string GetEnumMemberValue<T>(this T enumVal)
        {
            var enumType = typeof(T);
            var name = System.Enum.GetName(enumType, enumVal);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }
        public static T GetValueFromDescription<T>(string enumDesc)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();
            var fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                                .Description == enumDesc);
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
    }
}
