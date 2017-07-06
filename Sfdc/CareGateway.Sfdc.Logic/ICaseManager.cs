using System.Threading.Tasks;
using CareGateway.Sfdc.Model;

namespace CareGateway.Sfdc.Logic
{
    public interface ICaseManager
    {
        Task<CaseResponse> Create(CaseEx request);
        Task<CaseResponse> Update(CaseEx request);
        Task UpdateOFACStatus(UpdateOFACStatusRequest request);
    }
}
