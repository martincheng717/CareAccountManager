using CareGateway.Sfdc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic.CaseClientProxy
{
    public interface ICaseClientProxy
    {

        Task<ICaseClientProxy> GetForceClient(GetForceClientRequest req);
        Task<T> BasicInformationAsync<T>(string objectName);
        Task<ProxySuccessResponse> CreateAsync(string objectName, object record);
        void Dispose();
        Task<ProxyDescribeGlobalResult<T>> GetObjectsAsync<T>();
        Task<ProxyQueryResult<T>> QueryAllAsync<T>(string query);
        Task<ProxyQueryResult<T>> QueryAsync<T>(string query);
        Task<T> QueryByIdAsync<T>(string objectName, string recordId);
        Task<ProxySuccessResponse> UpdateAsync(string objectName, string recordId, object record);
        Task<T> UserInfo<T>(string url);
    }
}
