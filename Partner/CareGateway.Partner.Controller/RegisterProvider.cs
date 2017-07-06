using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.External.Client;
using CareGateway.External.Client.Interfaces;
using CareGateway.Partner.Logic;
using CareGateway.Partner.Model;
using Gdot.Care.Common.Dependency;
using Gdot.Care.Common.Interface;

namespace CareGateway.Partner.Controller
{
    public class RegisterProvider : IRegister
    {
        [ExcludeFromCodeCoverage]
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<PartnerController>()
                .InstancePerRequest()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<CaseActivityManager>().As<IPartner<List<CaseActivityRequest>>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
           
            builder.RegisterType<PartnerService>()
                .As<IPartnerService>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
