using Autofac;
using CareGateway.Sfdc.Controller;
using CareGateway.Sfdc.Model;
using Gdot.Care.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Tests.Controller.CaseControllerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SfdcControllerTest
    {
        private CaseController _controller;
        private CaseControllerFixture _fixture;

        public SfdcControllerTest()
        {
            _fixture = new CaseControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<CaseController>();
        }

        #region Basic Function test
        [Test]
        public async Task CreateCase_Success()
        {
            var response = await _controller.Post(new CaseEx()
            {
                RecordType = "CREATECASE",
                AccountIdentifier = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                First_Name__c = "Martin",
                Last_Name__c = "Cheng",

                Subject = "My t",
            });
            var Result = response as OkNegotiatedContentResult<CaseResponse>;
            Assert.AreEqual(true, Result.Content.Success);
        }
        [Test]
        public async Task CreateCase_BadRequest()
        {
            try
            {
                var response = await _controller.Post(new CaseEx()
                {
                    RecordType = "CREATECASE",
                    Intended_Recipient_Account_Number__c = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                    First_Name__c = "Martin",
                    Last_Name__c = "Cheng",

                    Subject = "My t",
                });
                var Result = response as OkNegotiatedContentResult<CaseResponse>;
            }
            catch (Exception ex)
            {

                Assert.AreEqual(true, ex is BadRequestException);
            }            
        }       

        [Test]
        public async Task UpdateCase_Success()
        {
            var response = await _controller.Put(new CaseEx()
            {
                RecordType = "UPDATECASE",                
                CaseNumber = "123456",
                AccountIdentifier = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                First_Name__c = "Martin",
                Last_Name__c = "Cheng",

                Subject = "My t",
            });
            var Result = response as OkNegotiatedContentResult<CaseResponse>;
            Assert.AreEqual(true, Result.Content.Success);
        }
        [Test]
        public async Task UpdateCase_BadRequest()
        {
            try
            {
                var response = await _controller.Put(new CaseEx()
                {
                    RecordType = "UPDATECASE",
                    //CaseNumber = "12356",
                    Intended_Recipient_Account_Number__c = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                    First_Name__c = "Martin",
                    Last_Name__c = "Cheng",

                    Subject = "My t",
                });
                var Result = response as OkNegotiatedContentResult<CaseResponse>;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, ex is BadRequestException);
            }
        }
        #endregion

        #region Task Test
        [Test]
        public async Task GetDataFromExternalTask_CaseNumberNotFoundException()
        {
            try
            {
                var response = await _controller.Put(new CaseEx()
                {
                    RecordType = "UPDATECASE",
                    CaseNumber = "111111",
                    AccountIdentifier = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                    First_Name__c = "Martin",
                    Last_Name__c = "Cheng",

                    Subject = "My t",
                });
                var Result = response as OkNegotiatedContentResult<CaseResponse>;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, ex is NotFoundException);
            }
        }

        [Test]
        public async Task GetDataFromExternalTask_OwnerIdNotFoundException()
        {
            try
            {
                var response = await _controller.Put(new CaseEx()
                {
                    RecordType = "UPDATECASE",
                    CaseNumber = "123456",
                    CaseOwner = "James",
                    AccountIdentifier = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                    First_Name__c = "Martin",
                    Last_Name__c = "Cheng",

                    Subject = "My t",
                });
                var Result = response as OkNegotiatedContentResult<CaseResponse>;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, ex is NotFoundException);
            }
        }

        [Test]
        public async Task CreateCase_GroupNameSuccess()
        {
            var response = await _controller.Post(new CaseEx()
            {
                RecordType = "CREATECASE",
                GroupName = "Group1",
                AccountIdentifier = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                First_Name__c = "Martin",
                Last_Name__c = "Cheng",

                Subject = "My t",
            });
            var Result = response as OkNegotiatedContentResult<CaseResponse>;
            Assert.AreEqual(true, Result.Content.Success);
        }

        [Test]
        public async Task CreateCase_CaseOwnerSuccess()
        {
            var response = await _controller.Post(new CaseEx()
            {
                RecordType = "CREATECASE",
                CaseOwner = "Gorden",
                AccountIdentifier = "2F21F18E-63F5-4AA9-9468-A9A95B7A51B6",
                First_Name__c = "Martin",
                Last_Name__c = "Cheng",

                Subject = "My t",
            });
            var Result = response as OkNegotiatedContentResult<CaseResponse>;
            Assert.AreEqual(true, Result.Content.Success);
        }
        #endregion

        #region CaseBase Test

        #endregion

        #region UpdateOfacStatus
        [Test]
        public async Task UpdateOfacStatus_Success()
        {
            var response = await _controller.UpdateOFACStatus(
                new UpdateOFACStatusRequest {  AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3",
                 CaseNumber = "123456",
                 IsOfacMatch = true});
            Assert.IsInstanceOf<OkResult>(response);
        }
        [Test]
        public async Task UpdateOfacStatus_GdException()
        {
            try
            {
                var response = await _controller.UpdateOFACStatus(
                new UpdateOFACStatusRequest
                {
                    AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC4",
                    CaseNumber = "123456",
                    IsOfacMatch = true
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
