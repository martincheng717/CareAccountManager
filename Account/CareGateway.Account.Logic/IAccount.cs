using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Account.Logic
{
    public interface IAccount<TResponse, in TRequest>
    {
        Task<TResponse> Execute(TRequest request);
    }

    public interface IAccount<in TRequest>
    {
        Task Execute(TRequest request);
    }
}