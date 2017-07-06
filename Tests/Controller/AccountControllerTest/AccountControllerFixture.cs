using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Account.Controller;
using CareGateway.Account.Logic;
using CareGateway.Account.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Response;
using CareGateway.QMaster.Logic;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using NSubstitute;
using CareGateway.External.Model.Request;
using Gdot.Care.Common.Interface;

namespace Tests.Controller.AccountControllerTest
{
    public class AccountControllerFixture : BaseFixture<AccountController>
    {
        public string AgentFullName { get; set; }
        public AccountControllerFixture()
        {
            AgentFullName = "GordenJames@greendotcorp.com";
            var requestHeaderInfo = Substitute.For<IRequestHeaderInfo>();
            requestHeaderInfo.GetUserName().Returns(AgentFullName);
            requestHeaderInfo.GetClientIpAddress().Returns("10.10.10.10");
            Builder.RegisterInstance(requestHeaderInfo);
            #region  Get Account Summary  

            Builder.RegisterType<GetAccountSummaryManager>().As<IAccount<AccountSummaryResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            var cRMCoreAccountService = Substitute.For<ICRMCoreService>();

            GetAccountSummary(cRMCoreAccountService);

            #endregion

            #region Get Customer Detail

            Builder.RegisterType<GetCustomerDetailManager>().As<IAccount<CustomerDetailResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            GetCustomerDetail(cRMCoreAccountService);
            #endregion

            #region Get Account Detail

            Builder.RegisterType<GetAccountDetailManager>().As<IAccount<AccountDetailResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            GetAccountDetail(cRMCoreAccountService);

            #endregion

            #region Full SSN

            Builder.RegisterType<GetFullSSNManager>().As<IAccount<GetFullSSNResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            GetFullSSN(cRMCoreAccountService);
            #endregion

            #region Account Search

            Builder.RegisterType<AccountSearchManager>().As<IAccount<List<AccountSearchInfo>, AccountSearchRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            AccountSearchBySSN(cRMCoreAccountService);
            AccountSearchByAccountNumber(cRMCoreAccountService);
            AccountSearchByCustomerInfo(cRMCoreAccountService);
            #endregion

            #region Log View Sensitive Data
            Builder.RegisterType<LogViewSensitiveManager>().As<IAccount<LogViewSensitiveDataRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            #endregion

            #region Get Monthly Statement Date
            Builder.RegisterType<GetMonthlyStatementDateManager>().As<IAccount<MonthlyStatementDateResponse, string>>()
              .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            GetMonthlyStatementDate(cRMCoreAccountService);
            #endregion

            Builder.RegisterInstance(cRMCoreAccountService);
        }

        private static void GetFullSSN(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.GetSSNBySSNToken(Arg.Is<GetSSNBySSNTokenRequest>(p => p.SSNToken == "0PTTLVH7WJZ"))
                .Returns(new GetSSNBySSNTokenResponse { SSN = "123456789" });
            cRMCoreAccountService.GetSSNBySSNToken(Arg.Is<GetSSNBySSNTokenRequest>(p => p.SSNToken == "1PTTLVH7WJZ"))
                .Returns((GetSSNBySSNTokenResponse)null);
            cRMCoreAccountService.When(
                m => m.GetSSNBySSNToken(Arg.Is<GetSSNBySSNTokenRequest>(p => p.SSNToken == "2PTTLVH7WJZ")))
                .Do(
                x =>
                {
                    throw new GdErrorException(
                        "Error while executing GetFullSSN 2PTTLVH7WJZ");
                }
                );
            cRMCoreAccountService.When(
                m => m.GetSSNBySSNToken(Arg.Is<GetSSNBySSNTokenRequest>(p => p.SSNToken == "3PTTLVH7WJZ")))
                .Do(
                x =>
                {
                    throw new ArgumentException();
                }
                );
        }

