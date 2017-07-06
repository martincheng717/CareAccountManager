using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AttributeCaching;
using CareGateway.Sfdc.Logic.Salesforce;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using CareGateway.Sfdc.Model.Salesforce;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Model;
using Gdot.Care.Common.Utilities;
using Newtonsoft.Json;
using Salesforce.Common;
using Salesforce.Common.Models;
using Salesforce.Force;
using FieldInfo = CareGateway.Sfdc.Model.FieldInfo;
using Task = System.Threading.Tasks.Task;
using CareGateway.Sfdc.Logic.CaseClientProxy.Model;
using CareGateway.Sfdc.Logic.CaseClientProxy;
using CareGateway.Sfdc.Logic.CaseService;

namespace CareGateway.Sfdc.Logic
{
    public class CaseRepository: ICaseRepository
    {
        public ICaseService CaseService { get; set; }
        protected static readonly ILogger Logger = Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        [Cacheable]
        public FieldConfigInfo GetFields(string recordType, CaseActionEnum action)
        {
            var eventType = $"CaseBase_Get{action}Fields";
            try
            { 
                if (string.IsNullOrWhiteSpace(recordType))
                {
                    throw new BadRequestException("Required field RecordType is empty",
                        new LogObject(eventType, null));
                }
                var caseSetting = SfdcSetting.Setting(recordType);
                if (caseSetting == null)
                {
                    throw new BadRequestException($"{action} required field RecordType is invalid",
                        new LogObject(eventType, null));
                }
                var settingPath = action == CaseActionEnum.Create ? caseSetting.CaseSettingPath : caseSetting.UpdateCaseSettingPath;
                var configPath = Utility.GetFullpath(Path.Combine("CaseConfiguration", settingPath));
                return JsonConvert.DeserializeObject<FieldConfigInfo>(File.ReadAllText(configPath));
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    $"An error has occurred when trying to deserialize case configuration for RecordType {recordType}",
                    new LogObject(eventType, new Dictionary<string, object> {{"RecordType", recordType}}), ex);
            }

        }
       
