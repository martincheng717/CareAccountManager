using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class DataRowExtension
    {
        public static T GetValue<T>(this DataRow dr, string fieldName)
        {
            try
            {
                if (string.IsNullOrEmpty(fieldName))
                {
                    throw new ArgumentNullException(nameof(fieldName), "FieldName cannot be null or empty");
                }
                if (!dr.Table.Columns.Contains(fieldName))
                {
                    return default(T);
                }
                if (dr.IsNull(fieldName))
                {
                    return default(T);
                }
                return dr[fieldName].ConvertTo<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to convert value for FieldName={fieldName}", ex);
            }

        }
    }
}
