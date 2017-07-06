using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public  static  class HttpStatusCodeExt
    {
        /// <summary>
        /// return HttpStatusCode in integer string representation
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns>string</returns>
        public static string ToIntegerString(this HttpStatusCode httpStatusCode)
        {
            return ((int)httpStatusCode).ToString();
        }
    }
}
