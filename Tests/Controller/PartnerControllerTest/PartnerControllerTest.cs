using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Autofac;
using CareGateway.Partner.Controller;
using CareGateway.Partner.Model;
using CareGateway.QMaster.Controller;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Extension;
using NUnit.Framework;
using Tests.Controller.QMasterControllerTest;

namespace Tests.Controller.PartnerControllerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class PartnerControllerTest
    {
        private PartnerController _controller;
        private PartnerControllerFixture _fixture;

        public PartnerControllerTest()
        {
            _fixture = new PartnerControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<PartnerController>();
        }

        #region Test Case Activity Transfer

        [Test]
        public async Task TestCaseActivity_Success200()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000001",
                    CaseNo = "SF000001",
                    PartnerCaseType = 1,
                    ParentCaseStatus = 4
                },
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000002",
                    CaseNo = "SF000002",
                    PartnerCaseType = 2,
                    ParentCaseStatus = 5
                }
            };

            var response = await _controller.Post(request);
            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public async Task TestCaseActivityNoCaseType_Success200()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000003",
                    CaseNo = "SF000001",
                    ParentCaseStatus = 5
                }
            };

            var response = await _controller.Post(request);

            Assert.IsInstanceOf<OkResult>(response);
        }
        [Test]
        public async Task TestCaseActivityNoCaseType_PartnerServiceException()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000009",
                    CaseNo = "SF000009",
                    ParentCaseStatus = 3
                }
            };

            var response = await _controller.Post(request);

            Assert.IsInstanceOf<OkResult>(response);
        }
        [Test]
        public async Task TestCaseActivityNoCaseType_AddActivityException()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000006",
                    CaseNo = "SF000006",
                    ParentCaseStatus = 2
                }
            };

            var response = await _controller.Post(request);

            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public async Task TestCaseActivity_SuccessWithCaseClosed()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000001",
                    CaseNo = "SF000001",
                    ParentCaseStatus = 1
                }
            };
            var response = await _controller.Post(request);
            Assert.IsInstanceOf<OkResult>(response);
        }
        [Test]
        public async Task TestCaseActivityNoCaseType_SuccessNoStatus()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000003",
                    CaseNo = "SF000001",
                    ParentCaseStatus = 4
                }
            };

            var response = await _controller.Post(request);

            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public async Task TestCaseActivity_SuccessWithNull()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000005",
                    CaseNo = "SF000005",
                    ParentCaseStatus = 1
                }
            };
            var response = await _controller.Post(request);
            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public async Task TestCaseActivity_Exception()
        {
            var request = new List<CaseActivityRequest>()
            {
                new CaseActivityRequest()
                {
                    PartnerCaseNo = "Partner0000009",
                    CaseNo = "SF000009",
                    ParentCaseStatus = 2
                }
            };
            var response = await _controller.Post(request);
            Assert.IsInstanceOf<OkResult>(response);
        }

        #endregion
    }
}
