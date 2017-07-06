using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Utilities;

namespace CareGateway.External.Client
{
    [ExcludeFromCodeCoverage]
    public class CRMCoreService:ICRMCoreService
    {
        private readonly IApiClient _apiClient;
        public CRMCoreService(IApiClient client)  
        {
            _apiClient = client ?? new ApiClient();
        }
        #region Account/Customer info
        public async Task<AccountInfo> GetAccountSummary(string request)
        {
            var logDic = new Dictionary<string, object> { { "AccountIdentifier", request } };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/account/getAccountSummary?accountIdentifier={request}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);
                var response =  await _apiClient.GetAsync<GetAccountSummaryResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetAccountSummaryResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response.AccountSummary;
            }
            catch (Exception ex)
            {
                var endPointDict = new Dictionary<string, object>
                {
                    {"EndPointUrl", endpointUrl}
                };
                throw new ExternalErrorException(
                    "Error when calling GetAccountSummary route from CRMCoreService",
                    new LogObject("CRMCoreService_GetAccountSummary",
                        endPointDict), ex);
            }

        }

        public async Task<CustomerDetail> GetCustomerDetail(string request)
        {

            var logDic = new Dictionary<string, object> { { "AccountIdentifier", request } };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/account/getCustomerDetail?accountIdentifier={request}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);

                var response= await _apiClient.GetAsync<GetCustomerDetailResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetCustomerDetailResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response.CustomerDetail;
            }
            catch (Exception ex)
            {
                var endPointDict = new Dictionary<string, object>
                {
                    {"EndPointUrl", endpointUrl}
                };
                throw new ExternalErrorException(
                    "Error when calling GetCustomerDetail route from CRMCoreService",
                    new LogObject("CRMCoreService_GetCustomerDetail",
                        endPointDict), ex);
            }

        }

        public async Task<AccountDetail> GetAccountDetail(string request)
        {
            var logDic = new Dictionary<string, object> { { "AccountIdentifier", request } };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/account/getAccountDetail?accountIdentifier={request}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.GetAsync<GetAccountDetailResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetAccountDetailResponse>(response, logDic,MethodBase.GetCurrentMethod().Name) ? null : response.AccountDetail;
            }
            catch (Exception ex)
            {
                var endPointDict = new Dictionary<string, object>
                {
                    {"EndPointUrl", endpointUrl}
                };
                throw new ExternalErrorException(
                    "Error when calling GetAccountDetail route from CRMCoreService",
                    new LogObject("CRMCoreService_GetAccountDetail",
                        endPointDict), ex);
            }
        }

        public async Task<List<CustomerInfo>> GetCustomerInfoBySSN(string request)
        {
            var logDic = new Dictionary<string, object> { { "Last4SSN", request.Substring(request.Length-4,4) } };

            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/account/getCustomerInfoBySSN";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.PostAsync<SearchAccountResponse, GetCustomerInfoBySSNRequest>(new Uri(endpointUrl), 
                                        new GetCustomerInfoBySSNRequest()
                                                    {Ssn = request}, 
                                        LogOptionEnum.FullLog,
                                        logDic);
                return HandleNoSuccessResponse<SearchAccountResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response.CustomerInfo;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling getCustomerInfoBySSN route from CRMcoreService",
                    new LogObject("CRMCoreService_GetCustomerInfoBySSN",
                        logDic), ex);
                throw;
            }
        }

        public async Task<List<CustomerInfo>> GetCustomerInfoByAccountNumber(string request)
        {
            var logDic = new Dictionary<string, object> { { "AccountNumber", request } };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/account/getCustomerByAccountNumber?accountNumber={request}";
            return await SearchAccount(endpointUrl, false,logDic);
        }

        public async Task<List<CustomerInfo>> GetCustomerInfoByCustomerDetail(SearchAccountByDetailRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "ZipCode", request.ZipCode },
                { "DOB", ""},
                { "FirstName", ""},
                { "LastName", ""},

            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/getCustomerInfoByDetails";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.PostAsync<SearchAccountResponse, SearchAccountByDetailRequest>(new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<SearchAccountResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response.CustomerInfo;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling getCustomerInfoByDetails route from CRMcoreService",
                    new LogObject("CRMCoreService_GetCustomerInfoByCustomerDetail",
                        logDic), ex);
                throw;
            }
        }
        private async Task<List<CustomerInfo>> SearchAccount(string endpointUrl, bool isSensitive,Dictionary<string, object> logDic, [CallerMemberName]string memberName = "")
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);

                var response = await _apiClient.GetAsync<SearchAccountResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog,
                    isSensitive,logDic);
                return HandleNoSuccessResponse<SearchAccountResponse>(response, logDic, MethodBase.GetCurrentMethod().Name)
                    ? null
                    : response.CustomerInfo;
            }
            catch (Exception ex)
            {
                if (!isSensitive)
                {
                    logDic.Add("EndPointUrl", endpointUrl);
                }
                throw new ExternalErrorException(
                    $"Error when calling {memberName} route from CRMCoreService",
                    new LogObject($"CRMCoreService_{memberName}",
                        logDic), ex);
            }
        }

        public async Task<AvailableDates> GetMonthlyStatementDate(string request)
        {
            var logDic = new Dictionary<string, object> { { "AccountIdentifier", request } };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/account/getDatesAvailable?accountIdentifier={request}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.GetAsync<GetDatesAvailableResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetDatesAvailableResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response.AvailableDates;
            }
            catch (Exception ex)
            {
                var endPointDict = new Dictionary<string, object>
                {
                    {"EndPointUrl", endpointUrl}
                };
                throw new ExternalErrorException(
                    "Error when calling GetMonthlyStatementDate route from CRMCoreService",
                    new LogObject("CRMCoreService_GetMonthlyStatementDate",
                        endPointDict), ex);
            }
        }

        #endregion

        #region Transaction

        public async Task<List<Transaction>> GetAllTransactionHistory(GetAllTransactionHistoryReqeust request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier } ,
                { "CycleDate", request.CycleDate}
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Transaction/getTransactionHistory?accountidentifier={request.AccountIdentifier}&cycledate={request.CycleDate}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);

                var response = await _apiClient.GetAsync<GetAllTransactionHistoryResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetAllTransactionHistoryResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response.Transactions;

            }
            catch (Exception ex)
            {
                var endPointDict = new Dictionary<string, object>
                {
                    {"EndPointUrl", endpointUrl}
                };
                throw new ExternalErrorException(
                    "Error when calling GetAllTransactionHistory route from CRMCoreService",
                    new LogObject("CRMCoreService_GetAllTransactionHistory",
                        endPointDict), ex);
            }
        }

        public async Task ReverseAuthorization(ReverseAuthorizationRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier } ,
                { "AuthorizedTransactionKey", request.AuthorizedTransactionKey}
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Transaction/reveralPendingTransaction";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);
                var rsp = await _apiClient.PostAsync<ResponseBase, ReverseAuthorizationRequest>(new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);
                if (rsp == null || rsp.ResponseHeader.StatusCode !=  HttpStatusCode.OK.ToIntegerString())
                {
                    logDic.Add("EndPointUrl", endpointUrl);
                    throw new ExternalErrorException(
                        "Error when calling Reverse route from CRMCoreService " + rsp.ResponseHeader.SubStatusMessage?.ToString(),
                        new LogObject("CRMCoreService_ReverseAuthorization",
                            logDic));
                }
            }
            catch (ExternalErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling Reverse route from CRMCoreService",
                    new LogObject("CRMCoreService_ReverseAuthorization",
                        logDic), ex);
            }
        }
        #endregion

        #region TakeAction      
        public async Task<GetStatusTransitionResponse> GetStatusTransition(GetStatusTransitionRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier }
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/getStatusTransition?accountIdentifier={request.AccountIdentifier}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.GetAsync<GetStatusTransitionResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetStatusTransitionResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling GetStatusTransition route from CRMCoreService",
                    new LogObject("CRMCoreService_GetStatusTransition",
                        logDic), ex);
            }
        }

        public async Task<GetSSNBySSNTokenResponse> GetSSNBySSNToken(GetSSNBySSNTokenRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "SSNToken", request.SSNToken}
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/getSSNBySSNToken?ssnToken={request.SSNToken}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                _apiClient.Client = new HttpClient(handler);               
                var response = await _apiClient.GetAsync<GetSSNBySSNTokenResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetSSNBySSNTokenResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling GetSSNBySSNToken route from CRMCoreService",
                    new LogObject("CRMCoreService_GetSSNBySSNToken",
                        logDic), ex);
            }                     
        }

        public async Task UpdAccountStatusReason(UpdAccountStatusReasonRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier},
                { "Status", request.Status},
                { "AccountStatusReason", request.AccountStatusReason}
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/updateAccountStatus";
            try
            {
                var rsp = await _apiClient.PostAsync<ResponseBase, UpdAccountStatusReasonRequest>
                (new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);
                if (rsp == null || rsp.ResponseHeader.StatusCode != HttpStatusCode.OK.ToIntegerString())
                {
                    logDic.Add("EndPointUrl", endpointUrl);
                    throw new ExternalErrorException(
                        "Error when calling Reverse route from CRMCoreService. " + rsp.ResponseHeader.SubStatusMessage?.ToString(),
                        new LogObject("CRMCoreService_UpdAccountStatusReason",
                            logDic));
                }
            }
            catch (ExternalErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling Reverse route from CRMCoreService",
                    new LogObject("CRMCoreService_UpdAccountStatusReason",
                        logDic), ex);
            }
        }

        public async Task<GetClsAccountOptsResponse> GetCloseAccountOptions(GetClsAccountOptsRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier}
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/getCloseAccountOptions?accountIdentifier={request.AccountIdentifier}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.GetAsync<GetClsAccountOptsResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetClsAccountOptsResponse>(response, logDic, MethodBase.GetCurrentMethod().Name)?null:response;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling GetCloseAccountOptions route from CRMcoreService",
                    new LogObject("CRMCoreService_GetCloseAccountOptions",
                        logDic), ex);
                throw;
            }
        }

        public async Task SendEmail(SendEmailRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifer", request.AccountIdentifier},
                { "TemplateName", request.TemplateName},
            };
            foreach (var item in request.DynamicElements)
            {
                logDic.Add(item.Key,item.Value);
            }
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/sendEmail";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                _apiClient.Client = new HttpClient(handler);
                var rsp = await _apiClient.PostAsync<ResponseBase, SendEmailRequest>(new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);
                if (rsp == null || rsp.ResponseHeader.StatusCode != HttpStatusCode.OK.ToIntegerString())
                {
                    logDic.Add("EndPointUrl", endpointUrl);
                    throw new ExternalErrorException(
                        "Error when calling SendEmail route from CRMCoreService. " + rsp.ResponseHeader.SubStatusMessage?.ToString(),
                        new LogObject("CRMCoreService_SendEmail",
                            logDic));
                }
            }
            catch (ExternalErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling SendEmail route from CRMcoreService",
                    new LogObject("CRMCoreService_SendEmail",
                        logDic), ex);
            }
        }

        public async Task<CloseAccountResponse> CloseAccount(CloseAccountRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier},
                { "Option", request.Option.ToString()},
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/closeAccount";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.PostAsync<CloseAccountResponse, CloseAccountRequest>(new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<CloseAccountResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling CloseAccount route from CRMcoreService",
                    new LogObject("CRMCoreService_CloseAccount",
                        logDic), ex);
                throw;
            }
        }



        /// <summary>
        /// Get All TransType according to AccountIdentifier
        /// </summary>
        /// <param name="request">AccountIdentifier</param>
        /// <returns>
        /// OK: 200
        /// ExternalError: 42200
        /// NotFound: 40400
        /// </returns>
        public async Task<GetAllTransTypeResponse> GetAllTransType(GetAllTransTypeRequest request)
        {
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier }
            };
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("CRMCoreService")}/Account/getTransTypeByAccountIdentity?accountIdentifier={request.AccountIdentifier}";
            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };

                _apiClient.Client = new HttpClient(handler);
                var response = await _apiClient.GetAsync<GetAllTransTypeResponse>(new Uri(endpointUrl), LogOptionEnum.FullLog, logDic);
                return HandleNoSuccessResponse<GetAllTransTypeResponse>(response, logDic, MethodBase.GetCurrentMethod().Name) ? null : response;
            }
            catch (Exception ex)
            {
                logDic.Add("EndPointUrl", endpointUrl);
                throw new ExternalErrorException(
                    "Error when calling GetAllTransType route from CRMCoreService",
                    new LogObject("CRMCoreService_GetAllTransType",
                        logDic), ex);
            }
        }

        #endregion
        private static bool HandleNoSuccessResponse<T>(T response, Dictionary<string, object> logDic, string method)
        {
            if (response == null)
            {
                throw new ExternalErrorException(
                    $"Error when calling {method} method from CRMCoreService" + " " +
                    "return null",
                    new LogObject($"CRMCoreService_{method}",
                        logDic));
            }
            var header = (response as ResponseBase)?.ResponseHeader;
            if (header == null)
            {
                throw new ExternalErrorException(
                    $"Error when calling {method} method from CRMCoreService No Response header",
                    new LogObject($"CRMCoreService_{method}",
                        logDic));
            }
            if (header.StatusCode == HttpStatusCode.NotFound.ToIntegerString())
            {
                return true;
            }
            if (header.StatusCode != HttpStatusCode.OK.ToIntegerString())
            {
                throw new ExternalErrorException(
                    $"Error when calling {method} method from CRMCoreService" + " StatusCode:" +
                    header.StatusCode + " SubStatusMessage:" + header.SubStatusMessage,
                    new LogObject($"CRMCoreService_{method}",
                        logDic));
            }
            return false;
        }       
    }
}
