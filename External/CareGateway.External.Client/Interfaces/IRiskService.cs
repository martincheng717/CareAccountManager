using System.Threading.Tasks;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;

namespace CareGateway.External.Client.Interfaces
{
    public interface IRiskService
    {
        Task UpdateOFACStatus(UpdateOFACStatusRequest request);
    }
}
