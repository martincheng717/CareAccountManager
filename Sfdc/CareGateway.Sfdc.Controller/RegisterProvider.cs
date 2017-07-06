using System;
using Autofac;
using CareGateway.Db.PartnerAuthentication.Logic;
using CareGateway.Sfdc.Logic;
using CareGateway.Sfdc.Logic.RecordTypes;
using CareGateway.Sfdc.Logic.Tasks;
using CareGateway.Sfdc.Model;
using Gdot.Care.Common.Dependency;
using CareGateway.Db.PartnerAuthentication.Model;
using CareGateway.Sfdc.Logic.Salesforce;
using Gdot.Care.Common.Interface;
using CareGateway.Sfdc.Logic.CaseService;
using CareGateway.Sfdc.Logic.CaseClientProxy;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Controller
{
    [ExcludeFromCodeCoverage]
    public class RegisterProvider: IRegister
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<CaseController>().InstancePerRequest().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);           
            builder.RegisterType<CaseManager>().As<ICaseManager>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<GetPartnerAuthentication>().As<ISqlCommand<GetPartnerAuthenticationOutput, GetPartnerAuthenticationInput>>();
            builder.RegisterType<CaseService>().As<ICaseService>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<SalesForceClientProxy>().As<ICaseClientProxy>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CaseRepository>().As<ICaseRepository>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            // Set tasks
            builder.RegisterType<CaseValidationTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<GetDataFromExternalTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<RecordTypeFieldMapperTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<SetDefaultValueTask>().As<ICaseTask<CaseEx>>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Set RecordType
            builder.RegisterType<Ofac>().Named<IRecordType>("Ofac").PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Set Create RecordType Factory
            builder.Register<Func<string, IRecordType>>(c =>
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
