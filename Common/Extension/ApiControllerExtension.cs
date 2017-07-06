using System.Diagnostics.CodeAnalysis;
using System.Web.Http;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class ApiControllerExtension
    {
        public static NotFoundActionResult NotFound(this ApiController controller, string message)
        {
            return new NotFoundActionResult(message, controller.Request);
        }
    }
}