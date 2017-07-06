using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Autofac;
using CareGateway.Account.Controller;
using CareGateway.Account.Model;
using CareGateway.Account.Model.Enum;
using CareGateway.External.Model.Data;
using CareGateway.QMaster.Controller;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using NUnit.Framework;
using Tests.Controller.QMasterControllerTest;
using CareGateway.External.Model.Response;
using NSubstitute;
using Gdot.Care.Common.Interface;

namespace Tests.Controller.AccountControllerTest
{
    [ExcludeFromCodeCoverage]
    public class AccountControllerTest
    {
        private AccountController _controller;
        private AccountControllerFixture _fixture;

        public AccountControllerTest()
        {
            _fixture = new AccountControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<AccountController>();
        }

        #region Test Get Account Summary
        [Test]
        public async Task TestAccountSummary_Success()
        {
          
       
            var response = await _controller.GetAccountSummary("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1");

            var negResult = response as OkNegotiatedContentResult<AccountSummaryResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(10, negResult.Content.AvailableBalance);
            Assert.AreEqual("Stage 01", negResult.Content.AccountStage);
            Assert.AreEqual("State 01", negResult.Content.AccountState);
            Assert.AreEqual("2017/05/10", negResult.Content.DOB);
            Assert.AreEqual("Roman", negResult.Content.FirstName);
            Assert.AreEqual("Zhang", negResult.Content.LastName);
            Assert.AreEqual("1231", negResult.Content.Last4SSN);
            Assert.AreEqual("Reason 1", negResult.Content.CareReason);
            Assert.AreEqual("SSNToken1", negResult.Content.SSNToken);
        }
        [Test]
        public async Task TestAccountSummaryWithOutDOB_Success()
        {
            var response = await _controller.GetAccountSummary("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2");

            var negResult = response as OkNegotiatedContentResult<AccountSummaryResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(-10, negResult.Content.AvailableBalance);
            Assert.AreEqual("Stage 02", negResult.Content.AccountStage);
            Assert.AreEqual("State 02", negResult.Content.AccountState);
            Assert.AreEqual("Roman", negResult.Content.FirstName);
            Assert.AreEqual("Zhang", negResult.Content.LastName);
            Assert.AreEqual("1232", negResult.Content.Last4SSN);
            Assert.AreEqual("SSNToken2", negResult.Content.SSNToken);
        }
        [Test]
        public async Task TestAccountSummaryWithOutName_Success()
        {
            var response = await _controller.GetAccountSummary("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3");

            var negResult = response as OkNegotiatedContentResult<AccountSummaryResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(10, negResult.Content.AvailableBalance);
            Assert.AreEqual("Stage 03", negResult.Content.AccountStage);
            Assert.AreEqual("State 03", negResult.Content.AccountState);
            Assert.AreEqual(null, negResult.Content.FirstName);
            Assert.AreEqual(null, negResult.Content.LastName);
            Assert.AreEqual("1233", negResult.Content.Last4SSN);
            Assert.AreEqual("SSNToken3", negResult.Content.SSNToken);
        }

