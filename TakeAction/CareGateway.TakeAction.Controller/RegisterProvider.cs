using Gdot.Care.Common.Dependency;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.External.Client;
using CareGateway.External.Client.Interfaces;
using CareGateway.TakeAction.Model;
using CareGateway.TakeAction.Logic;

namespace CareGateway.TakeAction.Controller
{
    public class RegisterProvider:IRegister
    {
        [ExcludeFromCodeCoverage]
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<TakeActionController>().InstancePerRequest()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetAccountStatusReasonManager>().As<ITakeAction<GetAccountStatusReasonResponse, GetAccountStatusReasonRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetCloseAccountOptionsManager>().As<ITakeAction<GetCloseAccountOptionsResponse, GetCloseAccountOptionsRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<UpdateAccountStatusReasonManager>().As<ITakeAction<UpdateAccountStatusReasonRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CloseAccountManager>().As<ITakeAction<CloseAccountResponse, CloseAccountRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<SendEmailTriggerManager>().As<ITakeAction<SendEmailTriggerReqeust>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<GetAllTransTypeManager>().As<ITakeAction<GetAllTransTypeResponse, GetAllTransTypeRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            builder.RegisterType<CRMCoreService>()
                .As<ICRMCoreService>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
