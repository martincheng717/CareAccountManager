using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Autofac;
using CareGateway.Account.Controller;
using CareGateway.Account.Model;
using CareGateway.Transaction.Controller;
using CareGateway.Transaction.Model;
using Gdot.Care.Common.Exceptions;
using NUnit.Framework;
using Tests.Controller.AccountControllerTest;

namespace Tests.Controller.Transaction
{
    public class TransactionControllerTest
    {
        private TransactionController _controller;
        private TransactionControllerFixture _fixture;

        public TransactionControllerTest()
        {
            _fixture = new TransactionControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<TransactionController>();
        }

        #region Test Reverse Pending Authorization
        [Test]
        public async Task TestReverseAuthorization_Success()
        {
            var response = await _controller.AuthorizationReversal(new AuthorizationReversalRequest { AccountIdentifier = "3F1E05AD - A52D - 4A43 - B3AA - C5B5D0B6D149", AuthorizedTransactionKey = 1 });
            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public async Task TestReverseAuthorization_GdErrorException()
        {
            try
            {
                var response = await _controller.AuthorizationReversal(new AuthorizationReversalRequest { AccountIdentifier = "3F1E05AD - A52D - 4A43 - B3AA - C5B5D0B6D149", AuthorizedTransactionKey = 2 });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestReverseAuthorization_GeneralException()
        {
            try
            {
                var response = await _controller.AuthorizationReversal(new AuthorizationReversalRequest { AccountIdentifier = "3F1E05AD - A52D - 4A43 - B3AA - C5B5D0B6D149", AuthorizedTransactionKey = 3 });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }
        #endregion

        #region Test Get All Transaction Historu

        [TestCase("2017/04/15")]
        [TestCase("")]
        public async Task TestGetAllTransactionHistory_Success(string date)
        {
            DateTime? cycledate =null;
            if (!string.IsNullOrEmpty(date))
            {
                cycledate = DateTime.Parse(date);
            }

            var response = await _controller.GetAllTransactionHistory(new AllTransactionHistoryRequest()
            {
                AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1",
                CycleDate = cycledate
            });

            var negResult = response as OkNegotiatedContentResult<AllTransactionHistoryResponse>;
            Assert.IsNotNull(negResult);
            Assert.IsNotNull(negResult.Content);
            Assert.AreEqual(2,negResult.Content.Transactions.Count);
            
            Assert.AreEqual("9177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1", negResult.Content.Transactions[0].TransactionIdentifier);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AvailableBalance);
            Assert.AreEqual(1, negResult.Content.Transactions[0].AuthorizedTransactionKey);
            Assert.AreEqual("TransactionDescription1", negResult.Content.Transactions[0].TransactionDescription);
            Assert.AreEqual(true, negResult.Content.Transactions[0].IsCredit);
            Assert.AreEqual(true, negResult.Content.Transactions[0].IsReversible);
            Assert.AreEqual("TransType", negResult.Content.Transactions[0].TransType);
            Assert.AreEqual(10, negResult.Content.Transactions[0].TransactionAmount);
            Assert.AreEqual("2017/01/01", negResult.Content.Transactions[0].TransactionDate);
            Assert.AreEqual("TransactionStatus1", negResult.Content.Transactions[0].TransactionStatus);

            Assert.AreEqual("3F1E05AD-A52D-4A43-B3AA-C5B5D0B6D149", negResult.Content.Transactions[0].AccountGUID);
            Assert.AreEqual("11111111", negResult.Content.Transactions[0].AccountProxy);
            Assert.AreEqual("AchOutName", negResult.Content.Transactions[0].AchOutCardholderCompleteName);
            Assert.AreEqual("AchOutAN", negResult.Content.Transactions[0].AchOutTargetAccountNumber);
            Assert.AreEqual("AchOutOriginalRequestID", negResult.Content.Transactions[0].AchOutOriginalRequestID);
            Assert.AreEqual("AchOutTargetAccountRoutingNumber",
                negResult.Content.Transactions[0].AchOutTargetAccountRoutingNumber);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AnotherSourceAccountAmount);
            Assert.AreEqual("AnotherSourceAccountType",
                negResult.Content.Transactions[0].AnotherSourceAccountType);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AnotherSourceAccountFee);
            Assert.AreEqual("ApprovalCode", negResult.Content.Transactions[0].ApprovalCode);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AuthorizationAmount);
            Assert.AreEqual("217/01/01", negResult.Content.Transactions[0].AuthorizationDate);
            Assert.AreEqual("217/01/01", negResult.Content.Transactions[0].AuthorizationReleaseDate);
            Assert.AreEqual(1, negResult.Content.Transactions[0].ConversionRate);
            Assert.AreEqual("DeclineCode1", negResult.Content.Transactions[0].DeclineCode);
            Assert.AreEqual("DeclineReason1", negResult.Content.Transactions[0].DeclineReason);
            Assert.AreEqual("MCCCode1", negResult.Content.Transactions[0].MCCCode);
            Assert.AreEqual("MCCCategory1", negResult.Content.Transactions[0].MCCCategory);
            Assert.AreEqual("MerchantLocation", negResult.Content.Transactions[0].MerchantLocation);
            Assert.AreEqual(10, negResult.Content.Transactions[0].P2PGrandTotal);
            Assert.AreEqual("P2PRecipientName1", negResult.Content.Transactions[0].P2PRecipientName);
            Assert.AreEqual("P2PSenderName1", negResult.Content.Transactions[0].P2PSenderName);
            Assert.AreEqual("P2PType1", negResult.Content.Transactions[0].P2PType);
            Assert.AreEqual("ReceiptStatus1", negResult.Content.Transactions[0].ReceiptStatus);
            Assert.AreEqual("TopUpCardType1", negResult.Content.Transactions[0].TopUpCardType);
            Assert.AreEqual(10, negResult.Content.Transactions[0].TopUpCardFee);
            Assert.AreEqual("TransactionCodeDescription1",
                negResult.Content.Transactions[0].TransactionCodeDescription);
            Assert.AreEqual("WalletID1", negResult.Content.Transactions[0].WalletID);

            Assert.AreEqual("ARN1", negResult.Content.Transactions[0].ARN);

            Assert.AreEqual(DateTime.Now.AddMonths(-1).Date, negResult.Content.ActivationDate.Value.Date);
            Assert.AreEqual(DateTime.Now.Date, negResult.Content.EndDate.Date);
            Assert.AreEqual(28, negResult.Content.DefaultCycleDay);

        }

