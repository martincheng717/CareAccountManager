using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Partner.Logic
{
    public interface IPartner<in TRequest>
    {
        Task Execute(TRequest request);
    }
}
