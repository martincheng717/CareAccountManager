using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public class NotFoundActionResult : IHttpActionResult
    {
        public NotFoundActionResult(string message, HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Message = message;
            Request = request;
        }

        public string Message { get; }

        public HttpRequestMessage Request { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
    
        public HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(Message, Encoding.UTF8, "application/json"),
                RequestMessage = Request
            };
            return response;
        }
    }
}