using Autofac;
using Autofac.Integration.WebApi;
using Gdot.Care.Common;
using Gdot.Care.Common.Dependency;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using CareGateway.Db.Note.Logic;
using CareGateway.Db.Note.Model;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Utilities;
using CareGateway.External.Client;
using CareGateway.External.Client.Interfaces;

namespace CareGateway
{
    [ExcludeFromCodeCoverage]
    public class Bootstrap
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();

            // Register common dependencies
            BuildCommonDependencies(builder);

            // Register external serivce
            BuildExternalDependencies(builder);

            // Build web api
            BuildWebApiDependencies(builder);

            // Build container
            var container = builder.Build();

            // Set DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        /// <summary>
        /// Build common dependencies
        /// </summary>
        /// <param name="builder"></param>
        private static void BuildCommonDependencies(ContainerBuilder builder)
        {
            //If build is null, skip to register
            if (builder == null)
            {
                return;
            }

            builder.RegisterType<AddNote>().As<ISqlCommand<AddNoteOutput, AddNoteInput>>();
            builder.RegisterType<RequestHeaderInfo>().As<IRequestHeaderInfo>();
            //To Do
            //builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>();
        }

        /// <summary>
        /// Builder the dependencies for restful
        /// </summary>
        /// <param name="builder"></param>
        private static void BuildExternalDependencies(ContainerBuilder builder)
        {
            //If build is null, skip to register
            if (builder == null)
            {
                return;
            }

            builder.RegisterType<ApiClient>().As<IApiClient>();
            //To Do
            builder.RegisterType<RiskService>()
                .As<IRiskService>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        /// <summary>
        ///     Build web api
        /// </summary>
        /// <param name="builder"></param>
        protected static void BuildWebApiDependencies(ContainerBuilder builder)
        {
            try
            {
                //If build is null, skip to register
                if (builder == null)
                {
                    return;
                }

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        if (assembly.FullName.StartsWith("CareGateway"))
                        {

                            foreach (var item in assembly.GetTypes())
                            {
                                bool a = item.IsInterface;
                                bool b = typeof(IRegister).IsAssignableFrom(item);
                                bool c = (item.IsClass && !item.IsAbstract);
                            }
                            var registers = (from t in assembly.GetTypes()
                                             where
                                                 !t.IsInterface && typeof(IRegister).IsAssignableFrom(t) && (t.IsClass && !t.IsAbstract)
                                             select t).ToList();

                            foreach (var register in registers)
                            {
                                var provider = Activator.CreateInstance(register) as IRegister;

                                if (provider != null)
                                {
                                    provider.Register(builder);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText($@"C:\GDC\BootstrapError_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt",
                        "Error when getting assemply " + assembly.FullName + ". Exception: " + ex);

                        throw new GdErrorException($"Error when getting assemply {assembly.FullName}",
                            new LogObject("BuildWebApiDependencies",
                                new Dictionary<string, object> { { "AssemplyName", assembly.FullName } }),
                            ErrorCodeEnum.InternalServerError, ex);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}