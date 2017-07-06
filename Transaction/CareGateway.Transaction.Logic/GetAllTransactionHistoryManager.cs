using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using CareGateway.Transaction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.Transaction.Logic
{
    public class GetAllTransactionHistoryManager:ITransaction<AllTransactionHistoryResponse,AllTransactionHistoryRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }

        protected int DefaultCycleDay = 28;
        public async Task<AllTransactionHistoryResponse> Execute(AllTransactionHistoryRequest request)
        {
            try
            {
                var customerDetail = await CRMCoreService.GetCustomerDetail(request.AccountIdentifier);

                if (customerDetail == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("AllTransactionHistoryManager",
                            new Dictionary<string, object> { { "AccountIdentifier", request.AccountIdentifier } }));
                }

                var response = new AllTransactionHistoryResponse()
                {
                    DefaultCycleDay = DefaultCycleDay
                };
                if (!request.CycleDate.HasValue)
                {
                    var currDate = DateTime.Now;
                    if (currDate.Day > DefaultCycleDay)
                    {
                        //getting next (future cycle date)
                        currDate = currDate.AddMonths(1);
                    }
                    request.CycleDate = new DateTime(currDate.Year, currDate.Month, DefaultCycleDay);
                }
                var transactionHistory = await CRMCoreService.GetAllTransactionHistory(new GetAllTransactionHistoryReqeust()
                {
                    AccountIdentifier = request.AccountIdentifier,
                    CycleDate = request.CycleDate.Value.ToString("yyyy-MM-dd")
                });
                
                MappingResponse(response, transactionHistory);
                response.ActivationDate = customerDetail.CreateDate ?? DateTime.Now.AddYears(-2);
                response.EndDate=DateTime.Now;
                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing GetAllTransactionHistory",
                    new LogObject("GetAllTransactionHistoryManager",
                        new Dictionary<string, object>
                            {
                                { "AccountIdentifier", request.AccountIdentifier },
                                { "CycleDate", request.CycleDate.ToString() }
                            }), ex);
            }
        }

        private static void MappingResponse(AllTransactionHistoryResponse response , List<External.Model.Data.Transaction> transactionHistory)
        {
            if (transactionHistory == null || transactionHistory.Count == 0)
                return;
            if(response.Transactions==null)
                response.Transactions=new List<Model.Data.Transaction>();
            response.Transactions.AddRange(transactionHistory.Select(trans => new Model.Data.Transaction()
            {
                TransactionIdentifier = trans.Summary.TransactionIdentifier,
                TransactionDate = trans.Summary.TransactionDate,
                TransactionDescription = trans.Summary.TransactionDescription,
                TransactionStatus = trans.Summary.TransactionStatus,
                IsCredit = trans.Summary.IsCredit,
                TransType = trans.Summary.TransType,
                TransactionAmount = trans.Summary.TransactionAmount,
                AvailableBalance = trans.Summary.AvailableBalance,
                IsReversible = trans.Summary.IsReversible,
                AuthorizedTransactionKey = trans.Summary.AuthorizedTransactionKey,
                TransactionCodeDescription = trans.Detail.TransactionCodeDescription,
                MerchantLocation = trans.Detail.MerchantLocation,
                MCCCode = trans.Detail.MCCCode,
                MCCCategory = trans.Detail.MCCCategory,
                DeclineCode = trans.Detail.DeclineCode,
                DeclineReason = trans.Detail.DeclineReason,
                ConversionRate = trans.Detail.ConversionRate,
                AuthorizationAmount = trans.Detail.AuthorizationAmount,
                AuthorizationDate = trans.Detail.AuthorizationDate,
                AuthorizationReleaseDate = trans.Detail.AuthorizationReleaseDate,
                ApprovalCode = trans.Detail.ApprovalCode,
                WalletID = trans.Detail.WalletID,
                AccountProxy = trans.Detail.AccountProxy,
                AccountGUID = trans.Detail.AccountGUID,
                P2PType = trans.Detail.P2PType,
                P2PSenderName = trans.Detail.P2PSenderName,
                P2PRecipientName = trans.Detail.P2PRecipientName,
                ReceiptStatus = trans.Detail.ReceiptStatus,
                AnotherSourceAccountType = trans.Detail.AnotherSourceAccountType,
                AnotherSourceAccountAmount = trans.Detail.AnotherSourceAccountAmount,
                AnotherSourceAccountFee = trans.Detail.AnotherSourceAmountFee,
                P2PGrandTotal = trans.Detail.P2PGrandTotal,
                AchOutOriginalRequestID = trans.Detail.AchOutOriginalRequestID,
                AchOutCardholderCompleteName = trans.Detail.AchOutCardholderCompleteName,
                AchOutTargetAccountRoutingNumber = trans.Detail.AchOutTargetAccountRoutingNumber,
                AchOutTargetAccountNumber = trans.Detail.AchOutTargetAccountNumber,
                TopUpCardType = trans.Detail.TopUpCardType,
                TopUpCardFee = trans.Detail.TopUpCardFee,
                ARN = trans.Detail.ARN
            }));
        }
    }
}
