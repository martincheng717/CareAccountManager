using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Autofac;
using CareGateway.Note.Model;
using CareGateway.QMaster.Controller;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Tests.Controller.QMasterControllerTest;

namespace Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class QMasterControllerTest
    {

        private QMasterController _controller;
        private QMasterControllerFixture _fixture;

        public QMasterControllerTest()
        {
            _fixture = new QMasterControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<QMasterController>();
        }

        #region Test Call Transfer
        [Test]
        public async Task TestCallTranfer_Success200()
        {
            var options = new Dictionary<string, string>();
            options.Add("test", "test");
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA84",
                OriginatorCaseNo = "Wave00001",
                IssueCode = "P01"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK.ToIntegerString(), negResult.Content.StatusCode);
            Assert.AreEqual("0001", negResult.Content.SessionId);
        }


        [Test]
        public async Task TestCallTranfer_Success201()
        {
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA85",
                OriginatorCaseNo = "Wave000001",
                IssueCode = "P01"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.Created.ToIntegerString(), negResult.Content.StatusCode);
            Assert.AreEqual("0002", negResult.Content.SessionId);
        }
        [Test]
        public async Task TestCallTranferWithoutAI_Success201()
        {
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "",
                OriginatorCaseNo = "Wave000011",
                IssueCode = "P01"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK.ToIntegerString(), negResult.Content.StatusCode);
            Assert.AreEqual("00011", negResult.Content.SessionId);
        }
        [Test]
        public async Task TestCallTranfer_Success203()
        {
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA86",
                OriginatorCaseNo = "Wave00003",
                IssueCode = "P001"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.NonAuthoritativeInformation.ToIntegerString(), negResult.Content.StatusCode);

            Assert.AreEqual("0003", negResult.Content.SessionId);

        }

        [Test]
        public async Task TestCallTranfer_Success203WithNoteException()
        {
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA87",
                OriginatorCaseNo = "Wave00004",
                IssueCode = "P001"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.NonAuthoritativeInformation.ToIntegerString(), negResult.Content.StatusCode);

            Assert.AreEqual("0004", negResult.Content.SessionId);

        }

        [Test]
        public async Task TestCallTranfer_InsertQMasterFailed500()
        {
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA88",
                OriginatorCaseNo = "Wave000011",
                IssueCode = "P001"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.InternalServerError.ToIntegerString(), negResult.Content.StatusCode);
            Assert.AreEqual(null, negResult.Content.SessionId);
        }

        [Test]
        public async Task TestCallTranfer_Failed500()
        {
            var request = new CallTransferRequest()
            {
                AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA89",
                OriginatorCaseNo = "Wave000011",
                IssueCode = "P001"
            };
            var response = await _controller.Post(request);

            var negResult = response as OkNegotiatedContentResult<CallTransferResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.InternalServerError.ToIntegerString(), negResult.Content.StatusCode);
            Assert.AreEqual(null, negResult.Content.SessionId);
        }
        #endregion

        #region Test GetQMaster Info

        [Test]
        public async Task TestGetQMasterInfo_Success()
        {
            var response = await _controller.Get(1);
            var negResult = response as OkNegotiatedContentResult<QMasterInfoResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual((new Guid("2099DF46706A424BA175BFD2339290B8")).ToString().ToUpper(), negResult.Content.SessionID.ToString().ToUpper());
            Assert.AreEqual(1, negResult.Content.QMasterKey);
            Assert.AreEqual("04C9E5B5716A43C2B55DD4B351C0AA84", negResult.Content.AccountIdentifier.ToString().ToUpper());
            Assert.AreEqual("Wave000001", negResult.Content.PartnerCaseNo);
            Assert.AreEqual(1, negResult.Content.PartnerCallTypeKey);
            Assert.AreEqual("", negResult.Content.CaseID);
            Assert.AreEqual("P01", negResult.Content.PartnerCallType);
        }
        [Test]
        public async Task TestGetQMasterInfoWithoutAI_Success()
        {
            var response = await _controller.Get(11);
            var negResult = response as OkNegotiatedContentResult<QMasterInfoResponse>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual((new Guid("2099DF46706A424BA175BFD2339290B8")).ToString().ToUpper(), negResult.Content.SessionID.ToString().ToUpper());
            Assert.AreEqual(11, negResult.Content.QMasterKey);
            Assert.AreEqual("", negResult.Content.AccountIdentifier);
            Assert.AreEqual("Wave000011", negResult.Content.PartnerCaseNo);
            Assert.AreEqual(1, negResult.Content.PartnerCallTypeKey);
            Assert.AreEqual("", negResult.Content.CaseID);
            Assert.AreEqual("P01", negResult.Content.PartnerCallType);
        }
        [Test]
        public async Task TestGetQMasterInfo_NoFound()
        {
            try
            {

                var response = await _controller.Get(5);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NotFoundException), ex.GetType());

                Assert.AreEqual("Record not found", ex.Message);
            }

        }
        #endregion

        #region Update QMaster CaseNo

        [Test]
        public async Task TestUpdateQMaster_Success()
        {
            var response = await _controller.Put(new UpdateQMasterRequest()
            {
                QMasterKey = 1,
                GDCaseNo = "100001",
                AgentFullName = "Test Test"
            });
            Assert.AreEqual(typeof(OkResult),response.GetType());
        }

        [Test]
        public async Task TestUpdateQMaster_Failed()
        {
            try
            {

                var response = await _controller.Put(new UpdateQMasterRequest()
                {
                    QMasterKey = 3,
                    GDCaseNo = "100003",
                    AgentFullName = "Test Test"
                });
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(GdErrorException), ex.GetType());
            }
        }

        #endregion
    }
}
