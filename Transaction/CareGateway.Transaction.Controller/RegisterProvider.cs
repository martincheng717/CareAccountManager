using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.External.Client;
using CareGateway.External.Client.Interfaces;
using CareGateway.Transaction.Logic;
using CareGateway.Transaction.Model;
using Gdot.Care.Common.Dependency;

namespace CareGateway.Transaction.Controller
{
    public class RegisterProvider:IRegister
    {
        [ExcludeFromCodeCoverage]
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionController>().InstancePerRequest()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetAllTransactionHistoryManager>().As<ITransaction<AllTransactionHistoryResponse,AllTransactionHistoryRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<ReverseAuthorizationManager>().As<ITransaction<AuthorizationReversalRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<CRMCoreService>()
                .As<ICRMCoreService>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
