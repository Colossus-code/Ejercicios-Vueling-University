using Autofac.Integration.WebApi;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using InfrastructureData;
using RepositoryContracts;
using Contracts;
using Implementations;
using Serilog;
using System.IO;

namespace WebApiPokemon.Controllers
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;
        private static string _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AplicationLog", "Log.txt");

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

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(_logFilePath)
                .CreateLogger();

            builder.RegisterInstance(Log.Logger)
                .As<ILogger>()
                .SingleInstance();

            builder.RegisterType<PokemonRepositoryUrls>().As<IPokemonRepositoryUrls>();
            builder.RegisterType<PokemonService>().As<IPokemonService>();
            builder.RegisterType<PokemonRepositoryByGeneration>().As<IPokemonRepositoryByGeneration>();
            builder.RegisterType<PokemonFinderAndTransform>().As<IPokemonFinderAndTransform>();

            //Set the dependency resolver to be Autofac.
            Container = builder.Build();

            return Container;
        }

    }
}