using System;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class StringExtension
    {
        /// <summary>
        /// Trim white space. If string is null or whitespace, then it will return null value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string TrimOrDefault(this string str, string def = null)
        {
            return String.IsNullOrWhiteSpace(str) ? def : str.Trim();
        }
        /// <summary>
        /// Mask and only shows last defined number of characters
        /// </summary>
        /// <param name="value"></param>
        /// <param name="showLast"></param>
        /// <param name="maskChar"></param>
        /// <returns></returns>
        public static string Mask(this string value, int showLast = 4, char maskChar = '*')
        {
            if (value.Length > showLast)
            {
                return new string(maskChar, value.Length - showLast) + value.Substring(value.Length - showLast);
            }
            return value;
        }
    }
}
