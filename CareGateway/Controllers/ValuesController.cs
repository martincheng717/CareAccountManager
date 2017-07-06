using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Web.Http;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.Controllers
{
    [ExcludeFromCodeCoverage]
    public class ValuesController : ApiController
    {
        private static readonly ILogger Log = Gdot.Care.Common.Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET api/values
        
        public IEnumerable<string> Get()
        {
            Log.Info(new LogObject("GetData", new Dictionary<string, object> { {"Data", new string[] { "value1", "value2" } }}));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
