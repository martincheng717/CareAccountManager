using CareGateway.Sfdc.Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Sfdc.Model;
using System.Web.Http.Results;

namespace Tests.Controller.SfdcControllerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SfdcControllerTest
    {
        private CaseController _controller;
        private SfdcControllerFixture _fixture;

        public SfdcControllerTest()
        {
            _fixture = new SfdcControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<CaseController>();
        }

        [Test]
        public async Task CreateCase_Success()
        {
            var response = await _controller.Post(new CaseEx()
            {
                RecordType = "Web Support",
                Subject = "My t",
                First_Name__c = "Martin",
                Last_Name__c = "Cheng",
                Last_4_of_Card_Number__c = "1234",
                ProductType__c = "Walmart",
                Customer_Question__c = "test1",
                AccountKey__c = "5223934",
                Email_Address__c = "mcheng2@greendotcorp.com",
                Phone_Number__c = "132456789"
            });
            Assert.AreEqual(typeof(OkResult), response.GetType());
        }
    }
}