        private static void GetAccountSummary(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.GetAccountSummary(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1"))
                .Returns(new AccountInfo()
                {
                    AvailableBalance = 10,
                    AccountStage = "Stage 01",
                    AccountState = "State 01",
                    DOB = "2017/05/10",
                    FirstName = "Roman",
                    LastName = "Zhang",
                    Last4SSN = "1231",
                    CareReason = "Reason 1",
                    SSNToken = "SSNToken1"
                });


            cRMCoreAccountService.GetAccountSummary(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2"))
                .Returns(new AccountInfo()
                {
                    AvailableBalance = -10,
                    AccountStage = "Stage 02",
                    AccountState = "State 02",
                    FirstName = "Roman",
                    LastName = "Zhang",
                    Last4SSN = "1232",
                    SSNToken = "SSNToken2"

                });

            cRMCoreAccountService.GetAccountSummary(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3"))
                .Returns(new AccountInfo()
                {

                    AvailableBalance = 10,
                    AccountStage = "Stage 03",
                    AccountState = "State 03",
                    DOB = "2017/05/10",
                    Last4SSN = "1233",
                    SSNToken = "SSNToken3"

                });
            cRMCoreAccountService.GetAccountSummary(
                    Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA87"))
                .Returns((AccountInfo) null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetAccountSummary(Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA89")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetAccountSummary 04C9E5B5716A43C2B55DD4B351C0AA89");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetAccountSummary(Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA88")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetAccountSummary 04C9E5B5716A43C2B55DD4B351C0AA88");
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
                        Address2 = "Address2" ,
                        City = "City 1",
                        Country = "Country1",
                        ZipCode = "ZipCode 1",
                        State = "State 1",
                        County= "County 1"
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

            cRMCoreAccountService.GetCustomerDetail(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2"))
                .Returns(new CustomerDetail()
                {

                    AccountExternalID = "111112",
                    DOB = "2017/05/10",
                    FirstName = "Roman",
                    MiddleName = "",
                    LastName = "Zhang",
                    Last4SSN = "1232",
                    SSNToken = "123456",
                    CreateDate = DateTime.Now.AddMonths(-2).Date

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
                .Returns((CustomerDetail) null);

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

        private static void GetAccountDetail(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.GetAccountDetail(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1"))
                .Returns(new AccountDetail()
                {
                    State = "State 1",
                    Stage = "Stage 1",
                    Association = "Association 1",
                    CardExternalID = "1111111",
                    AccountExternalID = "111111",
                    Cure = "Cure 1",
                    Processor = "Preocessor 1",
                    Reason = "Reason",
                    AccountNumber = "1111111111"
                });

            cRMCoreAccountService.GetAccountDetail(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2"))
                .Returns(new AccountDetail()
                {
                    State = "State 2",
                    Stage = "Stage 2",
                    Association = "Association 2",
                    CardExternalID = "1111112",
                    AccountExternalID = "111112",
                    Cure = "Cure 2",
                    Processor = "Preocessor 2",
                    AccountNumber = "1111111112"
                });

            cRMCoreAccountService.GetAccountDetail(Arg.Is<string>(p => p == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3"))
                .Returns(new AccountDetail()
                {
                    State = "State 3",
                    Stage = "Stage 3",
                    Association = "Association 3",
                    AccountExternalID = "111113",
                    Cure = "Cure 3",
                    Processor = "Preocessor 3",
                    Reason = "Reason",
                    AccountNumber = "1111111113"
                });
            cRMCoreAccountService.GetAccountDetail(
                    Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA87"))
                .Returns((AccountDetail) null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetAccountDetail(Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA89")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetCustomerDetail 04C9E5B5716A43C2B55DD4B351C0AA89");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetAccountDetail(Arg.Is<string>(p => p == "04C9E5B5716A43C2B55DD4B351C0AA88")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetAccountDetail 04C9E5B5716A43C2B55DD4B351C0AA88");
                    });
        }

        private static void AccountSearchBySSN(ICRMCoreService cRMCoreAccountService)
        {

            cRMCoreAccountService.GetCustomerInfoBySSN(Arg.Is<string>(p => p == "123456789"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName",
                        Last4SSN = "1234",
                        LastName = "LastName",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD1",
                        AccountState = "Closed",
                        AccountNumber = "111111111"
                    },
                    new CustomerInfo()
                    {
                        FirstName = "FirstName1",
                        Last4SSN = "1234",
                        LastName = "LastName1",
                        AccountIdentifier = "B9DFB55A-2FF9-4D79-B666-04892FBC9CD1",
                        AccountState = "Active",
                        AccountNumber = "211111111"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoBySSN(Arg.Is<string>(p => p == "123456782"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName2",
                        Last4SSN = "1232",
                        LastName = "LastName2",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD2",
                        AccountState = "Active",
                        AccountNumber = "111111112"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoBySSN(
                    Arg.Is<string>(p => p == "123456783"))
                .Returns((List<CustomerInfo>) null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerInfoBySSN(Arg.Is<string>(p => p == "123456784")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetCustomerInfoBySSN 123456784");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerInfoBySSN(Arg.Is<string>(p => p == "123456785")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetCustomerInfoBySSN 123456785");
                    });
        }
        private static void AccountSearchByAccountNumber(ICRMCoreService cRMCoreAccountService)
        {

            cRMCoreAccountService.GetCustomerInfoByAccountNumber(Arg.Is<string>(p => p == "123456789"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName",
                        Last4SSN = "1234",
                        LastName = "LastName",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD1",
                        AccountState = "Closed",
                        AccountNumber = "111111111"
                    },
                    new CustomerInfo()
                    {
                        FirstName = "FirstName1",
                        Last4SSN = "1234",
                        LastName = "LastName1",
                        AccountIdentifier = "B9DFB55A-2FF9-4D79-B666-04892FBC9CD1",
                        AccountState = "Active",
                        AccountNumber = "211111111"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoByAccountNumber(Arg.Is<string>(p => p == "123456782"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName2",
                        Last4SSN = "1232",
                        LastName = "LastName2",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD2",
                        AccountState = "Active",
                        AccountNumber = "111111112"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoByAccountNumber(
                    Arg.Is<string>(p => p == "123456783"))
                .Returns((List<CustomerInfo>)null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerInfoByAccountNumber(Arg.Is<string>(p => p == "123456784")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetCustomerInfoBySSN 123456784");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerInfoByAccountNumber(Arg.Is<string>(p => p == "123456785")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetCustomerInfoBySSN 123456785");
                    });
        }
        private static void AccountSearchByCustomerInfo(ICRMCoreService cRMCoreAccountService)
        {

            cRMCoreAccountService.GetCustomerInfoByCustomerDetail(Arg.Is<SearchAccountByDetailRequest>(p => p.FirstName == "FirstName1"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName1",
                        Last4SSN = "1234",
                        LastName = "LastName1",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD1",
                        AccountState = "Closed",
                        AccountNumber = "111111111"
                    },
                    new CustomerInfo()
                    {
                        FirstName = "FirstName1",
                        Last4SSN = "1234",
                        LastName = "LastName1",
                        AccountIdentifier = "B9DFB55A-2FF9-4D79-B666-04892FBC9CD1",
                        AccountState = "Active",
                        AccountNumber = "211111111"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoByCustomerDetail(Arg.Is<SearchAccountByDetailRequest>(p => p.LastName == "LastName2"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName2",
                        Last4SSN = "1232",
                        LastName = "LastName2",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD2",
                        AccountState = "Active",
                        AccountNumber = "111111112"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoByCustomerDetail(Arg.Is<SearchAccountByDetailRequest>(p => p.DOB== "2017-01-15"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName3",
                        Last4SSN = "1233",
                        LastName = "LastName3",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD3",
                        AccountState = "Active",
                        AccountNumber = "111111113"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoByCustomerDetail(Arg.Is<SearchAccountByDetailRequest>(p => p.ZipCode == "ZipCode4"))
                .Returns(new List<CustomerInfo>
                {
                    new CustomerInfo()
                    {
                        FirstName = "FirstName4",
                        Last4SSN = "1234",
                        LastName = "LastName4",
                        AccountIdentifier = "A9DFB55A-2FF9-4D79-B666-04892FBC9CD4",
                        AccountState = "Active",
                        AccountNumber = "111111114"
                    }
                });

            cRMCoreAccountService.GetCustomerInfoByCustomerDetail(
                    Arg.Is<SearchAccountByDetailRequest>(m => m.FirstName== "FirstName5"))
                .Returns((List<CustomerInfo>)null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerInfoByCustomerDetail(Arg.Is<SearchAccountByDetailRequest>(p => p.LastName == "LastName6")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetCustomerInfoByCustomerDetail LastName6");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetCustomerInfoByCustomerDetail(Arg.Is<SearchAccountByDetailRequest>(p => p.FirstName == "LastName7")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetCustomerInfoByCustomerDetail LastName7");
                    });
        }

        private static void GetMonthlyStatementDate(ICRMCoreService cRMCoreAccountService)
        {
            cRMCoreAccountService.GetMonthlyStatementDate(Arg.Is<string>(p => p == "A9DFB55A-2FF9-4D79-B666-04892FBC9CD1"))
                .Returns(
                    new AvailableDates()
                    {
                        StartDate = "2017-01-01",
                        EndDate = "2017-07-01"
                    
                });

            cRMCoreAccountService.GetMonthlyStatementDate(
                    Arg.Is<string>(m => m == "A9DFB55A-2FF9-4D79-B666-04892FBC9CD5"))
                .Returns((AvailableDates)null);

            cRMCoreAccountService.When(
                    m =>
                        m.GetMonthlyStatementDate(Arg.Is<string>(p => p == "A9DFB55A-2FF9-4D79-B666-04892FBC9CD6")))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetMonthlyStatementDate A9DFB55A-2FF9-4D79-B666-04892FBC9CD6");
                    });

            cRMCoreAccountService.When(
                    m =>
                        m.GetMonthlyStatementDate(Arg.Is<string>(p => p == "A9DFB55A-2FF9-4D79-B666-04892FBC9CD7")))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing GetMonthlyStatementDate A9DFB55A-2FF9-4D79-B666-04892FBC9CD7");
                    });
        }

        public static string GetAgentUserName()
        {
            return "";
        }
    }
}