        [Test]
        public async Task TestAccountSummary_NoFoundException()
        {
            try
            {
                var response = await _controller.GetAccountSummary("04C9E5B5716A43C2B55DD4B351C0AA87");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestAccountSummary_GdErrorException()
        {
            try
            {
                var response = await _controller.GetAccountSummary("04C9E5B5716A43C2B55DD4B351C0AA89");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestAccountSummary_Exception()
        {
            try
            {
                var response = await _controller.GetAccountSummary("04C9E5B5716A43C2B55DD4B351C0AA88");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }

        #endregion

        #region Test Get Customer Detail
        [Test]
        public async Task TestCustomerDetail_Success()
        {

            var response = await _controller.GetCustomerDetail("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1");

            var negResult = response as OkNegotiatedContentResult<CustomerDetailResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("111111", negResult.Content.AccountExternalID);
            Assert.AreEqual("2017/05/10", negResult.Content.DOB);
            Assert.AreEqual("Roman", negResult.Content.FirstName);
            Assert.AreEqual("MiddleName1", negResult.Content.MiddleName);
            Assert.AreEqual("Zhang", negResult.Content.LastName);
            Assert.AreEqual("1231", negResult.Content.Last4SSN);
            Assert.AreEqual("123456", negResult.Content.SSNToken);
            Assert.AreEqual("City 1", negResult.Content.Address.City);
            Assert.AreEqual("Country1", negResult.Content.Address.Country);
            Assert.AreEqual("County 1", negResult.Content.Address.County);
            Assert.AreEqual("State 1", negResult.Content.Address.State);
            Assert.AreEqual("ZipCode 1", negResult.Content.Address.ZipCode);
            Assert.AreEqual("Address1", negResult.Content.Address.Address1);
            Assert.AreEqual("Address2", negResult.Content.Address.Address2);
            Assert.AreEqual(DateTime.Now.AddMonths(-1).Date, negResult.Content.CreateDate);
        }
        [Test]
        public async Task TestCustomerDetailWithOutAddress_Success()
        {
            var response = await _controller.GetCustomerDetail("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2");

            var negResult = response as OkNegotiatedContentResult<CustomerDetailResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("111112", negResult.Content.AccountExternalID);
            Assert.AreEqual("2017/05/10", negResult.Content.DOB);
            Assert.AreEqual("Roman", negResult.Content.FirstName);
            Assert.AreEqual("", negResult.Content.MiddleName);
            Assert.AreEqual("Zhang", negResult.Content.LastName);
            Assert.AreEqual("1232", negResult.Content.Last4SSN);
            Assert.AreEqual("123456", negResult.Content.SSNToken);
            Assert.AreEqual(null, negResult.Content.Address);
            Assert.AreEqual(DateTime.Now.AddMonths(-2).Date, negResult.Content.CreateDate);
        }
        [Test]
        public async Task TestCustomerDetailWithOutAddressLine_Success()
        {
            var response = await _controller.GetCustomerDetail("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3");

            var negResult = response as OkNegotiatedContentResult<CustomerDetailResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("111113", negResult.Content.AccountExternalID);
            Assert.AreEqual("2017/05/10", negResult.Content.DOB);
            Assert.AreEqual("Roman", negResult.Content.FirstName);
            Assert.AreEqual("Zhang", negResult.Content.LastName);
            Assert.AreEqual("1233", negResult.Content.Last4SSN);
            Assert.AreEqual("123456", negResult.Content.SSNToken);
            Assert.AreEqual("City 3", negResult.Content.Address.City);
            Assert.AreEqual("Country3", negResult.Content.Address.Country);
            Assert.AreEqual("County 3", negResult.Content.Address.County);
            Assert.AreEqual("State 3", negResult.Content.Address.State);
            Assert.AreEqual("ZipCode 3", negResult.Content.Address.ZipCode);
            Assert.AreEqual(null, negResult.Content.Address.Address1);
            Assert.AreEqual(null, negResult.Content.CreateDate);
        }

        [Test]
        public async Task TestCustomerDetail_NoFoundException()
        {
            try
            {
                var response = await _controller.GetCustomerDetail("04C9E5B5716A43C2B55DD4B351C0AA87");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestCustomerDetail_GdErrorException()
        {
            try
            {
                var response = await _controller.GetCustomerDetail("04C9E5B5716A43C2B55DD4B351C0AA89");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestCustomerDetail_Exception()
        {
            try
            {
                var response = await _controller.GetCustomerDetail("04C9E5B5716A43C2B55DD4B351C0AA88");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }
        #endregion

        #region Test Get Account Detail
        [Test]
        public async Task TestAccountDetail_Success()
        {
            var response = await _controller.GetAccountDetail("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1");

            var negResult = response as OkNegotiatedContentResult<AccountDetailResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("111111", negResult.Content.AccountExternalID);
            Assert.AreEqual("State 1", negResult.Content.State);
            Assert.AreEqual("Stage 1", negResult.Content.Stage);
            Assert.AreEqual("Association 1", negResult.Content.Association);
            Assert.AreEqual("1111111", negResult.Content.CardExternalID);
            Assert.AreEqual("Cure 1", negResult.Content.Cure);
            Assert.AreEqual("Preocessor 1", negResult.Content.Processor);
            Assert.AreEqual("Reason", negResult.Content.Reason);
            Assert.AreEqual("1111111111", negResult.Content.AccountNumber);
        }
        [Test]
        public async Task TestAccountDetailWithOutReason_Success()
        {
            var response = await _controller.GetAccountDetail("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2");
            var negResult = response as OkNegotiatedContentResult<AccountDetailResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("111112", negResult.Content.AccountExternalID);
            Assert.AreEqual("State 2", negResult.Content.State);
            Assert.AreEqual("Stage 2", negResult.Content.Stage);
            Assert.AreEqual("Association 2", negResult.Content.Association);
            Assert.AreEqual("1111112", negResult.Content.CardExternalID);
            Assert.AreEqual("Cure 2", negResult.Content.Cure);
            Assert.AreEqual("Preocessor 2", negResult.Content.Processor);
            Assert.AreEqual("1111111112", negResult.Content.AccountNumber);
        }
        [Test]
        public async Task TestAccountDetailWithOutCardExternalID_Success()
        {
            var response = await _controller.GetAccountDetail("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3");

            var negResult = response as OkNegotiatedContentResult<AccountDetailResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("111113", negResult.Content.AccountExternalID);
            Assert.AreEqual("State 3", negResult.Content.State);
            Assert.AreEqual("Stage 3", negResult.Content.Stage);
            Assert.AreEqual("Association 3", negResult.Content.Association);
            Assert.AreEqual("Cure 3", negResult.Content.Cure);
            Assert.AreEqual("Preocessor 3", negResult.Content.Processor);
            Assert.AreEqual("Reason", negResult.Content.Reason);
            Assert.AreEqual("1111111113", negResult.Content.AccountNumber);
        }

        [Test]
        public async Task TestAccountDetail_NoFoundException()
        {
            try
            {
                var response = await _controller.GetAccountDetail("04C9E5B5716A43C2B55DD4B351C0AA87");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestAccountDetail_GdErrorException()
        {
            try
            {
                var response = await _controller.GetAccountDetail("04C9E5B5716A43C2B55DD4B351C0AA89");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestAccountDetail_Exception()
        {
            try
            {
                var response = await _controller.GetAccountDetail("04C9E5B5716A43C2B55DD4B351C0AA88");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }
        #endregion

        #region Test Account Search By SSN
        [Test]
        public async Task TestAccountSearchBySSN_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.SSN,
                Value = "123456789"
            };
            var response = await _controller.Search(searchCriteria);

            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(2,negResult.Content.Count);
            Assert.AreEqual("FirstName", negResult.Content[0].FirstName);
            Assert.AreEqual("1234", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD1", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Closed", negResult.Content[0].AccountState);
            Assert.AreEqual("111111111", negResult.Content[0].AccountNumber);
        }
        [Test]
        public async Task TestAccountSearchWithSingleResult_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.SSN,
                Value = "123456782"
            };

            var response = await _controller.Search(searchCriteria);
            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(1, negResult.Content.Count);
            Assert.AreEqual("FirstName2", negResult.Content[0].FirstName);
            Assert.AreEqual("1232", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName2", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD2", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Active", negResult.Content[0].AccountState);
            Assert.AreEqual("111111112", negResult.Content[0].AccountNumber);
        }

        [Test]
        public async Task TestAccountSearch_NoFoundException()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.SSN,
                    Value = "123456783"
                };

                var response = await _controller.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestAccountSearch_GdErrorException()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.SSN,
                    Value = "123456784"
                };
                var response = await _controller.Search(searchCriteria);
            
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestAccountSearch_Exception()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.SSN,
                    Value = "123456785"
                };
                var response = await _controller.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }
        #endregion

        #region Get Full SSN
        [Test]
        public async Task TestGetFullSSN_Success()
        {
            var response = await _controller.GetFullSSN("0PTTLVH7WJZ");
            var negResult = response as OkNegotiatedContentResult<GetFullSSNResponse>;
            Assert.AreEqual("123456789", negResult.Content.SSN);
        }


        [TestCase("1PTTLVH7WJZ")]
        [TestCase("2PTTLVH7WJZ")]
        [TestCase("3PTTLVH7WJZ")]
        public async Task TestGetFullSSN_Exception(string ssnToken)
        {
            try
            {
                var response = await _controller.GetFullSSN(ssnToken);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
            
        }
        #endregion

        #region Test Account Search By Customer Info
        [Test]
        public async Task TestAccountSearchByCustomerInfo_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'FirstName1',LastName:'LastName1',DOB:'',ZipCode:''}"
            };
            var response = await _controller.Search(searchCriteria);

            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(2, negResult.Content.Count);
            Assert.AreEqual("FirstName1", negResult.Content[0].FirstName);
            Assert.AreEqual("1234", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName1", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD1", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Closed", negResult.Content[0].AccountState);
            Assert.AreEqual("111111111", negResult.Content[0].AccountNumber);

        }
        [Test]
        public async Task TestAccountSearchCustomerInfoWithSingleResult_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'FirstName2',LastName:'LastName2',DOB:'',ZipCode:''}"
            };

            var response = await _controller.Search(searchCriteria);
            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(1, negResult.Content.Count);
            Assert.AreEqual("FirstName2", negResult.Content[0].FirstName);
            Assert.AreEqual("1232", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName2", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD2", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Active", negResult.Content[0].AccountState);
            Assert.AreEqual("111111112", negResult.Content[0].AccountNumber);
        }
        [Test]
        public async Task TestAccountSearchCustomerInfoWithDOB_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'FirstName3',LastName:'LastName3',DOB:'01152017',ZipCode:''}"
            };

            var response = await _controller.Search(searchCriteria);
            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(1, negResult.Content.Count);
            Assert.AreEqual("FirstName3", negResult.Content[0].FirstName);
            Assert.AreEqual("1233", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName3", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD3", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Active", negResult.Content[0].AccountState);
            Assert.AreEqual("111111113", negResult.Content[0].AccountNumber);
        }
        [Test]
        public async Task TestAccountSearchCustomerInfoWithZipCode_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'',LastName:'LastName4',DOB:'01252017',ZipCode:'ZipCode4'}"
            };

            var response = await _controller.Search(searchCriteria);
            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(1, negResult.Content.Count);
            Assert.AreEqual("FirstName4", negResult.Content[0].FirstName);
            Assert.AreEqual("1234", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName4", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD4", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Active", negResult.Content[0].AccountState);
            Assert.AreEqual("111111114", negResult.Content[0].AccountNumber);
        }
        [Test]
        public async Task TestAccountSearchCustomerInfoWithoutLastNameInput_Failed()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'',LastName:'',DOB:'01252017',ZipCode:'ZipCode4'}"
            };
            try
            {

                var response = await _controller.Search(searchCriteria);
                var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
            
        }
        [Test]
        public async Task TestAccountSearchCustomerInfoWithOneZipCodeInput_Failed()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'',LastName:'',DOB:'',ZipCode:'ZipCode4'}"
            };
            try
            {

                var response = await _controller.Search(searchCriteria);
                var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }

        }
        [Test]
        public async Task TestAccountSearchCustomerInfoWithoutNoValueInput_Failed()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
            };
            try
            {

                var response = await _controller.Search(searchCriteria);
                var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }

        }

        [Test]
        public async Task TestAccountSearchCustomerInfoWithInvalidateDOBInput_Failed()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.CustomerInfo,
                Value = "{FirstName:'',LastName:'',DOB:'13252017',ZipCode:'ZipCode4'}"
            };
            try
            {

                var response = await _controller.Search(searchCriteria);
                var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }

        }
        [Test]
        public async Task TestAccountSearch__CustomerInfo_FoundException()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.CustomerInfo,
                    Value = "{FirstName:'FirstName5',LastName:'LastName5',DOB:'',ZipCode:''}"
                };

                var response = await _controller.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestAccountSearch_CustomerInfo_ErrorException()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.CustomerInfo,
                    Value = "{FirstName:'FirstName6',LastName:'LastName6',DOB:'',ZipCode:''}"
                };
                var response = await _controller.Search(searchCriteria);

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestAccountSearch_CustomerInfo_Exception()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.CustomerInfo,
                    Value = "{FirstName:'FirstName7',LastName:'LastName7',DOB:'',ZipCode:''}"
                };
                var response = await _controller.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }
        #endregion

        #region Test Account Search By AccountNumber
        [Test]
        public async Task TestAccountSearchByAccountNumber_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.AccountNumber,
                Value = "123456789"
            };
            var response = await _controller.Search(searchCriteria);

            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(2, negResult.Content.Count);
            Assert.AreEqual("FirstName", negResult.Content[0].FirstName);
            Assert.AreEqual("1234", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD1", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Closed", negResult.Content[0].AccountState);
            Assert.AreEqual("111111111", negResult.Content[0].AccountNumber);
        }
        [Test]
        public async Task TestAccountSearchAccountNumberWithSingleResult_Success()
        {
            var searchCriteria = new AccountSearchRequest()
            {
                Option = SearchOptionEnum.AccountNumber,
                Value = "123456782"
            };

            var response = await _controller.Search(searchCriteria);
            var negResult = response as OkNegotiatedContentResult<List<AccountSearchInfo>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(1, negResult.Content.Count);
            Assert.AreEqual("FirstName2", negResult.Content[0].FirstName);
            Assert.AreEqual("1232", negResult.Content[0].Last4SSN);
            Assert.AreEqual("LastName2", negResult.Content[0].LastName);
            Assert.AreEqual("A9DFB55A-2FF9-4D79-B666-04892FBC9CD2", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Active", negResult.Content[0].AccountState);
            Assert.AreEqual("111111112", negResult.Content[0].AccountNumber);
        }

        [Test]
        public async Task TestAccountSearch__AccountNumber_FoundException()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.AccountNumber,
                    Value = "123456783"
                };

                var response = await _controller.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestAccountSearch_AccountNumber_ErrorException()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.AccountNumber,
                    Value = "123456784"
                };
                var response = await _controller.Search(searchCriteria);

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestAccountSearch_AccountNumber_Exception()
        {
            try
            {
                var searchCriteria = new AccountSearchRequest()
                {
                    Option = SearchOptionEnum.AccountNumber,
                    Value = "123456785"
                };
                var response = await _controller.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }
        #endregion


        #region Test Get Monthly Statement Date
        [Test]
        public async Task TestMonthlyStatementDate_Success()
        {


            var response = await _controller.GetMonthlyStatementDate("A9DFB55A-2FF9-4D79-B666-04892FBC9CD1");

            var negResult = response as OkNegotiatedContentResult<MonthlyStatementDateResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("2017-01-01", negResult.Content.StartDate);
            Assert.AreEqual("2017-07-01", negResult.Content.EndDate);
        }

        [Test]
        public async Task TestMonthlyStatementDate_NoFoundException()
        {
            try
            {
                var response = await _controller.GetMonthlyStatementDate("A9DFB55A-2FF9-4D79-B666-04892FBC9CD5");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestMonthlyStatementDate_GdErrorException()
        {
            try
            {
                var response = await _controller.GetMonthlyStatementDate("A9DFB55A-2FF9-4D79-B666-04892FBC9CD6");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestMonthlyStatementDate_Exception()
        {
            try
            {
                var response = await _controller.GetMonthlyStatementDate("A9DFB55A-2FF9-4D79-B666-04892FBC9CD7");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }

        #endregion

        #region Log View Sensitive Data
        [TestCase(ViewEventType.ViewWaveAccountSummaryDOB, ReferenceType.AccountIdentifier)]
        [TestCase(ViewEventType.ViewWaveAccountSummarySSN, ReferenceType.AccountNumber)]

        public async Task LogViewSensitiveData_Success(ViewEventType   eventType, ReferenceType referType)
        {
            var response = await _controller.LogViewSensitiveData(new LogViewSensitiveDataRequest
            {   ViewEvent = eventType,
               ReferenceType = referType,
               ReferenceValue = "JI#RJ*FJIODJFIFFFFF",
               FullName = "Martin Cheng",
            });
            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public async Task LogViewSensitiveData_BadRequestException()
        {
            try
            {
                var response = await _controller.LogViewSensitiveData(new LogViewSensitiveDataRequest
                {
                    FullName = "Martin Cheng",
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<BadRequestException>(ex);
            }
        }

        [Test]
        public async Task LogViewSensitiveData_HeaderBadRequest()
        {
            try
            {                
                var tmpFixture = new AccountControllerFixture();
                var requestHeaderInfo = Substitute.For<IRequestHeaderInfo>();
                requestHeaderInfo.GetUserName().Returns("");
                tmpFixture.Builder.RegisterInstance(requestHeaderInfo);
                tmpFixture.Container = tmpFixture.Builder.Build();
                var tmpController = tmpFixture.Container.Resolve<AccountController>();
                
                var response = await tmpController.LogViewSensitiveData(new LogViewSensitiveDataRequest
                {
                    FullName = "Martin Cheng",
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<BadRequestException>(ex);
            }
        }
        #endregion
    }
}
