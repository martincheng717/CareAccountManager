using Autofac;
using CareGateway.Sfdc.Controller;
using CareGateway.Sfdc.Logic;
using CareGateway.Sfdc.Logic.Salesforce;
using CareGateway.Sfdc.Model.Enum;
using NSubstitute;
using Salesforce.Force;
using Salesforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller.SfdcControllerTest
{
    public class SfdcControllerFixture:BaseFixture<CaseController>
    {
        public SfdcControllerFixture()
        {
            Builder.RegisterType<CaseManager>().As<ICase>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            //var forceClient = Substitute.For<IForceClient>();
            //forceClient.CreateAsync(Arg.Any<string>(), Arg.Any<object>())
            //    .Returns("123142");

            var forceClientProxy = Substitute.For<ICaseClientProxy>();
            forceClientProxy.CreateAsync(Arg.Any<string>(), Arg.Any<object>())
                .Returns(new ProxySuccessResponse {  Id = "13456789"});
            var salesforceService = Substitute.For<ICaseService>();
            salesforceService.CaseClientProxyObject = forceClientProxy;
        }

      
    }
}
