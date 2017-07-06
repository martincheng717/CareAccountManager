using System;
using System.Net.Http;
using System.Threading.Tasks;
using AttributeCaching;
using CareGateway.Db.PartnerAuthentication.Logic;
using CareGateway.Db.PartnerAuthentication.Model;
using CareGateway.Db.PartnerAuthentication.Model.Enum;
using CareGateway.Sfdc.Model.Enum;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Utilities;
using CareGateway.Sfdc.Logic.CaseClientProxy.Model;
using CareGateway.Sfdc.Logic.CaseClientProxy;

namespace CareGateway.Sfdc.Logic.CaseService
{
    public class CaseService : ICaseService
    {        
        public ISqlCommand<GetPartnerAuthenticationOutput, GetPartnerAuthenticationInput> GetPartnerAuthenticationCommand { get; set; }
        public ICaseClientProxy CaseClientProxyObject { get; set; }
       
        [Cacheable(Hours = 3)]
        public async Task<ICaseClientProxy> GetUserNamePasswordForceClientAsync(ClientEnum assignment)
        {
            if (GetPartnerAuthenticationCommand == null)
            {
                GetPartnerAuthenticationCommand = new GetPartnerAuthentication();
            }
            var auth = await GetPartnerAuthenticationCommand.ExecuteAsync(new GetPartnerAuthenticationInput
            {
                PartnerAuthenticationKey = PartnerAuthenticationEnum.Sfdc
            });
            return await CaseClientProxyObject.GetForceClient(new GetForceClientRequest
            {
                ClientId = auth.ConsumerKey,
                ClientSecret = auth.ConsumerSecret,
                LoginAccount = auth.Login,
                PlainTextPassword = auth.Password,
                Domain = auth.Domain,
                Assignment = assignment
            });
        }

    }
}
