using Autofac;
using CareGateway.TakeAction.Controller;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using CareGateway.External.Model.Enum;

namespace Tests.Controller.TakeActionControllerTest
{
    public class TakeActionControllerTest
    {
        private TakeActionController _controller;
        private TakeActionControllerFixture _fixture;

        public TakeActionControllerTest()
        {
            _fixture = new TakeActionControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<TakeActionController>();
        }

        [Test]
        public async Task TestGetAccountStatusReason_Success()
        {
            var response = await _controller.AccountStatusReason("18204E5C-C243-4096-8BC2-3A62E49B687C");

            var result = response as OkNegotiatedContentResult<GetAccountStatusReasonResponse>;
            Assert.AreEqual("Active", result.Content.CurrentStatus);
            Assert.IsNull(result.Content.CurrentStatusReason);
            Assert.AreEqual("Restricted", result.Content.TargetStatuses[0]);
            Assert.AreEqual("Locked", result.Content.TargetStatuses[1]);
            Assert.AreEqual(54, result.Content.ReasonKey);
            Assert.AreEqual("Agent Manual Review", result.Content.StatusReason);
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687D")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687E")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687F")]
        public async Task TestGetAccountStatusReason_GdErrorException(string AccountIdentifier)
        {
            try
            {
                var response = await _controller.AccountStatusReason(AccountIdentifier);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestGetCloseAccountOption_Success()
        {
            var response = await _controller.CloseAccountOptions("18204E5C-C243-4096-8BC2-3A62E49B687C");

            var result = response as OkNegotiatedContentResult<GetCloseAccountOptionsResponse>;
            Assert.AreEqual("Spend Down", result.Content.CloseAccountOptions[0]);
            Assert.AreEqual("Refund", result.Content.CloseAccountOptions[1]);

        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687D")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687E")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687F")]
        public async Task TestGetCloseAccountOption_GdErrorException(string AccountIdentifier)
        {
            try
            {
                var response = await _controller.CloseAccountOptions(AccountIdentifier);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        #region Test update account status reason
        [Test]
        public async Task TestUpdateAccountStatusReason_Success()
        {
            var response = await _controller.UpdateAccountStatusReason(new UpdateAccountStatusReasonRequest
            {
                AccountIdentifier = "18204E5C-C243-4096-8BC2-3A62E49B687C",
                ReasonKey = "54",
                Status = "Restricted"
            });
            Assert.IsInstanceOf<OkResult>(response);
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687D")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687E")]
        public async Task TestUpdateAccountStatusReason_GdErrorException(string accountIdentifier)
        {
            try
            {
                var response = await _controller.UpdateAccountStatusReason(new UpdateAccountStatusReasonRequest
                {
                    AccountIdentifier = accountIdentifier,
                    ReasonKey = "54",
                    Status = "Restricted"
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687C", "", "Restricted")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687C", "54", "Restrict")]
        public async Task TestUpdateAccountStatusReason_BadRequestException(string accountIdentifier, string accountStatusReason, string status)
        {
            try
            {
                var response = await _controller.UpdateAccountStatusReason(new UpdateAccountStatusReasonRequest
                {
                    AccountIdentifier = accountIdentifier,
                    ReasonKey = accountStatusReason,
                    Status = status
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<BadRequestException>(ex);
            }
        }
        #endregion
        #region Test send email 
        [Test]
        public async Task TestSendEmailTrigger_Success()
        {
            var response = await _controller.SendEmail(new SendEmailTriggerReqeust
            {
                AccountIdentifier = "18204E5C-C243-4096-8BC2-3A62E49B687C",
                 TemplateName = "",
                 DynamicElements = new Dictionary<string, string> { { "123", "123"} },

            });
            Assert.IsInstanceOf<OkResult>(response);
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687D")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687E")]
        public async Task TestSendEmailTrigger_GdErrorException(string accountIdentifier)
        {
            try
            {
                var response = await _controller.SendEmail(new SendEmailTriggerReqeust
                {
                    AccountIdentifier = accountIdentifier,
                    TemplateName = "",
                    DynamicElements = new Dictionary<string, string> { { "123", "123" } },
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }
        #endregion
        #region Close Account
        [TestCase(CloseAccountOptionEnum.CloseAccount)]
        [TestCase(CloseAccountOptionEnum.None)]
        [TestCase(CloseAccountOptionEnum.Refund)]
        [TestCase(CloseAccountOptionEnum.SpendDown)]
        public async Task TestCloseAccount_CloseAccount_Success(CloseAccountOptionEnum enumValue)
        {
            var response = await _controller.CloseAccount(new CloseAccountRequest()
            {
                AccountIdentifier = "18204E5C-C243-4096-8BC2-3A62E49B687C",
                Option = enumValue
            });

            var result = response as OkNegotiatedContentResult<CloseAccountResponse>;
            Assert.AreEqual(new Guid("18204E5C-C243-4096-8BC2-3A62E49B687C"), result.Content.AccountId);
            Assert.AreEqual(1000011, result.Content.AccountKey);
            Assert.AreEqual(1 ,result.Content.AccountStatusReasonKeys[0]);
            Assert.AreEqual(100001, result.Content.Balance.Actual);
            Assert.AreEqual(1000001, result.Content.Balance.Available);
            Assert.AreEqual("Bin1", result.Content.Bin);
            Assert.AreEqual("2017/01/01", result.Content.CreateDate);
            Assert.AreEqual("Cure1", result.Content.Cure);
            Assert.AreEqual("CurrencyCode1", result.Content.CurrencyCode);
            Assert.AreEqual(Stage.Identified, result.Content.DBStage);
            Assert.AreEqual(10001, result.Content.EventCounter);
            Assert.AreEqual("Identifier1", result.Content.Identifier);
            Assert.AreEqual("Pod1", result.Content.Pod);
            Assert.AreEqual("001", result.Content.ReasonCodes[1]);
            Assert.AreEqual(Stage.Identified, result.Content.Stage);
            Assert.AreEqual(State.Closed, result.Content.State);
            Assert.AreEqual(Status.Pending, result.Content.Status.Kyc);
            Assert.AreEqual(Status.Success, result.Content.Status.Ofac);
        }

        [Test]
        public async Task TestCloseAccount_CloseAccountWithNull_Exception()
        {
            try
            {

                var response = await _controller.CloseAccount(new CloseAccountRequest()
                {
                    AccountIdentifier = "28204E5C-C243-4096-8BC2-3A62E49B687C",
                    Option = CloseAccountOptionEnum.CloseAccount
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
            
       
        }


        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687D")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687E")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687F")]
        public async Task TesCloseAccount_GdErrorException(string accountIdentifier)
        {
            try
            {
                var response = await _controller.CloseAccount(new CloseAccountRequest()
                {
                    AccountIdentifier = accountIdentifier,
                    Option = CloseAccountOptionEnum.CloseAccount
                });
                
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        #endregion

        #region GetAllTransType

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687C")]
        public async Task TestGetAllTransType_Success(string accountIdentifier)
        {
            var response = await _controller.GetAllTransType(new GetAllTransTypeRequest()
            {
                AccountIdentifier = accountIdentifier
            });

            var result = response as OkNegotiatedContentResult<GetAllTransTypeResponse>;
            Assert.IsNotNull(result.Content.CreditTransType);
            Assert.IsNotNull(result.Content.DebitTransType);
            Assert.AreEqual(2, result.Content.CreditTransType.Count);
            Assert.AreEqual(2, result.Content.DebitTransType.Count);

            // CreditTransType
            Assert.AreEqual(1, result.Content.CreditTransType[0].GDTransCode);
            Assert.AreEqual("108-004", result.Content.CreditTransType[0].GDTransactionClass);
            Assert.AreEqual("Balance Transfer FUNDING", result.Content.CreditTransType[0].TransCodeDescription);
            Assert.AreEqual(2, result.Content.CreditTransType[1].GDTransCode);
            Assert.AreEqual("108-005", result.Content.CreditTransType[1].GDTransactionClass);
            Assert.AreEqual("Customer Courtesy", result.Content.CreditTransType[1].TransCodeDescription);

            // DebitTransType
            Assert.AreEqual(4, result.Content.DebitTransType[0].GDTransCode);
            Assert.AreEqual("108-006", result.Content.DebitTransType[0].GDTransactionClass);
            Assert.AreEqual("Balance Inquiry Fee", result.Content.DebitTransType[0].TransCodeDescription);
            Assert.AreEqual(5, result.Content.DebitTransType[1].GDTransCode);
            Assert.AreEqual("108-007", result.Content.DebitTransType[1].GDTransactionClass);
            Assert.AreEqual("Lost/Stolen Repl Card Fee", result.Content.DebitTransType[1].TransCodeDescription);
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687D")]
        public async Task TestGetAllTransType_ExternalErrorException(string accountIdentifier)
        {
            try
            {

                var response = await _controller.GetAllTransType(new GetAllTransTypeRequest()
                {
                    AccountIdentifier = accountIdentifier
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ExternalErrorException>(ex);
            }
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687G")]
        public async Task TestGetAllTransType_NotFound(string accountIdentifier)
        {
            try
            {

                var response = await _controller.GetAllTransType(new GetAllTransTypeRequest()
                {
                    AccountIdentifier = accountIdentifier
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }

        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687E")]
        [TestCase("18204E5C-C243-4096-8BC2-3A62E49B687F")]
        public async Task TesGetAllTransType_GdErrorException(string accountIdentifier)
        {
            try
            {
                var response = await _controller.GetAllTransType(new GetAllTransTypeRequest()
                {
                    AccountIdentifier = accountIdentifier
                });

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        #endregion
    }
}
