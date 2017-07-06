using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Logic
{
    public interface ITakeAction<TResponse, in TRequest>
    {
        Task<TResponse> Execute(TRequest request);        
    }

    public interface ITakeAction<in TRequest>
    {
        Task Execute(TRequest request);
    }
}
 