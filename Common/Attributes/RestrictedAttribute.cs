using System;

namespace Gdot.Care.Common.Attributes
{
    /// <summary>
    /// Restricted attribute is a indicator to ignore from logging for api's request/response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RestrictedAttribute : Attribute
    {
        //intentionally left empty
    }
}