        [Test]
        public async Task TestGetAllTransactionHistoryWithOutSomeFields_Success()
        {
            var response = await _controller.GetAllTransactionHistory(new AllTransactionHistoryRequest()
            {
                AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3",
                CycleDate = DateTime.Parse("2017/04/15")
            });

            var negResult = response as OkNegotiatedContentResult<AllTransactionHistoryResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("9177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1", negResult.Content.Transactions[0].TransactionIdentifier);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AvailableBalance);
            Assert.AreEqual("TransType", negResult.Content.Transactions[0].TransType);
            Assert.AreEqual(10, negResult.Content.Transactions[0].TransactionAmount);
            Assert.AreEqual("2017/01/01", negResult.Content.Transactions[0].TransactionDate);
            Assert.AreEqual("TransactionStatus1", negResult.Content.Transactions[0].TransactionStatus);
            Assert.AreEqual(true, negResult.Content.Transactions[0].IsCredit);
            Assert.AreEqual(true, negResult.Content.Transactions[0].IsReversible);


            Assert.AreEqual("3F1E05AD-A52D-4A43-B3AA-C5B5D0B6D149", negResult.Content.Transactions[0].AccountGUID);
            Assert.AreEqual("11111111", negResult.Content.Transactions[0].AccountProxy);
            Assert.AreEqual("AchOutName", negResult.Content.Transactions[0].AchOutCardholderCompleteName);
            Assert.AreEqual(null, negResult.Content.Transactions[0].AchOutTargetAccountNumber);
            Assert.AreEqual(null, negResult.Content.Transactions[0].AchOutOriginalRequestID);
            Assert.AreEqual(null, negResult.Content.Transactions[0].AchOutTargetAccountRoutingNumber);
            Assert.AreEqual(null, negResult.Content.Transactions[0].AnotherSourceAccountAmount);
            Assert.AreEqual("AnotherSourceAccountType", negResult.Content.Transactions[0].AnotherSourceAccountType);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AnotherSourceAccountFee);
            Assert.AreEqual("ApprovalCode", negResult.Content.Transactions[0].ApprovalCode);
            Assert.AreEqual(10, negResult.Content.Transactions[0].AuthorizationAmount);
            Assert.AreEqual("217/01/01", negResult.Content.Transactions[0].AuthorizationDate);
            Assert.AreEqual("217/01/01", negResult.Content.Transactions[0].AuthorizationReleaseDate);

            Assert.AreEqual("MCCCode1", negResult.Content.Transactions[0].MCCCode);
            Assert.AreEqual("MCCCategory1", negResult.Content.Transactions[0].MCCCategory);
            Assert.AreEqual("MerchantLocation", negResult.Content.Transactions[0].MerchantLocation);
            Assert.AreEqual(10, negResult.Content.Transactions[0].P2PGrandTotal);
            Assert.AreEqual("P2PRecipientName1", negResult.Content.Transactions[0].P2PRecipientName);
            Assert.AreEqual("TransactionCodeDescription1", negResult.Content.Transactions[0].TransactionCodeDescription);
            Assert.AreEqual("WalletID1", negResult.Content.Transactions[0].WalletID);
            Assert.AreEqual(DateTime.Now.AddYears(-2).Date, negResult.Content.ActivationDate.Value.Date);
            Assert.AreEqual("ARN1", negResult.Content.Transactions[0].ARN);
        }

        [Test]
        public async Task TestGetAllTransactionHistory_NoFoundException()
        {
            try
            {
                var response = await _controller.GetAllTransactionHistory(new AllTransactionHistoryRequest()
                {
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA87",
                    CycleDate = DateTime.Parse("2017/04/15")
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }
        [Test]
        public async Task TestGetAllTransactionHistory_GdErrorException()
        {
            try
            {
                var response = await _controller.GetAllTransactionHistory(new AllTransactionHistoryRequest()
                {
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA89",
                    CycleDate = DateTime.Parse("2017/04/15")
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestGetAllTransactionHistory_Exception()
        {
            try
            {
                var response = await _controller.GetAllTransactionHistory(new AllTransactionHistoryRequest()
                {
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA88",
                    CycleDate = DateTime.Parse("2017/04/15")
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }

        #endregion
    }
}
