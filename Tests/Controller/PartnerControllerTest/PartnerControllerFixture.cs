using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Db.QMaster.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;
using CareGateway.Partner.Controller;
using CareGateway.Partner.Logic;
using CareGateway.Partner.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using NSubstitute;

namespace Tests.Controller.PartnerControllerTest
{
    public class PartnerControllerFixture : BaseFixture<PartnerController>
    {

        public PartnerControllerFixture()
        {
            #region  Case Activity 

            Builder.RegisterType<CaseActivityManager>().As<IPartner<List<CaseActivityRequest>>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            var partnerService = Substitute.For<IPartnerService>();
            InitialPartnerCaseActivity(partnerService);
            #endregion
            Builder.RegisterInstance(partnerService);
        }
        

        private void InitialPartnerCaseActivity(IPartnerService partnerService)
        {
            partnerService.PublishCaseStatus(Arg.Is<PartnerCaseActivityRequest>(m => m.CaseId == "SF000001"))
                .Returns(new ResponseBase()
                {
                    ResponseHeader = new ResponseHeader()
                    {
                        StatusCode="200",
                        SubStatusCode="20001",
                        SubStatusMessage="Success"
                    }

                });
            partnerService.PublishCaseStatus(Arg.Is<PartnerCaseActivityRequest>(m => m.CaseId == "SF000002"))
               .Returns(new ResponseBase()
               {
                   ResponseHeader = new ResponseHeader()
                   {
                       StatusCode = "201",
                       SubStatusCode = "20101",
                       SubStatusMessage = "Failed"
                   }
               });

            partnerService.PublishCaseStatus(
                    Arg.Is<PartnerCaseActivityRequest>(m => m.CaseId == "Partner0000005"
                    || m.CaseType == "CaseType005" || m.PartnerCaseId == "PartnerCaseId005"
                    || m.Status == "Status01"))
                .Returns((ResponseBase)null);



            partnerService.When(
                    m =>
                        m.PublishCaseStatus(Arg.Is<PartnerCaseActivityRequest>(n => n.CaseId == "Partner0000009"
                        || n.Header.RequestId== "RequestId01"
                        || n.Header.RequestOptions == new Dictionary<string, string> {})))
                .Do(
                    x =>
                    {
                        throw new Exception("Error while executing PublishCaseStatus Partner0000009");
                    });
        }

        #region Common Data

        #endregion
    }
}
