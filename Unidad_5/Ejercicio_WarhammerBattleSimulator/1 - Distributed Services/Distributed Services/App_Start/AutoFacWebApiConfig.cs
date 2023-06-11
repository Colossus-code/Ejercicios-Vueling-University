using Autofac;
using Autofac.Integration.WebApi;
using Bussines.Services;
using Infrastructure.IRepository;
using Repository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Distributed_Services
{

    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            builder.RegisterType<UnitsService>().As<IUnitsService>();
            builder.RegisterType<CommanderService>().As<ICommanderService>();
            builder.RegisterType<ArmyService>().As<IArmyService>();
            builder.RegisterType<RepositoryUnitsProfile>().As<IRepositoryUnitsProfile>();
            builder.RegisterType<RepositoryCommanderProfile>().As<IRepositoryCommanderProfile>();
            builder.RegisterType<RepositoryArmy>().As<IRepositoryArmy>();


            //Set the dependency resolver to be Autofac.
            Container = builder.Build();

            return Container;
        }

    }
}
