using Autofac;
using Bussines;
using Bussines.IServices;
using Infrastructure.IRepository;
using Presentation.Contracts;
using Presentation.MenuManagers;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class ReflectionRegistrator
    {

        public IContainer RegisterDependencies()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterCustomDependencies(containerBuilder);
            var container = containerBuilder.Build();
            return container;
        }

        private void RegisterCustomDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<RepositoryITWorker>().As<IRepositoryITWorker>().SingleInstance();
            containerBuilder.RegisterType<RepositoryTask>().As<IRepositoryTask>().SingleInstance();
            containerBuilder.RegisterType<RepositoryTeam>().As<IRepositoryTeam>().SingleInstance();
            containerBuilder.RegisterType<Login>().As<ILogin>();
            containerBuilder.RegisterType<LoginMenu>().As<ILoginMenu>();
            containerBuilder.RegisterType<PrinterMenuOptions>().As<IPrinterMenuOptions>();
            containerBuilder.RegisterType<MenuManage>().As<IMenuManage>();
            containerBuilder.RegisterType<MenuManageTech>().As<IMenuManageTech>();
            containerBuilder.RegisterType<MenuManageAdmin>().As<IMenuManageAdmin>();
            containerBuilder.RegisterType<MenuManageTeamManager>().As<IMenuManageTeamManager>();
            containerBuilder.RegisterType<Builder>().As<IBuilder>();
            containerBuilder.RegisterType<PrinterServiceAdmin>().As<IPrinterServiceAdmin>();
            containerBuilder.RegisterType<AssignerServiceAdmin>().As<IAssignerServiceAdmin>();
            containerBuilder.RegisterType<AssignerServiceTeamManager>().As<IAssignerServiceTeamManager>();
            containerBuilder.RegisterType<PrinterServiceTeamManager>().As<IPrinterServiceTeamManager>();
            containerBuilder.RegisterType<PrinterServiceTech>().As<IPrinterServiceTech>();
            containerBuilder.RegisterType<AssignerServiceTech>().As<IAssignerServiceTech>();

        }
    }
}
