using CareGateway.Sfdc.Logic.CaseClientProxy;
using CareGateway.Sfdc.Logic.CaseClientProxy.Model;
using Salesforce.Force;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic.CaseService
{
    public interface ICaseService
    {
        ICaseClientProxy CaseClientProxyObject { get; set; }
        Task<ICaseClientProxy> GetUserNamePasswordForceClientAsync(ClientEnum assignment);        
    }
}