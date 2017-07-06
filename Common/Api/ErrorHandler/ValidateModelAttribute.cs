using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Api.ErrorHandler
{
    [ExcludeFromCodeCoverage]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                ExceptionHandling(actionContext);
            }
            base.OnActionExecuting(actionContext);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (!actionContext.ModelState.IsValid)
            {
                ExceptionHandling(actionContext);
            }
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private void ExceptionHandling(HttpActionContext actionContext)
        {
            var error = string.Join(". ", actionContext.ModelState.Values
                .SelectMany(ms => ms.Errors)
                .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception.GetExceptionMessages() : e.ErrorMessage));
            if (string.IsNullOrEmpty(error))
            {
                error = "Invalid request";
            }
            throw new BadRequestException(error, new LogObject("ValidateModel", null));
        }
    }
}
