using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Account.Logic;
using CareGateway.Account.Model;
using CareGateway.External.Client;
using CareGateway.External.Client.Interfaces;
using Gdot.Care.Common.Dependency;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Utilities;

namespace CareGateway.Account.Controller
{
    public class RegisterProvider:IRegister
    {
        [ExcludeFromCodeCoverage]
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AccountController>().InstancePerRequest()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetAccountSummaryManager>().As<IAccount<AccountSummaryResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetAccountDetailManager>().As<IAccount<AccountDetailResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            builder.RegisterType<GetCustomerDetailManager>().As<IAccount<CustomerDetailResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<AccountSearchManager>().As<IAccount<List<AccountSearchInfo>, AccountSearchRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<GetFullSSNManager>().As<IAccount<GetFullSSNResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<LogViewSensitiveManager>().As<IAccount<LogViewSensitiveDataRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<GetMonthlyStatementDateManager>().As<IAccount<MonthlyStatementDateResponse,string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<CRMCoreService>()
                .As<ICRMCoreService>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
