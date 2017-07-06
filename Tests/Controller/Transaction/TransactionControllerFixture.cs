using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Account.Logic;
using CareGateway.Account.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using CareGateway.Transaction.Controller;
using CareGateway.Transaction.Logic;
using CareGateway.Transaction.Model;
using Gdot.Care.Common.Exceptions;
using NSubstitute;

namespace Tests.Controller.Transaction
{
    public class TransactionControllerFixture : BaseFixture<TransactionController>
    {
        public TransactionControllerFixture()
        {
            var cRMCoreAccountService = Substitute.For<ICRMCoreService>();
            #region  Get All Transaction History
            Builder.RegisterType<GetAllTransactionHistoryManager>().As<ITransaction<AllTransactionHistoryResponse,AllTransactionHistoryRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            Builder.RegisterType<GetCustomerDetailManager>().As<IAccount<CustomerDetailResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            GetCustomerDetail(cRMCoreAccountService);
            GetAllTransactionHistory(cRMCoreAccountService);
            #endregion

            #region Reverse Pending Authorization Transactions
            Builder.RegisterType<ReverseAuthorizationManager>().As<ITransaction<AuthorizationReversalRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            ReversePendingAuthorizationTransaction(cRMCoreAccountService);
            #endregion  
            Builder.RegisterInstance(cRMCoreAccountService);
        }

