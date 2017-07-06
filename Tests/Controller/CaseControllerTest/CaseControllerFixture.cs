using Autofac;
using CareGateway.Db.PartnerAuthentication.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.Sfdc.Controller;
using CareGateway.Sfdc.Logic;
using CareGateway.Sfdc.Logic.CaseClientProxy;
using CareGateway.Sfdc.Logic.CaseClientProxy.Model;
using CareGateway.Sfdc.Logic.CaseService;
using CareGateway.Sfdc.Logic.RecordTypes;
using CareGateway.Sfdc.Logic.Tasks;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Salesforce;
using Gdot.Care.Common.Interface;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller.CaseControllerTest
{
    [ExcludeFromCodeCoverage]
    public class CaseControllerFixture : BaseFixture<CaseController>
    {
        public CaseControllerFixture()
        {
            Builder.RegisterType<CaseManager>().As<ICaseManager>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<CaseService>().As<ICaseService>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<CaseRepository>().As<ICaseRepository>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            var getPartnerAuthentivationCommand = Substitute.For<ISqlCommand<GetPartnerAuthenticationOutput, GetPartnerAuthenticationInput>>();
            getPartnerAuthentivationCommand.ExecuteAsync(Arg.Any<GetPartnerAuthenticationInput>())
                .Returns(new GetPartnerAuthenticationOutput
                {
                    ConsumerKey = "123",
                    ConsumerSecret = "Password-1234",
                    Domain = "https://test.salesforce.com/services/oauth2/token",
                    Login = "GordenJames@greendotcorp.com",
                    Password = "123456"
                });
            Builder.RegisterInstance(getPartnerAuthentivationCommand);

            var riskService = Substitute.For<IRiskService>();
            riskService.UpdateOFACStatus(Arg.Is<CareGateway.External.Model.Request.UpdateOFACStatusRequest>(p => p.AccountIdentifier == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3"))
                .Returns(Task.Run( () => { } ));
            riskService.UpdateOFACStatus(Arg.Is<CareGateway.External.Model.Request.UpdateOFACStatusRequest>(p => p.AccountIdentifier == "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC4"))
                .Returns(Task.Run(() => { new ArgumentException(); }));
            Builder.RegisterInstance(riskService);

            #region Set ClientProxy
            var ProxyQueryResultSuccessCase = new ProxyQueryResult<Case>
            {
                Done = true,
                Records = new List<Case> { new Case { Id = "987654321" } }
            };
            var ProxyQueryResultNotFoundCase = new ProxyQueryResult<Case>
            {
                Done = true,
                Records = new List<Case> { new Case { Id = null} }
            };

            var ProxyQueryResultSuccessGroup = new ProxyQueryResult<Group>
            {
                Done = true,
                Records = new List<Group> { new Group { Id = "3363965" } }
            };
            var ProxyQueryResultNotFoundGroup = new ProxyQueryResult<Group>
            {
                Done = true,
                Records = new List<Group> { new Group { Id = null } }
            };

            var ProxyQueryResultSuccessUser = new ProxyQueryResult<User>
            {
                Done = true,
                Records = new List<User> { new User { Id = "963258" } }
            };
            var ProxyQueryResultNotFoundUser = new ProxyQueryResult<User>
            {
                Done = true,
                Records = new List<User> { new User { Id = null } }
            };

            var forceClientProxy = Substitute.For<ICaseClientProxy>();
            forceClientProxy.CreateAsync(Arg.Any<string>(), Arg.Any<object>())
                .Returns(new ProxySuccessResponse { Id = "13456789", Success = true });
            forceClientProxy.UpdateAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Object>())
                .Returns(new ProxySuccessResponse { Id = "13456789", Success = true });
            forceClientProxy.GetForceClient(Arg.Any<GetForceClientRequest>())
                .Returns(forceClientProxy);
            forceClientProxy.QueryAsync<Case>(Arg.Is<string>(p => p.Contains("123456")))
                .Returns(ProxyQueryResultSuccessCase);
            forceClientProxy.QueryAsync<Case>(Arg.Is<string>(p => p.Contains("111111")))
                .Returns(ProxyQueryResultNotFoundCase);
            forceClientProxy.QueryAsync<Group>(Arg.Is<string>(p => p.Contains("Group1")))
                .Returns(ProxyQueryResultSuccessGroup);
            forceClientProxy.QueryAsync<Group>(Arg.Is<string>(p => p.Contains("Group2")))
                .Returns(ProxyQueryResultNotFoundGroup);
            forceClientProxy.QueryAsync<User>(Arg.Is<string>(p => p.Contains("Gorden")))
                .Returns(ProxyQueryResultSuccessUser);
            forceClientProxy.QueryAsync<User>(Arg.Is<string>(p => p.Contains("James")))
                .Returns(ProxyQueryResultNotFoundUser);
            Builder.RegisterInstance(forceClientProxy);
            #endregion

            // Set tasks
            Builder.RegisterType<CaseValidationTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<GetDataFromExternalTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<RecordTypeFieldMapperTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            Builder.RegisterType<SetDefaultValueTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Set RecordType
            Builder.RegisterType<Ofac>().Named<IRecordType>("Ofac").PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Set Create RecordType Factory
            Builder.Register<Func<string, IRecordType>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return type =>
                {
                    if (context.IsRegisteredWithName<IRecordType>(type))
                    {
                        return context.ResolveNamed<IRecordType>(type);
                    }
                    return null;
                };
            });

        }
    }
} 