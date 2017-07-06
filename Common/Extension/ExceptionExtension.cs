using System;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionExtension
    {
        public static string GetExceptionMessages(this Exception e)
        {
            if (e == null)
            {
                return string.Empty;
            }
            var msg = e.Message;
            if (e.InnerException != null)
            {
                msg += ". " + GetExceptionMessages(e.InnerException);
            }
            return msg;
        }
    }
}
