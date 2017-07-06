using CareGateway.Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Note.Logic
{
    public interface INote<TResponse, in TRequest>
    {
        Task<TResponse> Execute(TRequest request);
    }
}
