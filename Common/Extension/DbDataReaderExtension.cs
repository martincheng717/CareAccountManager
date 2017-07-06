using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class DbDataReaderExtension
    {
        public static T GetValue<T>(this DbDataReader dbDataReader, string fieldName)
        {
            try
            {
                if (string.IsNullOrEmpty(fieldName))
                {
                    throw new ArgumentNullException(nameof(fieldName), "FieldName cannot be null or empty");
                }
                if (dbDataReader[fieldName] == DBNull.Value)
                {
                    return default(T);
                }
                return dbDataReader[fieldName].ConvertTo<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value for FieldName={fieldName}", ex);
            }
            
        }
    }
}
