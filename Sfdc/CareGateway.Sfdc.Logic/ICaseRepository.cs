using CareGateway.Sfdc.Logic.CaseClientProxy;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using CareGateway.Sfdc.Model.Salesforce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic
{
    public interface ICaseRepository
    {
        FieldConfigInfo GetFields(string recordType, CaseActionEnum action);
        Task<string> GetRecordTypeId(string recordType, ObjectTypeEnum objectType = ObjectTypeEnum.Account);
        Task<string> GetOwnerId(string groupName);
        Task<string> GetCaseId(string caseNumber);
        Task<string> GetOwnerIdByUserName(string userName);
        PropertyInfo[] GetProperties<T>(T request);
        PropertyInfo GetProperty<T>(string name, T request);
        object ValidateField<T>(T request, Model.FieldInfo fieldInfo);
        void ValidateNotAllowedFields<T>(IEnumerable<PropertyInfo> notUseFields, T request) where T : class;
        Task<CaseResponse> CreateAsync(SalesforceEventTypeEnum eventType, string sObject, object record, IDictionary<string, object> logData);
        Task PreProcessing<TRequest>(TRequest request, dynamic tasks, CaseActionEnum action);
        Task<CaseResponse> UpdateAsync(SalesforceEventTypeEnum eventType, string sObject, string caseId, object record, IDictionary<string, object> logData);
        Task<Account> GetPersonAccount(PersonAccountInput input, ICaseClientProxy client = null);
    }
}
