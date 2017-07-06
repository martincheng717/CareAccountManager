using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Api.ErrorHandler
{
    [ExcludeFromCodeCoverage]
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                HttpResponseMessage response;
                if (context.Exception is BadRequestException)
                {
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.BadRequest);
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else if (context.Exception is ApiRequestException)
                {
                    var are = context.Exception as ApiRequestException;
                    response = new HttpResponseMessage(are.StatusCode);
                }
                else if (context.Exception is ExternalErrorException)
                {
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.ExternalError);
                    response = new HttpResponseMessage((HttpStatusCode)CustomHttpStatusCode.ExternalError);
                }
                else if (context.Exception is AggregateException)
                {
                    var aggex = context.Exception as AggregateException;
                    var innerEx = aggex.InnerExceptions.FirstOrDefault(ex => ex != null);
                    if (innerEx != null)
                    {
                        context.Exception = innerEx;
                        response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    }
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.InternalServerError);
                }
                else if (context.Exception is UserNotAllowedException)
                {
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.UserNotAllowed);
                    response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
                else if (context.Exception is NotFoundException)
                {
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.NotFound);
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else if (context.Exception is GdValidateException)
                {
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.ValidationError);
                    response = new HttpResponseMessage((HttpStatusCode)CustomHttpStatusCode.ValidateError);
                }
                else
                {
                    context.Exception = AssignDefaultErrorCode(context.Exception, ErrorCodeEnum.InternalServerError);
                    response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                response.Content = new ObjectContent<Exception>(context.Exception, new JsonMediaTypeFormatter());
                context.Response = response;
            }
            base.OnException(context);
        }

        private Exception AssignDefaultErrorCode(Exception ex, ErrorCodeEnum errorCode)
        {
            var gdEx = ex as GdErrorException;
            if (gdEx?.ErrorCode == ErrorCodeEnum.None)
            {
                gdEx.ErrorCode = errorCode;
                return gdEx;
            }
            return ex;
        }
    }
}
