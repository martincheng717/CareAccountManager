using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.QMaster.Logic
{
    public interface IQMasterManager<TResponse, in TRequest>
    {
        Task<TResponse> Execute(TRequest request);
    }
    public interface IQMasterManager<in TRequest>
    {
        Task Execute(TRequest request);
    }
}
