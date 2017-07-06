using Autofac;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;
using CareGateway.TakeAction.Controller;
using CareGateway.TakeAction.Logic;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Enum;

namespace Tests.Controller.TakeActionControllerTest
{
    public class TakeActionControllerFixture : BaseFixture<TakeActionController>
    {
        public TakeActionControllerFixture()
        {
            var cRMCoreAccountService = Substitute.For<ICRMCoreService>();
            Builder.RegisterType<GetAccountStatusReasonManager>().As<ITakeAction<GetAccountStatusReasonResponse, GetAccountStatusReasonRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<GetCloseAccountOptionsManager>().As<ITakeAction<GetCloseAccountOptionsResponse, GetCloseAccountOptionsRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<UpdateAccountStatusReasonManager>().As<ITakeAction<UpdateAccountStatusReasonRequest>>()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            Builder.RegisterType<CloseAccountManager>().As<ITakeAction<CareGateway.TakeAction.Model.CloseAccountResponse, CareGateway.TakeAction.Model.CloseAccountRequest>>()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<SendEmailTriggerManager>().As<ITakeAction<SendEmailTriggerReqeust>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<GetAllTransTypeManager>().As<ITakeAction<CareGateway.TakeAction.Model.GetAllTransTypeResponse, CareGateway.TakeAction.Model.GetAllTransTypeRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            GetAccountStatusReason(cRMCoreAccountService);
            GetCloseAccountOptions(cRMCoreAccountService);
            UpdateAccountStatusReason(cRMCoreAccountService);
            CloseAccount(cRMCoreAccountService);
            GetTransType(cRMCoreAccountService);
            Builder.RegisterInstance(cRMCoreAccountService);
        }

