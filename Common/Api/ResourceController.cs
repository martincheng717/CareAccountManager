using Gdot.Care.Common.Extension;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;

namespace Gdot.Care.Common.Api
{
    [ExcludeFromCodeCoverage]
    public abstract class ResourceController: ApiController
    {
        protected virtual IHttpActionResult CreateResponse<T>(T response)
        {
            if (response == null)
            {
                return this.NotFound("Not Found");
            }
            return Ok(response);
        }
    }
}
