using AttributeCaching;
using CareGateway.Sfdc.Logic.Salesforce;
using Gdot.Care.Common.Utilities;
using Salesforce.Force;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Sfdc.Model.Enum;
using System.Net.Http;
using Salesforce.Common;
using Gdot.Care.Common.Extension;
using Salesforce.Common.Models;
using CareGateway.Sfdc.Logic.CaseClientProxy;
using CareGateway.Sfdc.Logic.CaseClientProxy.Model;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Logic.Salesforce
{
    [ExcludeFromCodeCoverage]
    public sealed class SalesForceClientProxy: ICaseClientProxy
    {
        private static readonly Lazy<ICaseClientProxy> _instance =
            new Lazy<ICaseClientProxy>(()=> new SalesForceClientProxy());
        public static ICaseClientProxy Instance { get { return _instance.Value; } }
        public IForceClient ForceClient { get; set; }
   
        public async Task<ICaseClientProxy> GetForceClient(GetForceClientRequest req)
        {
            using (new MetricWatcher(SalesforceEventTypeEnum.ForceClientAuthentication.GetDescription()))
            {
                using (var authenticationClient = new AuthenticationClient())
                {
                    var httpClient = new HttpClient();
                    if (req.Assignment == ClientEnum.Header)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sforce-Auto-Assign", "FALSE");
                    }
                    await authenticationClient.UsernamePasswordAsync(
                        req.ClientId,
                        req.ClientSecret,
                        req.LoginAccount,
                        req.PlainTextPassword,
                        req.Domain);
                    var client = new SalesForceClientProxy();
                    client.ForceClient = new
                    ForceClient(
                        authenticationClient.InstanceUrl,
                        authenticationClient.AccessToken,
                        authenticationClient.ApiVersion, httpClient);
                    return client;
                }
            }
        }

        #region Same as IForceClient
        public Task<T> BasicInformationAsync<T>(string objectName)
        {
            return ForceClient.BasicInformationAsync<T>(objectName);
        }

        public async Task<ProxySuccessResponse> CreateAsync(string objectName, object record)
        {
            var rsp = await ForceClient.CreateAsync(objectName, record);
            return new ProxySuccessResponse { Id = rsp.Id, Success = rsp.Success, Errors = rsp.Errors };
        }
        public void Dispose()
        {
            Dispose();
        }

        public async Task<ProxyDescribeGlobalResult<T>> GetObjectsAsync<T>()
        {
            var rsp = await ForceClient.GetObjectsAsync<T>();
            return new ProxyDescribeGlobalResult<T> { SObjects = rsp.SObjects };
        }

        public async Task<ProxyQueryResult<T>> QueryAllAsync<T>(string query)
        {
            var rsp = await ForceClient.QueryAllAsync<T>(query);
            return new ProxyQueryResult<T> { Records = rsp.Records };
        }

        public async Task<ProxyQueryResult<T>> QueryAsync<T>(string query)
        {
            var rsp = await ForceClient.QueryAsync<T>(query);
            return new ProxyQueryResult<T> { Records = rsp.Records };
        }

        public async Task<T> QueryByIdAsync<T>(string objectName, string recordId)
        {
            var rsp = await ForceClient.QueryByIdAsync<T>(objectName, recordId);
            return rsp;
        }

        public async Task<ProxySuccessResponse> UpdateAsync(string objectName, string recordId, object record)
        {
            var rsp = await ForceClient.UpdateAsync(objectName, recordId, record);
            return new ProxySuccessResponse { Id = rsp.Id, Success = rsp.Success, Errors = rsp.Errors };
        }

        public async Task<T> UserInfo<T>(string url)
        {
            var rsp = await ForceClient.UserInfo<T>(url);
            return rsp;

        }
        #endregion
    }
}