        private void SendEmailTrigger(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.SendEmail(Arg.Is<SendEmailRequest>(p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(Task.Run(() => { }));
            cRMCoreAccountService.When(
                    m =>
                        m.SendEmail(Arg.Is<SendEmailRequest>(p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687D")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing UpdAccountStatusReason");
                    });
            cRMCoreAccountService.When(
                    m =>
                        m.SendEmail(Arg.Is<SendEmailRequest>(p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687E")))
                .Do(
                    x =>
                    {
                        throw new ArgumentException();
                    });
        }

        private void UpdateAccountStatusReason(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.UpdAccountStatusReason(Arg.Is<UpdAccountStatusReasonRequest>(p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(Task.Run(() => { }));
            cRMCoreAccountService.When(
                    m =>
                        m.UpdAccountStatusReason(Arg.Is<UpdAccountStatusReasonRequest>(p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687D")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing UpdAccountStatusReason");
                    });
            cRMCoreAccountService.When(
                    m =>
                        m.UpdAccountStatusReason(Arg.Is<UpdAccountStatusReasonRequest>(p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687E")))
                .Do(
                    x =>
                    {
                        throw new ArgumentException();
                    });
        }
        private void GetAccountStatusReason(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService
                .GetStatusTransition(Arg.Is<GetStatusTransitionRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(new GetStatusTransitionResponse
                {
                    CurrentStatus = "Active",
                    CurrentStatusReason = null,
                    TargetStatuses = new List<string> { "Restricted", "Locked" },
                    ReasonKey = 54,
                    StatusReason = "Agent Manual Review",
                });

            cRMCoreAccountService.When(m => m.GetStatusTransition(Arg.Is<GetStatusTransitionRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687D")))
                .Do(
                x =>
                {
                    throw new ArgumentException();
                });

            cRMCoreAccountService.When(m => m.GetStatusTransition(Arg.Is<GetStatusTransitionRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687E")))
                .Do(
                x =>
                {
                    throw new GdErrorException(
                           "Error while executing GetAccountStatusReason");
                });

            cRMCoreAccountService
               .GetStatusTransition(Arg.Is<GetStatusTransitionRequest>
               (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687F"))
               .Returns((GetStatusTransitionResponse)null);

        }

        private void GetCloseAccountOptions(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService
                .GetCloseAccountOptions(Arg.Is<GetClsAccountOptsRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(new GetClsAccountOptsResponse
                {
                     CloseAccountOptions = new List<string> { "Spend Down", "Refund"}
                });

            cRMCoreAccountService.When(m => m.GetCloseAccountOptions(Arg.Is<GetClsAccountOptsRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687D")))
                .Do(
                x =>
                {
                    throw new ArgumentException();
                });

            cRMCoreAccountService.When(m => m.GetCloseAccountOptions(Arg.Is<GetClsAccountOptsRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687E")))
                .Do(
                x =>
                {
                    throw new GdErrorException(
                           "Error while executing GetAccountStatusReason");
                });

            cRMCoreAccountService
               .GetCloseAccountOptions(Arg.Is<GetClsAccountOptsRequest>
               (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687F"))
               .Returns((GetClsAccountOptsResponse)null);

        }

        private void CloseAccount(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService
                .CloseAccount(Arg.Is<CareGateway.External.Model.Request.CloseAccountRequest>
                    (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(new CareGateway.External.Model.Response.CloseAccountResponse()
                {
                    ResponseHeader = new ResponseHeader()
                    {
                        StatusCode = HttpStatusCode.OK.ToString()
                    },
                    Account = new Account()
                    {
                        AccountId = new Guid("18204E5C-C243-4096-8BC2-3A62E49B687C"),
                        AccountKey = 1000011,
                        AccountStatusReasonKeys = new int[] {1, 2},

                        Balance = new Balance()
                        {
                            Actual = 100001,
                            Available = 1000001,
                        },
                        Bin = "Bin1",
                        CreateDate = "2017/01/01",

                        Cure = "Cure1",
                        CurrencyCode = "CurrencyCode1",
                        DBStage = Stage.Identified,
                        EventCounter = 10001,
                        Identifier = "Identifier1",
                        Pod = "Pod1",
                        ReasonCodes = new String[] {"01", "001"},
                        Stage = Stage.Identified,
                        State = State.Closed,
                        Status = new AccountStatus()
                        {
                            Kyc = Status.Pending,
                            Ofac = Status.Success
                        }
                    }
                });
            cRMCoreAccountService
                .CloseAccount(Arg.Is<CareGateway.External.Model.Request.CloseAccountRequest>
                    (p => p.AccountIdentifier == "28204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(new CareGateway.External.Model.Response.CloseAccountResponse()
                {
                    ResponseHeader = new ResponseHeader()
                    {
                        StatusCode = HttpStatusCode.OK.ToString()
                    },
                    Account = null
                });

            cRMCoreAccountService.When(
                    m => m.CloseAccount(Arg.Is<CareGateway.External.Model.Request.CloseAccountRequest>
                        (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687D")))
                .Do(
                    x =>
                    {
                        throw new Exception();
                    });

            cRMCoreAccountService.When(
                    m => m.CloseAccount(Arg.Is<CareGateway.External.Model.Request.CloseAccountRequest>
                        (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687E")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing CloseAccount");
                    });

            cRMCoreAccountService
                .CloseAccount(Arg.Is<CareGateway.External.Model.Request.CloseAccountRequest>
                    (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687F"))
                .Returns((CareGateway.External.Model.Response.CloseAccountResponse) null);

        }

        /// <summary>
        /// 5 status:
        /// Success
        /// ExternalErrorException
        /// Exception
        /// GdErrorException
        /// NotFoundException
        /// </summary>
        /// <param name="cRMCoreAccountService"></param>
        private void GetTransType(ICRMCoreService cRMCoreAccountService)
        {
            // Success
            cRMCoreAccountService
                .GetAllTransType(Arg.Is<CareGateway.External.Model.Request.GetAllTransTypeRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687C"))
                .Returns(new CareGateway.External.Model.Response.GetAllTransTypeResponse
                {
                    listTransType = new List<CareGateway.External.Model.Data.TransType>() {
                        new CareGateway.External.Model.Data.TransType() { TransCodeDescription = "Balance Transfer FUNDING", GDTransCode = 1, TransCodeCreditDebit = "C", GDTransactionClass = "108-004" },
                        new CareGateway.External.Model.Data.TransType() { TransCodeDescription = "Customer Courtesy", GDTransCode = 2, TransCodeCreditDebit = "C", GDTransactionClass = "108-005" },
                        new CareGateway.External.Model.Data.TransType() { TransCodeDescription = "Balance Inquiry Fee", GDTransCode = 4, TransCodeCreditDebit = "D", GDTransactionClass = "108-006" },
                        new CareGateway.External.Model.Data.TransType() { TransCodeDescription = "Lost/Stolen Repl Card Fee", GDTransCode = 5, TransCodeCreditDebit = "D", GDTransactionClass = "108-007" }
                    }
                });

            // ExternalError
            cRMCoreAccountService.When(m => m.GetAllTransType(Arg.Is<CareGateway.External.Model.Request.GetAllTransTypeRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687D")))
                .Do(
                x =>
                {
                    throw new ExternalErrorException("Error when calling GetAllTransType method from CRMCoreService");
                });

            // Exception
            cRMCoreAccountService.When(m => m.GetAllTransType(Arg.Is<CareGateway.External.Model.Request.GetAllTransTypeRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687E")))
                .Do(
                x =>
                {
                    throw new ArgumentException();
                });

            // GdErrorException
            cRMCoreAccountService.When(m => m.GetAllTransType(Arg.Is<CareGateway.External.Model.Request.GetAllTransTypeRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687F")))
                .Do(
                x =>
                {
                    throw new GdErrorException("Error while executing GetAllTransType");
                });

            // NotFound
            cRMCoreAccountService
                .GetAllTransType(Arg.Is<CareGateway.External.Model.Request.GetAllTransTypeRequest>
                (p => p.AccountIdentifier == "18204E5C-C243-4096-8BC2-3A62E49B687G"))
                .Returns((CareGateway.External.Model.Response.GetAllTransTypeResponse)null);
        }
    }
}
