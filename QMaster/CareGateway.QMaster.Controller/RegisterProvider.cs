using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Db.QMaster.Logic;
using CareGateway.Db.QMaster.Model;
using CareGateway.QMaster.Logic;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Dependency;
using Gdot.Care.Common.Interface;

namespace CareGateway.QMaster.Controller
{
    public class RegisterProvider:IRegister
    {
        [ExcludeFromCodeCoverage]
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<QMasterController>().InstancePerRequest().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<CallTransferManager>().As<IQMasterManager<CallTransferResponse, CallTransferRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetQMasterManager>().As<IQMasterManager<QMasterInfoResponse, int>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<UpdateQMasterManager>().As<IQMasterManager<UpdateQMasterRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            builder.RegisterType<GetAllPartnerCallType>()
                .As<ISqlCommand<SortedList<int, PartnerCallTypeOutput>>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CallTransfer>()
                .As<ISqlCommand<CallTransferOutput, CallTransferInput>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetQMasterbyPartnerCaseNo>()
                .As<ISqlCommand<QMasterInfoOutput, GetQMasterbyPartnerCaseNoInput>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GetQMasterbyQMasterKey>()
                .As<ISqlCommand<QMasterInfoOutput, GetQMasterByQMasterKeyInput>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<UpdateQMaster>()
                .As<ISqlCommand<bool, UpdateQMasterInput>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

        }
    }
}