        private static void ReversePendingAuthorizationTransaction(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.ReverseAuthorization(Arg.Is<ReverseAuthorizationRequest>(p => p.AuthorizedTransactionKey == 1))
                .Returns(Task.Run(()=> { }));
            cRMCoreAccountService.When(
                    m =>
                        m.ReverseAuthorization(Arg.Is<ReverseAuthorizationRequest>(p => p.AuthorizedTransactionKey == 2)))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing ReverseAuthorization AuthorizedTransactionKey == 2");
                    });
            cRMCoreAccountService.When(
                    m =>
                        m.ReverseAuthorization(Arg.Is<ReverseAuthorizationRequest>(p => p.AuthorizedTransactionKey == 3)))
                .Do(
                    x =>
                    {
                        throw new ArgumentException();
                    });
        }


        private static void GetCustomerDetail(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.GetCustomerDetail(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1"))
                .Returns(new CustomerDetail()
                {

                    Address = new Address()
                    {
                        Address1 = "Address1",
                        Address2 = "Address2",
                        City = "City 1",
                        Country = "Country1",
                        ZipCode = "ZipCode 1",
                        State = "State 1",
                        County = "County 1"
                    },
                    AccountExternalID = "111111",
                    DOB = "2017/05/10",
                    FirstName = "Roman",
                    MiddleName = "MiddleName1",
                    LastName = "Zhang",
                    Last4SSN = "1231",
                    SSNToken = "123456",
                    CreateDate = DateTime.Now.AddMonths(-1).Date

                });
            
            cRMCoreAccountService.GetCustomerDetail(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3"))
                .Returns(new CustomerDetail()
                {

                    Address = new Address()
                    {
                        City = "City 3",
                        Country = "Country3",
                        ZipCode = "ZipCode 3",
                        State = "State 3",
                        County = "County 3"
                    },
                    AccountExternalID = "111113",
                    DOB = "2017/05/10",
                    FirstName = "Roman",
                    LastName = "Zhang",
                    Last4SSN = "1233",
                    SSNToken = "123456"

                });
            cRMCoreAccountService.GetCustomerDetail(
                    Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA87"))
                .Returns((CustomerDetail)null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerDetail(Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA89")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetCustomerDetail 04C9E5B5716A43C2B55DD4B351C0AA89");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerDetail(Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA88")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetCustomerDetail 04C9E5B5716A43C2B55DD4B351C0AA88");
                    });
        }
        private static void GetAllTransactionHistory(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.GetAllTransactionHistory(
                    Arg.Is<GetAllTransactionHistoryReqeust>(
                        p => p.AccountIdentifier == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1"))
                .Returns(new List<CareGateway.External.Model.Data.Transaction>()
                {
                    new CareGateway.External.Model.Data.Transaction()
                    {
                        Summary = new TransactionSummary()
                        {
                            TransactionIdentifier = "9177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1",
                            AuthorizedTransactionKey = 1,
                            AvailableBalance = 10,
                            IsCredit = true,
                            IsReversible = true,
                            TransactionAmount = 10,
                            TransactionDate = "2017/01/01",
                            TransactionDescription = "TransactionDescription1",
                            TransactionStatus = "TransactionStatus1",
                            TransType = "TransType",

                        },
                        Detail = new TransactionDetail()
                        {
                            AccountGUID = "3F1E05AD-A52D-4A43-B3AA-C5B5D0B6D149",
                            AccountProxy = "11111111",
                            AchOutCardholderCompleteName = "AchOutName",
                            AchOutTargetAccountNumber = "AchOutAN",
                            AchOutOriginalRequestID = "AchOutOriginalRequestID",
                            AchOutTargetAccountRoutingNumber = "AchOutTargetAccountRoutingNumber",
                            AnotherSourceAccountAmount = 10,
                            AnotherSourceAccountType = "AnotherSourceAccountType",
                            AnotherSourceAmountFee = 10,
                            ApprovalCode = "ApprovalCode",
                            AuthorizationAmount = 10,
                            AuthorizationDate = "217/01/01",
                            AuthorizationReleaseDate = "217/01/01",
                            ConversionRate = 1,
                            DeclineCode = "DeclineCode1",
                            DeclineReason = "DeclineReason1",
                            MCCCode = "MCCCode1",
                            MCCCategory = "MCCCategory1",
                            MerchantLocation = "MerchantLocation",
                            P2PGrandTotal = 10,
                            P2PRecipientName = "P2PRecipientName1",
                            P2PSenderName="P2PSenderName1",
                            P2PType = "P2PType1",
                            ReceiptStatus = "ReceiptStatus1",
                            TopUpCardType = "TopUpCardType1",
                            TopUpCardFee = 10,
                            TransactionCodeDescription = "TransactionCodeDescription1",
                            WalletID = "WalletID1",
                            ARN = "ARN1"
                        }

                    },
                    new CareGateway.External.Model.Data.Transaction()
                    {
                        Summary = new TransactionSummary()
                        {
                            TransactionIdentifier = "9177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2",
                            AuthorizedTransactionKey = 2,
                            AvailableBalance = 10,
                            IsCredit = false,
                            IsReversible = false,
                            TransactionAmount = 12,
                            TransactionDate = "2017/01/02",
                            TransactionStatus = "TransactionStatus2",
                            TransactionDescription = "TransactionDescription2",
                            TransType = "TransType2",

                        },
                        Detail = new TransactionDetail()
                        {
                            AccountGUID = "3F1E05AD-A52D-4A43-B3AA-C5B5D0B6D149",
                            AccountProxy = "11111112",
                            AchOutCardholderCompleteName = "AchOutName2",
                            AchOutTargetAccountNumber = "AchOutAN2",
                            AchOutOriginalRequestID = "AchOutOriginalRequestID2",
                            AchOutTargetAccountRoutingNumber = "AchOutTargetAccountRoutingNumber2",
                            AnotherSourceAccountAmount = 12,
                            AnotherSourceAccountType = "AnotherSourceAccountType2",
                            AnotherSourceAmountFee = 12,
                            ApprovalCode = "ApprovalCode2",
                            AuthorizationAmount = 12,
                            AuthorizationDate = "217/01/02",
                            AuthorizationReleaseDate = "217/01/02",
                            ConversionRate = 2,
                            DeclineCode = "DeclineCode2",
                            DeclineReason = "DeclineReason2",
                              MCCCode = "MCCCode2",
                            MCCCategory = "MCCCategory2",
                            MerchantLocation = "MerchantLocation2",
                            P2PGrandTotal = 12,
                            P2PRecipientName = "P2PRecipientName2",
                            P2PType = "P2PType2",
                            ReceiptStatus = "ReceiptStatus2",
                            TopUpCardType = "TopUpCardType2",
                            TopUpCardFee = 10,
                            TransactionCodeDescription = "TransactionCodeDescription2",
                            WalletID = "WalletID2",
                            ARN = "ARN1"
                        }


                    }
                });

            cRMCoreAccountService.GetAllTransactionHistory(
                    Arg.Is<GetAllTransactionHistoryReqeust>(
                        p => p.AccountIdentifier == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3"))
                .Returns(new List<CareGateway.External.Model.Data.Transaction>()
                {
                    new CareGateway.External.Model.Data.Transaction()
                    {

                        Summary = new TransactionSummary()
                        {
                            TransactionIdentifier = "9177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1",
                            AuthorizedTransactionKey = 2,
                            AvailableBalance = 10,
                            IsCredit = true,
                            IsReversible = true,
                            TransactionAmount = 10,
                            TransactionDate = "2017/01/01",
                            TransactionStatus = "TransactionStatus1",
                            TransType = "TransType",

                        },
                        Detail = new TransactionDetail()
                        {
                            AccountGUID = "3F1E05AD-A52D-4A43-B3AA-C5B5D0B6D149",
                            AccountProxy = "11111111",
                            AchOutCardholderCompleteName = "AchOutName",
                            AnotherSourceAccountType = "AnotherSourceAccountType",
                            AnotherSourceAmountFee = 10,
                            ApprovalCode = "ApprovalCode",
                            AuthorizationAmount = 10,
                            AuthorizationDate = "217/01/01",
                            AuthorizationReleaseDate = "217/01/01",
                            MCCCode = "MCCCode1",
                            MCCCategory = "MCCCategory1",
                            MerchantLocation = "MerchantLocation",
                            P2PGrandTotal = 10,
                            P2PRecipientName = "P2PRecipientName1",
                            TransactionCodeDescription = "TransactionCodeDescription1",
                            WalletID = "WalletID1",
                            ARN = "ARN1"
                        }

                    }
                });


            cRMCoreAccountService.GetAllTransactionHistory(
                    Arg.Is<GetAllTransactionHistoryReqeust>(
                        p => p.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA87"))
                .Returns((List<CareGateway.External.Model.Data.Transaction>) null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetAllTransactionHistory(
                            Arg.Is<GetAllTransactionHistoryReqeust>(
                                p => p.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA89")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetAccountSummary 04C9E5B5716A43C2B55DD4B351C0AA89");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetAllTransactionHistory(
                            Arg.Is<GetAllTransactionHistoryReqeust>(
                                p => p.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA88")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetAccountSummary 04C9E5B5716A43C2B55DD4B351C0AA88");
                    });
        }
    }
}
