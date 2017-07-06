using System;
using Gdot.Care.Common.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Gdot.Care.Common.Attributes;
using Gdot.Care.Common.Extension;

namespace Gdot.Care.Common.Api
{
    /// <summary>
    /// Put [apiLog] attribute on top of the route, log api request will log by default
    /// Put [ApiLog(LogOption = ApiLogOption.LogResponse)]
    /// Put [ApiLog(LogOption = ApiLogOption.LogRequest | ApiLogOption.LogResponse)] to log both request and response
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApiLogAttribute : ActionFilterAttribute
    {
        private static readonly ILogger Log = Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static AsyncLocal<Dictionary<string,object>> LogParam = new AsyncLocal<Dictionary<string,object>>();
        public ApiLogAttribute()
        {
            LogOption = ApiLogOption.LogRequest;
        }
        public ApiLogOption LogOption { get; set; }
        public static void UpdateProperty(object obj)
        {
            Queue<object> queue = new Queue<object>();
            queue.Enqueue(obj);

            while (queue.Any())
            {
                object queueObject = queue.Dequeue();
                if (queueObject is ICollection)
                {
                    foreach (var colValue in (ICollection) queueObject)
                    {
                        queue.Enqueue(colValue);
                    }
                    continue;
                }
                var objType = queueObject.GetType();
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

                foreach (var objProp in objType.GetProperties(bindingFlags))
                {
                    var customAttribute = objProp.GetCustomAttributes(typeof(RestrictedAttribute));
                    if (customAttribute != null && customAttribute.Any())
                    {
                        objProp.SetValue(queueObject, null);
                    }
                    else
                    {
                        var objValue = objProp.GetValue(queueObject);
                        if (objValue != null && objValue.IsClass())
                        {
                            queue.Enqueue(objValue);
                        }
                    }

                }
            }
        }


        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            try
            {
                Task<object> responseTask = null;
                if ((LogOption & ApiLogOption.LogResponse) > 0 && actionExecutedContext?.Response?.Content != null)
                {
                    responseTask  = actionExecutedContext.Response.Content.ReadAsAsync<object>(cancellationToken);
                }

                var requestDic = new Dictionary<string, object>();
                if ((LogOption &  ApiLogOption.LogRequest) > 0)
                {
                    foreach (var action in actionExecutedContext.ActionContext.ActionArguments)
                    {

                        requestDic.Add(action.Key, action.Value);
                        UpdateProperty(action.Value);
                    }
                    LogParam.Value.Add("request", requestDic);
                }
                if (responseTask != null)
                {
                    var response = await responseTask;
                    var cpResponse = response.Copy();
                    UpdateProperty(cpResponse);

                    LogParam.Value.Add("response", cpResponse);
                }
                
            }
            catch (Exception ex)
            {
                Log.Warn(new LogObject("logRequest_Exception", null), ex);
            }

            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}