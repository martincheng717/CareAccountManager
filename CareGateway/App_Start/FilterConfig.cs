using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace CareGateway
{
    public class FilterConfig
    {
        [ExcludeFromCodeCoverage]
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
