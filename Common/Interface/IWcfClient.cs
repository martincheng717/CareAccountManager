using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Gdot.Care.Common.Interface
{
    public interface IWcfClient
    {
        Task<T> Execute<T>(Func<Task<T>> func, IDictionary<string, object> logData = null);
    }
}
