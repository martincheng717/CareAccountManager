using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;

namespace CareGateway.External.Client.Interfaces
{
    public interface ICRMCoreService
    {
        Task<AccountInfo> GetAccountSummary(string accountIdentifier);
        Task<CustomerDetail> GetCustomerDetail(string accountIdentifier);
        Task<AccountDetail> GetAccountDetail(string accountIdentifier);

        Task<List<CustomerInfo>> GetCustomerInfoBySSN(string ssn);
        Task<List<CustomerInfo>> GetCustomerInfoByAccountNumber(string request);
        Task<List<CustomerInfo>> GetCustomerInfoByCustomerDetail(SearchAccountByDetailRequest request);
        Task<AvailableDates> GetMonthlyStatementDate(string request);

        Task<List<Transaction>> GetAllTransactionHistory(GetAllTransactionHistoryReqeust request);
        Task ReverseAuthorization(ReverseAuthorizationRequest request);
        Task<GetStatusTransitionResponse> GetStatusTransition(GetStatusTransitionRequest request);
        Task<GetClsAccountOptsResponse> GetCloseAccountOptions(GetClsAccountOptsRequest request);
        Task UpdAccountStatusReason(UpdAccountStatusReasonRequest request);
        Task<CloseAccountResponse> CloseAccount(CloseAccountRequest request);
        Task<GetSSNBySSNTokenResponse> GetSSNBySSNToken(GetSSNBySSNTokenRequest request);
        Task SendEmail(SendEmailRequest request);
        Task<GetAllTransTypeResponse> GetAllTransType(GetAllTransTypeRequest request);
    }
}