        [Cacheable]
        [ExcludeFromCodeCoverage]
        protected FieldConfigInfo GetCaseCommentFields()
        {
            try
            {
                var configPath = Utility.GetFullpath(Path.Combine("CaseConfiguration", "CaseComment.json"));

                return JsonConvert.DeserializeObject<FieldConfigInfo>(File.ReadAllText(configPath));
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "An error has occurred when trying to deserialize case configuration for CaseComment",
                    new LogObject("CaseBase_GetCaseCommentFields", null), ex);
            }
        }
        public async Task<string> GetRecordTypeId(string recordType, ObjectTypeEnum objectType = ObjectTypeEnum.Account)
        {
            var recordTypeId = string.Empty;
            if (!string.IsNullOrWhiteSpace(recordType))
            {
                var response = await QueryObjectAsync<RecordType>(new Dictionary<string, object>
                {
                    { "sObjectType", objectType },
                    { "isActive", true },
                    { "Name", recordType },

                }, "Id");
                if (response?.Records.Count > 0)
                {
                    recordTypeId = response.Records[0].Id;
                }
            }
            return recordTypeId;
        }

        public async Task<string> GetOwnerId(string groupName)
        {
            var ownerId = string.Empty;
            if (!string.IsNullOrWhiteSpace(groupName))
            {
                //getting Id from Group object
                var response = await QueryObjectAsync<Group>(new Dictionary<string, object> { { "Name", groupName } }, "Id");
                if (response?.Records.Count > 0)
                {
                    ownerId = response.Records[0].Id;
                }
            }
            return ownerId;
        }

        public async Task<string> GetCaseId(string caseNumber)
        {
            var caseId = string.Empty;
            if (!string.IsNullOrWhiteSpace(caseNumber))
            {
                //getting Id from Group object
                var response = await QueryObjectAsync<Case>(new Dictionary<string, object> { { "CaseNumber", caseNumber } }, "Id");
                if (response?.Records.Count > 0)
                {
                    caseId = response.Records[0].Id;
                }
            }
            return caseId;
        }

        public async Task<string> GetOwnerIdByUserName(string userName)
        {
            var ownerId = string.Empty;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                //getting Id from Group object
                var response = await QueryObjectAsync<User>(new Dictionary<string, object> { { "UserName", userName } }, "Id");
                if (response?.Records.Count > 0)
                {
                    ownerId = response.Records[0].Id;
                }
            }
            return ownerId;
        }

        /// <summary>
        /// Generic function to get data from salesforce object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="outputFields"></param>
        /// <returns></returns>
        private async Task<ProxyQueryResult<T>> QueryObjectAsync<T>(IDictionary<string, object> conditions, params string[] outputFields)
        {
            return await QueryObjectAsync<T>(null, conditions, outputFields);
        }

        /// <summary>
        /// Query salesforce object async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="client"></param>
        /// <param name="outputFields"></param>
        /// <returns></returns>
        private async Task<ProxyQueryResult<T>> QueryObjectAsync<T>(ICaseClientProxy client, IDictionary<string, object> conditions, params string[] outputFields)
        {
            if (client == null)
            {
                client = await CaseService.GetUserNamePasswordForceClientAsync(ClientEnum.Default);
            }
            //build outputFields
            var fields = string.Join(", ", outputFields.Select(s => s));
            //build where clause
            var whereClause = string.Empty;
            foreach (var conditionValue in conditions)
            {
                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    whereClause += " and ";
                }
                if (conditionValue.Value.IsNumber() || conditionValue.Value.IsNumber())
                {
                    whereClause += string.Join("=", conditionValue.Key, conditionValue.Value);
                }
                if (conditionValue.Value.IsBoolean() || conditionValue.Value.IsBoolean())
                {
                    whereClause += string.Join("=", conditionValue.Key, conditionValue.Value);
                }
                else
                {
                    whereClause += string.Join("=", conditionValue.Key, $"'{conditionValue.Value}'");
                }
            }
            var soql = $"select {fields} from {typeof(T).Name} where {whereClause}";
            var response = await client.QueryAsync<T>(soql);
            return response;
        }

        public PropertyInfo[] GetProperties<T>(T request)
        {
            return request.GetType().GetProperties();
        }
        public PropertyInfo GetProperty<T>(string name, T request)
        {
            var prop = request.GetType().GetProperty(name);
            if (prop == null)
            {
                throw new GdErrorException($"Property {name} is not existed in Case object",
                    new LogObject("CaseBase_GetProperty", new Dictionary<string, object> {{"PropertyName", name}}));
            }
            return prop;
        }

        public object ValidateField<T>(T request, FieldInfo fieldInfo)
        {
            try
            {
                var property = GetProperty(fieldInfo.Name, request);
                var pValue = property.GetValue(request, null);
                if (fieldInfo.Required)
                {
                    var pStringValue = pValue as string;
                    if (pValue == null || string.IsNullOrWhiteSpace(pStringValue))
                    {
                        throw new BadRequestException($"Required field {fieldInfo.Name} is empty",
                            new LogObject("CaseBase_ValidateField",
                                new Dictionary<string, object> {{"FieldName", fieldInfo.Name}}));
                    }
                }
                //checking for picklist
                if (fieldInfo.PickList != null && pValue != null && !fieldInfo.PickList.Contains(pValue.ToString()))
                {
                    throw new BadRequestException($"Field {fieldInfo.Name} has invalid value",
                        new LogObject("CaseBase_ValidateField", new Dictionary<string, object> {{"FieldName", fieldInfo.Name}}));
                }
                return pValue;
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException($"An error has occurred while trying to validate field {fieldInfo.Name}",
                    new LogObject("CaseBase_ValidateField", new Dictionary<string, object> {{"FiedName", fieldInfo.Name}}), ex);
            }
        }

        public void ValidateNotAllowedFields<T>(IEnumerable<PropertyInfo> notUseFields, T request) where T : class
        {
            foreach (var field in notUseFields)
            {
                var obj = field.GetValue(request, null);
                if (obj != null)
                {
                    throw new BadRequestException($"Field {field.Name} is not allowed",
                        new LogObject("CaseBase_ValidateNotAllowedFields",
                            new Dictionary<string, object> {{"FieldName", field.Name}}));
                }
            }
        }

        private async Task<CaseResponse> CreateResponse(ProxySuccessResponse response, ICaseClientProxy client)
        {
            var caseResponse = await QueryObjectAsync<Case>(new Dictionary<string, object> { { "Id", response.Id } }, "CaseNumber");
            var caseNumber = string.Empty;
            if (caseResponse?.Records.Count > 0)
            {
                caseNumber = caseResponse.Records[0].CaseNumber;
            }
            return CreateResponse(response, caseNumber, client);
        }
        private CaseResponse CreateResponse(ProxySuccessResponse response, string caseNumber, ICaseClientProxy client)
        {
            var caseResponse = new CaseResponse
            {
                CaseNumber = string.IsNullOrWhiteSpace(caseNumber) ? null : caseNumber,
                Success = response.Success,
                Errors = response.Success ? null : response.Errors,
                Id = response.Id
            };
            return caseResponse;
        }


        public async Task<CaseResponse> CreateAsync(SalesforceEventTypeEnum eventType, string sObject, object record, IDictionary<string, object> logData)
        {
            return await CreateAsync(eventType, sObject, record, null, logData);
        }

        private async Task<CaseResponse> CreateAsync(SalesforceEventTypeEnum eventType, string sObject, object record, ICaseClientProxy client = null, IDictionary<string, object> logData = null)
        {
            ProxySuccessResponse response = null;
            CaseResponse caseResponse = null;
            var isError = false;
            var metric = new MetricWatcher(eventType.ToString(),
                new MetricWatcherOption
                {
                    ManualStartStop = true,
                    LoggingOption = LogOptionEnum.FullLog,
                    LogMessage = logData
                });
            try
            {
                metric.Start();
                if (client == null)
                {
                    client = await CaseService.GetUserNamePasswordForceClientAsync(ClientEnum.Default);
                }
                response = await client.CreateAsync(sObject, record);
            }
            catch (Exception ex)
            {
                isError = true;
                if (ex is ForceException)
                {
                    throw new ExternalErrorException(ex.GetExceptionMessages(), new LogObject(eventType.ToString(), logData), ex);
                }
                throw;
            }
            finally
            {
                if (response != null)
                {
                    caseResponse= await HandleCaseResponse(client, response, isError, metric);
                }
                metric.Stop(isError);
            }
            return caseResponse;
        }

        public async Task PreProcessing<TRequest>(TRequest request, dynamic tasks, CaseActionEnum action)
        {
            if (request == null)
            {
                throw new BadRequestException("Case request object is null", new LogObject("CaseBase_PreProcessing", null));
            }
            foreach (var task in tasks)
            {
                if (task.IsAsync)
                {
                    await task.ExecuteAsync(request, action);
                }
                else
                {
                    task.Execute(request, action);
                }
            }
        }

        public async Task<CaseResponse> UpdateAsync(SalesforceEventTypeEnum eventType, string sObject, string caseId, object record, IDictionary<string, object> logData)
        {
            return await UpdateAsync(eventType, sObject, caseId, record, null, logData);
        }
        private async Task<CaseResponse> UpdateAsync(SalesforceEventTypeEnum eventType, string sObject, string caseId, object record, ICaseClientProxy client = null, IDictionary<string, object> logData = null)
        {
            ProxySuccessResponse response = null;
            CaseResponse caseResponse = null;
            var isError = false;
            var metric = new MetricWatcher(eventType.ToString(),
                new MetricWatcherOption
                {
                    ManualStartStop = true,
                    LoggingOption = LogOptionEnum.FullLog,
                    LogMessage = logData
                });
            try
            {
                metric.Start();
                if (client == null)
                {
                    client = await CaseService.GetUserNamePasswordForceClientAsync(ClientEnum.Header);
                }
                response = await client.UpdateAsync(sObject, caseId,record);
            }
            catch (Exception ex)
            {
                isError = true;
                if (ex is ForceException)
                {
                    throw new ExternalErrorException(ex.GetExceptionMessages(), new LogObject(eventType.ToString(), logData), ex);
                }
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Id = caseId;
                    caseResponse = await HandleCaseResponse(client, response, isError, metric);
                }
                metric.Stop(isError);
            }
            return caseResponse;
        }

        private async Task<CaseResponse> HandleCaseResponse(ICaseClientProxy client, ProxySuccessResponse response, bool isError, MetricWatcher metric)
        {
            var caseResponse = await CreateResponse(response, client);
            if (!isError)
            {
                metric.Options.LogMessage = metric.Options.LogMessage.Merge(new Dictionary<string, object>
                {
                    {"Id", caseResponse.Id},
                    {"CaseNumber", string.IsNullOrWhiteSpace(caseResponse.CaseNumber) ? null : caseResponse.CaseNumber}
                });
            }
            return caseResponse;
        }

        #region Get / Create Customer Object

        //protected virtual async Task<string> GetCustomerId(string accountKey)
        //{
        //    ProxySuccessResponse response = null;
        //    var isError = false;
        //    var metric = new MetricWatcher(SalesforceEventTypeEnum.CreateCustomer.ToString(),
        //        new MetricWatcherOption
        //        {
        //            ManualStartStop = true,
        //            LoggingOption = LogOptionEnum.FullLog,
        //            LogMessage = new Dictionary<string, object>
        //            {
        //                {"AccountKey", accountKey},
        //                {"EventType", SalesforceEventTypeEnum.CreateCustomer}
        //            }
        //        });
        //    try
        //    {
        //        metric.Start();
                
        //        if (string.IsNullOrWhiteSpace(accountKey))
        //        {
        //            return "";
        //        }
        //        var customer =
        //            await QueryObjectAsync<Customer__c>(new Dictionary<string, object> {{ "AccountKey__c", accountKey}}, "Id");

        //        if (customer != null && customer.Records.Count > 0)
        //        {
        //            return customer.Records[0].Id;
        //        }
        //        var customerNew = new Customer__c()
        //        {
        //            Name = accountKey,
        //            AccountKey__c = accountKey
        //        };
        //        var client = await CaseService.GetUserNamePasswordForceClientAsync(ClientEnum.Default);
        //        response = await client.CreateAsync("Customer__c", customerNew);
        //        if (response != null && response.Success)
        //        {
        //            return response.Id;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        isError = true;
        //        if (ex is ForceException)
        //        {
        //            throw new ExternalErrorException(ex.GetExceptionMessages(),
        //                new LogObject(SalesforceEventTypeEnum.CreateCustomer.ToString(),
        //                    new Dictionary<string, object>
        //                    {
        //                        {"AccountKey", accountKey},
        //                        {"EventType", SalesforceEventTypeEnum.CreateCustomer}
        //                    }));
        //        }
        //    }
        //    finally
        //    {
        //        if (response != null
        //            && !isError)
        //        {
        //            metric.Options.LogMessage = metric.Options.LogMessage.Merge(new Dictionary<string, object>
        //            {
        //                {"Id", response.Id},
        //                {"EventType", SalesforceEventTypeEnum.CreateCustomer},
        //                {"Error", response.Errors}
        //            });
        //        }
        //        metric.Stop(isError);
        //    }
        //    return "";
        //}

        #endregion

        #region Get / Create Customer Object

        public async Task<Account> GetPersonAccount(PersonAccountInput input, ICaseClientProxy client = null)
        {
            ProxySuccessResponse response = null;
            var isError = false;
            var logData = new Dictionary<string, object>
            {
                {"AccountIdentifier", input.AccountIdentifier},
                {"EventType", SalesforceEventTypeEnum.CreatePersonAccount}
            };
            var metric = new MetricWatcher(SalesforceEventTypeEnum.CreatePersonAccount.ToString(),
                new MetricWatcherOption
                {
                    ManualStartStop = true,
                    LoggingOption = LogOptionEnum.FullLog,
                    LogMessage = logData
                });
            try
            {
                metric.Start();
                if (client == null)
                {
                    client = await CaseService.GetUserNamePasswordForceClientAsync(ClientEnum.Default);
                }
                if (string.IsNullOrWhiteSpace(input.AccountIdentifier))
                {
                    return null;
                }
                var account = await QueryObjectAsync<Account>(
                        new Dictionary<string, object> {{"Account_Identifier__c", input.AccountIdentifier}}, "Id",
                        "PersonContactId");

                if (account != null && account.Records.Count > 0)
                {
                    return account.Records[0];
                }
                if (string.IsNullOrEmpty(input.LastName))
                {
                    return null;
                }
                var waveRecordTypeId = await GetRecordTypeId("WaveAccount");
                var personAccount = new Account
                {
                    LastName = input.LastName,
                    FirstName = input.FirstName,
                    RecordTypeId = waveRecordTypeId,
                    Account_Identifier__c = input.AccountIdentifier,
                    Product__c = input.Product,
                    Brand__c = input.Brand
                    
                };
                response = await client.CreateAsync("Account", personAccount);
                if (response != null && response.Success)
                {
                    var newAccount = await QueryObjectAsync<Account>(new Dictionary<string, object> {{"ID", response.Id}},
                                "PersonContactId");
                    var contactId = string.Empty;
                    if (newAccount != null && newAccount.Records.Count > 0)
                    {
                        contactId = newAccount.Records[0].PersonContactId;
                    }
                    return new Account {Id = response.Id, PersonContactId = contactId};
                }
            }
            catch (Exception ex)
            {
                isError = true;
                var log = new LogObject(SalesforceEventTypeEnum.CreatePersonAccount.ToString(), logData);
                if (ex is ForceException)
                {
                    throw new ExternalErrorException(ex.GetExceptionMessages(), log, ex);
                }
                throw new GdErrorException("Unable to get Person Account data", log, ErrorCodeEnum.ExternalError, ex);
            }
            finally
            {
                if (response != null && isError)
                {
                    metric.Options.LogMessage = metric.Options.LogMessage.Merge(new Dictionary<string, object>
                    {
                        {"Id", response.Id},
                        {"EventType", SalesforceEventTypeEnum.CreatePersonAccount},
                        {"Error", response.Errors}
                    });
                }
                metric.Stop(isError);
            }
            return null;
        }

        #endregion
    }
}
