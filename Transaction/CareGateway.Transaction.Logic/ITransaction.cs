using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Transaction.Logic
{
    public interface ITransaction<TResponse, in TRequest>
    {
        Task<TResponse> Execute(TRequest request);
    }
    public interface ITransaction<in TRequest>
    {
        Task Execute(TRequest request);
    }
}
