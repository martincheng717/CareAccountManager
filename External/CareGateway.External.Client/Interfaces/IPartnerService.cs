using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;

namespace CareGateway.External.Client.Interfaces
{
    public interface IPartnerService
    {
        Task<ResponseBase> PublishCaseStatus(PartnerCaseActivityRequest request);
    }
}
